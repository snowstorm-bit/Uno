#region MÉTADONNÉES

// Nom du fichier : frmUno.cs
// Auteur : Mélina Hotte (1933760)
// Date de création : 2021-03-26
// Date de modification : 2021-04-03

#endregion

#region USING

using System;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
using _420_14C_FX_TP2.Classes;
using _420_14C_FX_TP2.Properties;

#endregion

namespace _420_14C_FX_TP2
{
    public partial class FrmUno : Form
    {
        #region CONSTANTES ET ATTRIBUTS STATIQUES

        /// <summary>
        /// Marge du formulaire Uno
        /// </summary>
        private const int MARGE = 30;

        /// <summary>
        /// Largeur d'une carte
        /// </summary>
        private const int LARGEUR_CARTE = 105;

        /// <summary>
        /// Hauteur d'une carte
        /// </summary>
        private const int HAUTEUR_CARTE = 150;

        #endregion

        #region ATTRIBUTS

        /// <summary>
        /// Formulaire serveur
        /// </summary>
        private FrmServeur _serveur;

        /// <summary>
        /// Liste de sockets pour les clients du serveur
        /// </summary>
        private Socket _clientSocket;

        /// <summary>
        /// Données reçues
        /// </summary>
        private byte[] _receivedBuf = new byte[FrmServeur.TAILLE_BUFFER];

        /// <summary>
        /// Partie de Uno en cours
        /// </summary>
        private Partie _partie;

        /// <summary>
        /// Joueur courant
        /// </summary>
        private Joueur _joueur;

        #endregion

        #region CONSTRUCTEURS

        /// <summary>
        /// Constructeur du formulaire Uno.
        /// </summary>
        public FrmUno()
        {
            InitializeComponent();
        }

        #endregion

        #region MÉTHODES

        /// <summary>
        /// Permet d'effectuer le chargement du formulaire Uno.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmUno_Load(object sender, EventArgs e)
        {
            //Ajustement du panel du jeu selon la largeur de la fenêtre.
            pnlTable.Width = Width - 25;
            pnlTable.Height = Height - 100;

            //Affichage de l'adresse IP de l'utilisateur
            Text = $"Uno ({Utilitaire.ObtenirAdresseIpLocale()})";

            lblStatut.Text = "Vous n'êtes pas connecté.";
        }

        /// <summary>
        /// Événement appelé lorsque le client reçoit des données du serveur.
        /// </summary>
        /// <param name="ar">Données reçues du serveur</param>
        private void ReceiveData(IAsyncResult ar)
        {
            Socket socket = (Socket)ar.AsyncState;
            int received = socket.EndReceive(ar);
            byte[] dataBuf = new byte[received];
            Array.Copy(_receivedBuf, dataBuf, received);

            Object obj = Utilitaire.Deserialiser(dataBuf);

            switch (obj)
            {
                case JetonAuthentification objJeton:
                    if (objJeton.Authentifie)
                    {
                        MiseAJourStatut("La connexion a été acceptée ! La partie va bientôt commencer . . .");
                        _joueur = new Joueur(objJeton.NomUtilisateur);
                    }
                    else
                    {
                        socket.Close();
                        MiseAJourStatut(
                            "La connexion a été refusée ! Il existe déjà un client authentifié sous ce nom ou les informations entrées sont incorrectes.");
                    }
                    break;
                case Partie objPartie:
                    {
                        if (_partie == null)
                        {
                            MiseAJourStatut("La partie est démarrée");
                        }

                        _partie = objPartie;
                        MiseAjourInterface();
                        break;
                    }
                case string objMessage:
                    MiseAJourStatut(objMessage);
                    MessageBox.Show(objMessage, "Formulaire Uno", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
            }

            //Si le client est connecté alors on initialise l'écoute pour la réception de données.
            if (socket.Connected)
            {
                _clientSocket.BeginReceive(_receivedBuf, 0, _receivedBuf.Length, SocketFlags.None,
                    ReceiveData, _clientSocket);
            }
        }

        /// <summary>
        /// Permet de se connecter au serveur.
        /// </summary>
        /// <param name="pAddressIp">Adresse du seveur</param>
        /// <param name="pPort">Numéro du port</param>
        /// <remarks>5 tentatives de connexion sont exécutées avant d'abandonner.</remarks>
        private void Connecter(string pAddressIp, int pPort)
        {
            //Initialisation d'un nouveau socket
            _clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            int nbEssais = 1;

            while (!_clientSocket.Connected && nbEssais <= 5)
                try
                {
                    _clientSocket.Connect(IPAddress.Parse(pAddressIp), pPort);
                }
                catch (Exception)
                {
                    nbEssais++;
                }

            if (!_clientSocket.Connected)
            {
                MessageBox.Show(
                    $"Impossible de se connecter au serveur à l'adresse {pAddressIp} sur le port {pPort}. Veuillez vous assurez que le serveur est démarré et que vos informations de connexion sont correctes.");
            }
        }

        /// <summary>
        /// Permet l'envoi d'un objet au serveur.
        /// </summary>
        /// <param name="pObj">Objet à sérialiser et à envoyer au serveur</param>
        private void EnvoyerDonnees(Object pObj)
        {
            if (_clientSocket.Connected)
            {
                //Sérialisation de l'objet à envoyer
                byte[] data = Utilitaire.Serialiser(pObj);

                //Envoi de l'objet au serveur.
                _clientSocket.Send(data);
            }
            else
            {
                MessageBox.Show("Impossible d'envoyer les données au serveur. Vous n'êtes pas connecté!",
                    "Envoi des données");
            }
        }

        /// <summary>
        /// Permet la mise à jour du contrôle lblStatut par un thread différent du UI.
        /// </summary>
        /// <param name="pMessage">Message à afficher dans le label.</param>
        private void MiseAJourStatut(string pMessage)
        {
            statutStrip.Invoke((Action)(() => { lblStatut.Text = pMessage; }));
        }

        /// <summary>
        /// Permet la mise à jour de l'interface du jeu après la réception de l'état de la partie venant du serveur.
        /// Le nom du joueur et ses cartes, la pioche, la défausse et le bouton Uno, s'il reste 2 cartes dans la main, sont dessinés.
        /// </summary>
        /// <remarks>On vérifie également s'il y a un gagnant.</remarks>
        private void MiseAjourInterface()
        {
            pnlTable.Invoke((Action)(() =>
            {
                //Pour le redimmensionnement afin d'éviter qu'une erreur soit lancée
                if (_partie != null)
                {
                    pnlTable.Controls.Clear();

                    Joueur joueurGagnant = _partie.Joueurs.TrouverGagnant();

                    if (joueurGagnant != null)
                    {
                        string msg =
                            $"Le joueur {joueurGagnant} a gagné !\n\nAppuyer sur le bouton 'Démarrer la partie' pour jouer une autre partie.";
                        MessageBox.Show(msg, "Fin de la partie", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        MiseAJourStatut(msg);
                    }
                    else
                    {
                        MiseAJourStatut($"C'est au tour à ' {_partie.JoueurCourant.Valeur} ' de jouer . . .");
                        Noeud joueurCourant = _partie.Joueurs.Debut;
                        int i = 0;

                        while (i < _partie.Joueurs.Taille)
                        {
                            //Mise à jour du joueur pour la fenêtre du formulaire Uno
                            if (joueurCourant.Valeur.Nom == _joueur.Nom)
                            {
                                _joueur = joueurCourant.Valeur;
                            }

                            //Dessine la main de tous les joueurs
                            DessinerMainJoueur(joueurCourant.Valeur, i + 1);

                            //Si un joueur a crié Uno!
                            if (joueurCourant.Valeur.Uno && joueurCourant.Valeur.Main.Count == 1)
                            {
                                MessageBox.Show($"{joueurCourant.Valeur} a crié Uno!", "Le joueur",
                                    MessageBoxButtons.OK);
                                //Pour éviter de toujours afficher le message une fois que l'on sait que le joueur a crié Uno!
                                joueurCourant.Valeur.Uno = false;
                            }

                            joueurCourant = joueurCourant.Suivant;
                            i++;
                        }

                        //Dessin du bouton Uno s'il ne reste que 2 cartes au joueur courant
                        if (_joueur.Main.Count == 2 && _joueur == _partie.JoueurCourant.Valeur)
                        {
                            DessinerBoutonUno();
                        }

                        DessinerDefausse();
                        DessinerPioche();
                    }
                }
            }));
        }

        /// <summary>
        /// Permet de dessiner le nom du joueur et ses cartes.
        /// </summary>
        /// <param name="pJoueur">Joueur à dessiner</param>
        /// <param name="pNoJoueur">Numéro du joueur. Nécessaire pour déterminer la position du joueur dans l'interface.</param>
        private void DessinerMainJoueur(Joueur pJoueur, int pNoJoueur)
        {
            int posXCarte = 0, posYCarte;
            int posXLabel = 0, posYLabel = 0;

            //Création du label du joueur
            Label lblNomJoueur = new Label();
            lblNomJoueur.Text = pJoueur.Nom;
            lblNomJoueur.Font = new Font("Cooper Black", 12);

            //Si le joueur en paramètre est le joueur courant du formulaire, alors la couleur est en bleu sinon en noir
            lblNomJoueur.ForeColor = _partie.JoueurCourant.Valeur == pJoueur ? Color.Blue : Color.Black;

            //On détermine la position graphique des éléments du joueur.
            switch (pNoJoueur)
            {
                case 1:
                    //Nom du joueur
                    posXLabel = FrmUno.MARGE;
                    posYLabel = FrmUno.MARGE;

                    //Cartes
                    posXCarte = posXLabel;

                    break;
                case 2:
                    //Nom du joueur
                    posXLabel = Width - FrmUno.MARGE - lblNomJoueur.Width;
                    posYLabel = FrmUno.MARGE;

                    //Cartes
                    posXCarte = Width - FrmUno.MARGE - FrmUno.LARGEUR_CARTE;

                    break;
                case 3:
                    //Nom du joueur
                    posXLabel = Width - FrmUno.MARGE - lblNomJoueur.Width;
                    posYLabel = Height - FrmUno.MARGE - FrmUno.HAUTEUR_CARTE - lblNomJoueur.Height - 5;

                    //Cartes
                    posXCarte = Width - FrmUno.MARGE - FrmUno.LARGEUR_CARTE;

                    break;
                case 4:
                    //Nom du joueur
                    posXLabel = FrmUno.MARGE;
                    posYLabel = Height - FrmUno.MARGE - FrmUno.HAUTEUR_CARTE - lblNomJoueur.Height - 5;

                    //Carte
                    posXCarte = posXLabel;

                    break;
            }

            //Position des cartes sous le nom du joueur
            posYCarte = posYLabel + lblNomJoueur.Height + 5;

            //Position du nom du joueur
            lblNomJoueur.Location = new Point(posXLabel, posYLabel);

            //Ajout du nom du joueur
            pnlTable.Controls.Add(lblNomJoueur);

            //Création des cartes du joueur
            if (pJoueur.Main != null)
            {
                foreach (Carte carte in pJoueur.Main)
                {
                    //Création d'une nouvelle image
                    PictureBox pboCarte = new PictureBox();

                    //S'il s'agit d'un adversaire, alors on ne montre que le dessus des cartes
                    pboCarte.Image = _joueur.Nom == pJoueur.Nom ? ObtenirImage(carte) : Resources.CarteDessus;

                    pboCarte.Width = FrmUno.LARGEUR_CARTE;
                    pboCarte.Height = FrmUno.HAUTEUR_CARTE;
                    pboCarte.SizeMode = PictureBoxSizeMode.Zoom;
                    pboCarte.AllowDrop = true;

                    //Positionnement de la carte
                    pboCarte.Location = new Point(posXCarte, posYCarte);

                    //Ajout d'un tuple contenant le joueur et la carte au PictureBox de la carte.
                    pboCarte.Tag = (pJoueur, carte);
                    pboCarte.Name = "pboCarte";

                    //Espace pour la juxtaposition des cartes
                    int diviseur = 4;

                    //Si le joueur reçu en paramètre s'agit du joueur courant, l'on espace les cartes
                    if (_partie.JoueurCourant.Valeur == pJoueur)
                    {
                        diviseur = 3;

                        //Si le joueur reçu en paramètre s'agit du joueur courant et du joueur du formulaire, alors on permet le déplacement des cartes.
                        if (pJoueur.Nom == _joueur.Nom)
                        {
                            pboCarte.MouseMove += PictureBox_MouseMove;
                            pboCarte.DragEnter += PictureBox_DragEnter;
                            pboCarte.DragDrop += PictureBox_DragDrop;
                        }
                    }

                    //Il s'agit du joueur 2 ou 3, on ajoute les cartes vers le centre du formulaire.
                    if (pNoJoueur == 1 || pNoJoueur == 4)
                    {
                        posXCarte += pboCarte.Width / diviseur;
                    }
                    else
                    {
                        posXCarte -= pboCarte.Width / diviseur;
                    }

                    //Ajout du PictureBox de la carte
                    pnlTable.Controls.Add(pboCarte);
                    pboCarte.BringToFront();
                }
            }

            pnlTable.Refresh();
        }

        /// <summary>
        /// Permet de dessiner la pioche.
        /// </summary>
        private void DessinerPioche()
        {
            if (_partie.Pioche.NbCartes > 0)
            {
                //Création d'une nouvelle image
                PictureBox pboPioche = new PictureBox();
                pboPioche.Name = "pboPioche";
                pboPioche.Width = FrmUno.LARGEUR_CARTE;
                pboPioche.Height = FrmUno.HAUTEUR_CARTE;
                pboPioche.SizeMode = PictureBoxSizeMode.Zoom;
                pboPioche.Image = Resources.CarteDessus;
                pboPioche.AllowDrop = true;

                pboPioche.Location = new Point(Width / 2 - FrmUno.LARGEUR_CARTE - 20,
                    Height / 2 - FrmUno.HAUTEUR_CARTE / 2);

                pboPioche.MouseMove += PictureBox_MouseMove;
                pboPioche.DragEnter += PictureBox_DragEnter;
                pboPioche.DragDrop += PictureBox_DragDrop;

                pnlTable.Controls.Add(pboPioche);
            }
        }

        /// <summary>
        /// Permet de dessiner la défausse.
        /// </summary>
        private void DessinerDefausse()
        {
            if (_partie.Defausse.Count > 0)
            {
                //Création d'une nouvelle image
                PictureBox pboDefausse = new PictureBox();
                pboDefausse.Name = "pboDefausse";
                pboDefausse.Width = FrmUno.LARGEUR_CARTE;
                pboDefausse.Height = FrmUno.HAUTEUR_CARTE;
                pboDefausse.SizeMode = PictureBoxSizeMode.Zoom;
                pboDefausse.AllowDrop = true;

                pboDefausse.Location =
                    new Point(Width / 2 + FrmUno.LARGEUR_CARTE, Height / 2 - FrmUno.HAUTEUR_CARTE / 2);

                //pboDefausse.MouseMove += new MouseEventHandler(PictureBox_MouseMove);
                pboDefausse.DragEnter += PictureBox_DragEnter;
                pboDefausse.DragDrop += PictureBox_DragDrop;

                Carte carteDefausse = _partie.Defausse.Peek();

                pboDefausse.Image = ObtenirImage(carteDefausse);

                pnlTable.Controls.Add(pboDefausse);
            }
        }

        /// <summary>
        /// Permet d'obtenir l'image d'une carte à partir de son nom.
        /// </summary>
        /// <param name="pCarte">Carte</param>
        /// <returns>L'image correspondant à la carte dans les ressources.</returns>
        private Image ObtenirImage(Carte pCarte)
        {
            return (Image)Resources.ResourceManager.GetObject(pCarte.Valeur.ToString() + pCarte.Couleur);
        }

        /// <summary>
        /// Permet de dessiner le bouton Uno!
        /// </summary>
        /// <remarks>Celui-ci est dessiné seulement s'il reste 2 cartes à jouer pour le joueur du formulaire. Il ne peut pas jouer son avant-dernière carte s'il n'a pas cliqué sur ce bouton.</remarks>
        private void DessinerBoutonUno()
        {
            //Création d'une nouvelle image
            PictureBox pboUno = new PictureBox();
            pboUno.Name = "pboUno";
            pboUno.Width = FrmUno.LARGEUR_CARTE;
            pboUno.Height = FrmUno.HAUTEUR_CARTE;
            pboUno.SizeMode = PictureBoxSizeMode.Zoom;
            pboUno.Image = Resources.uno;
            pboUno.Location = new Point(Width / 2, Height / 2 - FrmUno.HAUTEUR_CARTE / 2);
            pboUno.Click += PictureBoxUno_Click;

            pnlTable.Controls.Add(pboUno);
        }

        /// <summary>
        /// Permet au joueur de sélectionner une couleur dans le formulaire FrmSelectionCouleur.
        /// </summary>
        /// <returns>La couleur sélectionnée par le joueur</returns>
        private Couleur DemanderCouleur()
        {
            FrmSelectionCouleur frm = new FrmSelectionCouleur();
            frm.ShowDialog();
            Couleur choix = frm.CouleurSelectionnee;
            frm.Dispose();

            return choix;
        }

        /// <summary>
        /// Événement généré lors du déplacement d'une carte.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            //Si le bouton de gauche de la souris est enfoncé
            if (e.Button == MouseButtons.Left)
            {
                PictureBox pbo = (PictureBox)sender;

                //Démarrage du DragDrop
                pbo.DoDragDrop(pbo, DragDropEffects.Move);
            }
        }

        /// <summary>
        /// Événement généré lorsqu'une carte commence à se déplacer.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PictureBox_DragEnter(object sender, DragEventArgs e)
        {
            //Modification du curseur lors du déplacement
            e.Effect = DragDropEffects.Move;
        }

        /// <summary>
        /// Événement généré lorsque la carte est déposée.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PictureBox_DragDrop(object sender, DragEventArgs e)
        {
            //On obtient la cible 
            PictureBox target = (PictureBox)sender;
            if (e.Data.GetDataPresent(typeof(PictureBox)))
            {
                //On obtient la source
                PictureBox source = (PictureBox)e.Data.GetData(typeof(PictureBox));
                if (source != target)
                {
                    //Démarrage du timer pour l'action à effectuer suite au Drop.
                    //Requis pour ne pas que la fenêtre gèle lors de l'affichage du message ou du formulaire
                    tmrDragDrop.Tag = source;
                    tmrDragDrop.Start();
                }
            }
        }

        /// <summary>
        /// Événement du timer pour enclancher une action suite au déplacement d'une carte.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TmrDragDrop_Tick(object sender, EventArgs e)
        {
            //Arrêt du timer
            tmrDragDrop.Stop();

            //On obtient la source (picturebox) qui a été glissée et qui a démarré le timer
            PictureBox pictureBox = (PictureBox)tmrDragDrop.Tag;

            bool sourceEstOk = true;

            try
            {
                //Carte est pigée
                if (pictureBox.Name == "pboPioche")
                {
                    _partie.PigerCartes(_joueur, 1);
                }
                //Carte est jouée
                else if (pictureBox.Name == "pboCarte")
                {
                    //Obtient le tuple du tag
                    (Joueur, Carte) valeurTag = ((Joueur, Carte))pictureBox.Tag;
                    _partie.JouerCarte(valeurTag.Item1, valeurTag.Item2);

                    //Vérifie qu'une carte spéciale est jouée
                    bool cartePourChangerCouleur = valeurTag.Item2.Valeur == Valeur.Joker
                                                   || valeurTag.Item2.Valeur == Valeur.Plus4;

                    if (valeurTag.Item2.Couleur == Couleur.Noir)
                    {
                        if (cartePourChangerCouleur)
                        {
                            _partie.Defausse.Push(new Carte(DemanderCouleur(), _partie.Defausse.Pop().Valeur));
                        }

                        if (valeurTag.Item2.Valeur == Valeur.Plus4)
                        {
                            Console.WriteLine(_partie.JoueurCourant.Valeur);
                            _partie.PigerCartes(_partie.JoueurCourant.Valeur, 4);
                        }
                    }
                }
                else
                {
                    throw new InvalidOperationException("Vous ne pouvez pas déplacer la carte à cet endroit.");
                }
            }
            catch (InvalidOperationException exception)
            {
                sourceEstOk = false;
                MessageBox.Show(exception.Message, "FrmUno", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception exception)
            {
                sourceEstOk = false;
                MessageBox.Show($"Il s'est produite une erreur :\n'{exception.Message}'.\nVeuillez communiquez avec l'administrateur du système.", "FrmUno", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (sourceEstOk)
            {
                //Envoi de l'état de la partie au serveur.
                EnvoyerDonnees(_partie);
                MiseAJourStatut("Envoi de l'état de la partie au serveur . . .");
            }
        }

        /// <summary>
        /// Permet l'ouverture de la fenêtre de connexion au serveur.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuConnexionServeur_Click(object sender, EventArgs e)
        {
            if (_serveur == null || _serveur.IsDisposed)
            {
                _serveur = new FrmServeur();
            }

            _serveur.Show();
        }

        /// <summary>
        /// Permet l'ouverture de la fenêtre d'authentification.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuConnexionClient_Click(object sender, EventArgs e)
        {
            FrmAuthentification frmAuthentification = new FrmAuthentification();

            // Ouverture du formulaire d'authentification
            if (frmAuthentification.ShowDialog() == DialogResult.OK)
            {
                MiseAJourStatut("Connexion en cours. Veuillez patienter . . .");

                //Connexion au serveur
                Connecter(frmAuthentification.Serveur, frmAuthentification.Port);

                if (_clientSocket.Connected)
                {
                    //Initialisation de l'écoute de réception des données
                    _clientSocket.BeginReceive(_receivedBuf, 0, _receivedBuf.Length, SocketFlags.None,
                        ReceiveData, _clientSocket);

                    //Envoi des données au serveur
                    MiseAJourStatut("Authentification en cours. Veuillez patienter . . .");

                    EnvoyerDonnees(new JetonAuthentification(frmAuthentification.NomUtilisateur,
                        frmAuthentification.MotPasse));
                }
            }
        }

        /// <summary>
        /// Permet l'ouverture de la fenêtre de gestion des utilisateurs.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuUtilisateurs_Click(object sender, EventArgs e)
        {
            FrmUtilisateur frmUtilisateur = new FrmUtilisateur();
            frmUtilisateur.ShowDialog();
        }

        /// <summary>
        /// Permet de redessiner le jeu lorsqu'il y a un redimensionnement de l'écran.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmUno_Resize(object sender, EventArgs e)
        {
            //On vérifie si le formulaire est affiché
            if (Visible)
            {
                pnlTable.Width = Width - 25;
                pnlTable.Height = Height - 100;
                MiseAjourInterface();
            }
        }

        /// <summary>
        /// Événement exécuté lors du clique sur l'image Uno.
        /// Permet d'indiqué que le joueur a crié Uno!
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PictureBoxUno_Click(object sender, EventArgs e)
        {
            _joueur.Uno = true;
            MiseAJourStatut("Le bouton Uno a été pressé. Vous pouvez à présent jouer une carte valide.");
            ((PictureBox)pnlTable.Controls[pnlTable.Controls.IndexOfKey("pboUno")]).Hide();
        }

        #endregion
    }
}
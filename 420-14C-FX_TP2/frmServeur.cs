#region MÉTADONNÉES

// Nom du fichier : frmServeur.cs
// Auteur : Mélina Hotte (1933760)
// Date de création : 2021-03-26
// Date de modification : 2021-04-16

#endregion

#region USING

using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
using _420_14C_FX_TP2.Classes;

#endregion

namespace _420_14C_FX_TP2
{
    public partial class FrmServeur : Form
    {
        #region CONSTANTES ET ATTRIBUTS STATIQUES

        /// <summary>
        /// Taille du buffer pour l'envoi de données
        /// </summary>
        public const int TAILLE_BUFFER = 1024 * 1024;

        #endregion

        #region ATTRIBUTS

        /// <summary>
        /// Partie envoyée au clients
        /// </summary>
        private Partie _partie;

        /// <summary>
        /// Buffer contenant les données 
        /// </summary>
        private byte[] _buffer = new byte[FrmServeur.TAILLE_BUFFER];

        /// <summary>
        /// Liste des sockets clients
        /// </summary>
        private List<Socket> _socketsClient;

        /// <summary>
        /// Socket serveur
        /// </summary>
        private Socket _socketServeur;

        /// <summary>
        /// Indique si la fenêtre a été fermée
        /// </summary>
        private bool _fermer;

        #endregion

        #region CONSTRUCTEURS

        /// <summary>
        /// Constructeur du formulaire serveur.
        /// </summary>
        public FrmServeur()
        {
            InitializeComponent();
        }

        #endregion

        #region MÉTHODES

        /// <summary>
        /// Permet d'initialiser les contrôles du formulaire avant son affichage.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmServeur_Load(object sender, EventArgs e)
        {
            lblAdresseIP.Text += $"{Utilitaire.ObtenirAdresseIpLocale()}";
        }

        /// <summary>
        /// Permet la configuration du serveur.
        /// </summary>
        private void ConfigurerServer()
        {
            //Initialisation du socker serveur.
            _socketServeur = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            //Connexion du socket serveur au port spécifié pour l'acceptation des connexions.
            _socketServeur.Bind(new IPEndPoint(IPAddress.Any, int.Parse(txtNoPort.Text)));
            _socketServeur.Listen(1);

            //On commence à écouter sur le port
            _socketServeur.BeginAccept(AcceptConnexionCallback, null);
            MiseAJourStatut("En attente de connexions . . .");

            //Initialisation de la liste des sockets clients.
            _socketsClient = new List<Socket>();

            //Initialisation d'une nouvelle partie.
            _partie = new Partie();
        }

        /// <summary>
        /// Accepte toutes les demandes de connexion lorsqu'il y a moins de 5 joueurs dans la partie.
        /// <param name="ar"></param>
        private void AcceptConnexionCallback(IAsyncResult ar)
        {
            try
            {
                //Arrêt de la réception des demandes de connexion
                var socket = _socketServeur.EndAccept(ar);
                if (_partie.Joueurs.Taille < 5)
                {
                    _socketsClient.Add(socket);

                    MiseAJourControles();

                    //On écoute pour la réception de données du client
                    socket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None,
                        ReceiveCallback, socket);

                    //On écoute pour d'autres connexions
                    _socketServeur.BeginAccept(AcceptConnexionCallback, null);
                }
                else
                {
                    EnvoyerDonnees(socket, "Impossible de vous connecter. Le serveur est complet.");
                }
            }
            catch (NullReferenceException e)
            {
                // Le _socketServeur est probablement null du au fait que l'on ferme et ouvre la connexion au serveur rapidement
                Console.WriteLine(e);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        /// <summary>
        /// Permet de mettre à jour les contrôles du formulaire.
        /// </summary>
        private void MiseAJourControles()
        {
            //Affichage de la liste des clients connectés
            AfficherListeClients();

            //Mise à jour du statut du serveur
            MiseAJourStatut($"Nb. clients connecté(s) : {_socketsClient.Count}");

            //On permet le démarrage de la partie s'il y au moins une connexion.
            btnDemarrerPartie.Invoke(new Action(() => { btnDemarrerPartie.Enabled = _socketsClient.Count > 1; }));
        }

        /// <summary>
        /// Permet de recevoir les données envoyées par les clients.
        /// </summary>
        /// <param name="ar"></param>
        private void ReceiveCallback(IAsyncResult ar)
        {
            //Socket client qui a envoyé les données
            Socket socket = (Socket)ar.AsyncState;

            if (socket.Connected)
            {
                try
                {
                    int received = socket.EndReceive(ar);

                    //Si des données ont été reçues
                    if (received != 0)
                    {
                        byte[] dataBuf = new byte[received];
                        Array.Copy(_buffer, dataBuf, received);

                        //Désérialisation des données reçues.
                        Object obj = Utilitaire.Deserialiser(dataBuf);

                        if (obj is JetonAuthentification objJeton)
                        {
                            Authentifier(objJeton);

                            if (!objJeton.Authentifie)
                            {
                                _socketsClient.Remove(socket);
                                MiseAJourStatut($"Authentification de '{objJeton.NomUtilisateur}' : Refusée !");
                                MiseAJourControles();
                            }
                            else
                            {
                                MiseAJourStatut($"Authentification de '{objJeton.NomUtilisateur}' : Acceptée.");
                            }

                            EnvoyerDonnees(socket, objJeton);
                        }
                        else if (obj is Partie objPartie)
                        {
                            foreach (Socket socketClient in _socketsClient)
                            {
                                EnvoyerDonnees(socketClient, objPartie);
                            }
                        }

                        //Mise à jour de la liste des clients connectés
                        AfficherListeClients();
                    }

                    //On recommence à accepter des données.
                    socket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, ReceiveCallback,
                        socket);
                }
                catch (Exception)
                {
                    //Si le client est déconnecté on le retire de la liste
                    for (int i = 0; i < _socketsClient.Count; i++)
                    {
                        if (_socketsClient[i].RemoteEndPoint.ToString().Equals(socket.RemoteEndPoint.ToString()))
                        {
                            _socketsClient.RemoveAt(i);
                            MiseAJourStatut($"Client déconnecté : {socket.RemoteEndPoint}");
                            MiseAJourControles();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Permet de mettre à jour le statut des communications entre les clients et le serveur.
        /// </summary>
        /// <param name="pMessage"></param>
        private void MiseAJourStatut(string pMessage)
        {
            //Mise à jour du statut du serveur
            txtStatut.Invoke(new Action(() => { txtStatut.AppendText($" {pMessage} \r\n"); }));
        }

        /// <summary>
        /// Permet l'affichage de la liste des clients connectés.
        /// </summary>
        private void AfficherListeClients()
        {
            //Ajout de l'addresse ip du client à la liste
            lstClients.Invoke(new Action(() =>
            {
                lstClients.Items.Clear();

                for (int i = 0; i < _socketsClient.Count; i++)
                {
                    lstClients.Items.Add(_socketsClient[i].RemoteEndPoint.ToString());
                }
            }));
        }

        /// <summary>
        /// Permet d'authentifier un joueur auprès du serveur avec le jeton reçu en paramètre.
        /// </summary>
        /// <param name="pJeton">Le jeton du joueur à authentifier</param>
        /// <remarks>Le jeton est mis à jour selon le résultat de l'authentification. Si l'authentification réussie, alors un nouveau joueur est créé et est ajouté à la partie.</remarks>
        void Authentifier(JetonAuthentification pJeton)
        {
            pJeton.Authentifie = true;

            int cptPourTaille = 0;
            Noeud joueurCourant = _partie.Joueurs.Debut;

            //Vérifie s'il y a déjà 4 joueurs dans la partie
            if (_partie.Joueurs.Taille == 4)
            {
                pJeton.Authentifie = false;
            }
            else
            {
                //Vérifie si un client n'a pas tenté de se connecter avec un jeton déjà authentifié dans la partie
                while (cptPourTaille < _partie.Joueurs.Taille && pJeton.Authentifie)
                    if (joueurCourant.Valeur.Nom == pJeton.NomUtilisateur)
                    {
                        pJeton.Authentifie = false;
                    }
                    else
                    {
                        joueurCourant = joueurCourant.Suivant;
                        cptPourTaille++;
                    }

                if (pJeton.Authentifie)
                {
                    //Vérifie que le mot de passe est valide
                    Dictionary<string, (byte[], byte[])> dictUtilisateurs = Utilitaire.ObtenirUtilisateurs();

                    if (dictUtilisateurs.ContainsKey(pJeton.NomUtilisateur))
                    {
                        (byte[], byte[]) valeursDeCle = dictUtilisateurs[pJeton.NomUtilisateur];
                        pJeton.Authentifie =
                            Utilitaire.VerifierMdp(pJeton.MotPasse, valeursDeCle.Item1, valeursDeCle.Item2);

                        if (pJeton.Authentifie)
                        {
                            _partie.Joueurs.AjouterFin(new Joueur(pJeton.NomUtilisateur));
                        }
                    }
                    else
                    {
                        pJeton.Authentifie = false;
                    }
                }
            }
        }

        /// <summary>
        /// Permet l'envoi d'un objet sérialisé à un client.
        /// </summary>
        /// <param name="pSocket">Socket client à qui sera envoyé l'objet</param>
        /// <param name="pObj">Objet à sérialiser et à envoyer</param>
        void EnvoyerDonnees(Socket pSocket, Object pObj)
        {
            //Sérialisation de l'objet à envoyer
            byte[] data = Utilitaire.Serialiser(pObj);

            //Initialisation de l'envoie des données.
            pSocket.BeginSend(data, 0, data.Length, SocketFlags.None, SendCallback, pSocket);

            _socketServeur.BeginAccept(AcceptConnexionCallback, null);
        }

        /// <summary>
        /// Permet l'envoie du résultat asynchrone pour le socket du client.
        /// </summary>
        /// <param name="ar"></param>
        private void SendCallback(IAsyncResult ar)
        {
            Socket socket = (Socket)ar.AsyncState;
            socket.EndSend(ar);
        }

        /// <summary>
        /// Permet le démarrage du serveur.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDemarrerServeur_Click(object sender, EventArgs e)
        {
            MiseAJourStatut("Démarrage du serveur . . .");
            ConfigurerServer();

            //On désactive la possibilité de redémarrer le serveur
            btnDemarrerServeur.Enabled = false;
        }

        /// <summary>
        /// Permet de démarrer une partie.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>La partie est démarrée et envoyée à tous les clients.</remarks>
        private void BtnDemarrerPartie_Click(object sender, EventArgs e)
        {
            if (_partie.DemarrerPartie())
            {
                foreach (Socket clientSocket in _socketsClient)
                {
                    EnvoyerDonnees(clientSocket, _partie);
                }
            }
            else
            {
                MessageBox.Show("Impossible de démarrer la partie. Il faut au moins 2 joueurs dans la partie!");
            }
        }

        /// <summary>
        /// Permet de fermer le formulaire serveur lorsque l'événement est appelé.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCacher_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        /// <summary>
        /// Permet de fermer la connexion du serveur ou cacher la fenêtre du formulaire lorsque l'événement est appelé. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmServeur_FormClosing(object sender, FormClosingEventArgs e)
        {
            //On s'assure que l'utilisateur a bien voulu quitter et fermer le serveur. 
            //sinon, on cache la fenêtre.
            if (_socketServeur != null)
            {
                //Si oui, on indique à l'utilsateur que cela va arrêter le seveur.
                DialogResult resultat =
                    MessageBox.Show("Cette action arrêtera le serveur. Êtes-vous certain de vouloir quitter?",
                        "Arrêt du serveur", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (resultat == DialogResult.Yes)
                {
                    try
                    {
                        if (_socketServeur.Connected)
                        {
                            _socketServeur.Shutdown(SocketShutdown.Both);
                        }
                    }
                    finally
                    {
                        _socketServeur.Close();
                    }

                    _socketServeur.Dispose();
                    _socketServeur = null;
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }

        /// <summary>
        /// Permet d'arrêter le serveur et de fermer la fenêtre lorsque l'utilisateur veut quitter.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnQuitter_Click(object sender, EventArgs e)
        {
            Close();
        }

        #endregion
    }
}
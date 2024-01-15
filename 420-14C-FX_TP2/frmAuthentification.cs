#region MÉTADONNÉES

// Nom du fichier : frmAuthentification.cs
// Auteur : Mélina Hotte (1933760)
// Date de création : 2021-03-26
// Date de modification : 2021-04-16

#endregion

#region USING

using System;
using System.Windows.Forms;
using _420_14C_FX_TP2.Classes;

#endregion

namespace _420_14C_FX_TP2
{
    /// <summary>
    /// Formulaire permettant d'authentifier un client pour la connexion au serveur du jeu de Uno.
    /// </summary>
    public partial class FrmAuthentification : Form
    {
        #region ATTRIBUTS

        /// <summary>
        /// Nom de l'utilisateur entré dans le formulaire d'authentification
        /// </summary>
        private string _nomUtilisateur;

        /// <summary>
        /// Mot de passe de l'utilisateur entré dans le formulaire d'authentification
        /// </summary>
        private string _motPasse;

        /// <summary>
        /// Adresse IP du serveur entrée dans le formulaire d'authentification
        /// </summary>
        private string _serveur;

        /// <summary>
        /// Port de connexion entré dans le formulaire d'authentification
        /// </summary>
        private int _port;

        #endregion

        #region PROPRIÉTÉS ET INDEXEURS

        /// <summary>
        /// Obtient ou définit le nom de l'utilisateur entré dans le formulaire d'authentification
        /// </summary>
        public string NomUtilisateur
        {
            get { return _nomUtilisateur; }
            set { _nomUtilisateur = value; }
        }

        /// <summary>
        /// Obtient ou définit le mot de passe de l'utilisateur entré dans le formulaire d'authentification
        /// </summary>
        public string MotPasse
        {
            get { return _motPasse; }
            set { _motPasse = value; }
        }

        /// <summary>
        /// Obtient ou définit l'adresse IP du serveur entrée dans le formulaire d'authentification
        /// </summary>
        /// <exception cref="ArgumentNullException">Lancée lorsque l'adresse IP du serveur est nul ou est constitué d'espaces blancs.</exception>
        public string Serveur
        {
            get { return _serveur; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException("_serveur",
                        "L'adresse IP du serveur ne peut être nul ou constitué d'espaces blancs.");
                }

                _serveur = value;
            }
        }

        /// <summary>
        /// Obtient ou définit le port de connexion entré dans le formulaire d'authentification
        /// </summary>
        public int Port
        {
            get { return _port; }
            set { _port = value; }
        }

        #endregion

        #region CONSTRUCTEURS

        /// <summary>
        /// Constructeur du formulaire d'authentification d'un client.
        /// </summary>
        public FrmAuthentification()
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
        private void FrmClient_Load(object sender, EventArgs e)
        {
            txtAdresseServeur.Text = Utilitaire.ObtenirAdresseIpLocale();
            txtNomUtilisateur.Text = "bob";
            txtMotPasse.Text = "12345";
            txtPort.Text = "100";
            AcceptButton = btnConnecter;
        }

        /// <summary>
        /// Permet de valider les informations saisies par l'utilisateur avant la mise à jour des propriétés du formulaire.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnConnecter_Click(object sender, EventArgs e)
        {
            if (ValiderUtilisateur())
            {
                NomUtilisateur = txtNomUtilisateur.Text.ToLower();
                MotPasse = txtMotPasse.Text;
                Serveur = txtAdresseServeur.Text;
                Port = int.Parse(txtPort.Text);
                DialogResult = DialogResult.OK;
            }
            else
            {
                DialogResult = DialogResult.None;
            }
        }

        /// <summary>
        /// Permet de valider tous les champs du formulaire d'authentification.
        /// </summary>
        /// <returns>Vrai si tous les champs sont conformes. Sinon faux.</returns>
        private bool ValiderUtilisateur()
        {
            errorProvider.Clear();

            txtNomUtilisateur.Text = txtNomUtilisateur.Text.Trim();
            txtMotPasse.Text = txtMotPasse.Text.Trim();
            txtAdresseServeur.Text = txtAdresseServeur.Text.Trim();
            txtPort.Text = txtPort.Text.Trim();

            //Vérification pour le nom de l'utilisateur
            if (txtNomUtilisateur.Text.Length < Joueur.NOM_NB_CARAC_MIN)
            {
                errorProvider.SetError(txtNomUtilisateur,
                    $"Le nom de l'utilisateur doit contenir au moins {Joueur.NOM_NB_CARAC_MIN} caractères.");
            }
            else if (txtNomUtilisateur.Text.Contains(" "))
            {
                errorProvider.SetError(txtNomUtilisateur, "Le nom d'utilisateur ne peut pas contenir d'espace(s).");
            }

            //Vérification pour le mot de passe de l'utilisateur
            if (txtMotPasse.Text.Length < JetonAuthentification.MOT_PASSE_NB_CARAC_MIN)
            {
                errorProvider.SetError(txtMotPasse,
                    $"Le mot de passe doit contenir au moins {JetonAuthentification.MOT_PASSE_NB_CARAC_MIN} caractères.");
            }
            else if (txtMotPasse.Text.Contains(" "))
            {
                errorProvider.SetError(txtMotPasse, "Le mot de passe ne peut pas contenir d'espace(s).");
            }

            //Vérification pour l'adresse IP du serveur
            if (string.IsNullOrWhiteSpace(txtAdresseServeur.Text))
            {
                errorProvider.SetError(txtMotPasse,
                    "Veuillez saisir une adresse IP n'étant pas composée uniquement d'espace(s) blanc(s).");
            }

            //Vérification pour le numéro du port du serveur
            if (!int.TryParse(txtPort.Text, out int _))
            {
                errorProvider.SetError(txtMotPasse, "Veuillez saisir un nombre entier pour le numéro du port.");
            }

            string msgErreurs = "";
            foreach (Control ctrl in errorProvider.ContainerControl.Controls)
            {
                string strErreur = errorProvider.GetError(ctrl);

                if (!string.IsNullOrWhiteSpace(strErreur))
                {
                    msgErreurs += $"- {strErreur}\n";
                }
            }

            if (msgErreurs != "")
            {
                MessageBox.Show($"Veuillez corriger le(s) erreur(s) suivante(s) :\n{msgErreurs}",
                    "Erreur lors de l'authentification !");
                return false;
            }

            return true;
        }

        /// <summary>
        /// Permet d'annuler les actions en cours dans le formulaire.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAnnuler_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        #endregion
    }
}
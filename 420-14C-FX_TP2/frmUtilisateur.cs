#region MÉTADONNÉES

// Nom du fichier : frmUtilisateur.cs
// Auteur : Mélina Hotte (1933760)
// Date de création : 2021-03-26
// Date de modification : 2021-04-16

#endregion

#region USING

using System;
using System.Collections.Generic;
using System.Windows.Forms;
using _420_14C_FX_TP2.Classes;

#endregion

namespace _420_14C_FX_TP2
{
    /// <summary>
    /// Formulaire permettant la gestion des utilisateurs pour se connecter au serveur du jeu de Uno.
    /// </summary>
    public partial class FrmUtilisateur : Form
    {
        #region ATTRIBUTS

        /// <summary>
        /// Contient la liste des utilisateurs
        /// </summary>
        private Dictionary<string, (byte[], byte[])> _utilisateurs;

        #endregion

        #region CONSTRUCTEURS

        /// <summary>
        /// Constructeur du formulaire des utilisateurs.
        /// </summary>
        public FrmUtilisateur()
        {
            InitializeComponent();
        }

        #endregion

        #region MÉTHODES

        /// <summary>
        /// Permet d'initialiser les contrôles avant l'affichage du formulaire des utilisateurs.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmUtilisateur_Load(object sender, EventArgs e)
        {
            ActiveControl = txtNomUtilisateur;
            btnSupprimer.Enabled = false;
            _utilisateurs = Utilitaire.ObtenirUtilisateurs();
            InitialiserFormulaire();
        }

        /// <summary>
        /// Permet d'initialiser les contrôles du formulaire avant son affichage.
        /// </summary>
        private void InitialiserFormulaire()
        {
            if (!btnSupprimer.Enabled)
            {
                InitialiserListeUtilisateurs();
            }

            txtNomUtilisateur.Text = "";
            txtMotPasse.Text = "";
        }

        /// <summary>
        /// Permet d'obtenir et d'afficher la liste des utilisateurs.
        /// </summary>
        private void InitialiserListeUtilisateurs()
        {
            lstUtilisateurs.Items.Clear();

            // Pour activer le btnNouveau, il faut plus d'un joueur dans le dictionnaire
            if (_utilisateurs.Count > 0)
            {
                foreach (string nomUtilisateur in _utilisateurs.Keys)
                {
                    lstUtilisateurs.Items.Add(nomUtilisateur);
                }

                btnNouveau.Enabled = true;
            }
            else
            {
                btnNouveau.Enabled = false;
            }
        }

        /// <summary>
        /// Met à jour la barre de statut du formulaire des utilisateurs.
        /// </summary>
        /// <param name="pMsg"></param>
        private void MiseAJourStatut(string pMsg)
        {
            statusStrip.Invoke((Action)(() => { lblStatut.Text = pMsg; }));
        }

        /// <summary>
        /// Permet de créer une nouvelle liste d'utilisateurs en réinitialisant le formulaire.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnNouveau_Click(object sender, EventArgs e)
        {
            _utilisateurs = new Dictionary<string, (byte[], byte[])>();
            InitialiserFormulaire();
        }

        /// <summary>
        /// Permet l'ajout d'un nouvel utilisateur.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAjouter_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValiderUtilisateur())
                {
                    txtNomUtilisateur.Text = txtNomUtilisateur.Text.ToLower();
                    MiseAJourStatut($"Création de l'utilisateur {txtNomUtilisateur.Text} en cours . . .");

                    byte[] salt = Utilitaire.GenererSalt();
                    _utilisateurs.Add(txtNomUtilisateur.Text,
                        (salt, Utilitaire.HashMotDePasse(txtMotPasse.Text, salt)));
                    MiseAJourStatut($"Succès pour l'ajout de ' {txtNomUtilisateur.Text} '.");
                    InitialiserFormulaire();
                }
            }
            catch (Exception msg)
            {
                MessageBox.Show($"Une erreur s'est produite lors de l'ajout de l'utilisateur :\n{msg}");
            }
        }

        /// <summary>
        /// Permet la suppression d'un utilisateur.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSupprimer_Click(object sender, EventArgs e)
        {
            string nomUtilisateur = lstUtilisateurs.SelectedItem.ToString();
            if (MessageBox.Show($"Voulez-vous vraiment supprimer {nomUtilisateur} de la liste ?",
                    "Gestion des utilisateurs - Supprimer", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) ==
                DialogResult.Yes)
            {
                MiseAJourStatut($"Suppression de {nomUtilisateur} en cours . . .");
                try
                {
                    _utilisateurs.Remove(nomUtilisateur);
                    lstUtilisateurs.SelectedIndex = -1;
                    MiseAJourStatut($"Succès pour la suppression de {nomUtilisateur} !");
                }
                catch (Exception msgException)
                {
                    MessageBox.Show(
                        $"Il a été impossible de supprimer l'utilisateur.\nException lancée : {msgException}",
                        "Erreur pour supprimer l'utilisateur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    MiseAJourStatut($"Erreur pour la suppression de {nomUtilisateur}.");
                }
            }
            else
            {
                lstUtilisateurs.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Efface le contenu du formulaire et modifie l'état des contrôles pour permettre l'action de la suppression de l'utilisateur sélectionné.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LstUtilisateur_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = lstUtilisateurs.SelectedIndex;

            //Selon si un item a été sélectionné dans la liste, activer ou désactiver le bouton pour la suppression d'un utilisateur
            btnSupprimer.Enabled = index != -1;

            InitialiserFormulaire();

            //Lorsque le bouton supprimer est accessible, alors tous les autres ne le sont pas et vise versa.
            bool controleEstAccessible = !btnSupprimer.Enabled;

            txtNomUtilisateur.Enabled = controleEstAccessible;
            txtMotPasse.Enabled = controleEstAccessible;
            btnNouveau.Enabled = controleEstAccessible;
            btnAjouter.Enabled = controleEstAccessible;
            btnEnregistrer.Enabled = controleEstAccessible;
        }

        /// <summary>
        /// Permet de valider les données saisies pour la création d'un utilisateur.
        /// </summary>
        /// <returns>True si les données sont valides, false sinon</returns>
        private bool ValiderUtilisateur()
        {
            MiseAJourStatut("Validation en cours . . .");

            errorProvider.Clear();

            txtNomUtilisateur.Text = txtNomUtilisateur.Text.Trim();
            txtMotPasse.Text = txtMotPasse.Text.Trim();

            //Vérification pour le nom de l'utilisateur
            if (txtNomUtilisateur.Text.Length < Joueur.NOM_NB_CARAC_MIN)
            {
                errorProvider.SetError(txtNomUtilisateur,
                    $"Le nom de l'utilisateur doit contenir au moins {Joueur.NOM_NB_CARAC_MIN} caractères.");
            }

            if (txtNomUtilisateur.Text.Contains(" "))
            {
                errorProvider.SetError(txtNomUtilisateur, "Le nom d'utilisateur ne peut pas contenir d'espace(s).");
            }

            if (_utilisateurs.ContainsKey(txtNomUtilisateur.Text.ToLower()))
            {
                errorProvider.SetError(txtNomUtilisateur,
                    "Veuillez saisir un nom différent de ceux dans la liste des utilisateurs existants.");
            }

            //Vérification pour le mot de passe de l'utilisateur
            if (txtMotPasse.Text.Length < JetonAuthentification.MOT_PASSE_NB_CARAC_MIN)
            {
                errorProvider.SetError(txtMotPasse,
                    $"Le mot de passe doit contenir au moins {JetonAuthentification.MOT_PASSE_NB_CARAC_MIN} caractères.");
            }

            if (txtMotPasse.Text.Contains(" "))
            {
                errorProvider.SetError(txtMotPasse, "Le mot de passe ne peut pas contenir d'espace(s).");
            }

            string msgErreurs = "";
            foreach (Control ctrl in errorProvider.ContainerControl.Controls[gboUtilisateur.Name].Controls)
            {
                Console.WriteLine(ctrl.Name);
                string strErreur = errorProvider.GetError(ctrl);

                if (!string.IsNullOrWhiteSpace(strErreur))
                {
                    msgErreurs += $"- {strErreur}\n";
                }
            }

            if (msgErreurs != "")
            {
                MessageBox.Show($"Veuillez corriger le(s) erreur(s) suivante(s) :\n{msgErreurs}",
                    "Erreur dans le(s) champs du formulaire !");
                MiseAJourStatut("Validation échouée !");
                return false;
            }

            MiseAJourStatut("Validation réussie !");

            return true;
        }

        /// <summary>
        /// Permet d'enregistrer la liste des utilisateurs dans le fichier .dat des utilisateurs et ferme le formulaire des utilisateurs.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnEnregistrer_Click(object sender, EventArgs e)
        {
            MiseAJourStatut("Enregistrement des utilisateurs en cours . . .");
            try
            {
                Utilitaire.SauvegarderUtilisateurs(_utilisateurs);
                MiseAJourStatut("Succès de l'enregistrement des utilisateurs.");
                MessageBox.Show("Succès de l'enregistrement des utilisateurs !", "Enregistrement des utilisateurs",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
            }
            catch (Exception msg)
            {
                MessageBox.Show($"Il s'est produit une erreur ! : {msg}", "Enregistrement des utilisateurs",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                MiseAJourStatut("Erreur lors de l'enregistrement des utilisateurs !");
            }
        }

        /// <summary>
        /// Annule les modifications et ferme le formulaire.
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
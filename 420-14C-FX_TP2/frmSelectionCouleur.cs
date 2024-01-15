#region MÉTADONNÉES

// Nom du fichier : frmSelectionCouleur.cs
// Auteur : Mélina Hotte (1933760)
// Date de création : 2021-03-26
// Date de modification : 2021-04-16

#endregion

#region USING

using System;
using System.Drawing;
using System.Windows.Forms;
using _420_14C_FX_TP2.Classes;

#endregion

namespace _420_14C_FX_TP2
{
    /// <summary>
    /// Formulaire permettant de sélectionner la couleur d'une carte.
    /// </summary>
    public partial class FrmSelectionCouleur : Form
    {
        #region CONSTANTES ET ATTRIBUTS STATIQUES

        /// <summary>
        /// Largeur de base d'un PictureBox du formulaire
        /// </summary>
        private const int LARGEUR = 90;

        /// <summary>
        /// Hauteur de base d'un PictureBox du formulaire
        /// </summary>
        private const int HAUTEUR = 90;

        #endregion

        #region ATTRIBUTS

        /// <summary>
        /// Couleur sélectionnée dans le formulaire
        /// </summary>
        private Couleur _couleurSelectionnee;

        #endregion

        #region PROPRIÉTÉS ET INDEXEURS

        /// <summary>
        /// Obtient ou définit la couleur sélectionnée dans le formulaire
        /// </summary>
        public Couleur CouleurSelectionnee
        {
            get { return _couleurSelectionnee; }
            private set { _couleurSelectionnee = value; }
        }

        #endregion

        #region CONSTRUCTEURS

        /// <summary>
        /// Constructeur du formulaire de sélection d'une couleur.
        /// </summary>
        public FrmSelectionCouleur()
        {
            InitializeComponent();
        }

        #endregion

        #region MÉTHODES

        /// <summary>
        /// Permet d'initialiser les contrôles avant l'affichage du formulaire de sélection d'une couleur.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="s"></param>
        private void FrmSelectionCouleur_Load(object sender, EventArgs e)
        {
            Width = FrmSelectionCouleur.LARGEUR * 3;
            Height = FrmSelectionCouleur.HAUTEUR * 3;

            // PictureBox de la couleur bleu
            pboBleu.Width = FrmSelectionCouleur.LARGEUR;
            pboBleu.Height = FrmSelectionCouleur.HAUTEUR;
            pboBleu.Location = new Point((Width / 2) - FrmSelectionCouleur.LARGEUR - 10, 25);
            pboBleu.Tag = Couleur.Bleu;

            // PictureBox de la couleur jaune
            pboJaune.Width = FrmSelectionCouleur.LARGEUR;
            pboJaune.Height = FrmSelectionCouleur.HAUTEUR;
            pboJaune.Location = new Point(pboVert.Location.X, pboVert.Location.Y + pboVert.Height);
            pboJaune.Tag = Couleur.Jaune;

            // PictureBox de la couleur vert
            pboVert.Width = FrmSelectionCouleur.LARGEUR;
            pboVert.Height = FrmSelectionCouleur.HAUTEUR;
            pboVert.Location = new Point(pboBleu.Location.X + pboBleu.Width, pboBleu.Location.Y);
            pboVert.Tag = Couleur.Vert;

            // PictureBox de la couleur rouge
            pboRouge.Width = FrmSelectionCouleur.LARGEUR;
            pboRouge.Height = FrmSelectionCouleur.HAUTEUR;
            pboRouge.Location = new Point(pboBleu.Location.X, pboBleu.Location.Y + pboBleu.Height);
            pboRouge.Tag = Couleur.Rouge;
        }

        /// <summary>
        /// Événement appelé lorsqu'une couleur est sélectionnée.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>Permet de sélectionnée la couleur puis de renvoyer OK pour le résultat du dialogue.</remarks>
        private void PictureBox_Click(object sender, EventArgs e)
        {
            PictureBox pboCouleur = (PictureBox)sender;
            CouleurSelectionnee = (Couleur)(pboCouleur.Tag);

            DialogResult = DialogResult.OK;
        }

        #endregion
    }
}
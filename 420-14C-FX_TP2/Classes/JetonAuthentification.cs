#region MÉTADONNÉES

// Nom du fichier : JetonAuthentification.cs
// Auteur : Mélina Hotte (1933760)
// Date de création : 2021-03-27
// Date de modification : 2021-04-14

#endregion

#region USING

using System;

#endregion

namespace _420_14C_FX_TP2.Classes
{
    /// <summary>
    /// Classe représentant un jeton d'authentification d'un client pour sa connexion au serveur.
    /// </summary>
    [SerializableAttribute]
    public class JetonAuthentification
    {
        #region CONSTANTES ET ATTRIBUTS STATIQUES

        /// <summary>
        /// Nombre de caractères minimum pour le mot de passe du jeton d'authentification
        /// </summary>
        public const int MOT_PASSE_NB_CARAC_MIN = 5;

        #endregion

        #region ATTRIBUTS

        /// <summary>
        /// Nom de l'utilisateur du jeton d'authentification
        /// </summary>
        private string _nomUtilisateur;

        /// <summary>
        /// Mot de passe du jeton d'authentification de l'utilisateur
        /// </summary>
        private string _motPasse;

        /// <summary>
        /// Détermine si l'authentification a été un succès ou non
        /// </summary>
        private bool _authentifie;

        #endregion

        #region PROPRIÉTÉS ET INDEXEURS

        /// <summary>
        /// Obtient ou définit le nom d'utilisateur du jeton d'authentification de l'utilisateur
        /// </summary>
        /// <exception cref="ArgumentNullException">Lancée lorsque le nom est nul ou vide.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Lancée lorsque le nombre de caractères dans le nom de l'utilisateur est
        /// inférieur à la valeur de la constante du nombre de caractères requis pour le nom.</exception>
        public string NomUtilisateur
        {
            get { return _nomUtilisateur; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException("_nomUtilisateur", "Le nom d'utilisateur ne peut être nul ou vide");
                }

                if (value.Length < Joueur.NOM_NB_CARAC_MIN)
                {
                    throw new ArgumentOutOfRangeException("_nomUtilisateur",
                        $"Le nom d'utilisateur doit contenir au moins {Joueur.NOM_NB_CARAC_MIN} caractères.");
                }

                _nomUtilisateur = value;
            }
        }

        /// <summary>
        /// Obtient ou définit le mot de passe du jeton d'authentification de l'utilisateur
        /// </summary>
        /// <exception cref="ArgumentNullException">Lancée lorsque le mot de passe est nul ou vide.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Lancée lorsque le nombre de caractères dans le mot de passe est
        /// inférieur à la valeur de la constante du nombre de caractères requis pour le nom.</exception>
        public string MotPasse
        {
            get { return _motPasse; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException("_motPasse", "Le mot de passe ne peut être vide ou nul");
                }

                if (value.Length < JetonAuthentification.MOT_PASSE_NB_CARAC_MIN)
                {
                    throw new ArgumentOutOfRangeException("_motPasse",
                        $"Le mot de passe doit contenir au moins {JetonAuthentification.MOT_PASSE_NB_CARAC_MIN} caractères.");
                }

                _motPasse = value;
            }
        }

        /// <summary>
        /// Obtient ou définit si l'utilisateur a été authentifié avec succès
        /// </summary>
        public bool Authentifie
        {
            get { return _authentifie; }
            set { _authentifie = value; }
        }

        #endregion

        #region CONSTRUCTEURS

        /// <summary>
        /// Contructeur permettant de créer le jeton d'authentification d'un utilisateur.
        /// </summary>
        /// <param name="pNomUtilisateur">Nom de l'utilisateur</param>
        /// <param name="pMotPasse">Mot de passe de l'utilisateur</param>
        public JetonAuthentification(string pNomUtilisateur, string pMotPasse)
        {
            NomUtilisateur = pNomUtilisateur;
            MotPasse = pMotPasse;
        }

        #endregion
    }
}
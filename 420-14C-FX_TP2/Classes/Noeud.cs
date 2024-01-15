#region MÉTADONNÉES

// Nom du fichier : Noeud.cs
// Auteur : Mélina Hotte (1933760)
// Date de création : 2021-03-27
// Date de modification : 2021-04-16

#endregion

#region USING

using System;

#endregion

namespace _420_14C_FX_TP2.Classes
{
    /// <summary>
    /// Classe représentant un noeud dans la liste des joueurs de la partie courante de Uno.
    /// </summary>
    [Serializable]
    public class Noeud
    {
        #region ATTRIBUTS

        /// <summary>
        /// Valeur du noeud qui est un joueur
        /// </summary>
        private Joueur _valeur;

        /// <summary>
        /// Noeud suivant
        /// </summary>
        private Noeud _suivant;

        /// <summary>
        /// Noeud précédent
        /// </summary>
        private Noeud _precedent;

        #endregion

        #region PROPRIÉTÉS ET INDEXEURS

        /// <summary>
        /// Obtient ou définit la valeur du noeud dont celle-ci représente un joueur
        /// </summary>
        /// <exception cref="ArgumentNullException">Lancée lorsque la valeur du noeud est nulle.</exception>
        public Joueur Valeur
        {
            get { return _valeur; }
            set
            {
                _valeur = value ??
                          throw new ArgumentNullException("_valeur", "La valeur du noeud ne peut pas être nulle.");
            }
        }

        /// <summary>
        /// Obtient ou définit le noeud suivant du noeud
        /// </summary>
        public Noeud Suivant
        {
            get { return _suivant; }
            set { _suivant = value; }
        }

        /// <summary>
        /// Obtient ou définit le noeud précédent du noeud
        /// </summary>
        public Noeud Precedent
        {
            get { return _precedent; }
            set { _precedent = value; }
        }

        #endregion

        #region CONSTRUCTEURS

        /// <summary>
        /// Constructeur d'un noeud.
        /// </summary>
        public Noeud(Joueur pJoueur)
        {
            Valeur = pJoueur;
        }

        #endregion
    }
}
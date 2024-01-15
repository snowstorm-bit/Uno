#region MÉTADONNÉES

// Nom du fichier : Carte.cs
// Auteur : Mélina Hotte (1933760)
// Date de création : 2021-03-26
// Date de modification : 2021-04-16

#endregion

#region USING

using System;

#endregion

namespace _420_14C_FX_TP2.Classes
{
    /// <summary>
    /// Classe représentant une carte du jeu Uno.
    /// </summary>
    [SerializableAttribute]
    public class Carte : IComparable
    {
        #region ATTRIBUTS

        /// <summary>
        /// Couleur de la carte
        /// </summary>
        private Couleur _couleur;

        /// <summary>
        /// Valeur de la carte
        /// </summary>
        private Valeur _valeur;

        #endregion

        #region PROPRIÉTÉS ET INDEXEURS

        /// <summary>
        /// Obtient ou définit la couleur de la carte
        /// </summary>
        public Couleur Couleur
        {
            get { return _couleur; }
            set { _couleur = value; }
        }

        /// <summary>
        /// Obtient ou définit la valeur de la carte
        /// </summary>
        public Valeur Valeur
        {
            get { return _valeur; }
            set { _valeur = value; }
        }

        #endregion

        #region CONSTRUCTEUR

        /// <summary>
        /// Constructeur d'une carte du jeu Uno.
        /// </summary>
        /// <param name="pCouleur">Couleur de la carte</param>
        /// <param name="pValeur">Valeur de la carte</param>
        public Carte(Couleur pCouleur, Valeur pValeur)
        {
            Couleur = pCouleur;
            Valeur = pValeur;
        }

        #endregion

        #region MÉTHODE

        /// <summary>
        /// Permet de comparer deux cartes entre elles afin de pouvoir trier les cartes de la main d'un joueur.
        /// </summary>
        /// <param name="pObj">La carte avec laquelle l'instance courante est comparée</param>
        /// <returns>-1 si la carte est plus petite, 0 si égale, 1 si plus grande</returns>
        public int CompareTo(object pObj)
        {
            Carte carteDeParam = pObj as Carte;

            if (carteDeParam != null && Couleur < carteDeParam.Couleur)
            {
                return -1;
            }
            else if (carteDeParam != null && Couleur == carteDeParam.Couleur)
            {
                if (Valeur < carteDeParam.Valeur)
                {
                    return -1;
                }
                else if (Valeur > carteDeParam.Valeur)
                {
                    return 1;
                }

                return 0;
            }

            return 1;
        }

        #endregion
    }
}
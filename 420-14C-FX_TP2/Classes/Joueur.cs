#region MÉTADONNÉES

// Nom du fichier : Joueur.cs
// Auteur : Mélina Hotte (1933760)
// Date de création : 2021-03-26
// Date de modification : 2021-04-16

#endregion

#region USING

using System;
using System.Collections.Generic;

#endregion

namespace _420_14C_FX_TP2.Classes
{
    /// <summary>
    /// Classe représentant un joueur de Uno.
    /// </summary>
    [SerializableAttribute]
    public class Joueur
    {
        #region CONSTANTES ET ATTRIBUTS STATIQUES

        /// <summary>
        /// Nombre de caractères minimum pour le nom d'un joueur
        /// </summary>
        public const int NOM_NB_CARAC_MIN = 3;

        #endregion

        #region ATTRIBUTS

        /// <summary>
        /// Nom du joueur
        /// </summary>
        private string _nom;

        /// <summary>
        /// Le joueur a crié ou non Uno puisqu'il ne reste qu'une seule carte dans sa main
        /// </summary>
        private bool _uno;

        /// <summary>
        /// La liste des cartes à jouer du joueur
        /// </summary>
        private List<Carte> _main;

        #endregion

        #region PROPRIÉTÉS ET INDEXEURS

        /// <summary>
        /// Obtient ou définit le nom du joueur
        /// </summary>
        /// <exception cref="ArgumentNullException">Lancée lorsque le nom du joueur est vide ou nul.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Lancée lorsque le nombre de caractères dans le nom du joueur est
        /// inférieur à la valeur de la constante du nombre de caractères requis pour le nom.</exception>
        public string Nom
        {
            get { return _nom; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException("_nom", "Le nom ne peut être vide ou nul");
                }

                if (value.Length < Joueur.NOM_NB_CARAC_MIN)
                {
                    throw new ArgumentOutOfRangeException("_nom",
                        $"Le nom du joueur doit contenir au moins {Joueur.NOM_NB_CARAC_MIN} caractères.");
                }

                _nom = value;
            }
        }

        /// <summary>
        /// Obtient ou définit si un jouer a dit Uno!
        /// </summary>
        public bool Uno
        {
            get { return _uno; }
            set { _uno = value; }
        }

        /// <summary>
        /// Obtient ou définit les cartes du joueur
        /// </summary>
        public List<Carte> Main
        {
            get { return _main; }
            set { _main = value; }
        }

        #endregion

        #region CONSTRUCTEURS

        /// <summary>
        /// Constructeur permettant de créer un joueur.
        /// </summary>
        /// <param name="pNom">Nom du joueur</param>
        public Joueur(string pNom)
        {
            Nom = pNom;
            Uno = false;
            Main = new List<Carte>();
        }

        #endregion

        #region MÉTHODES

        /// <summary>
        /// Permet de créer une chaîne de caractères avec le nom du joueur.
        /// </summary>
        /// <returns>Chaîne de caractères représentant le joueur.</returns>
        public override string ToString()
        {
            return Nom;
        }

        #endregion
    }
}
#region MÉTADONNÉES

// Nom du fichier : ListeJoueurs.cs
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
    /// Classe représentant la liste des joueurs d'une partie de Uno en cours.
    /// </summary>
    [SerializableAttribute]
    public class ListeJoueurs
    {
        #region ATTRIBUTS

        /// <summary>
        /// Noeud du début de la liste des joueurs
        /// </summary>
        private Noeud _debut;

        /// <summary>
        /// Taille de la liste des joueurs
        /// </summary>
        private int _taille;

        #endregion

        #region PROPRIÉTÉS ET INDEXEURS

        /// <summary>
        /// Obtient ou définit le noeud du début de la liste des joueurs
        /// </summary>
        public Noeud Debut
        {
            get { return _debut; }
            private set { _debut = value; }
        }

        /// <summary>
        /// Obtient ou définit la taille de la liste des joueurs
        /// </summary>
        public int Taille
        {
            get { return _taille; }
            private set { _taille = value; }
        }

        #endregion

        #region CONSTRUCTEURS

        /// <summary>
        /// Constructeur d'une liste de joueurs de la partie de Uno.
        /// </summary>
        public ListeJoueurs()
        {
            Init();
        }

        #endregion

        #region MÉTHODES

        /// <summary>
        /// Permet d'ajouter le noeud d'un joueur au début de la liste des joueurs.
        /// </summary>
        /// <param name="pValeur">Valeur du joueur à ajouter à la liste des joueurs.</param>
        /// <exception cref="ArgumentOutOfRangeException">Lancée lorsque l'on tente d'ajouter plus de 4 joueurs à la partie de Uno.</exception>
        public void AjouterDebut(Joueur pValeur)
        {
            if (Taille + 1 > 4)
            {
                throw new ArgumentOutOfRangeException(nameof(pValeur),
                    "Il est impossible d'ajouter plus de 4 joueurs à la partie.");
            }

            Noeud nouvNoeud = new Noeud(pValeur);

            //S'il n'y a qu'un élément dans la liste
            if (Debut == null)
            {
                Debut = nouvNoeud;
            }
            else
            {
                //S'il y a au moins 2 joueurs dans la liste des joueurs, le noeud fin est le noeud suivant du début,
                //sinon le noeud de fin est Debut puisque suivant est nul
                Noeud noeudFin = Debut.Suivant ?? Debut;

                //Introduction du nouveau noeud dans la liste
                nouvNoeud.Precedent = noeudFin;
                nouvNoeud.Suivant = Debut;

                //Permet la double liaison circulaire de la liste
                noeudFin.Suivant = nouvNoeud;
                Debut.Precedent = nouvNoeud;

                //Permet de mettre le dernier élément ajouté au début de la liste
                Debut = nouvNoeud;
            }

            Taille++;
        }

        /// <summary>
        /// Permet d'ajouter le noeud d'un joueur à la fin de la liste des joueurs.
        /// </summary>
        /// <param name="pValeur">Valeur du joueur à ajouter à la liste des joueurs.</param>
        /// <exception cref="ArgumentOutOfRangeException">Lancée lorsque l'on tente d'ajouter plus de 4 joueurs à la partie de Uno.</exception>
        public void AjouterFin(Joueur pValeur)
        {
            if (Taille + 1 > 4)
            {
                throw new ArgumentOutOfRangeException(nameof(pValeur),
                    "Il est impossible d'ajouter plus de 4 joueurs à la partie.");
            }

            Noeud nouvNoeud = new Noeud(pValeur);

            //S'il n'y a qu'un élément dans la liste
            if (Debut == null)
            {
                Debut = nouvNoeud;
            }
            else
            {
                //S'il y a au moins 2 joueurs dans la liste des joueurs, le noeud fin est le noeud précédent du début,
                //sinon le noeud de fin est Debut puisque précédent est nul
                Noeud noeudFin = Debut.Precedent ?? Debut;

                //Introduction du nouveau noeud dans la liste
                noeudFin.Suivant = nouvNoeud;
                Debut.Precedent = nouvNoeud;

                //Permet la double liaison circulaire de la liste
                nouvNoeud.Precedent = noeudFin;
                nouvNoeud.Suivant = Debut;
            }

            Taille++;
        }

        /// <summary>
        /// Permet de trouver le joueur gagnant la partie de Uno.
        /// </summary>
        /// <remarks>Le joueur est gagnant lorsqu'il n'a plus de carte dans sa main.</remarks>
        /// <returns>Le joueur gagnant de la partie de Uno.</returns>
        public Joueur TrouverGagnant()
        {
            Joueur joueurGagnant = null;

            Noeud joueurCourant = Debut;
            for (int i = 0; i < Taille; i++)
            {
                if (joueurCourant.Valeur.Main.Count == 0)
                {
                    joueurGagnant = joueurCourant.Valeur;
                }
            }

            return joueurGagnant;
        }

        /// <summary>
        /// Permet de réinitialiser la liste des joueurs.
        /// </summary>
        public void Effacer()
        {
            Init();
        }

        /// <summary>
        /// Permet de mettre les attributs de la liste des joueurs à leur valeur initiale.
        /// </summary>
        private void Init()
        {
            _debut = null;
            Taille = 0;
        }

        #endregion
    }
}
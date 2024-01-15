#region MÉTADONNÉES

// Nom du fichier : JeuCarte.cs
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
    /// Classe représentant un jeu de cartes Uno.
    /// </summary>
    [SerializableAttribute]
    public class JeuCarte
    {
        #region ATTRIBUTS

        /// <summary>
        /// Jeu de cartes de la partie en cours
        /// </summary>
        private Stack<Carte> _cartes;

        #endregion

        #region PROPRIÉTÉS ET INDEXEURS

        /// <summary>
        /// Obtient ou définit pile de cartes du jeu de cartes
        /// </summary>
        public Stack<Carte> Cartes
        {
            get { return _cartes; }
            set { _cartes = value; }
        }

        /// <summary>
        /// Obtient le nombre de cartes dans la pile du jeu de cartes
        /// </summary>
        public int NbCartes
        {
            get { return Cartes.Count; }
        }

        #endregion

        #region CONSTRUCTEURS

        /// <summary>
        /// Constructeur permettant de construire un jeu de cartes contenant 108 cartes mélangées.
        /// </summary>
        /// <remarks>Ajoute les cartes à la pile et mélange le jeu.</remarks>
        public JeuCarte()
        {
            Cartes = new Stack<Carte>();

            //Ajout des 108 cartes requises dans le jeu
            foreach (Couleur couleur in Enum.GetValues(typeof(Couleur)))
                foreach (Valeur valeur in Enum.GetValues(typeof(Valeur)))
                {
                    int nbCartesPourAjout = 0;

                    switch (valeur)
                    {
                        case Valeur.Joker:
                        case Valeur.Plus4:
                            if (couleur == Couleur.Noir)
                            {
                                nbCartesPourAjout = 4;
                            }

                            break;
                        case Valeur.Zero:
                            if (couleur != Couleur.Noir)
                            {
                                nbCartesPourAjout = 1;
                            }

                            break;
                        default:
                            if (couleur != Couleur.Noir)
                            {
                                nbCartesPourAjout = 2;
                            }

                            break;
                    }

                    for (int i = 0; i < nbCartesPourAjout; i++)
                    {
                        Cartes.Push(new Carte(couleur, valeur));
                    }
                }

            Melanger();
        }

        /// <summary>
        /// Constructeur permettant de créer un jeu de cartes à partir d'une pile de cartes existante (ex. la défausse).
        /// </summary>
        /// <remarks>Remplace les cartes par celles reçues en paramètre et mélange le jeu.</remarks>
        public JeuCarte(Stack<Carte> pCartes)
        {
            Cartes = pCartes;
            Melanger();
        }

        #endregion

        #region MÉTHODES

        /// <summary>
        /// Permet de mélanger le jeu de cartes.
        /// </summary>
        /// <remarks>La méthode permet de retirer chacune des cartes à un endroit aléatoire et de l'ajouter dans un nouveau jeu de cartes.</remarks>
        private void Melanger()
        {
            Random rnd = new Random();
            Stack<Carte> jeuCartesNouv = new Stack<Carte>();

            while (Cartes.Count > 0)
            {
                Stack<Carte> cartesRetirees = new Stack<Carte>();

                //Sélectionne le nombre de cartes/position dans le jeu de cartes
                int nbCartesARetirer = rnd.Next(0, Cartes.Count - 1);

                //Retire les cartes du jeu de carte jusqu'à la position sélectionnée aléatoirement dans la pile temporaire
                for (int i = 0; i < nbCartesARetirer; i++)
                {
                    cartesRetirees.Push(Cartes.Pop());
                }

                //Retire la carte sur le dessus pour l'ajouter à la nouvelle pile du jeu de cartes
                jeuCartesNouv.Push(Cartes.Pop());

                //Remet les cartes retirées dans le jeu de cartes. Donc, il reste une carte en moins.
                foreach (Carte carteDepilee in cartesRetirees)
                {
                    Cartes.Push(carteDepilee);
                }
            }

            Cartes = jeuCartesNouv;
        }

        /// <summary>
        /// Permet de retirer un nombre de cartes dans le jeu de cartes.
        /// </summary>
        /// <param name="pNbCartes">Nombre de cartes à retirer</param>
        /// <returns>Liste contenant les cartes retirées du jeu de cartes</returns>
        public List<Carte> RetirerCartes(int pNbCartes = 1)
        {
            List<Carte> lstCartesNouv = new List<Carte>();
            for (int i = 0; i < pNbCartes; i++)
            {
                lstCartesNouv.Add(Cartes.Pop());
            }

            return lstCartesNouv;
        }

        #endregion
    }
}
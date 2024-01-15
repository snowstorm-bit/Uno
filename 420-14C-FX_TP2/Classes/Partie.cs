#region MÉTADONNÉES

// Nom du fichier : Partie.cs
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
    /// Classe représentant une partie de jeu de Uno et permet la gestion de celle-ci.
    /// </summary>
    [SerializableAttribute]
    public class Partie
    {
        #region CONSTANTES ET ATTRIBUTS STATIQUES

        /// <summary>
        /// Nombre de cartes requises pour la main d'un joueur
        /// </summary>
        private const int NB_CARTES_MAIN = 7;

        #endregion

        #region ATTRIBUTS

        /// <summary>
        /// Pioche de la partie de Uno en cours
        /// </summary>
        private JeuCarte _pioche;

        /// <summary>
        /// Défausse de la partie de Uno en cours
        /// </summary>
        private Stack<Carte> _defausse;

        /// <summary>
        /// Liste des joueurs de la partie de Uno en cours
        /// </summary>
        private ListeJoueurs _joueurs;

        /// <summary>
        /// Joueur devant jouer son tour
        /// </summary>
        private Noeud _joueurCourant;

        /// <summary>
        /// Indique le sens de rotation du jeu
        /// </summary>
        private bool _sensHoraire = true;

        #endregion

        #region PROPRIÉTÉS ET INDEXEURS

        /// <summary>
        /// Obtient ou définit la pioche du jeu de Uno en cours
        /// </summary>
        public JeuCarte Pioche
        {
            get { return _pioche; }
            set { _pioche = value; }
        }

        /// <summary>
        /// Obtient ou définit la pile contenant les cartes jouées de la partie de Uno en cours
        /// </summary>
        public Stack<Carte> Defausse
        {
            get { return _defausse; }
            set { _defausse = value; }
        }

        /// <summary>
        /// Obtient ou définit la liste des joueurs
        /// </summary>
        public ListeJoueurs Joueurs
        {
            get { return _joueurs; }
            set { _joueurs = value; }
        }

        /// <summary>
        /// Obtient ou définit le noeud du joueur courant
        /// </summary>
        public Noeud JoueurCourant
        {
            get { return _joueurCourant; }
            set { _joueurCourant = value; }
        }

        #endregion

        #region CONSTRUCTEURS

        /// <summary>
        /// Constructeur d'une partie de jeu Uno.
        /// </summary>
        public Partie()
        {
            _joueurs = new ListeJoueurs();
        }

        #endregion

        #region MÉTHODES

        /// <summary>
        /// Permet de démarrer une partie. 
        /// </summary>
        /// <returns>True s'il y a au moins 2 joueurs dans la partie. False sinon.</returns>
        /// <remarks>7 cartes sont distribuées (une à la fois) à chaque joueur. Une première carte est tournée sur la défausse et le joueur courant est initialisé. La première carte tournée ne doit pas être une carte spéciale (+2, +4, Joker, InverserSens ou SauterTour).</remarks>
        public bool DemarrerPartie()
        {
            if (Joueurs.Taille > 1)
            {
                JoueurCourant = Joueurs.Debut;

                //Parcours tous les joueurs de la liste
                for (int i = 0; i < Joueurs.Taille; i++)
                {
                    //Si la main du joueur n'est pas vide
                    if (JoueurCourant.Valeur.Main.Count > 0)
                    {
                        JoueurCourant.Valeur.Main.Clear();
                    }

                    JoueurSuivant();
                }

                Pioche = new JeuCarte();
                DistribuerCartes();
                InitialiserDefausse();

                return true;
            }

            return false;
        }

        /// <summary>
        /// Permet de distribuer les cartes aux joueurs.
        /// </summary>
        /// <remarks>Chaque joueur se voit distribuer une carte à la fois.</remarks>
        private void DistribuerCartes()
        {
            //Distribution des cartes tour à tour
            for (int i = 0; i < Partie.NB_CARTES_MAIN; i++)
                for (int j = 0; j < Joueurs.Taille; j++)
                {
                    //Tri de la main du joueur courant pour la carte retirée de la pioche
                    Carte carteATrier = Pioche.Cartes.Pop();
                    JoueurCourant.Valeur.Main.Insert(TrierCarte(JoueurCourant.Valeur.Main, carteATrier), carteATrier);
                    JoueurSuivant();
                }
        }

        /// <summary>
        /// Permet de créer une nouvelle défausse et de tourner la première carte de la pioche sur la défausse.
        /// </summary>
        /// <remarks>La valeur de la carte tournée ne doit pas être une autre qu'un chiffre.</remarks>
        private void InitialiserDefausse()
        {
            Defausse = new Stack<Carte>();
            bool premiereCarteEstOk = false;
            while (!premiereCarteEstOk)
            {
                Carte cartePourDefausse = Pioche.Cartes.Pop();
                Defausse.Push(cartePourDefausse);

                if (cartePourDefausse.Couleur != Couleur.Noir &&
                    cartePourDefausse.Valeur != Valeur.Plus2 &&
                    cartePourDefausse.Valeur != Valeur.InverserSens &&
                    cartePourDefausse.Valeur != Valeur.SauterTour)
                {
                    premiereCarteEstOk = true;
                }
            }
        }

        /// <summary>
        /// Permet à un joueur de piger des cartes dans la pioche.
        /// </summary>
        /// <param name="pJoueur">Le joueur qui pige la carte</param>
        /// <param name="pNbCartes">Le nombre de cartes pigées</param>
        /// <remarks>S'il n'y a plus de carte dans la pioche durant la pige, alors on refait la pioche à partir de la défausse et on pige les cartes restantes à piger. La main du joueur doit être triée après la pige et on passe au joueur suivant.</remarks>
        public void PigerCartes(Joueur pJoueur, int pNbCartes)
        {
            List<Carte> lstCartesPigees;

            int nbCartesApresPioche = Pioche.NbCartes - pNbCartes;

            //Si la pioche sera vide lors de la pige
            if (nbCartesApresPioche <= 0)
            {
                //Retirer toutes les cartes restantes dans la pioche
                lstCartesPigees = Pioche.RetirerCartes(Pioche.NbCartes);

                //Refait la pioche avec la défausse sans la première carte sur la défausse 
                Carte carteDessus = Defausse.Pop();

                Pioche = new JeuCarte(Defausse);

                Defausse.Clear();
                Defausse.Push(carteDessus);

                //Retire le nombre de cartes moins le nombre de cartes déjà retirées
                nbCartesApresPioche *= -1;

                List<Carte> lstCartesPigeesTemp = Pioche.RetirerCartes(nbCartesApresPioche);
                for (int i = 0; i < lstCartesPigeesTemp.Count; i++)
                {
                    lstCartesPigees.Add(lstCartesPigeesTemp[i]);
                }
            }
            else
            {
                lstCartesPigees = Pioche.RetirerCartes(pNbCartes);
            }

            //Ajout et tri des cartes retirées dans la main du joueur
            if (lstCartesPigees.Count > 0)
            {
                foreach (Carte cartePigee in lstCartesPigees)
                {
                    pJoueur.Main.Insert(TrierCarte(pJoueur.Main, cartePigee), cartePigee);
                }

                JoueurSuivant();
            }
        }

        /// <summary>
        /// Permet à un joueur de jouer une carte.
        /// </summary>
        /// <param name="pJoueur">Joueur qui joue son tour</param>
        /// <param name="pCarte">Carte jouée par le joueur</param>
        /// <exception cref="InvalidOperationException">Lancée lorsque la carte ne peut pas être jouée ou que le joueur n'a pas crié Uno avant de jouer son avant-dernière carte.</exception>
        public void JouerCarte(Joueur pJoueur, Carte pCarte)
        {
            //Validation sur la carte
            string msgErreur = "";
            if (pJoueur != JoueurCourant.Valeur)
            {
                msgErreur = "Ce n'est pas votre tour. Vous ne pouvez pas jouer votre carte.";
            }
            else if (!ValiderCarte(pCarte))
            {
                msgErreur = "La carte jouée est invalide.";
            }
            else if (pJoueur.Main.Count == 2 && !pJoueur.Uno)
            {
                msgErreur = "Il faut appuyer sur le bouton Uno avant de jouer l'avant-dernière carte.";
            }

            if (msgErreur != "")
            {
                throw new InvalidOperationException(msgErreur);
            }

            //Ajout de la carte retirée de la main du joueur
            if (pJoueur.Main.Remove(pCarte))
            {
                Defausse.Push(pCarte);

                switch (pCarte.Valeur)
                {
                    //Vérification sur la carte jouée
                    case Valeur.InverserSens:
                        _sensHoraire = !_sensHoraire;
                        if (Joueurs.Taille == 2)
                        {
                            JoueurSuivant();
                        }

                        break;
                    case Valeur.Plus2:
                        PigerCartes(JoueurCourant.Suivant.Valeur, 2);
                        break;
                    case Valeur.SauterTour:
                        JoueurSuivant();
                        break;
                }

                JoueurSuivant();
            }
        }

        /// <summary>
        /// Permet de se déplacer dans la liste selon le sens du jeu pour sélectionner le joueur suivant.
        /// </summary>
        public void JoueurSuivant()
        {
            if (Joueurs.Taille > 1)
            {
                JoueurCourant = _sensHoraire
                    ? JoueurCourant.Suivant
                    : JoueurCourant.Precedent;
            }
        }

        /// <summary>
        /// Permet de valider si la carte jouée est valide selon celle qui se trouve sur la défausse.
        /// </summary>
        /// <param name="pCarte">Carte jouée</param>
        /// <returns>True si la carte est valide, False sinon.</returns>
        private bool ValiderCarte(Carte pCarte)
        {
            Carte carteDefaussse = Defausse.Peek();
            return pCarte.Couleur == Couleur.Noir || carteDefaussse.Couleur == pCarte.Couleur ||
                   carteDefaussse.Valeur == pCarte.Valeur;
        }

        /// <summary>
        /// Permet de trier une liste de cartes selon sa couleur et sa valeur.
        /// </summary>
        /// <param name="pLstCartesPourTri">Liste des cartes pour trier</param>
        /// <param name="pCarteATrier">Carte à trier dans la liste</param>
        /// <returns>La position de la carte à trier dans la liste des cartes</returns>
        private int TrierCarte(List<Carte> pLstCartesPourTri, Carte pCarteATrier)
        {
            int indexCartePourTri = 0;

            //Trouve l'index de la carte avant celle à trier pour toutes les cartes de la même couleur
            while (indexCartePourTri < pLstCartesPourTri.Count &&
                   pCarteATrier.CompareTo(pLstCartesPourTri[indexCartePourTri]) < 0)
                indexCartePourTri++;

            return indexCartePourTri;
        }

        #endregion
    }
}
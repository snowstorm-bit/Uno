#region MÉTADONNÉES

// Nom du fichier : JeuTests.cs
// Auteur : Mélina Hotte (1933760)
// Date de création : 2021-04-10
// Date de modification : 2021-04-14

#endregion

#region USING

using System;
using System.Collections.Generic;
using System.Linq;
using _420_14C_FX_TP2.Classes;
using Xunit;

#endregion

namespace _420_14C_FX_TP2_Tests
{
    public class JeuTests
    {
        [Fact]
        public void Constructeur_Devrait_Creer_ListeJoueur_Vide_Quand_Est_Initialise()
        {
            // Arrange
            Partie partie = new Partie();

            // Assert
            Assert.Null(partie.Joueurs.Debut);
            Assert.Equal(0, partie.Joueurs.Taille);
        }

        #region TESTS DE DEMARRERPARTIE

        [Fact]
        public void DemarrerPartie_Devrait_Retourner_Faux_Quand_Nombre_Joueurs_Est_Moins_De_2()
        {
            // Arrange
            Partie partie = new Partie();
            partie.Joueurs.AjouterFin(new Joueur("Test_Joueur_1"));

            // Act et assert 
            Assert.False(partie.DemarrerPartie());
        }

        [Fact]
        public void DemarrerPartie_Devrait_Retourner_Vrai_Quand_Nombre_Joueurs_Est_Plus_De_1()
        {
            // Arrange
            Partie partieTest1 = new Partie();
            partieTest1.Joueurs.AjouterFin(new Joueur("Test_Joueur_1"));
            partieTest1.Joueurs.AjouterFin(new Joueur("Test_Joueur_2"));

            Partie partieTest2 = new Partie();
            partieTest2.Joueurs.AjouterFin(new Joueur("Test_Joueur_1"));
            partieTest2.Joueurs.AjouterFin(new Joueur("Test_Joueur_2"));
            partieTest2.Joueurs.AjouterFin(new Joueur("Test_Joueur_3"));

            // Act et assert 
            Assert.True(partieTest1.DemarrerPartie());
            Assert.True(partieTest2.DemarrerPartie());
        }

        [Fact]
        public void DemarrerPartie_Devrait_Determiner_Joueur_Courant_Avec_Debut_De_Joueurs()
        {
            // Arrange
            Partie partie = new Partie();
            partie.Joueurs.AjouterFin(new Joueur("Test_Joueur_1"));
            partie.Joueurs.AjouterFin(new Joueur("Test_Joueur_2"));
            partie.Joueurs.AjouterFin(new Joueur("Test_Joueur_3"));

            // Act
            partie.DemarrerPartie();

            // Assert
            Assert.Equal(partie.Joueurs.Debut, partie.JoueurCourant);
        }

        [Fact]
        public void DemarrerPartie_Devrait_Avoir_Pioche_Non_Vide()
        {
            // Arrange
            Partie partie = new Partie();
            partie.Joueurs.AjouterFin(new Joueur("Test_Joueur_1"));
            partie.Joueurs.AjouterFin(new Joueur("Test_Joueur_2"));

            // Act
            partie.DemarrerPartie();

            // Assert
            Assert.NotNull(partie.Pioche);
            Assert.True(partie.Pioche.NbCartes > 0);
        }

        [Fact]
        public void DemarrerPartie_Devrait_Avoir_Distribuer_7_Cartes_Pour_Chaque_Joueur_Dans_Joueurs()
        {
            // Arrange
            Partie partie = new Partie();
            partie.Joueurs.AjouterFin(new Joueur("Test_Joueur_1"));
            partie.Joueurs.AjouterFin(new Joueur("Test_Joueur_2"));
            partie.Joueurs.AjouterFin(new Joueur("Test_Joueur_3"));

            // Act
            partie.DemarrerPartie();

            // Assert
            Noeud joueurCourant = partie.Joueurs.Debut;

            for (int i = 0; i < partie.Joueurs.Taille; i++)
            {
                Assert.Equal(7, joueurCourant.Valeur.Main.Count);
                joueurCourant = joueurCourant.Suivant;
            }
        }

        [Fact]
        public void DemarrerPartie_Devrait_Avoir_Defausse_Non_Vide()
        {
            // Arrange
            Partie partie = new Partie();
            partie.Joueurs.AjouterFin(new Joueur("Test_Joueur_1"));
            partie.Joueurs.AjouterFin(new Joueur("Test_Joueur_2"));

            // Act
            partie.DemarrerPartie();

            // Assert
            Assert.NotNull(partie.Defausse);
            Assert.True(partie.Defausse.Count > 0);
        }

        [Fact]
        public void DemarrerPartie_Devrait_Contenir_108_Cartes_Requises_Dans_Partie_Apres_Initialisation_Defausse()
        {
            // Arrange
            Partie partie = new Partie();
            partie.Joueurs.AjouterFin(new Joueur("Test_Joueur_1"));
            partie.Joueurs.AjouterFin(new Joueur("Test_Joueur_2"));

            // Act
            partie.DemarrerPartie();

            // Obtient le nombre total dans la partie
            int nbCartes = partie.Pioche.NbCartes + partie.Defausse.Count;

            Noeud joueurCourant = partie.Joueurs.Debut;

            for (int i = 0; i < partie.Joueurs.Taille; i++)
            {
                nbCartes += joueurCourant.Valeur.Main.Count;
                joueurCourant = joueurCourant.Suivant;
            }

            // Assert
            Assert.Equal(108, nbCartes);

            // Vérifie s'il contient le bon nombre de cartes pour chaque couleur
            foreach (Couleur couleur in Enum.GetValues(typeof(Couleur)))
            {
                int nbCartesPourCouleurRequis = couleur == Couleur.Noir ? 8 : 25;

                int nbCartesPourCouleurDansPartie = 0;

                // Nombre de cartes pour la valeur et la couleur courante
                nbCartesPourCouleurDansPartie += partie.Pioche.Cartes.Count(pCarte => pCarte.Couleur == couleur);
                // Si le nombre toujours pas celui qu'il devrait être
                if (nbCartesPourCouleurRequis != nbCartesPourCouleurDansPartie)
                {
                    nbCartesPourCouleurDansPartie += partie.Defausse.Count(pCarte => pCarte.Couleur == couleur);
                }

                if (nbCartesPourCouleurRequis != nbCartesPourCouleurDansPartie)
                {
                    for (int i = 0; i < partie.Joueurs.Taille; i++)
                    {
                        nbCartesPourCouleurDansPartie +=
                            joueurCourant.Valeur.Main.Count(pCarte => pCarte.Couleur == couleur);
                        joueurCourant = joueurCourant.Suivant;
                    }
                }

                Assert.Equal(nbCartesPourCouleurRequis, nbCartesPourCouleurDansPartie);

                // Vérifie s'il contient le bon nombre de cartes pour chaque valeur
                foreach (Valeur valeur in Enum.GetValues(typeof(Valeur)))
                {
                    int nbCartesPourValeurRequis = 0;

                    switch (valeur)
                    {
                        case Valeur.Joker:
                        case Valeur.Plus4:
                            if (couleur == Couleur.Noir)
                            {
                                nbCartesPourValeurRequis = 4;
                            }

                            break;
                        case Valeur.Zero:
                            if (couleur != Couleur.Noir)
                            {
                                nbCartesPourValeurRequis = 1;
                            }

                            break;
                        case Valeur.Un:
                        case Valeur.Deux:
                        case Valeur.Trois:
                        case Valeur.Quatre:
                        case Valeur.Cinq:
                        case Valeur.Six:
                        case Valeur.Sept:
                        case Valeur.Huit:
                        case Valeur.Neuf:
                        case Valeur.Plus2:
                        case Valeur.InverserSens:
                        case Valeur.SauterTour:
                            if (couleur != Couleur.Noir)
                            {
                                nbCartesPourValeurRequis = 2;
                            }

                            break;
                    }

                    int nbCartesDansPartie = 0;

                    // Nombre de cartes pour la valeur et la couleur courante
                    nbCartesDansPartie +=
                        partie.Pioche.Cartes.Count(pCarte => pCarte.Valeur == valeur && pCarte.Couleur == couleur);
                    // Si le nombre toujours pas celui qu'il devrait être
                    if (nbCartesPourValeurRequis != nbCartesDansPartie)
                    {
                        nbCartesDansPartie +=
                            partie.Defausse.Count(pCarte => pCarte.Valeur == valeur && pCarte.Couleur == couleur);
                    }

                    if (nbCartesPourValeurRequis != nbCartesDansPartie)
                    {
                        for (int i = 0; i < partie.Joueurs.Taille; i++)
                        {
                            nbCartesDansPartie += joueurCourant.Valeur.Main.Count(pCarte =>
                                pCarte.Valeur == valeur && pCarte.Couleur == couleur);
                            joueurCourant = joueurCourant.Suivant;
                        }
                    }

                    // Assert
                    Assert.Equal(nbCartesPourValeurRequis, nbCartesDansPartie);
                }
            }
        }

        #endregion

        #region TESTS DE PIGERCARTES

        [Fact]
        public void PigerCarte_Devrait_Passer_Au_Joueur_Suivant_Apres_La_Pige()
        {
            // Arrange
            Partie partie = new Partie();
            partie.Joueurs.AjouterFin(new Joueur("Test_Joueur_1"));
            partie.Joueurs.AjouterFin(new Joueur("Test_Joueur_2"));

            // Act
            partie.JoueurCourant = partie.Joueurs.Debut;
            partie.Pioche = new JeuCarte();
            partie.PigerCartes(partie.JoueurCourant.Valeur, 2);

            // Assert
            Assert.False(partie.JoueurCourant == partie.Joueurs.Debut);
        }

        [Fact]
        public void PigerCarte_Devrait_Retirer_Nb_Cartes_Pigees_De_Pioche_Pour_Ajouter_A_Main_Du_Joueur_En_Parametre()
        {
            // Arrange
            Partie partie = new Partie();
            partie.Joueurs.AjouterFin(new Joueur("Test_Joueur_1"));
            partie.Joueurs.AjouterFin(new Joueur("Test_Joueur_2"));

            // Act 
            partie.JoueurCourant = partie.Joueurs.Debut;
            partie.Pioche = new JeuCarte();

            List<Carte> lstCartesPigees = new List<Carte>();
            int nbCartesPourPige = 3;

            // Copie des cartes retirées de la pioche
            for (int i = 0; i < nbCartesPourPige; i++)
            {
                lstCartesPigees.Add(partie.Pioche.Cartes.Pop());
            }

            // Remise des cartes dans la pioche pour l'appel de la méthode PigerCarte
            foreach (Carte carte in lstCartesPigees)
            {
                partie.Pioche.Cartes.Push(carte);
            }

            // Tri des cartes
            lstCartesPigees = JeuTests.TrierCartePourTestSurPigerCartes(lstCartesPigees);

            partie.JoueurCourant.Valeur.Main.Clear();

            int nbCartesDansPiocheAvantPige = partie.Pioche.NbCartes;
            partie.PigerCartes(partie.JoueurCourant.Valeur, nbCartesPourPige);

            // Assert
            Assert.Equal(nbCartesPourPige, nbCartesDansPiocheAvantPige - partie.Pioche.NbCartes);
            Assert.Equal(lstCartesPigees, partie.JoueurCourant.Precedent.Valeur.Main);
        }

        [Fact]
        public void PigerCarte_Devrait_Laisser_Premiere_Carte_Sur_Defausse_Quand_Pioche_Est_Vide_Durant_Pige()
        {
            // Arrange
            Partie partie = new Partie();
            partie.Joueurs.AjouterFin(new Joueur("Test_Joueur_1"));
            partie.Joueurs.AjouterFin(new Joueur("Test_Joueur_2"));

            partie.JoueurCourant = partie.Joueurs.Debut;

            // Ajout d'une seule carte dans la pioche
            Stack<Carte> cartes = new Stack<Carte>();
            cartes.Push(new Carte(Couleur.Jaune, Valeur.Cinq));

            partie.Pioche = new JeuCarte(cartes);

            // Initialisation de la défausse
            partie.Defausse = new Stack<Carte>();
            partie.Defausse.Push(new Carte(Couleur.Vert, Valeur.Neuf));
            partie.Defausse.Push(new Carte(Couleur.Rouge, Valeur.Trois));
            partie.Defausse.Push(new Carte(Couleur.Vert, Valeur.InverserSens));
            Carte carte = new Carte(Couleur.Bleu, Valeur.SauterTour);
            partie.Defausse.Push(carte);

            // Act
            partie.PigerCartes(partie.JoueurCourant.Valeur, partie.Pioche.NbCartes + 2);

            // Assert
            Assert.Equal(carte, partie.Defausse.Peek());
        }

        [Fact]
        public void PigerCarte_Devrait_Refaire_Pioche_Avec_Defausse_Sans_Carte_Dessus_Quand_Pioche_Est_Vide_Durant_Pige()
        {
            // Arrange
            Partie partie = new Partie();
            partie.Joueurs.AjouterFin(new Joueur("Test_Joueur_1"));
            partie.Joueurs.AjouterFin(new Joueur("Test_Joueur_2"));

            partie.JoueurCourant = partie.Joueurs.Debut;

            // Ajout d'une seule carte dans la pioche
            Stack<Carte> cartes = new Stack<Carte>();
            cartes.Push(new Carte(Couleur.Jaune, Valeur.Cinq));

            partie.Pioche = new JeuCarte(cartes);

            // Initialisation de la défausse
            partie.Defausse = new Stack<Carte>();
            partie.Defausse.Push(new Carte(Couleur.Vert, Valeur.Neuf));
            partie.Defausse.Push(new Carte(Couleur.Rouge, Valeur.Trois));
            partie.Defausse.Push(new Carte(Couleur.Vert, Valeur.InverserSens));
            Carte carte = new Carte(Couleur.Bleu, Valeur.SauterTour);
            partie.Defausse.Push(carte);

            int nbCartePourPige = partie.Pioche.NbCartes + 2;
            int nbCartesAttendueDansPioche = partie.Defausse.Count - nbCartePourPige;

            // Act
            partie.PigerCartes(partie.JoueurCourant.Valeur, nbCartePourPige);

            // Assert
            Assert.Equal(nbCartesAttendueDansPioche, partie.Pioche.NbCartes);
        }

        [Fact]
        public void PigerCarte_Devrait_Ajouter_Bonnes_Cartes_Dans_Main_Du_Joueur_En_Parametre_Alors_Que_Pioche_Est_Vide_Durant_Pige()
        {
            // Arrange
            Partie partie = new Partie();
            partie.Joueurs.AjouterFin(new Joueur("Test_Joueur_1"));
            partie.Joueurs.AjouterFin(new Joueur("Test_Joueur_2"));

            Carte carte1 = new Carte(Couleur.Jaune, Valeur.Cinq);
            Carte carte2 = new Carte(Couleur.Vert, Valeur.Neuf);
            Carte carte3 = new Carte(Couleur.Rouge, Valeur.Trois);
            Carte carte4 = new Carte(Couleur.Vert, Valeur.InverserSens);
            Carte carte5 = new Carte(Couleur.Bleu, Valeur.SauterTour);

            List<Carte> lstCartesAttendue =
                JeuTests.TrierCartePourTestSurPigerCartes(new List<Carte> { carte1, carte2, carte3, carte4 });

            partie.JoueurCourant = partie.Joueurs.Debut;

            // Ajout d'une seule carte dans la pioche
            Stack<Carte> cartes = new Stack<Carte>();
            cartes.Push(new Carte(Couleur.Jaune, Valeur.Cinq));
            partie.Pioche = new JeuCarte(cartes);

            // Initialisation de la défausse
            partie.Defausse = new Stack<Carte>();
            partie.Defausse.Push(carte2);
            partie.Defausse.Push(carte3);
            partie.Defausse.Push(carte4);
            partie.Defausse.Push(carte5);

            int nbCartesPourPige = partie.Pioche.NbCartes + 3;

            // Act
            partie.PigerCartes(partie.JoueurCourant.Valeur, nbCartesPourPige);

            // Assert
            Assert.Equal(lstCartesAttendue, partie.JoueurCourant.Precedent.Valeur.Main);
        }

        private static List<Carte> TrierCartePourTestSurPigerCartes(List<Carte> pLstCartesPourTri)
        {
            // Tri des cartes
            List<Carte> lstCartesTriee = new List<Carte>();

            foreach (Carte carte in pLstCartesPourTri)
            {
                int indexCartePourTri = 0;

                //Trouve l'index de la carte avant celle à trier pour toutes les cartes de la même couleur
                while (indexCartePourTri < lstCartesTriee.Count &&
                       carte.CompareTo(lstCartesTriee[indexCartePourTri]) < 0)
                    indexCartePourTri++;

                lstCartesTriee.Insert(indexCartePourTri, carte);
            }

            return lstCartesTriee;
        }

        #endregion

        #region TESTS DE JOUERCARTE

        [Fact]
        public void JouerCarte_Devrait_Lancer_InvalidOperationException_Quand_Joueur_En_Parametre_Est_Pas_JoueurCourant()
        {
            // Arrange
            Partie partie = new Partie();
            partie.Joueurs.AjouterFin(new Joueur("Test_Joueur_1"));
            partie.Joueurs.AjouterFin(new Joueur("Test_Joueur_2"));

            partie.JoueurCourant = partie.Joueurs.Debut;

            // Assert et act
            Assert.Throws<InvalidOperationException>(() => partie.JouerCarte(partie.JoueurCourant.Suivant.Valeur,
                new Carte(Couleur.Vert, Valeur.InverserSens)));
        }

        [Fact]
        public void JouerCarte_Devrait_Lancer_InvalidOperationException_Quand_Carte_Jouee_Est_Invalide()
        {
            // Arrange
            Partie partie = new Partie();
            partie.Joueurs.AjouterFin(new Joueur("Test_Joueur_1"));

            partie.JoueurCourant = partie.Joueurs.Debut;
            partie.Defausse = new Stack<Carte>();

            partie.Defausse.Push(new Carte(Couleur.Vert, Valeur.Cinq));

            // Assert et act
            Assert.Throws<InvalidOperationException>(() => partie.JouerCarte(partie.Joueurs.Debut.Valeur,
                new Carte(Couleur.Rouge, Valeur.InverserSens)));
        }

        [Fact]
        public void JouerCarte_Devrait_Lancer_InvalidOperationException_Quand_Joueur_Pas_Appuyer_Uno_Avant_Jouer_Avant_Derniere_Carte()
        {
            // Arrange
            Partie partie = new Partie();
            partie.Joueurs.AjouterFin(new Joueur("Test_Joueur_1"));

            partie.JoueurCourant = partie.Joueurs.Debut;
            partie.Defausse = new Stack<Carte>();
            Carte carteARetirer = new Carte(Couleur.Jaune, Valeur.Cinq);
            partie.Defausse.Push(carteARetirer);

            partie.JoueurCourant.Valeur.Main.Add(new Carte(Couleur.Bleu, Valeur.Huit));
            partie.JoueurCourant.Valeur.Main.Add(new Carte(Couleur.Rouge, Valeur.Quatre));

            // Act et Assert
            Assert.Throws<InvalidOperationException>(() => partie.JouerCarte(partie.JoueurCourant.Valeur, carteARetirer));
        }

        [Fact]
        public void JouerCarte_Devrait_Passer_Au_Joueur_Suivant_Quand_Carte_Jouee_Est_Valide()
        {
            // Arrange
            Partie partie = new Partie();
            Joueur joueurSuivant = new Joueur("Test_Joueur_2");
            partie.Joueurs.AjouterFin(new Joueur("Test_Joueur_1"));
            partie.Joueurs.AjouterFin(joueurSuivant);

            Carte carteARetirer = new Carte(Couleur.Jaune, Valeur.Cinq);

            partie.JoueurCourant = partie.Joueurs.Debut;
            partie.Defausse = new Stack<Carte>();

            partie.Defausse.Push(carteARetirer);

            partie.Joueurs.Debut.Valeur.Main.Add(carteARetirer);

            // Act
            partie.JouerCarte(partie.JoueurCourant.Valeur, carteARetirer);

            // Assert
            Assert.Equal(joueurSuivant, partie.JoueurCourant.Valeur);
        }

        [Fact]
        public void JouerCarte_Devrait_Retirer_Carte_De_Main_Joueur_En_Parametre_Quand_Est_Valide()
        {
            // Arrange
            Partie partie = new Partie();
            partie.Joueurs.AjouterFin(new Joueur("Test_Joueur_1"));

            Carte carteARetirer = new Carte(Couleur.Jaune, Valeur.Cinq);

            List<Carte> lstCartesPourJoueur = new List<Carte>
            {
                carteARetirer,
                new Carte(Couleur.Vert, Valeur.Neuf),
                new Carte(Couleur.Rouge, Valeur.Trois),
                new Carte(Couleur.Vert, Valeur.InverserSens),
                new Carte(Couleur.Bleu, Valeur.SauterTour)
            };

            partie.JoueurCourant = partie.Joueurs.Debut;
            partie.Defausse = new Stack<Carte>();

            partie.Defausse.Push(carteARetirer);

            partie.Joueurs.Debut.Valeur.Main = lstCartesPourJoueur;

            lstCartesPourJoueur.Remove(carteARetirer);

            // Act
            partie.JouerCarte(partie.Joueurs.Debut.Valeur, carteARetirer);

            // Assert
            Assert.Equal(lstCartesPourJoueur, partie.Joueurs.Debut.Valeur.Main);
        }

        [Fact]
        public void JouerCarte_Devrait_Ajouter_Carte_A_Defausse_Quand_Est_Valide()
        {
            // Arrange
            Partie partie = new Partie();
            partie.Joueurs.AjouterFin(new Joueur("Test_Joueur_1"));


            partie.JoueurCourant = partie.Joueurs.Debut;
            partie.Defausse = new Stack<Carte>();

            Carte carteARetirer = new Carte(Couleur.Jaune, Valeur.Cinq);
            partie.Defausse.Push(carteARetirer);

            partie.Joueurs.Debut.Valeur.Main.Add(carteARetirer);

            // Act
            partie.JouerCarte(partie.Joueurs.Debut.Valeur, carteARetirer);

            // Assert
            Assert.Equal(carteARetirer, partie.Defausse.Peek());
        }

        [Fact]
        public void JouerCarte_Devrait_Inverser_Sens_Partie_Quand_Carte_InverserSens_Est_Jouee()
        {
            // Arrange partie 0 pour 1 joueurs
            Partie partie0 = new Partie();
            Joueur joueurPrecedent0 = new Joueur("Test_Joueur_1");

            partie0.Joueurs.AjouterFin(joueurPrecedent0);

            partie0.JoueurCourant = partie0.Joueurs.Debut;
            partie0.Defausse = new Stack<Carte>();

            Carte carteARetirer = new Carte(Couleur.Jaune, Valeur.InverserSens);
            partie0.Defausse.Push(carteARetirer);

            List<Carte> lstCartesPourJoueur = new List<Carte>
            {
                carteARetirer,
                new Carte(Couleur.Vert, Valeur.Neuf),
                new Carte(Couleur.Rouge, Valeur.Trois),
                new Carte(Couleur.Vert, Valeur.InverserSens),
                new Carte(Couleur.Bleu, Valeur.SauterTour)
            };

            partie0.Joueurs.Debut.Valeur.Main = lstCartesPourJoueur;

            // Arrange partie 1 pour 2 joueurs
            Partie partie1 = new Partie();

            partie1.Joueurs.AjouterFin(joueurPrecedent0);
            partie1.Joueurs.AjouterFin(new Joueur("Test_Joueur_2"));

            partie1.JoueurCourant = partie1.Joueurs.Debut;
            partie1.Defausse = new Stack<Carte>();

            partie1.Defausse.Push(carteARetirer);

            partie1.Joueurs.Debut.Valeur.Main = lstCartesPourJoueur;

            // Arrange partie 2 pour 3 joueurs
            Partie partie2 = new Partie();
            Joueur joueurPrecedent2 = new Joueur("Test_Joueur_3");

            partie2.Joueurs.AjouterFin(new Joueur("Test_Joueur_1"));
            partie2.Joueurs.AjouterFin(new Joueur("Test_Joueur_2"));
            partie2.Joueurs.AjouterFin(joueurPrecedent2);

            partie2.JoueurCourant = partie2.Joueurs.Debut;
            partie2.Defausse = new Stack<Carte>();

            partie2.Defausse.Push(carteARetirer);
            partie2.Joueurs.Debut.Valeur.Main = lstCartesPourJoueur;

            // Act
            partie0.JouerCarte(partie0.JoueurCourant.Valeur, carteARetirer);
            lstCartesPourJoueur.Add(carteARetirer);
            partie1.JouerCarte(partie1.JoueurCourant.Valeur, carteARetirer);
            lstCartesPourJoueur.Add(carteARetirer);
            partie2.JouerCarte(partie2.JoueurCourant.Valeur, carteARetirer);

            // Assert
            Assert.Equal(joueurPrecedent0, partie0.JoueurCourant.Valeur);
            Assert.Equal(joueurPrecedent0, partie1.JoueurCourant.Valeur);
            Assert.Equal(joueurPrecedent2, partie2.JoueurCourant.Valeur);
        }

        [Fact]
        public void JouerCarte_Devrait_Ajouter_Plus_2_Cartes_Au_Joueur_Suivant_Quand_Carte_Plus2_Est_Jouee()
        {
            // Arrange partie 1 pour 2 joueurs
            Partie partie1 = new Partie();

            Joueur joueurPrecedent0 = new Joueur("Test_Joueur_1");

            partie1.Joueurs.AjouterFin(joueurPrecedent0);
            partie1.Joueurs.AjouterFin(new Joueur("Test_Joueur_2"));

            partie1.JoueurCourant = partie1.Joueurs.Debut;
            partie1.Defausse = new Stack<Carte>();

            partie1.Pioche = new JeuCarte();

            Carte carteARetirer = new Carte(Couleur.Jaune, Valeur.Plus2);

            List<Carte> lstCartesPourJoueur = new List<Carte>
            {
                carteARetirer,
                new Carte(Couleur.Vert, Valeur.Neuf),
                new Carte(Couleur.Rouge, Valeur.Trois),
                new Carte(Couleur.Vert, Valeur.InverserSens),
                new Carte(Couleur.Bleu, Valeur.SauterTour)
            };

            partie1.Defausse.Push(carteARetirer);
            partie1.Joueurs.Debut.Valeur.Main = lstCartesPourJoueur;
            int nbCartesPourJoueurSuivant1 = partie1.Joueurs.Debut.Suivant.Valeur.Main.Count;

            // Arrange partie 2 pour 3 joueurs
            Partie partie2 = new Partie();
            Joueur joueurPrecedent2 = new Joueur("Test_Joueur_3");

            partie2.Joueurs.AjouterFin(new Joueur("Test_Joueur_1"));
            partie2.Joueurs.AjouterFin(new Joueur("Test_Joueur_2"));
            partie2.Joueurs.AjouterFin(joueurPrecedent2);

            partie2.JoueurCourant = partie2.Joueurs.Debut;
            partie2.Defausse = new Stack<Carte>();

            partie2.Pioche = new JeuCarte();

            partie2.Defausse.Push(carteARetirer);
            partie2.Joueurs.Debut.Valeur.Main = lstCartesPourJoueur;
            int nbCartesPourJoueurSuivant2 = partie2.Joueurs.Debut.Suivant.Valeur.Main.Count;

            // Act
            partie1.JouerCarte(partie1.JoueurCourant.Valeur, carteARetirer);
            lstCartesPourJoueur.Add(carteARetirer);
            partie2.JouerCarte(partie2.JoueurCourant.Valeur, carteARetirer);

            // Assert
            Assert.Equal(joueurPrecedent0, partie1.JoueurCourant.Valeur);
            Assert.Equal(joueurPrecedent2, partie2.JoueurCourant.Valeur);

            Assert.Equal(nbCartesPourJoueurSuivant1 + 2, partie1.JoueurCourant.Precedent.Valeur.Main.Count);
            Assert.Equal(nbCartesPourJoueurSuivant2 + 2, partie2.JoueurCourant.Precedent.Valeur.Main.Count);
        }

        [Fact]
        public void JouerCarte_Devrait_Sauter_Tour_Du_Joueur_Suivant_Quand_Carte_SauterTour_Est_Jouee()
        {
            // Arrange partie 1 pour 2 joueurs
            Partie partie1 = new Partie();
            Joueur joueurPrecedent1 = new Joueur("Test_Joueur_1");

            partie1.Joueurs.AjouterFin(joueurPrecedent1);
            partie1.Joueurs.AjouterFin(new Joueur("Test_Joueur_2"));

            partie1.JoueurCourant = partie1.Joueurs.Debut;
            partie1.Defausse = new Stack<Carte>();

            Carte carteARetirer1 = new Carte(Couleur.Jaune, Valeur.SauterTour);
            partie1.Defausse.Push(carteARetirer1);

            partie1.Joueurs.Debut.Valeur.Main.Add(carteARetirer1);

            // Arrange partie 2 pour 3 joueurs
            Partie partie2 = new Partie();
            Joueur joueurPrecedent2 = new Joueur("Test_Joueur_3");

            partie2.Joueurs.AjouterFin(new Joueur("Test_Joueur_1"));
            partie2.Joueurs.AjouterFin(new Joueur("Test_Joueur_2"));
            partie2.Joueurs.AjouterFin(joueurPrecedent2);

            partie2.JoueurCourant = partie2.Joueurs.Debut;
            partie2.Defausse = new Stack<Carte>();

            Carte carteARetirer2 = new Carte(Couleur.Jaune, Valeur.SauterTour);
            partie2.Defausse.Push(carteARetirer2);

            partie2.Joueurs.Debut.Valeur.Main.Add(carteARetirer2);

            // Act
            partie1.JouerCarte(partie1.JoueurCourant.Valeur, carteARetirer1);
            partie2.JouerCarte(partie2.JoueurCourant.Valeur, carteARetirer2);

            // Assert
            Assert.Equal(joueurPrecedent1, partie1.JoueurCourant.Valeur);
            Assert.Equal(joueurPrecedent2, partie2.JoueurCourant.Valeur);
        }

        #endregion

        [Fact]
        public void JoueurSuivant_Devrait_Passer_Au_Joueur_Suivant()
        {
            // Arrange
            Partie partie1 = new Partie();
            partie1.Joueurs.AjouterFin(new Joueur("Test_Joueur_1"));

            Partie partie2 = new Partie();
            Joueur joueurSuivant = new Joueur("Test_Joueur_2");

            partie2.Joueurs.AjouterFin(new Joueur("Test_Joueur_1"));
            partie2.Joueurs.AjouterFin(joueurSuivant);

            partie1.JoueurCourant = partie1.Joueurs.Debut;
            partie2.JoueurCourant = partie2.Joueurs.Debut;

            // Act
            partie1.JoueurSuivant();
            partie2.JoueurSuivant();

            // Assert
            Assert.Equal(partie1.Joueurs.Debut, partie1.JoueurCourant);
            Assert.Equal(joueurSuivant, partie2.JoueurCourant.Valeur);
        }
    }
}
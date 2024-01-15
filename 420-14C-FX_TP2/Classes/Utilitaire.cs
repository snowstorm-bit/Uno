#region MÉTADONNÉES

// Nom du fichier : Utilitaire.cs
// Auteur : Mélina Hotte (1933760)
// Date de création : 2021-03-26
// Date de modification : 2021-04-16

#endregion

#region USING

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using Konscious.Security.Cryptography;

#endregion

namespace _420_14C_FX_TP2.Classes
{
    /// <summary>
    /// Classe utilitaire permettant de sérialiser et désérialiser des objets pour la gestion des utilisateurs.
    /// </summary>
    public static class Utilitaire
    {
        #region CONSTANTES ET ATTRIBUTS STATIQUES

        /// <summary>
        /// Chemin du fichier contenant les informations des utilisateurs
        /// </summary>
        private static string _CHEMIN_FICHIER_UTILISATEURS = Path.GetDirectoryName(
            System.Reflection.Assembly.GetExecutingAssembly().Location) + "/utilisateurs.dat";

        #endregion

        #region MÉTHODES

        /// <summary>
        /// Permet d'obtenir la liste des utilisateurs.
        /// </summary>
        /// <returns>Dictionnaire de (nom, (salt, motPasse))></returns>
        public static Dictionary<string, (byte[], byte[])> ObtenirUtilisateurs()
        {
            Dictionary<string, (byte[], byte[])> dictUtilisateurs = new Dictionary<string, (byte[], byte[])>();


            if (!File.Exists(_CHEMIN_FICHIER_UTILISATEURS))
            {
                File.Create(_CHEMIN_FICHIER_UTILISATEURS).Close();
            }

            //Lecture du fichier
            StreamReader fichierEntree = new StreamReader(_CHEMIN_FICHIER_UTILISATEURS);

            string[] vectLignes = fichierEntree.ReadToEnd()
                .Replace("\r", "")
                .Split('\n');

            foreach (string ligne in vectLignes)
            {
                if (ligne != "")
                {
                    string[] vectChamps = ligne.Split(';');

                    //Ajout des champs au dictionnaire des utilisateurs
                    dictUtilisateurs.Add(vectChamps[0], (
                        Convert.FromBase64String(vectChamps[1]),
                        Convert.FromBase64String(vectChamps[2])));
                }
            }

            fichierEntree.Close();

            return dictUtilisateurs;
        }

        /// <summary>
        /// Permet de sauvegarder les utilisateurs dans le fichier .dat.
        /// </summary>
        /// <param name="pUtilisateurs">Dictionnaire de (nom, (salt, motPasse)) contenant les informations des utlisateurs.</param>
        public static void SauvegarderUtilisateurs(Dictionary<string, (byte[], byte[])> pUtilisateurs)
        {
            StreamWriter fichierSortie = new StreamWriter(Utilitaire._CHEMIN_FICHIER_UTILISATEURS, false);

            foreach (string nomUtilisateur in pUtilisateurs.Keys)
            {
                (byte[], byte[]) valeurDeCle = pUtilisateurs[nomUtilisateur];
                fichierSortie.WriteLine(
                    $"{nomUtilisateur};{Convert.ToBase64String(valeurDeCle.Item1)};{Convert.ToBase64String(valeurDeCle.Item2)};");
            }

            fichierSortie.Close();
        }

        /// <summary>
        /// Permet d'obtenir l'adresse IP de l'ordinateur.
        /// </summary>
        /// <returns>Adresse IP de l'ordinateur.</returns>
        public static string ObtenirAdresseIpLocale()
        {
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }

            return null;
        }

        #region Serialisation et désérialisation

        /// <summary>
        /// Permet de sérialiser un objet.
        /// </summary>
        /// <param name="pObjetSerialisable">Objet à sérialiser</param>
        /// <returns>Vecteur de bits représentant l'objet sérialisé</returns>
        public static Byte[] Serialiser(object pObjetSerialisable)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                (new BinaryFormatter()).Serialize(memoryStream, pObjetSerialisable);
                return memoryStream.ToArray();
            }
        }

        /// <summary>
        /// Permet de désérialiser un objet.
        /// </summary>
        /// <param name="pBytes">Vecteur de bits</param>
        /// <returns>Un objet</returns>
        public static object Deserialiser(Byte[] pBytes)
        {
            using (MemoryStream memoryStream = new MemoryStream(pBytes))
                return (new BinaryFormatter()).Deserialize(memoryStream);
        }

        #endregion

        #region Salt et Hash

        /// <summary>
        /// Permet de générer un salt.
        /// </summary>
        /// <returns>Un vecteur de byte représentant le salt.</returns>
        public static byte[] GenererSalt()
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buffer = new byte[16];
            rng.GetBytes(buffer);

            return buffer;
        }

        /// <summary>
        /// Permet de hacher un mot de passe.
        /// </summary>
        /// <param name="pMotPasse">Mot de passe</param>
        /// <param name="pSalt">Salt</param>
        /// <returns>Vecteur de bytes présentant le hachage du mot de passe.</returns>
        public static byte[] HashMotDePasse(string pMotPasse, byte[] pSalt)
        {
            Argon2id argon2 = new Argon2id(Encoding.UTF8.GetBytes(pMotPasse))
            {
                Salt = pSalt,
                DegreeOfParallelism = 8,
                Iterations = 4,
                MemorySize = 1024 * 1024
            };

            GC.Collect();

            return argon2.GetBytes(16);
        }

        /// <summary>
        /// Permet de vérifier la validité d'un mot de passe.
        /// </summary>
        /// <param name="pMotPasse">Mot de passe de l'utilisateur</param>
        /// <param name="pSalt">Salt de l'utilisateur</param>
        /// <param name="pHash">Hash du mot de passe de l'utilisateur</param>
        /// <returns>True si le nouveau hash du mot de passe correspond à celui reçu en paramètre.</returns>
        public static bool VerifierMdp(string pMotPasse, byte[] pSalt, byte[] pHash)
        {
            return pHash.SequenceEqual(Utilitaire.HashMotDePasse(pMotPasse, pSalt));
        }

        #endregion

        #endregion
    }
}
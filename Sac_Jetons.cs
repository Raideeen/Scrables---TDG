using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Scrables___TDG
{
    class Sac_Jetons
    {
        //Initialisation de la classe random
        Random aleatoire = new Random();
        //Variable d'instance ou champ d'instance
        private SortedList<string, Jeton> sac_jetons;
        private int nb_ligne = 0;

        #region Constructeurs

        public Sac_Jetons(string nom_de_fichier)
        {
            this.sac_jetons = new SortedList<string, Jeton>();
            this.ReadFile(nom_de_fichier);
        }

        #endregion

        #region Propriétés

        public SortedList<string, Jeton> Sac_jetons_Get
        {
            get { return this.sac_jetons; }
        }

        #endregion

        #region Méthodes
        /// <summary>
        /// Méthode qui permet d'initialiser la SortedList(string, jeton) où en "string" on a le nom de la lettre pour identifier rapidement de quel jeton on considère
        /// et en "jeton" il y a les caractéristiques du jeton considéré (le nom de la lettre, sa valeur et son nombre dans le sac)
        /// </summary>
        /// <param name="fichier"></param>
        public void ReadFile(string fichier)
        {
            StreamReader lecture = new StreamReader(fichier);
            string ligne = lecture.ReadLine();
            while (ligne != null)
            {
                string nom_lettre = ""; //On récupère le nom de la lettre à la i-ème ligne
                int valeur_lettre = 0; //On récupère la valeur de la lettre à la i-ème ligne
                int nombre_lettre = 0; //On récupère le nombre de lettre dans le sac considéré à la i-ème ligne 
                bool probleme = false; //Variable permettant, si la ligne i du fichier pose problème quelle soit ignorée et ne provoque pas d'erreur dans l'execution du programme
                nb_ligne++;
                string[] liste = ligne.Split(';'); //On divise la ligne en plusieurs éléments grâce au séparateur ';'
                for (int i = 0; i <= 2; i++)
                {
                    try
                    {
                        if (i == 0)
                        {
                            nom_lettre = liste[i];
                        }
                        if (i == 1)
                        {
                            valeur_lettre = Convert.ToInt32(liste[i]);
                        }
                        if (i == 2)
                        {
                            nombre_lettre = Convert.ToInt32(liste[i]);
                        }
                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine($"L'exception est : {exception.Message} et concernait la ligne {nb_ligne} du fichier {fichier}");
                        probleme = true;
                    }
                }
                if (!probleme)
                {
                    Jeton jeton = new Jeton(nom_lettre, valeur_lettre, nombre_lettre);
                    sac_jetons.Add(nom_lettre, jeton);
                }
                ligne = lecture.ReadLine();
            }
        }


        /// <summary>
        /// Méthode qui permet de ré-écrire le fichier "Jetons.txt" pour d'autres parties (avec les valeurs modifiées des instances de jetons)
        /// </summary>
        /// <param name="fichier"></param>
        public void WriteFile(string fichier)
        {
            StreamWriter writer = new StreamWriter(fichier);
            for (int i = 0; i < sac_jetons.Count; i++)
            {
                KeyValuePair<string, Jeton> jeton_considere = sac_jetons.ElementAt(i);
                writer.WriteLine($"{jeton_considere.Key};{jeton_considere.Value.Valeur_jeton};{jeton_considere.Value.Nombre_jeton}");
            }
            writer.Close();
        }


        /// <summary>
        /// Méthode pour afficher le contenu du sac_jeton !
        /// </summary>
        /// <returns></returns>
        public string toString()
        {
            string descriptif = "";
            for (int i = 0; i < this.sac_jetons.Count; i++)
            {
                descriptif += this.sac_jetons.ElementAt(i).Value.ToString(); //On fait le descriptif du i-ème jeton (ElementAt appartient à System.Linq
                                                                             //et permet d'avoir le couple (key, value) du i-ème élément de la SortedList)
            }
            return descriptif;
        }


        /// <summary>
        /// Méthode pour retirer un jeton aléatoirement du sac si le nombre_jeton != 0. Il est à noter quelle retire le jeton et retourne le jeton retiré
        /// </summary>
        /// <returns></returns>
        public Jeton Retire_Jeton()
        {
            int nombre = aleatoire.Next(0, 27);
            Jeton jeton_considere = sac_jetons.ElementAt(nombre).Value;
            if (jeton_considere.Nombre_jeton != 0) //On vérifie que le nombre de jeton dans le sac est différent de zéro (car sinon il ne peut pas être pris)
            {
                jeton_considere.Nombre_jeton--;
            }
            return jeton_considere;
        }
        public Jeton Convert_To_Jeton(char jeton)
        {
            Jeton jeton_considere;
            string nom = "";
            int valeur = 0;
            int nombre = 0;
            for (int i = 0; i < sac_jetons.Count; i++)
            {
                if (Convert.ToString(jeton) == sac_jetons.ElementAt(i).Value.Nom_jeton)
                {
                    nom = sac_jetons.ElementAt(i).Value.Nom_jeton;
                    valeur = sac_jetons.ElementAt(i).Value.Valeur_jeton;
                    nombre = sac_jetons.ElementAt(i).Value.Nombre_jeton;
                }
            }
            jeton_considere = new Jeton(nom, valeur, nombre);
            return jeton_considere;
        }
        public List<Jeton> Retourne_liste()
        {
            List<Jeton> liste_retourne = new List<Jeton>(7);
            liste_retourne.Add(sac_jetons.ElementAt(sac_jetons.IndexOfKey("R")).Value);
            liste_retourne.Add(sac_jetons.ElementAt(sac_jetons.IndexOfKey("U")).Value);
            liste_retourne.Add(sac_jetons.ElementAt(sac_jetons.IndexOfKey("E")).Value);
            liste_retourne.Add(sac_jetons.ElementAt(sac_jetons.IndexOfKey("O")).Value);
            liste_retourne.Add(sac_jetons.ElementAt(sac_jetons.IndexOfKey("E")).Value);
            liste_retourne.Add(sac_jetons.ElementAt(sac_jetons.IndexOfKey("*")).Value);
            liste_retourne.Add(sac_jetons.ElementAt(sac_jetons.IndexOfKey("T")).Value);
            return liste_retourne;
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Scrables___TDG
{
    class Plateau
    {
        //Variable d'instance ou champ d'instance
        private Dictionnaire dictionnaire;
        private Joueur[] joueurs;
        private string InstancePlateau_chemin;
        private char[,] matrice_jeu = new char[15,15];
        private bool nouvelle_partie;

        int[,] matrice_affichage = 
                               {
                               { 4,0,0,1,0,0,0,4,0,0,0,1,0,0,4},
                               { 0,3,0,0,0,2,0,0,0,2,0,0,0,3,0},
                               { 0,0,3,0,0,0,1,0,1,0,0,0,3,0,0},
                               { 1,0,0,3,0,0,0,1,0,0,0,3,0,0,1},
                               { 0,0,0,0,3,0,0,0,0,0,3,0,0,0,0},
                               { 0,2,0,0,0,2,0,0,0,2,0,0,0,2,0},
                               { 0,0,1,0,0,0,1,0,1,0,0,0,1,0,0}, 
                               { 4,0,0,1,0,0,0,5,0,0,0,1,0,0,4}, //On commence là 
                               { 0,0,1,0,0,0,1,0,1,0,0,0,1,0,0},
                               { 0,2,0,0,0,2,0,0,0,2,0,0,0,2,0},
                               { 0,0,0,0,3,0,0,0,0,0,3,0,0,0,0},
                               { 1,0,0,3,0,0,0,1,0,0,0,3,0,0,1},
                               { 0,0,3,0,0,0,1,0,1,0,0,0,3,0,0},
                               { 0,3,0,0,0,2,0,0,0,2,0,0,0,3,0},
                               { 4,0,0,1,0,0,0,4,0,0,0,1,0,0,4}};
        public Plateau(Dictionnaire dictionnaire, Joueur[] joueurs) //Constructeur d'une nouvelle partie
        {
            this.dictionnaire = dictionnaire;
            this.joueurs = joueurs;
        }

        public Plateau(Dictionnaire dictionnaire, Joueur[] joueurs, string InstancePlateau_chemin)
        {
            this.dictionnaire = dictionnaire;
            this.joueurs = joueurs;
            this.InstancePlateau_chemin = InstancePlateau_chemin;
            this.ReadFile(InstancePlateau_chemin);
        }

        #region Propriétés 

        public char[,] Matrice_jeu
        {
            get { return matrice_jeu; }
        }

        #endregion

        #region Méthodes

        public void AffichageMatriceIntBrut()
        {
            if (matrice_affichage == null || matrice_affichage.Length == 0)
            {
                Console.WriteLine("Matrice null ou vide");
            }
            else
            {
                for (int ligne = 0; ligne < matrice_affichage.GetLength(0); ligne++)
                {
                    for (int colonne = 0; colonne < matrice_affichage.GetLength(1); colonne++) //GetLength(1) récupère le nombre de case dans une colonne. 
                    {
                        if (matrice_affichage[ligne, colonne] < 10)
                        {
                            Console.Write($" {matrice_affichage[ligne, colonne]} ");
                        }
                        else Console.Write($"{matrice_affichage[ligne, colonne]} ");
                    }
                    Console.Write("\n");
                }
            }
        }

        public void AffichageMatriceStringBrut()
        {
            if (this.matrice_jeu == null || matrice_jeu.Length == 0)
            {
                Console.WriteLine("Matrice null ou vide");
            }
            else
            {
                for (int ligne = 0; ligne < this.matrice_jeu.GetLength(0); ligne++)
                {
                    for (int colonne = 0; colonne < this.matrice_jeu.GetLength(1); colonne++) //GetLength(1) récupère le nombre de case dans une colonne. 
                    {
                        Console.Write($"{matrice_jeu[ligne, colonne]} ");
                    }
                    Console.Write("\n");
                }
            }
        }

        public void toStringCouleur()
        {
            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    switch (matrice_affichage[i, j])
                    {
                        case 0:
                            Console.BackgroundColor = ConsoleColor.White;
                            break;
                        case 1:
                            Console.BackgroundColor = ConsoleColor.DarkCyan;
                            break;
                        case 2:
                            Console.BackgroundColor = ConsoleColor.DarkBlue;
                            break;
                        case 3:
                            Console.BackgroundColor = ConsoleColor.Magenta;
                            break;
                        case 4:
                            Console.BackgroundColor = ConsoleColor.Red;
                            break;
                        case 5:
                            Console.BackgroundColor = ConsoleColor.Green;
                            break;
                    }
                    Console.Write("  ");
                    Console.ResetColor();
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Fonction permettant de lire un fichier "InstancePlateau.txt" et créé une matrice de char[15,15] à partir de celle-ci
        /// pour la manipulation dans le programme
        /// </summary>
        /// <param name="fichier">Variable indiquant le chemin du fichier et son nom. Si pas de chemin spécifié écrire seulement
        /// le nom du fichier et il sera créé dans le répertoire "bin\Debug\net5.0" de la solution</param>
        public void ReadFile(string fichier)
        {
            StreamReader lecture = new StreamReader(fichier);
            string ligne = lecture.ReadLine();
            int nb_ligne = 0;
            while (ligne != null)
            {
                bool probleme = false;
                string[] liste = ligne.Split(';');
                for (int ligne_matrice = 0; ligne_matrice < 15; ligne_matrice++)
                {
                    matrice_jeu[ligne_matrice, nb_ligne] = Convert.ToChar(liste[ligne_matrice]);
                }
                nb_ligne++;
                ligne = lecture.ReadLine();
            }
        }

        /// <summary>
        /// Fonction qui permet d'écrire le fichier contenant le plateau et le placement des lettres. 
        /// Il y a distinctions de deux cas : 
        /// - nouvelle_partie == true : on fait un plateau vierge de mots
        /// - nouvelle_partie == false : on écrit le plateau dans un fichier sauvegarde qui sera re-utilisé pour une autre partie
        /// </summary>
        /// <param name="fichier">Variable indiquant le chemin du fichier et son nom. Si pas de chemin spécifié écrire seulement
        /// le nom du fichier et il sera créé dans le répertoire "bin\Debug\net5.0" de la solution</param>
        /// <param name="nouvelle_partie">Variable qui permet de choisir si on créé un plateau vierge ou un plateau
        /// de sauvegarde</param>
        public void WriteFile(string fichier, bool nouvelle_partie)
        {
            StreamWriter writer = new StreamWriter(fichier);
            
            if (nouvelle_partie)
            {
                for (int ligne = 0; ligne < 15; ligne++)
                {
                    for (int colonne = 0; colonne < 15; colonne++)
                    {
                        if (colonne == 14) writer.Write("_\n");
                        else writer.Write("_;");
                    }
                }
            }
            else
            {
                for (int ligne = 0; ligne < 15; ligne++)
                {
                    for (int colonne = 0; colonne < 15; colonne++)
                    {
                        if (colonne == 14) writer.Write($"{this.matrice_jeu[ligne, colonne]}\n");
                        else writer.Write($"{this.matrice_jeu[ligne, colonne]};");
                    }
                }
            }
            writer.Close();
        }

        public bool Test_Plateau(string mot, int ligne, int colonne, char direction)
        {

        }
        #endregion

    }
}

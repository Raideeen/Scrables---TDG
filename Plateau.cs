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
            //Console.WriteLine(ligne);

        }
        #endregion

    }
}

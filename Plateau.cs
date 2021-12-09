﻿using System;
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
        private char[,] matrice_jeu_imaginaire = new char[15, 15];
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
            for (int i = 1; i <= 15 ; i++)
            {
                if (i<10)
                {
                    Console.Write(" " + i + " ");
                }
                else
                {
                    Console.Write(" " + i);
                }
            }
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine();
            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    switch (matrice_affichage[i, j])
                    {
                        case 0:
                            Console.BackgroundColor = ConsoleColor.Green;
                            Console.Write(" " + "g" + " ");
                            Console.BackgroundColor = ConsoleColor.Black;
                            break;
                        case 1:
                            Console.BackgroundColor = ConsoleColor.DarkCyan;
                            Console.Write(" " + "g" + " ");
                            Console.BackgroundColor = ConsoleColor.Black;
                            break;
                        case 2:
                            Console.BackgroundColor = ConsoleColor.DarkBlue;
                            Console.Write(" " + "g" + " ");
                            Console.BackgroundColor = ConsoleColor.Black;
                            break;
                        case 3:
                            Console.BackgroundColor = ConsoleColor.Magenta;
                            Console.Write(" " + "g" + " ");
                            Console.BackgroundColor = ConsoleColor.Black;
                            break;
                        case 4:
                            Console.BackgroundColor = ConsoleColor.Red;
                            Console.Write(" " + "g" + " ");
                            Console.BackgroundColor = ConsoleColor.Black;
                            break;
                        case 5:
                            Console.BackgroundColor = ConsoleColor.Yellow;
                            Console.Write(" " + "g" + " ");
                            Console.BackgroundColor = ConsoleColor.Black;
                            break;
                    }  
                }
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(" " + (i + 1) + " ");
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Black; 
            }
            Console.WriteLine();
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
                    matrice_jeu[nb_ligne, ligne_matrice] = Convert.ToChar(liste[ligne_matrice]);
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

        /// <summary>
        /// Fonction qui permet de copier une matrice dans une autre matrice
        /// </summary>
        /// <param name="matrice1">Matrice qui sera remplie</param>
        /// <param name="matrice2">Matrice qui sera copier</param>
        public static char[,] CopieMatrice(char[,] matrice1 ,char[,] matrice2)
        {
            for (int ligne = 0; ligne < 15; ligne++)
            {
                for (int colonne = 0; colonne < 15; colonne++)
                {
                    matrice1[ligne, colonne] = matrice2[ligne, colonne];
                }
            }
            return matrice1;
        }

        public bool Test_Plateau(string mot, int ligne, int colonne, char direction, Joueur joueur)
        {
            
            //ligne et colonne représente la position du DEBUT DU MOT
            bool possible = false;
            //Cas 'h'
            string MotAvant_h = "";
            string MotApres_h = "";
            string MotFinal_h = "";
            //Cas 'v'
            string MotAvantHaut_v = "";
            string MotApresBas_v = "";
            string MotFinalVertical_v = "";

            string motCopie = mot;
            List<string> LettrePresente = new List<string>();
            List<char> LettreManquante = new List<char>();
            Queue<char> LettreManquante_sousQueue = new Queue<char>();
            //Direction peut prendre deux valeurs 'v' pour vertical ou 'h' pour horizontal
            
            if (this.dictionnaire.RechDico(mot.ToUpper())) //On vérifie déjà si le mot est dans le dictionnaire, si oui alors on continue. On fait toUpper() car les mots dans le dictionnaire sont en majuscules
            {
                string[] Jeton_joueur_tableau = joueur.Jeton_joueur_liste().Split(';');
                List<char> Jeton_joueur = new List<char>();
                for (int i = 0; i < Jeton_joueur_tableau.Length; i++)
                {
                    Jeton_joueur.Add(Convert.ToChar(Jeton_joueur_tableau[i]));
                }
                this.matrice_jeu_imaginaire = CopieMatrice(matrice_jeu_imaginaire, Matrice_jeu); //On créé une matrice imaginaire ou on va placer nos lettres et essayer de voir si le mot est placable grâce à une série de test
                if (direction == 'h')
                {
                    #region Test : est-ce que le joueur possède les jetons des lettres manquantes du mot ?
                    for (int i = 0; i < mot.Length; i++) //Permet de remplir la liste LettrePresente du mot qu'on veut poser au début de la ligne et de la colonne d'entrée
                    {
                        if (this.matrice_jeu[ligne, colonne + i] != '_') LettrePresente.Add(Convert.ToString(this.matrice_jeu[ligne, colonne + i]));
                    }
                    foreach(string toRemove in LettrePresente) //permet de faire : mot - lettre_presente = lettre_manquante. On remplace les lettre présente par du vide 
                    {
                        motCopie = motCopie.Replace(toRemove, string.Empty);
                    }
                    for (int i = 0; i < motCopie.Length; i++) //On ajoute les lettres manquantes dans une list de char qu'on utilise juste après
                    {
                        LettreManquante.Add(Convert.ToChar(motCopie[i]));
                    }
                    bool tag_jeton_present = true; //Variable de test : est-ce que le joueur possède les jetons des lettres manquantes du mot
                    for (int i = 0; i < LettreManquante.Count && tag_jeton_present; i++)
                    {
                        char Lettre_considere = LettreManquante[i];
                        tag_jeton_present = Jeton_joueur.Contains(Lettre_considere);
                    }
                    #endregion

                    if (tag_jeton_present)
                    {   
                        for (int index = 0; index < LettreManquante.Count; index++) //Création de la liste des lettre manquante du mot sous forme de queue. Plus simple d'utilisation pour la prochaine boucle
                        {
                            LettreManquante_sousQueue.Enqueue(LettreManquante[index]);
                        }

                        for (int i = 0; i < mot.Length; i++) //Boucle qui permet de remplir les cases imaginaires avec les lettres manquantes du mot
                        {
                            if (matrice_jeu_imaginaire[ligne, colonne + i] == '_') matrice_jeu_imaginaire[ligne, colonne + i] = LettreManquante_sousQueue.Dequeue();
                        }

                        #region Test : est-ce que la combinaison de mot créé par le placement du mot, avant et après celui-ci appartient au dictionnaire ?

                        //matrice_jeu_imaginaire[14, 1] = 'C';
                        //matrice_jeu_imaginaire[14, 0] = 'V';
                        bool horizontalement_correct = false;
                        bool underscore_avant = false;
                        bool underscore_apres = false;
                        for (int i = colonne-1; i >= 0 && !underscore_avant; i--) //On se place au début du mot, et on recule jusqu'à croiser un underscore tout en ajoutant chaque lettre à MotAvant
                        {
                            if (matrice_jeu_imaginaire[ligne, i] == '_') underscore_avant = true;
                            if (matrice_jeu_imaginaire[ligne, i] != '_') MotAvant_h += matrice_jeu_imaginaire[ligne, i]; //Evite d'avoir un '_' qui se rajoute dans le MotAvant
                        }
                        MotAvant_h = ReverseString(MotAvant_h); //On inverse le string car il est à l'envers
                        for (int i = colonne + mot.Length; i < 15 && !underscore_apres; i++) //On se place à la fin du mot, et on avance jusqu'à croiser un underscore tout en ajoutant chaque lettre à MotApres
                        {
                            if (matrice_jeu_imaginaire[ligne, i] == '_') underscore_apres = true;
                            if (matrice_jeu_imaginaire[ligne, i] != '_') MotApres_h += matrice_jeu_imaginaire[ligne, i]; //Evite d'avoir un '_' qui se rajoute dans le MotApres
                        }
                        MotFinal_h = MotAvant_h + mot + MotApres_h;
                        horizontalement_correct = dictionnaire.RechDico(MotFinal_h); //On teste si le mot est horizontalement correct

                        #endregion

                        if (horizontalement_correct)
                        {
                            #region Test : est-ce que la combinaison de mot créé par le palcement de mot, en haut et en bas de chaque lettre appartient au dictionnaire ? 
                            bool verticalement_correct = true;
                            for (int i = 0; i < mot.Length && verticalement_correct; i++) //On va tester verticalement si chaque mot au dessus/au dessous de chaque lettre de notre mot qu'on veut placer forme une combinaison correcte
                            {
                                string MotAvantHaut = "";
                                string MotApresBas = "";
                                string MotFinalVertical = "";
                                bool underscore_haut = false;
                                bool underscore_bas = false;
                                for (int index = ligne-1; index > 0 && !underscore_haut; index--) //On se place à la i-ème lettre du mot, et on monte jusqu'à croiser un underscore tout en ajoutant chaque lettre à MotAvantHaut
                                {
                                    if (matrice_jeu_imaginaire[index, colonne+i] == '_') underscore_haut = true;
                                    if (matrice_jeu_imaginaire[index, colonne+i] != '_') MotAvantHaut += matrice_jeu_imaginaire[index, colonne+i];
                                }
                                MotAvantHaut = ReverseString(MotAvantHaut);
                                for (int index = ligne+1; index < 15 && !underscore_bas; index++) //On se place à la i-ème lettre du mot, et on descend jusqu'à croiser un underscore tout en ajoutant chaque lettre à MotApresBas
                                {
                                    if (matrice_jeu_imaginaire[index, colonne+i] == '_') underscore_bas = true;
                                    if (matrice_jeu_imaginaire[index, colonne+i] != '_') MotApresBas += matrice_jeu_imaginaire[index, colonne+i];
                                }
                                MotFinalVertical = MotAvantHaut + matrice_jeu_imaginaire[ligne, colonne+i] + MotApresBas;
                                if (MotFinalVertical.Length == 1) verticalement_correct = true; //permet d'éviter l'erreur si on a juste un mot d'une longueur 1, qui n'est pas reconnu par le dictionnaire
                                else
                                {
                                    verticalement_correct = dictionnaire.RechDico(MotFinalVertical); //On teste si le mot est verticalement correct
                                }
                            }
                            #endregion
                            if (verticalement_correct) possible = true;   
                        }
                    }
                }
                else //direction == 'v'
                {
                    #region Test : est-ce que le joueur possède les jetons des lettres manquantes du mot ?
                    for (int i = 0; i < mot.Length; i++) //Permet de remplir la liste LettrePresente du mot qu'on veut poser au début de la ligne et de la colonne d'entrée
                    {
                        if (this.matrice_jeu[ligne + i, colonne] != '_') LettrePresente.Add(Convert.ToString(this.matrice_jeu[ligne + i, colonne]));
                    }
                    foreach (string toRemove in LettrePresente) //permet de faire : mot - lettre_presente = lettre_manquante. On remplace les lettre présente par du vide 
                    {
                        motCopie = motCopie.Replace(toRemove, string.Empty);
                    }
                    for (int i = 0; i < motCopie.Length; i++) //On ajoute les lettres manquantes dans une list de char qu'on utilise juste après
                    {
                        LettreManquante.Add(Convert.ToChar(motCopie[i]));
                    }
                    bool tag_jeton_present = true; //Variable de test : est-ce que le joueur possède les jetons des lettres manquantes du mot
                    for (int i = 0; i < LettreManquante.Count && tag_jeton_present; i++)
                    {
                        char Lettre_considere = LettreManquante[i];
                        tag_jeton_present = Jeton_joueur.Contains(Lettre_considere);
                    }
                    #endregion

                    if (tag_jeton_present)
                    {
                        for (int index = 0; index < LettreManquante.Count; index++) //Création de la liste des lettre manquante du mot sous forme de queue. Plus simple d'utilisation pour la prochaine boucle
                        {
                            LettreManquante_sousQueue.Enqueue(LettreManquante[index]);
                        }

                        for (int i = 0; i < mot.Length; i++) //Boucle qui permet de remplir les cases imaginaires avec les lettres manquantes du mot
                        {
                            if (matrice_jeu_imaginaire[ligne + i, colonne] == '_') matrice_jeu_imaginaire[ligne + i, colonne] = LettreManquante_sousQueue.Dequeue();
                        }

                        #region Test : est-ce que la combinaison de mot créé par le placement du mot, avant et après celui-ci appartient au dictionnaire ?

                        bool verticalement_correct = false;
                        bool underscore_haut = false;
                        bool underscore_bas = false;
                        for (int i = ligne - 1; i >= 0 && !underscore_haut; i--) //On se place au début du mot, et on monte jusqu'à croiser un underscore tout en ajoutant chaque lettre à MotAvantHaut
                        {
                            if (matrice_jeu_imaginaire[i, colonne] == '_') underscore_haut = true;
                            if (matrice_jeu_imaginaire[i, colonne] != '_') MotAvantHaut_v += matrice_jeu_imaginaire[i, colonne]; //Evite d'avoir un '_' qui se rajoute dans le MotAvant
                        }
                        MotAvantHaut_v = ReverseString(MotAvantHaut_v); //On inverse le string car il est à l'envers
                        for (int i = ligne + mot.Length; i < 15 && !underscore_bas; i++) //On se place à la fin du mot, et on avance jusqu'à croiser un underscore tout en ajoutant chaque lettre à MotApres
                        {
                            if (matrice_jeu_imaginaire[i, colonne] == '_') underscore_bas = true;
                            if (matrice_jeu_imaginaire[i, colonne] != '_') MotApresBas_v += matrice_jeu_imaginaire[i, colonne]; //Evite d'avoir un '_' qui se rajoute dans le MotApresBas
                        }
                        MotFinalVertical_v = MotAvantHaut_v + mot + MotApresBas_v;
                        verticalement_correct = dictionnaire.RechDico(MotFinalVertical_v); //On teste si le mot est horizontalement correct

                        #endregion
                        matrice_jeu_imaginaire[2, 1] = '_';
                        matrice_jeu_imaginaire[2, 2] = '_';
                        matrice_jeu_imaginaire[2, 3] = '_';
                        matrice_jeu_imaginaire[2, 4] = '_';
                        matrice_jeu_imaginaire[2, 5] = '_';
                        matrice_jeu_imaginaire[2, 6] = '_';
                        if (verticalement_correct)
                        {
                            #region Test : est-ce que la combinaison de mot créé par le palcement de mot, en haut et en bas de chaque lettre appartient au dictionnaire ? 
                            bool horizontalement_correct = true;
                            for (int i = 0; i < mot.Length && horizontalement_correct; i++) //On va tester verticalement si chaque mot au dessus/au dessous de chaque lettre de notre mot qu'on veut placer forme une combinaison correcte
                            {
                                string MotAvant = "";
                                string MotApres = "";
                                string MotFinal = "";
                                bool underscore_avant = false;
                                bool underscore_apres = false;
                                for (int index = colonne - 1; index > 0 && !underscore_avant; index--) //On se place à la i-ème lettre du mot, et on recule jusqu'à croiser un underscore tout en ajoutant chaque lettre à MotAvant
                                {
                                    if (matrice_jeu_imaginaire[colonne + i, index] == '_') underscore_avant = true;
                                    if (matrice_jeu_imaginaire[colonne + i, index] != '_') MotAvant += matrice_jeu_imaginaire[colonne + i, index];
                                }
                                MotAvant = ReverseString(MotAvant);
                                for (int index = colonne + 1; index < 15 && !underscore_apres; index++) //On se place à la i-ème lettre du mot, et on avance jusqu'à croiser un underscore tout en ajoutant chaque lettre à MotApres
                                {
                                    if (matrice_jeu_imaginaire[colonne + i, index] == '_') underscore_apres = true;
                                    if (matrice_jeu_imaginaire[colonne + i, index] != '_') MotApres += matrice_jeu_imaginaire[colonne + i, index];
                                }
                                MotFinal = MotAvant + matrice_jeu_imaginaire[ligne + i, colonne] + MotApres;
                                if (MotFinal.Length == 1) horizontalement_correct = true; //permet d'éviter l'erreur si on a juste un mot d'une longueur 1, qui n'est pas reconnu par le dictionnaire
                                else
                                {
                                    horizontalement_correct = dictionnaire.RechDico(MotFinal); //On teste si le mot est verticalement correct
                                }
                            }
                            #endregion
                            if (horizontalement_correct) possible = true;
                        }
                    }
                }
            }
            return possible;
        }
        /// <summary>
        /// Fonction qui permet d'inverser un string de sens. Utilisé pour le "MotAvant" dans TestPlateau
        /// </summary>
        /// <param name="s">String que l'on veut inverser</param>
        /// <returns></returns>
        public static string ReverseString(string s)
        {
            char[] chars = s.ToCharArray();
            for (int i = 0, j = s.Length - 1; i < j; i++, j--)
            {
                char c = chars[i];
                chars[i] = chars[j];
                chars[j] = c;
            }
            return new string(chars);
        }
        #endregion

    }
}

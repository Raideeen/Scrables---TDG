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
        private Sac_Jetons sac_jetons;
        private string InstancePlateau_chemin;
        private int[,] matrice_score = new int[15, 15];
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
                                //1 : Lettre double
                                //2 : Lettre triple
                                //3 : Mot compte double
                                //4 : Mot compte triple
        public Plateau(Dictionnaire dictionnaire, Joueur[] joueurs) //Constructeur d'une nouvelle partie
        {
            this.dictionnaire = dictionnaire;
            this.joueurs = joueurs;
        }

        public Plateau(Dictionnaire dictionnaire, Joueur[] joueurs, Sac_Jetons sac_jetons,string InstancePlateau_chemin, string InstanceScore_chemin)
        {
            this.dictionnaire = dictionnaire;
            this.joueurs = joueurs;
            this.InstancePlateau_chemin = InstancePlateau_chemin;
            this.sac_jetons = sac_jetons;
            this.ReadFileMatricePlateau(InstancePlateau_chemin);
            this.ReadFileMatriceScore(InstanceScore_chemin);
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
                if (i < 10) Console.Write($" {i} ");
                else Console.Write($" {i}");
            }
            Console.WriteLine();
            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    switch (matrice_score[i, j])
                    {
                        case 0:
                            Console.BackgroundColor = ConsoleColor.Green;
                            Console.Write($" {matrice_jeu[i, j]} ");
                            Console.BackgroundColor = ConsoleColor.Black;
                            break;
                        case 1:
                            Console.BackgroundColor = ConsoleColor.Cyan;
                            Console.Write($" {matrice_jeu[i, j]} ");
                            Console.BackgroundColor = ConsoleColor.Black;
                            break;
                        case 2:
                            Console.BackgroundColor = ConsoleColor.Blue;
                            Console.Write($" {matrice_jeu[i, j]} ");
                            Console.BackgroundColor = ConsoleColor.Black;
                            break;
                        case 3:
                            Console.BackgroundColor = ConsoleColor.Magenta;
                            Console.Write($" {matrice_jeu[i, j]} ");
                            Console.BackgroundColor = ConsoleColor.Black;
                            break;
                        case 4:
                            Console.BackgroundColor = ConsoleColor.Red;
                            Console.Write($" {matrice_jeu[i, j]} ");
                            Console.BackgroundColor = ConsoleColor.Black;
                            break;
                        case 5:
                            Console.BackgroundColor = ConsoleColor.DarkCyan;
                            Console.Write($" {matrice_jeu[i, j]} ");
                            Console.BackgroundColor = ConsoleColor.Black;
                            break;
                        case 6:
                            Console.BackgroundColor = ConsoleColor.Blue;
                            Console.Write($" {matrice_jeu[i, j]} ");
                            Console.BackgroundColor = ConsoleColor.Black;
                            break;
                        case 7:
                            Console.BackgroundColor = ConsoleColor.DarkMagenta;
                            Console.Write($" {matrice_jeu[i, j]} ");
                            Console.BackgroundColor = ConsoleColor.Black;
                            break;
                        case 8:
                            Console.BackgroundColor = ConsoleColor.DarkRed;
                            Console.Write($" {matrice_jeu[i, j]} ");
                            Console.BackgroundColor = ConsoleColor.Black;
                            break;
                        case 9:
                            Console.BackgroundColor = ConsoleColor.Yellow;
                            Console.Write($" {matrice_jeu[i, j]} ");
                            Console.BackgroundColor = ConsoleColor.Black;
                            break;
                    }  
                }
                Console.ForegroundColor = ConsoleColor.White; //permet d'éviter que les i soient noir sur noir
                Console.Write($" {i+1} ");
                Console.WriteLine();
                Console.ResetColor();
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Fonction permettant de lire un fichier "InstancePlateau.txt" et créé une matrice de char[15,15] à partir de celle-ci
        /// pour la manipulation dans le programme
        /// </summary>
        /// <param name="fichier">Variable indiquant le chemin du fichier et son nom. Si pas de chemin spécifié écrire seulement
        /// le nom du fichier et il sera créé dans le répertoire "bin\Debug\net5.0" de la solution</param>
        public void ReadFileMatricePlateau(string fichier)
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
        public void ReadFileMatriceScore(string fichier)
        {
            StreamReader lecture = new StreamReader(fichier);
            string ligne = lecture.ReadLine();
            int nb_ligne = 0;
            while (ligne != null)
            {
                string[] liste = ligne.Split(';');
                for (int ligne_matrice = 0; ligne_matrice < 15; ligne_matrice++)
                {
                    matrice_score[nb_ligne, ligne_matrice] = Convert.ToInt32(liste[ligne_matrice]);
                }
                nb_ligne++;
                ligne = lecture.ReadLine();
            }
        }

        /// <summary>
        /// Fonction qui permet d'écrire le fichier contenant le plateau et le placement des lettres mais aussi la matrice_score qui 
        /// permettra de déterminer si un bonus à été utiliser ou pas. 
        /// Il y a distinctions de deux cas : 
        /// - nouvelle_partie == true : on fait un plateau vierge de mots
        /// - nouvelle_partie == false : on écrit le plateau dans un fichier sauvegarde qui sera re-utilisé pour une autre partie
        /// </summary>
        /// <param name="fichier">Variable indiquant le chemin du fichier et son nom. Si pas de chemin spécifié écrire seulement
        /// le nom du fichier et il sera créé dans le répertoire "bin\Debug\net5.0" de la solution</param>
        /// <param name="nouvelle_partie">Variable qui permet de choisir si on créé un plateau vierge ou un plateau
        /// de sauvegarde</param>
        public void WriteFile(string fichier_InstancePlateau,string fichier_InstanceScore, bool nouvelle_partie)
        {
            StreamWriter writer_plateau = new StreamWriter(fichier_InstancePlateau);
            StreamWriter writer_score = new StreamWriter(fichier_InstanceScore);
            if (nouvelle_partie)
            {
                for (int ligne = 0; ligne < 15; ligne++)
                {
                    for (int colonne = 0; colonne < 15; colonne++)
                    {
                        if (colonne == 14) writer_plateau.Write("_\n");
                        else writer_plateau.Write("_;");
                    }
                }
            }
            else
            {
                for (int ligne = 0; ligne < 15; ligne++)
                {
                    for (int colonne = 0; colonne < 15; colonne++)
                    {
                        if (colonne == 14)
                        {
                            writer_plateau.Write($"{this.matrice_jeu[ligne, colonne]}\n");
                            writer_score.Write($"{this.matrice_score[ligne, colonne]}\n");
                        }
                        else
                        {
                            writer_plateau.Write($"{this.matrice_jeu[ligne, colonne]};");
                            writer_score.Write($"{this.matrice_score[ligne, colonne]};");
                        }
                    }
                }
            }
            writer_plateau.Close();
            writer_score.Close();
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
            int ligne_decalage = ligne - 1;
            int colonne_decalage = colonne - 1;
            //ligne et colonne représente la position du DEBUT DU MOT
            bool possible = false;
            //Multiplicateur
            int multiplicateur_horizontal = 1;
            int multiplicateur_vertical = 1;
            //Cas 'h'
            string MotAvant_h = "";
            string MotApres_h = "";
            string MotFinal_h = "";
            int score_horizontal = 0;
            //Cas 'v'
            string MotAvantHaut_v = "";
            string MotApresBas_v = "";
            string MotFinalVertical_v = "";
            int score_vertical = 0;

            string motCopie = mot;
            List<int> PositionLettreManquante = new List<int>();
            List<string> LettrePresente = new List<string>();
            List<char> LettreManquante = new List<char>();
            Queue<char> LettreManquante_sousQueue = new Queue<char>();
            //Direction peut prendre deux valeurs 'v' pour vertical ou 'h' pour horizontal

            #region Test : global 

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
                        if (this.matrice_jeu[ligne_decalage, colonne_decalage + i] != '_') LettrePresente.Add(Convert.ToString(this.matrice_jeu[ligne_decalage, colonne_decalage + i]));
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
                            if (matrice_jeu_imaginaire[ligne_decalage, colonne_decalage + i] == '_')
                            {
                                matrice_jeu_imaginaire[ligne_decalage, colonne_decalage + i] = LettreManquante_sousQueue.Dequeue();
                                PositionLettreManquante.Add(colonne_decalage + i);

                            }
                        }

                        #region Test : est-ce que la combinaison de mot créé par le placement du mot, avant et après celui-ci appartient au dictionnaire ?

                        //matrice_jeu_imaginaire[14, 1] = 'C';
                        //matrice_jeu_imaginaire[14, 0] = 'V';
                        //matrice_jeu_imaginaire[13, 3] = 'F';
                        bool horizontalement_correct = false;
                        bool underscore_avant = false;
                        bool underscore_apres = false;
                        for (int i = colonne_decalage - 1; i >= 0 && !underscore_avant; i--) //On se place au début du mot, et on recule jusqu'à croiser un underscore tout en ajoutant chaque lettre à MotAvant
                        {
                            if (matrice_jeu_imaginaire[ligne_decalage, i] == '_') underscore_avant = true;
                            if (matrice_jeu_imaginaire[ligne_decalage, i] != '_') MotAvant_h += matrice_jeu_imaginaire[ligne_decalage, i]; //Evite d'avoir un '_' qui se rajoute dans le MotAvant
                        }
                        MotAvant_h = ReverseString(MotAvant_h); //On inverse le string car il est à l'envers
                        for (int i = colonne_decalage + mot.Length; i < 15 && !underscore_apres; i++) //On se place à la fin du mot, et on avance jusqu'à croiser un underscore tout en ajoutant chaque lettre à MotApres
                        {
                            if (matrice_jeu_imaginaire[ligne_decalage, i] == '_') underscore_apres = true;
                            if (matrice_jeu_imaginaire[ligne_decalage, i] != '_') MotApres_h += matrice_jeu_imaginaire[ligne_decalage, i]; //Evite d'avoir un '_' qui se rajoute dans le MotApres
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
                                for (int index = ligne_decalage - 1; index > 0 && !underscore_haut; index--) //On se place à la i-ème lettre du mot, et on monte jusqu'à croiser un underscore tout en ajoutant chaque lettre à MotAvantHaut
                                {
                                    if (matrice_jeu_imaginaire[index, colonne_decalage + i] == '_') underscore_haut = true;
                                    if (matrice_jeu_imaginaire[index, colonne_decalage + i] != '_') MotAvantHaut += matrice_jeu_imaginaire[index, colonne_decalage + i];
                                }
                                MotAvantHaut = ReverseString(MotAvantHaut);
                                for (int index = ligne_decalage + 1; index < 15 && !underscore_bas; index++) //On se place à la i-ème lettre du mot, et on descend jusqu'à croiser un underscore tout en ajoutant chaque lettre à MotApresBas
                                {
                                    if (matrice_jeu_imaginaire[index, colonne_decalage + i] == '_') underscore_bas = true;
                                    if (matrice_jeu_imaginaire[index, colonne_decalage + i] != '_') MotApresBas += matrice_jeu_imaginaire[index, colonne_decalage + i];
                                }
                                MotFinalVertical = MotAvantHaut + matrice_jeu_imaginaire[ligne_decalage, colonne_decalage + i] + MotApresBas;
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
                    else Console.WriteLine("Il vous manque les jetons pour compléter le mot !");
                }
                else //direction == 'v'
                {
                    #region Test : est-ce que le joueur possède les jetons des lettres manquantes du mot ?
                    for (int i = 0; i < mot.Length; i++) //Permet de remplir la liste LettrePresente du mot qu'on veut poser au début de la ligne et de la colonne d'entrée
                    {
                        if (this.matrice_jeu[ligne_decalage + i, colonne_decalage] != '_') LettrePresente.Add(Convert.ToString(this.matrice_jeu[ligne_decalage + i, colonne_decalage]));
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
                            if (matrice_jeu_imaginaire[ligne_decalage + i, colonne_decalage] == '_')
                            {
                                matrice_jeu_imaginaire[ligne_decalage + i, colonne_decalage] = LettreManquante_sousQueue.Dequeue();
                                PositionLettreManquante.Add(ligne_decalage + i);
                            }
                            
                        }

                        #region Test : est-ce que la combinaison de mot créé par le placement du mot, avant et après celui-ci appartient au dictionnaire ?

                        bool verticalement_correct = false;
                        bool underscore_haut = false;
                        bool underscore_bas = false;
                        for (int i = ligne_decalage - 1; i >= 0 && !underscore_haut; i--) //On se place au début du mot, et on monte jusqu'à croiser un underscore tout en ajoutant chaque lettre à MotAvantHaut
                        {
                            if (matrice_jeu_imaginaire[i, colonne_decalage] == '_') underscore_haut = true;
                            if (matrice_jeu_imaginaire[i, colonne_decalage] != '_') MotAvantHaut_v += matrice_jeu_imaginaire[i, colonne_decalage]; //Evite d'avoir un '_' qui se rajoute dans le MotAvant
                        }
                        MotAvantHaut_v = ReverseString(MotAvantHaut_v); //On inverse le string car il est à l'envers
                        for (int i = ligne_decalage + mot.Length; i < 15 && !underscore_bas; i++) //On se place à la fin du mot, et on avance jusqu'à croiser un underscore tout en ajoutant chaque lettre à MotApres
                        {
                            if (matrice_jeu_imaginaire[i, colonne_decalage] == '_') underscore_bas = true;
                            if (matrice_jeu_imaginaire[i, colonne_decalage] != '_') MotApresBas_v += matrice_jeu_imaginaire[i, colonne_decalage]; //Evite d'avoir un '_' qui se rajoute dans le MotApresBas
                        }
                        MotFinalVertical_v = MotAvantHaut_v + mot + MotApresBas_v;
                        verticalement_correct = dictionnaire.RechDico(MotFinalVertical_v); //On teste si le mot est horizontalement correct

                        #endregion
                        matrice_jeu_imaginaire[2, 1] = '_';
                        //matrice_jeu_imaginaire[2, 2] = '_';
                        //matrice_jeu_imaginaire[2, 3] = '_';
                        //matrice_jeu_imaginaire[2, 4] = '_';
                        //matrice_jeu_imaginaire[2, 5] = '_';
                        //matrice_jeu_imaginaire[2, 6] = '_';
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
                                for (int index = colonne_decalage - 1; index > 0 && !underscore_avant; index--) //On se place à la i-ème lettre du mot, et on recule jusqu'à croiser un underscore tout en ajoutant chaque lettre à MotAvant
                                {
                                    if (matrice_jeu_imaginaire[colonne_decalage + i, index] == '_') underscore_avant = true;
                                    if (matrice_jeu_imaginaire[colonne_decalage + i, index] != '_') MotAvant += matrice_jeu_imaginaire[colonne_decalage + i, index];
                                }
                                MotAvant = ReverseString(MotAvant);
                                for (int index = colonne_decalage + 1; index < 15 && !underscore_apres; index++) //On se place à la i-ème lettre du mot, et on avance jusqu'à croiser un underscore tout en ajoutant chaque lettre à MotApres
                                {
                                    if (matrice_jeu_imaginaire[colonne_decalage + i, index] == '_') underscore_apres = true;
                                    if (matrice_jeu_imaginaire[colonne_decalage + i, index] != '_') MotApres += matrice_jeu_imaginaire[colonne_decalage + i, index];
                                }
                                MotFinal = MotAvant + matrice_jeu_imaginaire[ligne_decalage + i, colonne_decalage] + MotApres;
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
                    else Console.WriteLine("Il vous manque les jetons pour compléter le mot !");
                }
            }
            else Console.WriteLine($"Le mot {mot} n'appartient pas au dictionnaire. (Une erreur de syntaxe ?)");

            #endregion

            if (possible) //On recopie la matrice_imaginaire dans la matrice_jeu 
            {
                this.matrice_jeu = CopieMatrice(this.matrice_jeu, this.matrice_jeu_imaginaire);
                #region Calcul du score 
                //Cas horizontal
                if (direction == 'h')
                {
                    #region Calcul des points du mot de base
                    for (int i = 0; i < mot.Length; i++) //On ajoute le score du mot de base, horizontalement SANS MUTLIPLICATEUR DE MOT
                    {
                        int jeton_considere = sac_jetons.Sac_jetons_Get.IndexOfKey(Convert.ToString(matrice_jeu[ligne_decalage, colonne_decalage + i])); //Récupère l'index dans le sac_jetons de la lettre considérée
                        if (matrice_score[ligne_decalage, colonne_decalage + i] == 3) multiplicateur_horizontal *= 2;
                        if (matrice_score[ligne_decalage, colonne_decalage + i] == 4) multiplicateur_horizontal *= 3;
                        if (matrice_score[ligne_decalage, colonne_decalage + i] == 1) //Si la i-ème lettre est sur un lettre compte double
                        {
                            matrice_score[ligne_decalage, colonne_decalage + i] = 5; //Permet de dire que le bonus de la case est utilisé et se transforme en "Case lettre compte double utilisée"
                            score_horizontal += sac_jetons.Sac_jetons_Get.ElementAt(jeton_considere).Value.Valeur_jeton*2; //On ajoute la valeur de la lettre mutipliée par 2
                        }
                        if (matrice_score[ligne_decalage, colonne_decalage + i] == 2)
                        {
                            matrice_score[ligne_decalage, colonne_decalage + i] = 6; //Permet de dire que le bonus de la case est utilisé et se transforme en "Case lettre compte triple utilisée"
                            score_horizontal += sac_jetons.Sac_jetons_Get.ElementAt(jeton_considere).Value.Valeur_jeton*3; //On ajoute la valeur de la lettre mutipliée par 3
                        }
                        if (matrice_score[ligne_decalage, colonne_decalage + i] == 0)
                        {
                            score_horizontal += sac_jetons.Sac_jetons_Get.ElementAt(jeton_considere).Value.Valeur_jeton;
                        }
                    }
                    #endregion

                    #region Calcul des points des combinaisons de nouveaux mots à partir des lettres manquantes 
                    for (int j = 0; j < PositionLettreManquante.Count; j++) //On parcourt seulement les positions des lettres manquantes ajoutés qui forme des nouvelles combinaisons de mots SANS MUTLIPLICATEUR
                    {
                        int jeton_considere = 0;
                        multiplicateur_vertical = 1;
                        bool underscore_haut = false;
                        bool underscore_bas = false;
                        for (int index = ligne_decalage - 1; index > 0 && !underscore_haut; index--) //On se place à la i-ème lettre manquante du mot, et on monte jusqu'à croiser un underscore tout en ajoutant le score de chaque lettre avec les multiplicateurs
                        {
                            jeton_considere = sac_jetons.Sac_jetons_Get.IndexOfKey(Convert.ToString(matrice_jeu[index, PositionLettreManquante[j]])); //Récupère l'index dans le sac_jetons de la lettre considérée
                            if (matrice_jeu[index, PositionLettreManquante[j]] == '_') underscore_haut = true;
                            if (matrice_score[index, PositionLettreManquante[j]] == 3) multiplicateur_vertical *= 2;
                            if (matrice_score[index, PositionLettreManquante[j]] == 4) multiplicateur_vertical *= 3;
                            if (matrice_jeu[index, PositionLettreManquante[j]] != '_' && matrice_score[index, PositionLettreManquante[j]] == 1)
                            {
                                matrice_score[index, PositionLettreManquante[j]] = 5; //Permet de dire que le bonus de la case est utilisé et se transforme en "Case lettre compte double utilisée"
                                score_vertical += sac_jetons.Sac_jetons_Get.ElementAt(jeton_considere).Value.Valeur_jeton*2; //On ajoute la valeur de la lettre mutipliée par 2
                            }
                            if (matrice_jeu[index, PositionLettreManquante[j]] != '_' && matrice_score[index, PositionLettreManquante[j]] == 2)
                            {
                                matrice_score[index, PositionLettreManquante[j]] = 6; //Permet de dire que le bonus de la case est utilisé et se transforme en "Case lettre compte triple utilisée"
                                score_vertical += sac_jetons.Sac_jetons_Get.ElementAt(jeton_considere).Value.Valeur_jeton * 2; //On ajoute la valeur de la lettre mutipliée par 3
                            }
                            if (matrice_jeu[index, PositionLettreManquante[j]] != '_') score_vertical += sac_jetons.Sac_jetons_Get.ElementAt(jeton_considere).Value.Valeur_jeton;
                        }
                        for (int index = ligne_decalage + 1; index < 15 && !underscore_bas; index++) //On se place à la i-ème lettre manquante du mot, et on descend jusqu'à croiser un underscore tout en ajoutant le score de chaque lettre
                        {
                            jeton_considere = sac_jetons.Sac_jetons_Get.IndexOfKey(Convert.ToString(matrice_jeu[index, PositionLettreManquante[j]])); //Récupère l'index dans le sac_jetons de la lettre considérée
                            if (matrice_jeu[index, PositionLettreManquante[j]] == '_') underscore_bas = true;
                            if (matrice_score[index, PositionLettreManquante[j]] == 3) multiplicateur_vertical *= 2;
                            if (matrice_score[index, PositionLettreManquante[j]] == 4) multiplicateur_vertical *= 3;
                            if (matrice_jeu[index, PositionLettreManquante[j]] != '_' && matrice_score[index, PositionLettreManquante[j]] == 1)
                            {
                                matrice_score[index, PositionLettreManquante[j]] = 5; //Permet de dire que le bonus de la case est utilisé et se transforme en "Case lettre compte double utilisée"
                                score_vertical += sac_jetons.Sac_jetons_Get.ElementAt(jeton_considere).Value.Valeur_jeton * 2; //On ajoute la valeur de la lettre mutipliée par 2
                            }
                            if (matrice_jeu[index, PositionLettreManquante[j]] != '_' && matrice_score[index, PositionLettreManquante[j]] == 2)
                            {
                                matrice_score[index, PositionLettreManquante[j]] = 6; //Permet de dire que le bonus de la case est utilisé et se transforme en "Case lettre compte triple utilisée"
                                score_vertical += sac_jetons.Sac_jetons_Get.ElementAt(jeton_considere).Value.Valeur_jeton * 2; //On ajoute la valeur de la lettre mutipliée par 3
                            }
                            if (matrice_jeu[index, PositionLettreManquante[j]] != '_') score_vertical += sac_jetons.Sac_jetons_Get.ElementAt(jeton_considere).Value.Valeur_jeton;
                        }
                        score_vertical *= multiplicateur_vertical;
                    }
                    #endregion
                    joueur.Add_Score(score_horizontal*multiplicateur_horizontal + score_vertical); //On ajoute le score final du joueur en multipliant vert
                }
                //Cas vertical
                else
                {
                    for (int i = 0; i < mot.Length; i++) //On ajoute le score du mot de base, verticalement 
                    {
                        int jeton_considere = sac_jetons.Sac_jetons_Get.IndexOfKey(Convert.ToString(matrice_jeu[ligne_decalage + i, colonne_decalage])); //Récupère l'index dans le sac_jetons de la lettre considérée
                        score_vertical += sac_jetons.Sac_jetons_Get.ElementAt(jeton_considere).Value.Valeur_jeton; //On ajoute la valeur de la lettre
                        //rajouter les conditions if (ligne_decalage == 0 && colonne_decalage + i == 0) = mot compte triple...
                    }
                }
                #endregion
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

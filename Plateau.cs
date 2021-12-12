using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Scrables___TDG
{
    public class Plateau
    {
        //Variable d'instance ou champ d'instance
        private Dictionnaire dictionnaire;
        private List<Joueur> joueurs;
        private Sac_Jetons sac_jetons;
        private string InstancePlateau_chemin;
        private string InstanceScore_chemin;
        private int[,] matrice_score = new int[15, 15];
        private char[,] matrice_jeu = new char[15,15];
        private char[,] matrice_jeu_imaginaire = new char[15, 15];
        private bool nouvelle_partie;
        private string nom_partie;

        #region Constructeurs

        public Plateau(Dictionnaire dictionnaire, List<Joueur> joueurs, Sac_Jetons sac_jetons,string InstancePlateau_chemin, string InstanceScore_chemin, string nom_partie, bool nouvelle_partie) //Plateau de nouvelle partie
        {
            this.dictionnaire = dictionnaire;
            this.joueurs = joueurs;
            this.sac_jetons = sac_jetons;
            this.InstancePlateau_chemin = InstancePlateau_chemin;
            this.InstanceScore_chemin = InstanceScore_chemin;
            this.nom_partie = nom_partie;
            if (nouvelle_partie)
            {
                this.WriteFile($"Fichier\\{nom_partie}\\{InstancePlateau_chemin}", $"Fichier\\{ nom_partie}\\{InstanceScore_chemin}", true);
                this.ReadFileMatricePlateau($"Fichier\\{nom_partie}\\{InstancePlateau_chemin}");
                this.ReadFileMatriceScore(InstanceScore_chemin);
            }
            else
            {
                this.ReadFileMatricePlateau($"Fichier\\{nom_partie}\\{InstancePlateau_chemin}");
                this.ReadFileMatriceScore($"Fichier\\{nom_partie}\\{InstanceScore_chemin}");
            }
        }
        public Plateau(Dictionnaire dictionnaire, List<Joueur> joueurs, Sac_Jetons sac_jetons, string InstancePlateau_chemin, string InstanceScore_chemin, bool test) //Plateau de test unitaire
        {
            this.dictionnaire = dictionnaire;
            this.joueurs = joueurs;
            this.sac_jetons = sac_jetons;
            this.InstancePlateau_chemin = InstancePlateau_chemin;
            this.InstanceScore_chemin = InstanceScore_chemin;
            this.ReadFileMatricePlateau(InstancePlateau_chemin);
            this.ReadFileMatriceScore(InstanceScore_chemin);
        }

        #endregion

        #region Propriétés 

        public char[,] Matrice_jeu
        {
            get { return matrice_jeu; }
        }

        #endregion

        #region Méthodes

        public void AffichageMatriceIntBrut()
        {
            if (matrice_score == null || matrice_score.Length == 0)
            {
                Console.WriteLine("Matrice null ou vide");
            }
            else
            {
                for (int ligne = 0; ligne < matrice_score.GetLength(0); ligne++)
                {
                    for (int colonne = 0; colonne < matrice_score.GetLength(1); colonne++) //GetLength(1) récupère le nombre de case dans une colonne. 
                    {
                        if (matrice_score[ligne, colonne] < 10)
                        {
                            Console.Write($" {matrice_score[ligne, colonne]} ");
                        }
                        else Console.Write($"{matrice_score[ligne, colonne]} ");
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
        /// <summary>
        /// Affichage de la matrice_jeu avec les couleurs indiquées par les valeurs [i,j] de la matrice_score. C'est jolie ! 
        /// </summary>
        public void toString()
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
            if (!Directory.Exists($"Fichier\\{nom_partie}"))
            {
                Directory.CreateDirectory($"Fichier\\{nom_partie}");
            }
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
                for (int ligne = 0; ligne < 15; ligne++)
                {
                    for (int colonne = 0; colonne < 15; colonne++)
                    {
                        if (colonne == 14) writer_score.Write($"{matrice_score[ligne, colonne]}\n");
                        else writer_score.Write($"{matrice_score[ligne, colonne]};");
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
        /// <summary>
        /// Fonction test_plateau qui : 
        /// - Test si un mot est posable (vérification présent dictionnaire, vérification adjacent à un mot...)
        /// - Vérifie si le joueur possède des jokers et si il veut les utiliser
        /// - Retire les jetons de la main courante du joueur utilisé pour la completion d'un mot
        /// - Le pose dans la matrice_jeu
        /// - Calcul le score associé à la pose du mot
        /// </summary>
        /// <param name="mot">Mot que le joueur veut poser</param>
        /// <param name="ligne">Ligne où LA PREMIERE LETTRE DU MOT EST</param>
        /// <param name="colonne">Colonne où LA PREMIERE LETTRE DU MOT EST</param>
        /// <param name="direction">Variable qui détermine si le mot doit être poser horizontalement ou verticalement</param>
        /// <param name="joueur">Joueur qui veut poser le mot</param>
        /// <returns></returns>
        public bool Test_Plateau(string mot, int ligne, int colonne, char direction, int nb_tour, Joueur joueur)
        {
            #region Variables 
            int ligne_decalage = ligne - 1;
            int colonne_decalage = colonne - 1;
            //ligne et colonne représente la position du DEBUT DU MOT
            bool possible = false;
            bool joker = false;
            //Multiplicateur
            int multiplicateur_horizontal = 1;
            int multiplicateur_vertical = 1;
            int jeton_considere_pivot = 0;
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

            string mot_a_rajouter = "";
            string motCopie = mot;
            List<int> PositionLettreManquante = new List<int>();
            List<int> PositionPivot = new List<int>();
            List<int> PositionJoker = new List<int>();
            List<string> LettrePresente = new List<string>();
            List<char> LettreManquante = new List<char>();
            Queue<char> LettreManquante_sousQueue = new Queue<char>();
            #endregion

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

                #region Joker 
                for (int i = 0; i < Jeton_joueur_tableau.Length; i++)
                {
                    if (joueur.Jeton_joueur[i].Nom_jeton == "*")
                    {
                        joker = true;
                        PositionJoker.Add(i);
                    }
                }
                if (joker)
                {
                    if (PositionJoker.Count == 1)
                    {
                        Console.WriteLine("Vous possédez un joker. Voulez-vous l'utiliser ? (o/n)");
                        string recu = Console.ReadLine();
                        if (recu.ToLower() == "o")
                        {
                            bool error = true;
                            Console.WriteLine("En quoi voulez-vous transformer votre joker ?\nExplication:\n- IMPOSSIBLE de choisir un joker pour transformer en joker.\n-Il faut rentrer seulement une lettre entre A et Z de cette manière : A");
                            char lettre_recu = ' ';
                            while (error)
                            {
                                try
                                {
                                    lettre_recu = Convert.ToChar(Console.ReadLine().ToUpper());
                                    if (lettre_recu.GetType().Equals(typeof(char))) error = false;
                                }
                                catch
                                {
                                    error = true;
                                    Console.WriteLine("Veuillez respecter la syntaxe. (c'est un char)");
                                }
                            }
                            joueur.Jeton_joueur.RemoveAt(PositionJoker[0]);
                            joueur.Jeton_joueur.Insert(PositionJoker[0], sac_jetons.Convert_To_Jeton(lettre_recu));
                            Jeton_joueur.Clear();
                            Jeton_joueur_tableau = joueur.Jeton_joueur_liste().Split(';');
                            for (int i = 0; i < Jeton_joueur_tableau.Length; i++)
                            {
                                Jeton_joueur.Add(Convert.ToChar(Jeton_joueur_tableau[i]));
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Vous possédez deux joker. Voulez-vous en utiliser ? (o/n)");
                        string recu = Console.ReadLine();
                        if (recu.ToLower() == "o")
                        {
                            bool mauvaise_conversion = false;
                            Console.WriteLine("Combien voulez-vous en utiliser (1 ou 2): ");
                            string nombre_recu = Console.ReadLine();
                            int nombre_desiree = 0;
                            if (nombre_recu == "1" || nombre_recu == "2") nombre_desiree = Convert.ToInt32(nombre_recu);
                            else mauvaise_conversion = true;
                            while (mauvaise_conversion)
                            {
                                Console.WriteLine("Combien voulez-vous en utiliser (1 ou 2): ");
                                nombre_recu = Console.ReadLine();
                                if (nombre_recu == "1" || nombre_recu == "2") nombre_desiree = Convert.ToInt32(nombre_recu);
                                else mauvaise_conversion = true;

                            }
                            for (int i = 0; i < nombre_desiree; i++)
                            {
                                bool error = true;
                                Console.WriteLine($"En quoi voulez-vous transformer votre joker n°{i + 1} ?\nExplication:\n- IMPOSSIBLE de choisir un joker pour transformer en joker.\n- Il faut rentrer seulement une lettre entre A et Z de cette manière : A");
                                char lettre_recu = ' ';
                                while (error)
                                {
                                    try
                                    {
                                        lettre_recu = Convert.ToChar(Console.ReadLine().ToUpper());
                                        if (lettre_recu == '*')
                                        {
                                            error = true;
                                            Console.WriteLine("Impossible d'entrer un joker. Veuillez réessayer !");
                                        }
                                        else if (lettre_recu.GetType().Equals(typeof(char))) error = false;
                                    }
                                    catch
                                    {
                                        error = true;
                                        Console.WriteLine("Veuillez respecter la syntaxe. (c'est un char)");
                                    }
                                }
                                Console.WriteLine("Conversion effective !");
                                joueur.Jeton_joueur.RemoveAt(PositionJoker[i]);
                                joueur.Jeton_joueur.Insert(PositionJoker[i], sac_jetons.Convert_To_Jeton(lettre_recu));
                            }

                        }
                    }
                    Jeton_joueur.Clear();
                    Jeton_joueur_tableau = joueur.Jeton_joueur_liste().Split(';');
                    for (int index = 0; index < Jeton_joueur_tableau.Length; index++)
                    {
                        Jeton_joueur.Add(Convert.ToChar(Jeton_joueur_tableau[index]));
                    }
                }
                #endregion

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
                        if (nb_tour == 0) //Au tour 0, le joueur doit pouvoir placer le mot au centre sans problème
                        {
                            bool toucheMilieu = false;
                            for (int index = 0; index < mot.Length && !toucheMilieu; index++)
                            {
                                if (ligne_decalage == 7 && colonne_decalage + index == 7) toucheMilieu = true;
                            }
                            if (toucheMilieu)
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
                                possible = true;
                            }     
                            else Console.WriteLine("Faites en sorte qu'une lettre du mot passe par le milieu en (8,8) !");
                        }
                        else
                        {
                            if (ToucheMot(mot, ligne_decalage, colonne_decalage, direction))
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
                            else Console.WriteLine("Vous ne touchez pas un mot déjà existant!"); //On vérifie que la position où on veut placer le mot touche un autre mot
                        }   
                    }
                    else Console.WriteLine("Il vous manque les jetons pour compléter le mot !");
                }
                else if (direction == 'v') //direction == 'v'
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
                        if (nb_tour == 0)
                        {
                            bool toucheMilieu = false;
                            for (int index = 0; index < mot.Length && !toucheMilieu; index++)
                            {
                                if (ligne_decalage + index == 7 && colonne_decalage == 7) toucheMilieu = true;
                            }
                            if (toucheMilieu)
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
                                possible = true;
                            }
                            else Console.WriteLine("Faites en sorte qu'une lettre du mot passe par le milieu en (8,8) !");
                        }
                        else
                        {
                            if (ToucheMot(mot, ligne_decalage, colonne_decalage, direction))
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
                                            horizontalement_correct = dictionnaire.RechDico(MotFinal); //On teste si le mot est horizontalement correct
                                        }
                                    }
                                    #endregion

                                    if (horizontalement_correct) possible = true;
                                }
                            }
                            else Console.WriteLine("Vous ne touchez pas un mot déjà existant!");
                        }
                    }
                    else Console.WriteLine("Il vous manque les jetons pour compléter le mot !");
                }
                else Console.WriteLine("Entrez soit :\n-'h' : direction horizontale\n-'v' : direction verticale");
            }
            else Console.WriteLine($"Le mot {mot} n'appartient pas au dictionnaire. (Une erreur de syntaxe ?)");

            #endregion

            if (possible) //On recopie la matrice_imaginaire dans la matrice_jeu 
            {
                this.matrice_jeu = CopieMatrice(this.matrice_jeu, this.matrice_jeu_imaginaire);
                #region On retire les jetons de la main du joueur
                foreach (char element in LettreManquante)
                {
                    bool tag = false;
                    for (int i = 0; i < joueur.Jeton_joueur.Count && !tag; i++)
                    {
                        if (Convert.ToString(element) == joueur.Jeton_joueur[i].Nom_jeton)
                        {
                            tag = true;
                            joueur.Jeton_joueur.RemoveAt(i);
                        }
                    }
                    
                }
                #endregion

                #region Calcul du score 
                //Cas horizontal
                if (direction == 'h')
                {
                    #region Calcul des points du mot de base
                    for (int i = 0; i < mot.Length; i++) //On ajoute le score du mot de base, horizontalement SANS MUTLIPLICATEUR DE MOT
                    {
                        bool tag_1 = false;
                        bool tag_2 = false;
                        int jeton_considere = sac_jetons.Sac_jetons_Get.IndexOfKey(Convert.ToString(matrice_jeu_imaginaire[ligne_decalage, colonne_decalage + i])); //Récupère l'index dans le sac_jetons de la lettre considérée
                        if (matrice_score[ligne_decalage, colonne_decalage + i] == 3) multiplicateur_horizontal *= 2;
                        if (matrice_score[ligne_decalage, colonne_decalage + i] == 4) multiplicateur_horizontal *= 3;
                        if (matrice_score[ligne_decalage, colonne_decalage + i] == 1) //Si la i-ème lettre est sur un lettre compte double
                        {
                            matrice_score[ligne_decalage, colonne_decalage + i] = 5; //Permet de dire que le bonus de la case est utilisé et se transforme en "Case lettre compte double utilisée"
                            score_horizontal += sac_jetons.Sac_jetons_Get.ElementAt(jeton_considere).Value.Valeur_jeton * 2; //On ajoute la valeur de la lettre mutipliée par 2
                        }
                        if (matrice_score[ligne_decalage, colonne_decalage + i] == 2)
                        {
                            matrice_score[ligne_decalage, colonne_decalage + i] = 6; //Permet de dire que le bonus de la case est utilisé et se transforme en "Case lettre compte triple utilisée"
                            score_horizontal += sac_jetons.Sac_jetons_Get.ElementAt(jeton_considere).Value.Valeur_jeton * 3; //On ajoute la valeur de la lettre mutipliée par 3
                        }
                        if ((matrice_score[ligne_decalage, colonne_decalage + i] == 3 || matrice_score[ligne_decalage, colonne_decalage + i] == 4 || matrice_score[ligne_decalage, colonne_decalage + i] == 0 || matrice_score[ligne_decalage, colonne_decalage + i] == 7 || matrice_score[ligne_decalage, colonne_decalage + i] == 8) && !tag_1 && !tag_2)
                        {
                            score_horizontal += sac_jetons.Sac_jetons_Get.ElementAt(jeton_considere).Value.Valeur_jeton;
                        }
                    }
                    #endregion

                    #region Position pivot : remplissage
                    for (int i = 0; i < PositionLettreManquante.Count; i++)
                    {
                        if (ligne_decalage != 14 && ligne_decalage != 0)
                        {
                            if (matrice_jeu[ligne_decalage - 1, PositionLettreManquante[i]] != '_') PositionPivot.Add(PositionLettreManquante[i]);
                            if (matrice_jeu[ligne_decalage + 1, PositionLettreManquante[i]] != '_') PositionPivot.Add(PositionLettreManquante[i]);
                        }
                        if (ligne_decalage == 0)
                        {
                            if (matrice_jeu[ligne_decalage + 1, PositionLettreManquante[i]] != '_') PositionPivot.Add(PositionLettreManquante[i]);
                        }
                        if (ligne_decalage == 14)
                        {
                            if (matrice_jeu[ligne_decalage - 1, PositionLettreManquante[i]] != '_') PositionPivot.Add(PositionLettreManquante[i]);
                        }
                    }
                    #endregion

                    #region Ajout des mots dans la liste du joueur
                    mot_a_rajouter += $"{mot};"; //On rajoute le mot de base
                    for (int j = 0; j < PositionPivot.Count; j++) //On rajoute les nouveaux mots formés 
                    {
                        bool underscore_haut = false;
                        bool underscore_bas = false;
                        string mottemp_haut = "";
                        string mottemp_bas = "";
                        string mot_final = "";
                        for (int index = ligne_decalage - 1; index > 0 && !underscore_haut; index--) //On se place à la i-ème lettre du mot, et on monte jusqu'à croiser un underscore tout en ajoutant chaque lettre à mottemp_haut
                        {
                            if (matrice_jeu[index, PositionPivot[j]] == '_') underscore_haut = true;
                            if (matrice_jeu[index, PositionPivot[j]] != '_') mottemp_haut += matrice_jeu[index, PositionPivot[j]];
                        }
                        mottemp_haut = ReverseString(mottemp_haut);
                        for (int index = ligne_decalage + 1; index < 15 && !underscore_bas; index++) //On se place à la i-ème lettre du mot, et on descend jusqu'à croiser un underscore tout en ajoutant chaque lettre à mottemp_bas
                        {
                            if (matrice_jeu[index, PositionPivot[j]] == '_') underscore_bas = true;
                            if (matrice_jeu[index, PositionPivot[j]] != '_') mottemp_bas += matrice_jeu[index, PositionPivot[j]];
                        }
                        mot_final = mottemp_haut + matrice_jeu[ligne_decalage, PositionPivot[j]] + mottemp_bas;
                        mot_a_rajouter += mot_final;
                    }
                    string[] tab_a_rajouter = mot_a_rajouter.Split(';');
                    for (int i = 0; i < tab_a_rajouter.Length; i++)
                    {
                        joueur.Add_Mot(tab_a_rajouter[i]);
                    }
                    #endregion

                    foreach (int element in PositionPivot) //Permet d'ajouter les scores des positions pivots UNE SEULE FOIS 
                    {
                        jeton_considere_pivot = sac_jetons.Sac_jetons_Get.IndexOfKey(Convert.ToString(matrice_jeu[ligne_decalage, element]));
                        score_vertical += sac_jetons.Sac_jetons_Get.ElementAt(jeton_considere_pivot).Value.Valeur_jeton;
                    }

                    #region Calcul des points des combinaisons de nouveaux mots à partir des lettres manquantes 
                    for (int j = 0; j < PositionPivot.Count; j++) //On parcourt seulement les positions des lettres manquantes ajoutés qui forme des nouvelles combinaisons de mots SANS MUTLIPLICATEUR
                    {
                        int jeton_considere = 0;
                        multiplicateur_vertical = 1;
                        bool underscore_haut = false;
                        bool underscore_bas = false;

                        for (int index = ligne_decalage - 1; index > 0 && !underscore_haut; index--) //On se place à la i-ème lettre manquante du mot, et on monte jusqu'à croiser un underscore tout en ajoutant le score de chaque lettre avec les multiplicateurs
                        {
                            jeton_considere = sac_jetons.Sac_jetons_Get.IndexOfKey(Convert.ToString(matrice_jeu[index, PositionPivot[j]])); //Récupère l'index dans le sac_jetons de la lettre considérée
                            if (matrice_jeu[index, PositionPivot[j]] == '_') underscore_haut = true;
                            if (matrice_score[ligne_decalage, PositionPivot[j]] == 3) multiplicateur_vertical *= 2;
                            if (matrice_score[ligne_decalage, PositionPivot[j]] == 4) multiplicateur_vertical *= 3;
                            if (matrice_jeu[index, PositionPivot[j]] != '_') score_vertical += sac_jetons.Sac_jetons_Get.ElementAt(jeton_considere).Value.Valeur_jeton;
                        }
                        for (int index = ligne_decalage + 1; index < 15 && !underscore_bas; index++) //On se place à la i-ème lettre manquante du mot, et on descend jusqu'à croiser un underscore tout en ajoutant le score de chaque lettre
                        {
                            jeton_considere = sac_jetons.Sac_jetons_Get.IndexOfKey(Convert.ToString(matrice_jeu[index, PositionPivot[j]])); //Récupère l'index dans le sac_jetons de la lettre considérée
                            if (matrice_jeu[index, PositionPivot[j]] == '_') underscore_bas = true;
                            if (matrice_score[index, PositionPivot[j]] == 3) multiplicateur_vertical *= 2;
                            if (matrice_score[index, PositionPivot[j]] == 4) multiplicateur_vertical *= 3;
                            if (matrice_jeu[index, PositionPivot[j]] != '_' && (matrice_score[index, PositionPivot[j]] == 0 || matrice_score[index, PositionPivot[j]] == 5 || matrice_score[index, PositionPivot[j]] == 6 || matrice_score[index, PositionPivot[j]] == 7 || matrice_score[index, PositionPivot[j]] == 8)) score_vertical += sac_jetons.Sac_jetons_Get.ElementAt(jeton_considere).Value.Valeur_jeton;
                        }
                        score_vertical *= multiplicateur_vertical;
                    }
                    #endregion

                    joueur.Add_Score((score_horizontal * multiplicateur_horizontal) + score_vertical); //On ajoute le score final du joueur en multipliant par les multiplicateurs éventuels
                }
                //Cas vertical
                else if (direction == 'v')
                {
                    #region Calcul des points du mot de base
                    for (int i = 0; i < mot.Length; i++) //On ajoute le score du mot de base, horizontalement SANS MUTLIPLICATEUR DE MOT
                    {
                        bool tag_1 = false;
                        bool tag_2 = false;
                        int jeton_considere = sac_jetons.Sac_jetons_Get.IndexOfKey(Convert.ToString(matrice_jeu[ligne_decalage + i, colonne_decalage])); //Récupère l'index dans le sac_jetons de la lettre considérée
                        if (matrice_score[ligne_decalage + i, colonne_decalage] == 3) multiplicateur_vertical *= 2;
                        if (matrice_score[ligne_decalage + i, colonne_decalage] == 4) multiplicateur_vertical *= 3;
                        if (matrice_score[ligne_decalage + i, colonne_decalage] == 1) //Si la i-ème lettre est sur un lettre compte double
                        {
                            tag_1 = true;
                            matrice_score[ligne_decalage + i, colonne_decalage] = 5; //Permet de dire que le bonus de la case est utilisé et se transforme en "Case lettre compte double utilisée"
                            score_vertical += sac_jetons.Sac_jetons_Get.ElementAt(jeton_considere).Value.Valeur_jeton * 2; //On ajoute la valeur de la lettre mutipliée par 2
                        }
                        if (matrice_score[ligne_decalage + i, colonne_decalage] == 2)
                        {
                            tag_2 = true;
                            matrice_score[ligne_decalage + i, colonne_decalage] = 6; //Permet de dire que le bonus de la case est utilisé et se transforme en "Case lettre compte triple utilisée"
                            score_vertical += sac_jetons.Sac_jetons_Get.ElementAt(jeton_considere).Value.Valeur_jeton * 3; //On ajoute la valeur de la lettre mutipliée par 3
                        }
                        if ((matrice_score[ligne_decalage + i, colonne_decalage] == 0 || matrice_score[ligne_decalage + i, colonne_decalage] == 3 || matrice_score[ligne_decalage + i, colonne_decalage] == 4 || matrice_score[ligne_decalage + i, colonne_decalage] == 7 || matrice_score[ligne_decalage + i, colonne_decalage] == 8) && !tag_1 && !tag_2)
                        {
                            score_vertical += sac_jetons.Sac_jetons_Get.ElementAt(jeton_considere).Value.Valeur_jeton;
                        }
                    }
                    #endregion

                    #region Position pivot : remplissage
                    for (int i = 0; i < PositionLettreManquante.Count; i++)
                    {
                        if (colonne_decalage != 14 && colonne_decalage != 0)
                        {
                            if (matrice_jeu[PositionLettreManquante[i], colonne_decalage - 1] != '_') PositionPivot.Add(PositionLettreManquante[i]);
                            if (matrice_jeu[PositionLettreManquante[i], colonne_decalage + 1] != '_') PositionPivot.Add(PositionLettreManquante[i]);
                        }
                        if (colonne_decalage == 0)
                        {
                            if (matrice_jeu[PositionLettreManquante[i], colonne_decalage + 1] != '_') PositionPivot.Add(PositionLettreManquante[i]);
                        }
                        if (colonne_decalage == 14)
                        {
                            if (matrice_jeu[PositionLettreManquante[i], colonne_decalage - 1] != '_') PositionPivot.Add(PositionLettreManquante[i]);
                        }
                    }
                    #endregion

                    #region Ajout des mots dans la liste du joueur
                    mot_a_rajouter += $"{mot};"; //On rajoute le mot de base
                    for (int j = 0; j < PositionPivot.Count; j++) //On rajoute les nouveaux mots formés 
                    {
                        string mottemp_avant = "";
                        string mottemp_apres = "";
                        string mot_final = "";
                        bool underscore_avant = false;
                        bool underscore_apres = false;
                        for (int index = colonne_decalage - 1; index > 0 && !underscore_avant; index--) //On se place à la i-ème lettre du mot, et on recule jusqu'à croiser un underscore tout en ajoutant chaque lettre à MotAvant
                        {
                            if (matrice_jeu[PositionPivot[j], index] == '_') underscore_avant = true;
                            if (matrice_jeu[PositionPivot[j], index] != '_') mottemp_avant += matrice_jeu[PositionPivot[j], index];
                        }
                        mottemp_avant = ReverseString(mottemp_avant);
                        for (int index = colonne_decalage + 1; index < 15 && !underscore_apres; index++) //On se place à la i-ème lettre du mot, et on avance jusqu'à croiser un underscore tout en ajoutant chaque lettre à MotApres
                        {
                            if (matrice_jeu[PositionPivot[j], index] == '_') underscore_apres = true;
                            if (matrice_jeu[PositionPivot[j], index] != '_') mottemp_apres += matrice_jeu[PositionPivot[j], index];
                        }
                        mot_final = mottemp_avant + matrice_jeu_imaginaire[PositionPivot[j], colonne_decalage] + mottemp_apres;
                        mot_a_rajouter += mot_final;
                    }
                    string[] tab_a_rajouter = mot_a_rajouter.Split(';');
                    for (int i = 0; i < tab_a_rajouter.Length; i++)
                    {
                        joueur.Add_Mot(tab_a_rajouter[i]);
                    }
                    #endregion

                    foreach (int element in PositionPivot) //Permet d'ajouter les scores des positions pivots UNE SEULE FOIS 
                    {
                        jeton_considere_pivot = sac_jetons.Sac_jetons_Get.IndexOfKey(Convert.ToString(matrice_jeu[element, colonne_decalage]));
                        score_horizontal += sac_jetons.Sac_jetons_Get.ElementAt(jeton_considere_pivot).Value.Valeur_jeton;
                    }
                    
                    #region Calcul des points des combinaisons de nouveaux mots à partir des lettres manquantes 
                    for (int j = 0; j < PositionPivot.Count; j++) //On parcourt seulement les positions des lettres manquantes ajoutés qui forme des nouvelles combinaisons de mots SANS MUTLIPLICATEUR
                    {
                        int jeton_considere = 0;
                        multiplicateur_horizontal = 1;
                        bool underscore_gauche = false;
                        bool underscore_droite = false;
                        for (int index = colonne_decalage - 1; index > 0 && !underscore_gauche; index--) //On se place à la i-ème lettre manquante du mot, et on va a gauche jusqu'à croiser un underscore tout en ajoutant le score de chaque lettre avec les multiplicateurs
                        {
                            jeton_considere = sac_jetons.Sac_jetons_Get.IndexOfKey(Convert.ToString(matrice_jeu[PositionPivot[j], index])); //Récupère l'index dans le sac_jetons de la lettre considérée
                            if (matrice_jeu[PositionPivot[j], index] == '_') underscore_gauche = true;
                            if (matrice_score[PositionPivot[j], colonne_decalage] == 3) multiplicateur_horizontal *= 2;
                            if (matrice_score[PositionPivot[j], colonne_decalage] == 4) multiplicateur_horizontal *= 3;
                            if (matrice_jeu[PositionPivot[j], index] != '_') score_horizontal += sac_jetons.Sac_jetons_Get.ElementAt(jeton_considere).Value.Valeur_jeton;
                        }
                        for (int index = colonne_decalage + 1; index < 15 && !underscore_droite; index++) //On se place à la i-ème lettre manquante du mot, et on descend jusqu'à croiser un underscore tout en ajoutant le score de chaque lettre
                        {
                            jeton_considere = sac_jetons.Sac_jetons_Get.IndexOfKey(Convert.ToString(matrice_jeu[PositionPivot[j], index])); //Récupère l'index dans le sac_jetons de la lettre considérée
                            if (matrice_jeu[PositionPivot[j], index] == '_') underscore_droite = true;
                            if (matrice_score[PositionPivot[j], index] == 3) multiplicateur_horizontal *= 2;
                            if (matrice_score[PositionPivot[j], index] == 4) multiplicateur_horizontal *= 3;
                            if (matrice_jeu[PositionPivot[j], index] != '_' && (matrice_score[PositionPivot[j], index] == 0 || matrice_score[PositionPivot[j], index] == 5 || matrice_score[PositionPivot[j], index] == 6 || matrice_score[PositionPivot[j], index] == 7 || matrice_score[PositionPivot[j], index] == 8)) score_horizontal += sac_jetons.Sac_jetons_Get.ElementAt(jeton_considere).Value.Valeur_jeton;
                        }
                        score_horizontal *= multiplicateur_horizontal;
                    }
                    #endregion

                    joueur.Add_Score((score_vertical * multiplicateur_vertical) + score_horizontal); //On ajoute le score final du joueur en multipliant par les multiplicateurs éventuels
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
        /// <summary>
        /// Fonction qui permet de déterminer si un mot en touche un autre. 
        /// </summary>
        /// <param name="mot"></param>
        /// <param name="ligne"></param>
        /// <param name="colonne"></param>
        /// <param name="direction"></param>
        /// <returns></returns>
        public bool ToucheMot(string mot ,int ligne, int colonne, char direction)
        {
            bool tag_touche = false;
            if (direction == 'h')
            {
                for (int i = 0; i < mot.Length && !tag_touche; i++)
                {
                    #region Première lettre
                    if (i == 0)
                    {
                        if (colonne != 0 && ligne != 14 && ligne != 0 && colonne != 14)
                        {
                            if (matrice_jeu[ligne, colonne - 1] != '_') tag_touche = true; //à gauche 
                            if (matrice_jeu[ligne-1, colonne] != '_') tag_touche = true; //au dessus 
                            if (matrice_jeu[ligne+1, colonne] != '_') tag_touche = true; //en bas
                        }
                        if (colonne == 0 && ligne != 0 && ligne != 14)
                        {
                            if (matrice_jeu[ligne - 1, colonne] != '_') tag_touche = true; //au dessus 
                            if (matrice_jeu[ligne + 1, colonne] != '_') tag_touche = true; //en bas
                        }
                        if(colonne == 0 && ligne == 0)
                        {
                            if (matrice_jeu[ligne + 1, colonne] != '_') tag_touche = true; //en bas
                        }
                        if (colonne == 0 && ligne == 14)
                        {
                            if (matrice_jeu[ligne - 1, colonne] != '_') tag_touche = true; //au dessus 
                        }
                        if (colonne != 0 && ligne == 14)
                        {
                            if (matrice_jeu[ligne - 1, colonne] != '_') tag_touche = true; //au dessus 
                            if (matrice_jeu[ligne, colonne - 1] != '_') tag_touche = true; //à gauche 
                        }
                        if (colonne != 0 && ligne == 0)
                        {
                            if (matrice_jeu[ligne, colonne - 1] != '_') tag_touche = true; //à gauche 
                            if (matrice_jeu[ligne + 1, colonne] != '_') tag_touche = true; //en bas
                        }
                    }
                    #endregion
                    #region Dernière lettre
                    if (i == mot.Length - 1)
                    {
                        if (colonne != 14 && ligne != 14 && ligne != 0 && colonne != 0)
                        {
                            if (matrice_jeu[ligne, colonne + 1] != '_') tag_touche = true; //à droite 
                            if (matrice_jeu[ligne - 1, colonne] != '_') tag_touche = true; //au dessus 
                            if (matrice_jeu[ligne + 1, colonne] != '_') tag_touche = true; //en bas
                        }
                        if (colonne != 0 && colonne != 14 && ligne == 14)
                        {
                            if (matrice_jeu[ligne - 1, colonne] != '_') tag_touche = true; //au dessus 
                            if (matrice_jeu[ligne, colonne + 1] != '_') tag_touche = true; //à droite 
                        }
                        if (colonne == 14 && ligne != 0 && ligne != 14)
                        {
                            if (matrice_jeu[ligne - 1, colonne] != '_') tag_touche = true; //au dessus 
                            if (matrice_jeu[ligne + 1, colonne] != '_') tag_touche = true; //en bas
                        }
                        if (colonne == 14 && ligne == 0)
                        {
                            if (matrice_jeu[ligne + 1, colonne] != '_') tag_touche = true; //en bas
                        }
                        if (colonne == 14 && ligne == 14)
                        {
                            if (matrice_jeu[ligne - 1, colonne] != '_') tag_touche = true; //au dessus 
                        }
                        if (colonne != 14 && ligne == 0)
                        {
                            if (matrice_jeu[ligne, colonne + 1] != '_') tag_touche = true; //à droite 
                            if (matrice_jeu[ligne + 1, colonne] != '_') tag_touche = true; //en bas
                        }
                    }
                    #endregion
                    else if (i != 0 && i != mot.Length - 1)
                    {
                        if (ligne == 14)
                        {
                            if (matrice_jeu[ligne - 1, colonne] != '_') tag_touche = true; //au dessus  
                        }
                        if (ligne == 0)
                        {
                            if (matrice_jeu[ligne + 1, colonne] != '_') tag_touche = true; //en bas
                        }
                        if (ligne != 14 && ligne != 0)
                        {
                            if (matrice_jeu[ligne - 1, colonne] != '_') tag_touche = true; //au dessus 
                            if (matrice_jeu[ligne + 1, colonne] != '_') tag_touche = true; //en bas
                        }
                    }
                    
                }
            }
            else
            {
                for (int i = 0; i < mot.Length && !tag_touche; i++)
                {
                    #region Première lettre
                    if (i == 0)
                    {
                        if (colonne != 0 && ligne != 14 && ligne != 0 && colonne != 14)
                        {
                            if (matrice_jeu[ligne, colonne - 1] != '_') tag_touche = true; //à gauche 
                            if (matrice_jeu[ligne, colonne + 1] != '_') tag_touche = true; //à droite
                            if (matrice_jeu[ligne - 1, colonne] != '_') tag_touche = true; //au dessus 
                        }
                        if (colonne == 0 && ligne != 0 && ligne != 14)
                        {
                            if (matrice_jeu[ligne - 1, colonne] != '_') tag_touche = true; //au dessus 
                            if (matrice_jeu[ligne, colonne + 1] != '_') tag_touche = true; //à droite
                        }
                        if (colonne == 0 && ligne == 0)
                        {
                            if (matrice_jeu[ligne, colonne + 1] != '_') tag_touche = true; //à droite
                        }
                        if (colonne == 14 && ligne == 0)
                        {
                            if (matrice_jeu[ligne, colonne - 1] != '_') tag_touche = true; //à gauche 
                        }
                        if (colonne == 14 && ligne != 0)
                        {
                            if (matrice_jeu[ligne - 1, colonne] != '_') tag_touche = true; //au dessus 
                            if (matrice_jeu[ligne, colonne - 1] != '_') tag_touche = true; //à gauche 
                        }
                        if (colonne != 0 && ligne == 0 && colonne != 14)
                        {
                            if (matrice_jeu[ligne, colonne - 1] != '_') tag_touche = true; //à gauche 
                            if (matrice_jeu[ligne, colonne + 1] != '_') tag_touche = true; //à droite
                        }
                    }
                    #endregion
                    #region Dernière lettre
                    if (i == mot.Length - 1)
                    {
                        if (colonne != 14 && ligne != 14 && ligne != 0 && colonne != 0)
                        {
                            if (matrice_jeu[ligne, colonne + 1] != '_') tag_touche = true; //à droite 
                            if (matrice_jeu[ligne, colonne - 1] != '_') tag_touche = true; //à gauche 
                            if (matrice_jeu[ligne + 1, colonne] != '_') tag_touche = true; //en bas
                        }
                        if (colonne != 0 && colonne != 14 && ligne == 14)
                        {
                            if (matrice_jeu[ligne, colonne + 1] != '_') tag_touche = true; //à droite 
                            if (matrice_jeu[ligne, colonne - 1] != '_') tag_touche = true; //à gauche 
                        }
                        if (colonne == 14 && ligne != 0 && ligne != 14)
                        {
                            if (matrice_jeu[ligne, colonne - 1] != '_') tag_touche = true; //à gauche  
                            if (matrice_jeu[ligne + 1, colonne] != '_') tag_touche = true; //en bas
                        }
                        if (colonne == 14 && ligne == 14)
                        {
                            if (matrice_jeu[ligne, colonne - 1] != '_') tag_touche = true; //à gauche
                        }
                        if (colonne == 0 && ligne == 14)
                        {
                            if (matrice_jeu[ligne, colonne + 1] != '_') tag_touche = true; //à droite  
                        }
                        if (colonne == 0 && ligne != 14)
                        {
                            if (matrice_jeu[ligne, colonne + 1] != '_') tag_touche = true; //à droite 
                            if (matrice_jeu[ligne + 1, colonne] != '_') tag_touche = true; //en bas
                        }
                    }
                    #endregion
                    else if (i != 0 && i != mot.Length - 1)
                    {
                        if (colonne == 14)
                        {
                            if (matrice_jeu[ligne, colonne - 1] != '_') tag_touche = true; //à gauche  
                        }
                        if (colonne == 0)
                        {
                            if (matrice_jeu[ligne, colonne + 1] != '_') tag_touche = true; //à droite 
                        }
                        if (colonne != 14 && colonne != 0)
                        {
                            if (matrice_jeu[ligne, colonne - 1] != '_') tag_touche = true; //à gauche  
                            if (matrice_jeu[ligne, colonne + 1] != '_') tag_touche = true; //à droite 
                        }
                    }

                }
               
            }
            return tag_touche;
        }
        #endregion

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Scrables___TDG
{
    public class Jeu
    {
        //Variable d'instance ou champ d'instance
        private Dictionnaire mondico;
        private Plateau monplateau;
        private Sac_Jetons sac_jetons;
        private List<Joueur> joueurs;
        private string nom_partie;

        #region Constructeurs
        public Jeu() //Initialisation début de partie par le constructeurs
        {
            Console.WriteLine("Entrez le nom du dictionnaire (sans le .txt, ici le dictionnaire est 'Francais'): ");
            string dictionnaire_chemin = Console.ReadLine();
            #region Test : bonne entrée dictionnaire
            bool passe = true;
            if (dictionnaire_chemin == "Francais") passe = true;
            else passe = false;
            while (!passe)
            {
                Console.WriteLine("Entrez le nom du dictionnaire (sans le .txt, ici le dictionnaire est 'Francais'): ");
                dictionnaire_chemin = Console.ReadLine();
                if (dictionnaire_chemin == "Francais") passe = true;
                else passe = false;
            }
            #endregion
            Dictionnaire dictionnaire = new Dictionnaire(dictionnaire_chemin + ".txt");
            Console.WriteLine("Tapez N pour jouer une nouvelle partie, sinon tapez A pour rejouer une ancienne partie: ");
            string choix = Console.ReadLine();
            #region Test : est-ce que choix == N ou choix == A
            bool sortir = false;
            if (choix == "N" || choix == "A") sortir = true;
            while (!sortir)
            {
                Console.WriteLine("Tapez N pour jouer une nouvelle partie, sinon tapez A pour rejouer une ancienne partie (seulement N ou A): ");
                choix = Console.ReadLine();
                if (choix == "N" || choix == "A") sortir = true;
            }
            #endregion
            #region Nouvelle partie 
            if (choix == "N")
            {
                Console.WriteLine("Entrez le nom de la partie: ");
                string nom_partie = Console.ReadLine();
                Sac_Jetons sac_jetons = new Sac_Jetons("Jetons.txt", nom_partie, true); //Nouvelle partie donc sac_jeton pleins
                Console.WriteLine("Entrez le nombre de joueur(s) (maximum 4 joueurs): ");
                string joueur_recu_string = Console.ReadLine();
                int nombre_joueur_voulu = 0;
                #region Test : entrée du nombre de joueur correcte

                bool mauvaise_conversion = false;
                if (joueur_recu_string == "2" || joueur_recu_string == "3" || joueur_recu_string == "4") nombre_joueur_voulu = Convert.ToInt32(joueur_recu_string);
                else if (joueur_recu_string == "1")
                {
                    Console.WriteLine("Vous ne pouvez pas jouer tout seul....");
                    mauvaise_conversion = true;
                }
                else mauvaise_conversion = true;
                while (mauvaise_conversion)
                {
                    Console.WriteLine("Entrez le nombre de joueur(s) (maximum 4 joueurs): ");
                    joueur_recu_string = Console.ReadLine();
                    if (joueur_recu_string == "2" || joueur_recu_string == "3" || joueur_recu_string == "4")
                    {
                        mauvaise_conversion = false;
                        nombre_joueur_voulu = Convert.ToInt32(joueur_recu_string);
                    }
                    else mauvaise_conversion = true;
                }
                #endregion
                List<Joueur> joueurs = new List<Joueur>();
                for (int i = 0; i < nombre_joueur_voulu; i++)
                {
                    Console.WriteLine($"Entrez le nom du joueur n°{i + 1}: ");
                    string nom_joueur = Console.ReadLine();
                    Joueur joueur_ajouter = new Joueur(nom_joueur, sac_jetons, nom_partie, true);
                    joueurs.Add(joueur_ajouter);
                }
                Plateau plateau = new Plateau(dictionnaire, joueurs, sac_jetons, "InstancePlateau.txt", "InstancePlateauScore.txt", nom_partie, true);
                this.mondico = dictionnaire;
                this.monplateau = plateau;
                this.sac_jetons = sac_jetons;
                this.joueurs = joueurs;
                this.nom_partie = nom_partie;
            }
            #endregion
            #region Ancienne partie
            else if (choix == "A")
            {
                Console.WriteLine("Entrez le nom de la partie que vous voulez reprendre (voir dans bin/net5.0/Fichier pour ne pas faire d'erreur): ");
                string nom_partie_reprise = Console.ReadLine();
                Sac_Jetons sac_jetons_ancien = new Sac_Jetons("Jetons.txt", nom_partie_reprise, false);
                Console.WriteLine($"Entrez le nombre de joueurs qui était présent dans la partie (voir dans  bin/net5.0/Fichier/{nom_partie_reprise} pour ne pas faire d'erreur): ");
                string joueur_recu_string = Console.ReadLine();
                int nombre_joueur_avant = 0;
                #region Test : entrée du nombre de joueur correcte
                bool mauvaise_conversion = false;
                if (joueur_recu_string == "2" || joueur_recu_string == "3" || joueur_recu_string == "4") nombre_joueur_avant = Convert.ToInt32(joueur_recu_string);
                else if (joueur_recu_string == "1")
                {
                    Console.WriteLine("Vous ne pouvez pas jouer tout seul....");
                    mauvaise_conversion = true;
                }
                else mauvaise_conversion = true;
                while (mauvaise_conversion)
                {
                    Console.WriteLine("Entrez le nombre de joueur(s) (maximum 4 joueurs): ");
                    joueur_recu_string = Console.ReadLine();
                    if (joueur_recu_string == "2" || joueur_recu_string == "3" || joueur_recu_string == "4")
                    {
                        mauvaise_conversion = false;
                        nombre_joueur_avant = Convert.ToInt32(joueur_recu_string);
                    }
                    else mauvaise_conversion = true;
                }
                #endregion
                List<Joueur> joueurs = new List<Joueur>();
                for (int i = 0; i < nombre_joueur_avant; i++)
                {
                    Console.WriteLine($"Entrez le nom du joueur n°{i + 1}: ");
                    string nom_joueur = Console.ReadLine();
                    Joueur joueur_ajouter = new Joueur(nom_joueur, sac_jetons_ancien, nom_partie_reprise, false);
                    joueurs.Add(joueur_ajouter);
                }
                Plateau plateau = new Plateau(dictionnaire, joueurs, sac_jetons_ancien, "InstancePlateau.txt", "InstancePlateauScore.txt", nom_partie_reprise, false);
                this.mondico = dictionnaire;
                this.monplateau = plateau;
                this.sac_jetons = sac_jetons_ancien;
                this.joueurs = joueurs;
                this.nom_partie = nom_partie;
            }
            #endregion
        }
        #endregion

        #region Propriétés
        public Dictionnaire Dictionnaire_GetSet
        {
            get { return this.mondico; }
            set { this.mondico = value; }
        }
        public Plateau Plateau_GetSet
        {
            get { return this.monplateau; }
            set { this.monplateau = value; }
        }
        public Sac_Jetons Sac_Jetons_GetSet
        {
            get { return this.sac_jetons; }
            set { this.sac_jetons = value; }
        }
        public List<Joueur> Joueurs_GetSet
        {
            get { return this.joueurs; }
            set { this.joueurs = value; }
        }
        public string Nom_Partie
        {
            get { return this.nom_partie; }
        }
        #endregion


        static void Main(string[] args)
        {
            Jeu jeu = new Jeu();
            string nom_partie = jeu.Nom_Partie;
            int nb_jetonTotal = jeu.Sac_Jetons_GetSet.nb_jetonTotal_get;
            int nb_jetonRetire = jeu.Sac_Jetons_GetSet.nb_jetonRetire_get;
            Sac_Jetons sac_jetons = jeu.Sac_Jetons_GetSet;
            List<Joueur> listeJoueur = jeu.Joueurs_GetSet;
            Plateau plateau = jeu.Plateau_GetSet;
            int nbr_tour = 0;
            bool EnvieDeJouer = true;
            Console.WriteLine("Combien de temps voulez-vous qu'un joueur puisse se décider (en minutes): ");
            int temps_decide = Convert.ToInt32(Console.ReadLine());
            TimeSpan temps = new TimeSpan(0, temps_decide, 0);
            while (nb_jetonTotal - nb_jetonRetire > 0 && EnvieDeJouer) //On continue de jouer tant que le sac est pleins 
            {
                for (int i = 0; i < listeJoueur.Count; i++)
                {
                    DateTime FinDeTour = DateTime.Now + temps;
                    bool TourFini = false;
                    bool Choix_passe = false;
                    while (TourFini == false && Choix_passe == false)
                    {
                        Console.Clear();
                        DateTime TempsDebutDuTour = DateTime.Now;
                        Joueur joueur_considere = listeJoueur[i];
                        #region Ajoute si il manque des jetons dans la main du joueur les jetons manquants
                        int nombre_jeton_a_rajouter = 7 - joueur_considere.Jeton_joueur.Count;
                        for (int j = 0; j < nombre_jeton_a_rajouter; j++)
                        {
                            joueur_considere.Jeton_joueur.Add(sac_jetons.Retire_Jeton());
                        }
                        #endregion
                        Console.WriteLine($"C'est le tour de {joueur_considere.Nom_Joueur} !");
                        joueur_considere.Jeton_joueur_ToString();
                        Console.WriteLine($"Le joueur {joueur_considere.Nom_Joueur} possède {joueur_considere.Score_Joueur} points.");
                        plateau.toString();
                        Console.WriteLine("Voulez-vous changer de main, attention cela va passer votre tour (o/n): ");
                        string changer = Console.ReadLine();
                        #region Code pour changer la main
                        if (changer.ToLower() == "o")
                        {
                            List<Jeton> jeton_joueurConsidere = joueur_considere.Jeton_joueur;
                            for (int index = 0; index < jeton_joueurConsidere.Count; index++)
                            {
                                sac_jetons.Ajoute_Jeton(jeton_joueurConsidere[index]);
                            }
                            jeton_joueurConsidere.Clear();
                            for (int index = 0; index < 7; index++)
                            {
                                jeton_joueurConsidere.Add(sac_jetons.Retire_Jeton());
                            }
                            joueur_considere.Jeton_joueur_ToString();
                            Choix_passe = true;
                        }
                        #endregion
                        if (nbr_tour == 0 && Choix_passe == false)
                        {
                            Console.WriteLine("C'est le premier tour. Il faut nécessairement poser une mot qui passe par le centre en (8,8) !");
                            Console.WriteLine();
                            Console.WriteLine($"Vous avez {temps.Minutes} minutes pour ce tour.");
                            Console.WriteLine("Entrez le mot que vous voulez-placer (si vous êtes bloquer et vous voulez passer votre tour tapez 'B'): ");
                            string mot_recu = Console.ReadLine();
                            if (mot_recu == "B")
                            {
                                Choix_passe = true;
                            }
                            else
                            {
                                Console.WriteLine("Entrez la direction du mot (h/v) (h pour horizontale et v pour verticale): ");
                                char direction_recu = Convert.ToChar(Console.ReadLine());
                                Console.WriteLine("Entrez la ligne du début du mot: ");
                                #region Test : input ligne correcte
                                string ligne_string = Console.ReadLine();

                                bool ligne_bool_test = false;
                                int ligne_test = 0;
                                int ligne_recu = 0;
                                ligne_bool_test = int.TryParse(ligne_string, out ligne_test);
                                while (ligne_bool_test == false)
                                {
                                    Console.WriteLine("Entrez la ligne du début du mot: ");
                                    ligne_string = Console.ReadLine();
                                    ligne_bool_test = int.TryParse(ligne_string, out ligne_test);
                                }
                                ligne_recu = ligne_test;
                                #endregion
                                Console.WriteLine("Entrez la colonne du début du mot: ");
                                #region Test : input colonne correcte
                                string colonne_string = Console.ReadLine();
                                bool colonne_bool_test = false;
                                int colonne_test = 0;
                                int colonne_recu = 0;
                                colonne_bool_test = int.TryParse(colonne_string, out colonne_test);
                                while (colonne_bool_test == false)
                                {
                                    Console.WriteLine("Entrez la colonne du début du mot: ");
                                    colonne_string = Console.ReadLine();
                                    colonne_bool_test = int.TryParse(colonne_string, out colonne_test);
                                }
                                colonne_recu = colonne_test;
                                #endregion
                                if (DateTime.Now > FinDeTour)
                                {
                                    Console.WriteLine("Temps écoulé !");
                                    TourFini = true;
                                }
                                else
                                {
                                    bool poser = plateau.Test_Plateau(mot_recu.ToUpper(), ligne_recu, colonne_recu, direction_recu, 0, joueur_considere);
                                    while (poser == false)
                                    {
                                        Console.WriteLine("Placement incorrect. Veuillez réessayer en tenant compte des remarques !");
                                        Console.WriteLine();
                                        Console.WriteLine("Entrez le mot que vous voulez-placer : ");
                                        mot_recu = Console.ReadLine();
                                        Console.WriteLine();
                                        Console.WriteLine("Entrez la direction du mot (h/v) (h pour horizontale et v pour verticale): ");
                                        direction_recu = Convert.ToChar(Console.ReadLine());
                                        Console.WriteLine();
                                        Console.WriteLine("Entrez la ligne du début du mot: ");
                                        #region Test : input ligne correcte
                                        ligne_string = Console.ReadLine();

                                        ligne_bool_test = false;
                                        ligne_test = 0;
                                        ligne_recu = 0;
                                        ligne_bool_test = int.TryParse(ligne_string, out ligne_test);
                                        while (ligne_bool_test == false)
                                        {
                                            Console.WriteLine("Entrez la ligne du début du mot: ");
                                            ligne_string = Console.ReadLine();
                                            ligne_bool_test = int.TryParse(ligne_string, out ligne_test);
                                        }
                                        ligne_recu = ligne_test;
                                        #endregion
                                        Console.WriteLine();
                                        Console.WriteLine("Entrez la colonne du début du mot: ");
                                        #region Test : input colonne correcte
                                        colonne_string = Console.ReadLine();
                                        colonne_bool_test = false;
                                        colonne_test = 0;
                                        colonne_recu = 0;
                                        colonne_bool_test = int.TryParse(colonne_string, out colonne_test);
                                        while (colonne_bool_test == false)
                                        {
                                            Console.WriteLine("Entrez la colonne du début du mot: ");
                                            colonne_string = Console.ReadLine();
                                            colonne_bool_test = int.TryParse(colonne_string, out colonne_test);
                                        }
                                        colonne_recu = colonne_test;
                                        #endregion
                                        Console.WriteLine();
                                        poser = plateau.Test_Plateau(mot_recu.ToUpper(), ligne_recu, colonne_recu, direction_recu, 0, joueur_considere);
                                    }
                                    Console.WriteLine("Placement réussi !");
                                    nbr_tour++;
                                }
                            }
                        }
                        else if (Choix_passe == false)
                        {
                            if (DateTime.Now > FinDeTour)
                            {
                                Console.WriteLine("Temps écoulé !");
                                TourFini = true;
                            }
                            else
                            {
                                Console.WriteLine($"C'est le tour n°{nbr_tour + 1} !");
                                Console.WriteLine();
                                Console.WriteLine($"Vous avez {temps.Minutes} minutes pour ce tour.");
                                Console.WriteLine("Entrez le mot que vous voulez-placer (si vous êtes bloquer et vous voulez passer votre tour tapez 'B'): ");
                                string mot_recu = Console.ReadLine();
                                if (mot_recu == "B")
                                {
                                    Choix_passe = true;
                                }
                                else
                                {
                                    Console.WriteLine("Entrez la direction du mot (h/v) (h pour horizontale et v pour verticale): ");
                                    char direction_recu = Convert.ToChar(Console.ReadLine());
                                    Console.WriteLine("Entrez la ligne du début du mot: ");
                                    #region Test : input ligne correcte
                                    string ligne_string = Console.ReadLine();
                                    bool ligne_bool_test = false;
                                    int ligne_test = 0;
                                    int ligne_recu = 0;
                                    ligne_bool_test = int.TryParse(ligne_string, out ligne_test);
                                    while (ligne_bool_test == false)
                                    {
                                        Console.WriteLine("Entrez la ligne du début du mot: ");
                                        ligne_string = Console.ReadLine();
                                        ligne_bool_test = int.TryParse(ligne_string, out ligne_test);
                                    }
                                    ligne_recu = ligne_test;
                                    #endregion
                                    Console.WriteLine("Entrez la colonne du début du mot: ");
                                    #region Test : input colonne correcte
                                    string colonne_string = Console.ReadLine();
                                    bool colonne_bool_test = false;
                                    int colonne_test = 0;
                                    int colonne_recu = 0;
                                    colonne_bool_test = int.TryParse(colonne_string, out colonne_test);
                                    while (colonne_bool_test == false)
                                    {
                                        Console.WriteLine("Entrez la colonne du début du mot: ");
                                        colonne_string = Console.ReadLine();
                                        colonne_bool_test = int.TryParse(colonne_string, out colonne_test);
                                    }
                                    colonne_recu = colonne_test;
                                    #endregion
                                    bool poser = plateau.Test_Plateau(mot_recu.ToUpper(), ligne_recu, colonne_recu, direction_recu, nbr_tour, joueur_considere);
                                    while (poser == false)
                                    {
                                        Console.WriteLine("Placement incorrect. Veuillez réessayer en tenant compte des remarques !");
                                        Console.WriteLine();
                                        Console.WriteLine("Entrez le mot que vous voulez-placer : ");
                                        mot_recu = Console.ReadLine();
                                        Console.WriteLine();
                                        Console.WriteLine("Entrez la direction du mot (h/v) (h pour horizontale et v pour verticale): ");
                                        direction_recu = Convert.ToChar(Console.ReadLine());
                                        Console.WriteLine();
                                        Console.WriteLine("Entrez la ligne du début du mot: ");
                                        #region Test : input ligne correcte
                                        ligne_string = Console.ReadLine();
                                        ligne_bool_test = false;
                                        ligne_test = 0;
                                        ligne_recu = 0;
                                        ligne_bool_test = int.TryParse(ligne_string, out ligne_test);
                                        while (ligne_bool_test == false)
                                        {
                                            Console.WriteLine("Entrez la ligne du début du mot: ");
                                            ligne_string = Console.ReadLine();
                                            ligne_bool_test = int.TryParse(ligne_string, out ligne_test);
                                        }
                                        ligne_recu = ligne_test;
                                        #endregion
                                        Console.WriteLine();
                                        Console.WriteLine("Entrez la colonne du début du mot: ");
                                        #region Test : input colonne correcte
                                        colonne_string = Console.ReadLine();
                                        colonne_bool_test = false;
                                        colonne_test = 0;
                                        colonne_recu = 0;
                                        colonne_bool_test = int.TryParse(colonne_string, out colonne_test);
                                        while (colonne_bool_test == false)
                                        {
                                            Console.WriteLine("Entrez la colonne du début du mot: ");
                                            colonne_string = Console.ReadLine();
                                            colonne_bool_test = int.TryParse(colonne_string, out colonne_test);
                                        }
                                        colonne_recu = colonne_test;
                                        #endregion
                                        Console.WriteLine();
                                        poser = plateau.Test_Plateau(mot_recu.ToUpper(), ligne_recu, colonne_recu, direction_recu, nbr_tour, joueur_considere);
                                    }
                                    Console.WriteLine("Placement réussi !");
                                    nbr_tour++;
                                }
                                
                            }
                        }
                        nb_jetonRetire = jeu.Sac_Jetons_GetSet.nb_jetonRetire_get;
                        TourFini = true;
                        Console.WriteLine("Fin de tour ! Appuyez sur une touche pour continuer.");
                        Console.ReadKey();
                    }
                }
                Console.WriteLine("Voulez-vous continuer la partie (o/n): ");
                string EndGameRecu = Console.ReadLine();
                if (EndGameRecu.ToLower() == "o") EnvieDeJouer = true;
                else EnvieDeJouer = false;
                if (EnvieDeJouer == false) //il faut sauvegarder
                {
                    plateau.WriteFile($"Fichier\\{nom_partie}\\InstancePlateau.txt", $"Fichier\\{nom_partie}\\InstancePlateauScore.txt", false); //On sauvegarde l'instance du plateau et l'instance du plateau score
                    for (int i = 0; i < listeJoueur.Count; i++)
                    {
                        Joueur jouer_considere = listeJoueur[i];
                        jouer_considere.WriteFile($"Fichier\\{nom_partie}\\{jouer_considere.Nom_Joueur}.txt");
                    }
                    sac_jetons.WriteFile($"Fichier\\{nom_partie}\\Jetons.txt");
                }
            }
            int score_final = listeJoueur[0].Score_Joueur;
            int index_joueur_gagnant = 0;
            for (int i = 0; i < listeJoueur.Count; i++)
            {
                if (listeJoueur[i].Score_Joueur > score_final)
                {
                    score_final = listeJoueur[i].Score_Joueur;
                    index_joueur_gagnant = i;
                }
            }
            Console.WriteLine($"Le joueur {listeJoueur[index_joueur_gagnant].Nom_Joueur} est vainqueur (pour cette session) avec un score de : {score_final}! GG à lui !");
        }


    }
}

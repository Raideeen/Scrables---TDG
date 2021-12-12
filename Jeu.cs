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
        private static List<string> Nom_Parties = new List<string>();
        //Propriété de classe 
        public static List<string> Nom_Parties_Get { get { return Nom_Parties; } }

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
                if (joueur_recu_string == "1" || joueur_recu_string == "2" || joueur_recu_string == "3" || joueur_recu_string == "4") nombre_joueur_voulu = Convert.ToInt32(joueur_recu_string);
                else mauvaise_conversion = true;
                while (mauvaise_conversion)
                {
                    Console.WriteLine("Entrez le nombre de joueur(s) (maximum 4 joueurs): ");
                    joueur_recu_string = Console.ReadLine();
                    if (joueur_recu_string == "1" || joueur_recu_string == "2" || joueur_recu_string == "3" || joueur_recu_string == "4")
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
            }
            #endregion
            #region Ancienne partie
            else if (choix == "A")
            {
                Console.WriteLine("Entrez le nom de la partie que vous voulez reprendre: ");
                string nom_partie_reprise = Console.ReadLine();
                Sac_Jetons sac_jetons_ancien = new Sac_Jetons("Jetons.txt", nom_partie_reprise, false);
                Console.WriteLine("Entrez le nombre de joueurs qui était présent dans la partie: ");
                string joueur_recu_string = Console.ReadLine();
                int nombre_joueur_avant = 0;
                #region Test : entrée du nombre de joueur correcte

                bool mauvaise_conversion = false;
                if (joueur_recu_string == "1" || joueur_recu_string == "2" || joueur_recu_string == "3" || joueur_recu_string == "4") nombre_joueur_avant = Convert.ToInt32(joueur_recu_string);
                else mauvaise_conversion = true;
                while (mauvaise_conversion)
                {
                    Console.WriteLine("Entrez le nombre de joueur(s) (maximum 4 joueurs): ");
                    joueur_recu_string = Console.ReadLine();
                    if (joueur_recu_string == "1" || joueur_recu_string == "2" || joueur_recu_string == "3" || joueur_recu_string == "4")
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
            }
            #endregion
            //this.WriteFileNom_Parties(Nom_Parties);
            //this.ReadFileNom_Parties("Liste_partie.txt");
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
        #endregion

        #region Méthodes
        //public void WriteFileNom_Parties(List<string> Nom_Parties)
        //{
        //    StreamWriter writer = new StreamWriter($"Fichier\\Liste_partie.txt");
        //    File.WriteAllText($"Fichier\\Liste_partie.txt", String.Empty);
        //    for (int i = 0; i < Nom_Parties.Count; i++)
        //    {
        //        if (i == Nom_Parties.Count - 1) writer.Write($"{Nom_Parties[i]}");
        //        else writer.Write($"{Nom_Parties[i]};");
        //    }
        //}
        //public void ReadFileNom_Parties(string fichier)
        //{
        //    StreamReader lecture = new StreamReader(fichier);
        //    string ligne = lecture.ReadLine();
        //    string[] tab = ligne.Split(';');
        //    for (int i = 0; i < tab.Length; i++)
        //    {
        //        Nom_Parties.Add(tab[i]);
        //    }
        //}
        #endregion
        static void Main(string[] args)
        {
            Jeu jeu = new Jeu();
            
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
                        Console.WriteLine($"C'est le tour de {joueur_considere.Nom_Joueur} !");
                        joueur_considere.Jeton_joueur_ToString();
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
                        
                        if (nbr_tour == 0)
                        {
                            Console.WriteLine("C'est le premier tour. Il faut nécessairement poser une mot qui passe par le centre en (8,8) !");
                            Console.WriteLine("Entrez le mot que vous voulez-placer : ");
                            string mot_recu = Console.ReadLine();
                            Console.WriteLine("Entrez la direction du mot (h/v) (h pour horizontale et v pour verticale): ");
                            char direction_recu = Convert.ToChar(Console.ReadLine());
                            Console.WriteLine("Entrez la ligne du début du mot: ");
                            int ligne_recu = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Entrez la colonne du début du mot: ");
                            int colonne_recu = Convert.ToInt32(Console.ReadLine());
                            bool poser = plateau.Test_Plateau(mot_recu, ligne_recu, colonne_recu, direction_recu, 0, joueur_considere);
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
                                ligne_recu = Convert.ToInt32(Console.ReadLine());
                                Console.WriteLine();
                                Console.WriteLine("Entrez la colonne du début du mot: ");
                                colonne_recu = Convert.ToInt32(Console.ReadLine());
                                Console.WriteLine();
                                poser = plateau.Test_Plateau(mot_recu, ligne_recu, colonne_recu, direction_recu, 0, joueur_considere);
                            }
                            Console.WriteLine("Placement réussi !");
                            
                        }
                        Console.ReadKey();
                        #endregion
                    }
                }
                
            }
        }


    }
}

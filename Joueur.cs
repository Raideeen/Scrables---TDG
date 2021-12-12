using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Scrables___TDG
{
    public class Joueur
    {
        //Variable d'instance ou champ d'instance
        private string nom_joueur;
        private int score_joueur = 0;
        private string chemin_joueur;
        private Sac_Jetons sac_jeton;
        private List<string> mot_joueur = null;
        private List<Jeton> jeton_joueur = new List<Jeton>(7); //Les joueurs commencent avec une main courante de 7 jetons
        private string nom_partie;

        #region Constructeurs
        /// <summary>
        /// Constructeur pour créé pour initialiser un joueur 
        /// </summary>
        /// <param name="nom_joueur"></param>
        /// <param name="sac_jeton"></param>
        public Joueur(string nom_joueur, Sac_Jetons sac_jeton, string nom_partie, bool nouvelle_partie)
        {
            this.nom_joueur = nom_joueur;
            this.sac_jeton = sac_jeton;
            this.chemin_joueur = this.nom_joueur + ".txt";
            this.mot_joueur = new List<string>();
            this.nom_partie = nom_partie;
            if (nouvelle_partie)
            {
                for (int i = 0; i < 7; i++)
                {
                    Jeton jeton_considere = sac_jeton.Retire_Jeton();
                    jeton_joueur.Add(jeton_considere);
                }
                this.WriteFile($"Fichier\\{nom_partie}\\{nom_joueur}.txt"); //On créé l'instance du joueur avec les caractéristiques en début de partie
            }
            else
            {
                this.ReadFile(nom_joueur);
            }
            
        }
        /// <summary>
        /// Constructeur utilisé pour le projet TestUnitaire
        /// </summary>
        /// <param name="nom_joueur"></param>
        /// <param name="sac_jeton"></param>
        /// <param name="JoueurTest">Variable d'initialisation de créé un joueur avec une liste de jeton précise</param>
        public Joueur(string nom_joueur, Sac_Jetons sac_jeton, bool JoueurTest)
        {
            this.nom_joueur = nom_joueur;
            this.sac_jeton = sac_jeton;
            this.chemin_joueur = this.nom_joueur + ".txt";
            this.mot_joueur = new List<string>();
            jeton_joueur = sac_jeton.Retourne_liste();
        }

        #endregion

        #region Propriétés

        public List<Jeton> Jeton_joueur
        {
            get { return this.jeton_joueur; }
            set { this.jeton_joueur = value; }
        }
        public string Nom_Joueur
        {
            get { return this.nom_joueur; }
        }
        #endregion

        #region Méthodes
        public void ReadFile(string fichier)
        {
            StreamReader reader = new StreamReader($"Fichier\\{nom_partie}\\{fichier}.txt");
            string ligne = reader.ReadLine();
            string[] tab = ligne.Split(';');
            this.nom_joueur = tab[0];
            this.score_joueur = Convert.ToInt32(tab[1]);
            ligne = reader.ReadLine();
            if (ligne == null)
            {
                ligne = reader.ReadLine();
            }
            else //on lit les mots
            {
                tab = ligne.Split(';');
                for (int i = 0; i < tab.Length; i++)
                {
                    this.mot_joueur.Add(tab[i]);
                }
            }
            ligne = reader.ReadLine();
            tab = ligne.Split(';');
            for (int i = 0; i < tab.Length; i++)
            {
                Jeton jeton_considere = sac_jeton.Convert_To_Jeton(Convert.ToChar(tab[i]));
                this.jeton_joueur.Add(jeton_considere);
            }
            reader.Close();
        }

        public void WriteFile(string fichier)
        {
            if (!Directory.Exists($"Fichier\\{nom_partie}"))
            {
                Directory.CreateDirectory($"Fichier\\{nom_partie}");
            }
            StreamWriter writer = new StreamWriter(fichier);
            writer.WriteLine($"{nom_joueur};{score_joueur}"); //On écrit d'abord le nom_joueur puis son score
            if (mot_joueur == null)
            {
                writer.WriteLine();
            }
            else
            {
                for (int i = 0; i < mot_joueur.Count; i++) //On écrit les mots trouvés par le joueur successivement sur une même ligne
                {
                    if (i == mot_joueur.Count - 1) writer.Write($"{mot_joueur[i]}"); //Permet de ne pas avoir le point virgule à la fin pour avoir la bonne structure de fichier
                    else writer.Write($"{mot_joueur[i]};");
                }
            }
            writer.WriteLine();
            for (int i = 0; i < jeton_joueur.Count; i++) //On écrit les jetons présent dans la main du joueur
            {
                if (i == jeton_joueur.Count - 1) writer.Write($"{jeton_joueur[i].Nom_jeton}");
                else writer.Write($"{jeton_joueur[i].Nom_jeton};");
            }
            writer.Close();
        }

        public void Add_Mot(string mot)
        {
            mot_joueur.Add(mot);
            this.WriteFile($"{nom_joueur}.txt");
        }

        public void Add_Score(int val)
        {
            this.score_joueur += val;
        }

        public void Add_main_Courante(Jeton monjeton)
        {
            //La ligne 77, 78 et 82 est une solution temporaire au fait que, quand on appelle "Add_main_Courante"
            //c'est généralement pour tirer un jeton du sac_jeton de manière aléatoire, or on appelle alors la fonction "Retire_jetons()" 
            //qui fait -1 au nombre_jeton tirer aléatoirement du sac. Or, si le joueur ne peut pas tirer car sa main est pleine
            //alors le jeton est enlevé du jeu, alors qu'on veut qu'il soit encore dans le sac. Ainsi, on rajoute +1 au nombre_jeton,
            //du jeton considéré pour pouvoir réequilibrer
            string nom_jeton = monjeton.Nom_jeton; 
            int index_key = sac_jeton.Sac_jetons_Get.IndexOfKey(nom_jeton);

            if (jeton_joueur.Count < 7 && monjeton.Nombre_jeton != 0) jeton_joueur.Add(monjeton); //On ajoute un jeton ssi la main ne possède pas déjà 7 jetons et si le nombre de jeton du jeton_considere dans le sac est != 0
            else //Il n'est pas possible de tirer un jeton si la main est pleine
            {
                if (sac_jeton.Sac_jetons_Get.ElementAt(index_key).Value.Nombre_jeton == 0) //Si le nombre du jeton_considere est = 0, alors ça ne sert à rien de rajouter +1 car la fonction Retire_Jeton
                                                                                           //ne retire un jeton que s'il est != 0.
                {
                    Console.WriteLine($"Impossible d'avoir plus de 7 jetons dans la main! De plus, il n'y a plus de jeton {sac_jeton.Sac_jetons_Get.ElementAt(index_key).Value.Nom_jeton} dans le sac."); 
                }
                else
                {
                    Console.WriteLine("Impossible d'avoir plus de 7 jetons dans la main!");
                    sac_jeton.Sac_jetons_Get.ElementAt(index_key).Value.Nombre_jeton++;
                }
            }
        }

        public void Remove_Main_Courante(Jeton monjeton)
        {
            this.jeton_joueur.RemoveAt(jeton_joueur.IndexOf(monjeton)); //Retire le jeton "monjeton" à son index dans la liste
        }

        /// <summary>
        /// Fonction qui a pour but d'afficher en string la main du joueur 
        /// </summary>
        public void Jeton_joueur_ToString() 
        {
            string s = "";
            for (int i = 0; i < jeton_joueur.Count; i++)
            {
                if (i == jeton_joueur.Count - 1) s += $"{jeton_joueur[i].Nom_jeton}";
                else s += $"{jeton_joueur[i].Nom_jeton};";
            }
            Console.WriteLine($"Voici la liste des jetons dans la main du joueur {this.nom_joueur} : {s}");
        }
        /// <summary>
        /// Fonction qui retourne une descriptions du joueur
        /// </summary>
        /// <returns></returns>
        public string toString()
        {
            string s = "";
            s += $"Nom joueur: {this.nom_joueur}\nScore: {this.score_joueur}\nMot(s) joueur: ";
            for (int i = 0; i < this.mot_joueur.Count; i++)
            {
                if (i == this.mot_joueur.Count - 1) s += $"{this.mot_joueur[i]}";
                else s += $"{this.mot_joueur[i]} ";
            }
            s += "\nJetons joueur: ";
            for (int i = 0; i < this.jeton_joueur.Count; i++)
            {
                if (i == this.jeton_joueur.Count - 1) s += $"{this.jeton_joueur[i].Nom_jeton}";
                else s += $"{this.jeton_joueur[i].Nom_jeton} ";
            }
            return s;
        }
        public string Jeton_joueur_liste()
        {
            string s = "";
            for (int i = 0; i < jeton_joueur.Count; i++)
            {
                if (i == jeton_joueur.Count - 1) s += $"{jeton_joueur[i].Nom_jeton}";
                else s += $"{jeton_joueur[i].Nom_jeton};";
            }
            return s;
        }
        #endregion
    }
}

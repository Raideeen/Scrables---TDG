using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Scrables___TDG
{
    class Dictionnaire
    {
        //Variable d'instance ou champ d'instance
        private string langue; //Cette variable est la langue du dictionnaire. En pratique, elle représente le nom du fichier. Ex: "Français.txt"
        private SortedList<int,List<string>> listeMots_taille;
        //On choisi une SortedList et non un tableau pour stocker les mots d'une longueur i
        //compris entre 2 et 15 (inclus) car on ne sait pas à l'avance le nombre de mots d'une 
        //longueur i. De plus, on a une SortedList avec comme paramètre <int, List<string>> car le int correspond à la longueur du mot, 
        //donc cela permet d'identifier rapidement si on veut un mot d'une longueur de 2 quelle liste on va obtenir en value. Et, 
        //le choix d'avoir une "liste de liste" et que on peut ainsi utiliser toutes les méthodes utiles d'une liste càd "Contains", permettant de
        //voir si un mot est présent parmi la liste de mot de longueur i. 
        //Exemple : On cherche à savoir si le mot "boo" est présent dans le dictionnaire. On remarque qu'il a une longueur de 3, donc on va pouvoir
        //utiliser le clé listeMots_taille.ElementAt(IndexOfKey(3)).Value pour obtenir la liste des mots d'une longueur de 3. Ensuite, on va pouvoir
        //manipuler cette liste pour soit faire une recherche dichotomique (+ rapide) ou directement la fonction .Contains("boo"), permettant de savoir
        //si le mot existe dans le dictionnaire.
        private int longueur_fichier = 0;
        private int nb_ligne = 0;

        #region Constructeurs
        public Dictionnaire(string langue)
        {
            this.langue = langue;
            this.listeMots_taille = new SortedList<int, List<string>>();
            this.compteLigne(); //permet de donner le nombre de ligne du fichier à "longueur_fichier", qui sera utilisée dans toString();
            this.ReadFile(langue);
        }
        #endregion

        #region Méthodes
        public void ReadFile(string fichier)
        {
            StreamReader lecture = new StreamReader(fichier);
            string ligne = lecture.ReadLine();
            int nb_lignes = this.longueur_fichier; //Variable qui permet de lire le fichier de la bonne manière si il ne possède pas la bonne structure.
                                                   //càd si le fichier a une longueur de 29, cela implique qu'on a la ligne 29 juste le numéro "16" qui
                                                   //représente le nombre de caractère que possède le mot, mais 29 lignes implique qu'il n'y a pas de 
                                                   //liste de mots associés à ce "16". Ainsi, on ne doit pas le lire
            if (this.longueur_fichier % 2 != 0)
            {
                nb_lignes--; //On fait en sorte qu'on ne lise pas la ligne en trop en enlevant de 1 la taille du fichier
            }
            while (ligne != null)
            {
                int nb_caractere = 0;
                List<string> liste_caractere = new List<string>();
                //string[] tableau_caractere = ligne.Split(' ');
                bool probleme = false; //Variable permettant, si la ligne i du fichier pose problème quelle soit ignorée et ne provoque pas d'erreur dans l'execution du programme
                nb_ligne++;
                string[] liste = ligne.Split(' '); //On divise la ligne en plusieurs éléments grâce au séparateur ' '
                for (int i = 0; i <= 1; i++) //on commence à partir d'un numéro qui correspond aux nombres de caractère de la liste à la ligne suivante
                {
                    try
                    {
                        if (i % 2 == 0)
                        {
                            nb_caractere = Convert.ToInt32(ligne);
                        }
                        else
                        {
                            string[] tableau_caractere = ligne.Split(' ');
                            for (int index = 0; index < tableau_caractere.Length; index++)
                            {
                                liste_caractere.Add(tableau_caractere[index]);
                            }
                        }
                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine($"L'exception est : {exception.Message} et concernait la ligne {nb_ligne} du fichier {fichier}");
                        probleme = true;
                    }
                    ligne = lecture.ReadLine();
                }
                if (!probleme)
                {
                    listeMots_taille.Add(nb_caractere, liste_caractere);
                }
            }
        }

        public bool RechDico(string mot)
        {
            List<string> liste = listeMots_taille.ElementAt(listeMots_taille.IndexOfKey(mot.Length)).Value; //permet d'obtenir la liste correspondant à longueur du mot
            return liste.Contains(mot);
        }

        public string toString()
        {
            string langue_utilise = $"Le dictionnaire utilisé est ici : {langue}";
            string retour = "";
            for (int i = 0; i < listeMots_taille.Count; i++)
            {
                int int_key = listeMots_taille.ElementAt(i).Key;
                List<string> list_value = listeMots_taille.ElementAt(i).Value;
                retour += $"Nombre de mots d'une longueur de {int_key} : {list_value.Count}\n";
            }
            return retour;
        }

        //public string toString() code obsolète pour afficher tout le dictionnaire 
        //{
        //    string langue_utilise = $"Le dictionnaire utilisé est ici : {langue}";
        //    string retour = "";
        //    for (int i = 0; i < listeMots_taille.Count; i++)
        //    {
        //        int int_key = listeMots_taille.ElementAt(i).Key;
        //        List<string> list_value = listeMots_taille.ElementAt(i).Value;
        //        retour += $"Liste des mots d'une longueur de {int_key}\n";
        //        for (int index = 0; index < listeMots_taille.ElementAt(i).Value.Count; index++)
        //        {
        //            retour += $"{listeMots_taille.ElementAt(i).Value[index]} ";
        //        }
        //        retour += "\n";
        //    }
        //    return retour;
        //}

        /// <summary>
        /// Fonction qui permet de compter le nombre de ligne dans le fichier afin de déterminer le nombre de liste de mots avec une longueur
        /// i. Par exemple, si le fichier fait 28 lignes, on sait que d'après la structure du fichier "Langue.txt" qu'on aura 28/2 = 14 listes
        /// de mots de longueur différentes. Donc, cela veut dire que un fichier de 29 lignes (nombre impair) est impossible, car cela impliquerait
        /// qu'il ne respecte pas la structure du fichier "Langue.txt".
        /// </summary>
        public void compteLigne()
        {        
            StreamReader lecture = new StreamReader(langue);
            string ligne = lecture.ReadLine();
            while (ligne != null)
            {
                this.longueur_fichier++;
                ligne = lecture.ReadLine();
            }
            lecture.Close();
        }
        #endregion
    }
}

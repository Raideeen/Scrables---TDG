using System;

namespace Scrables___TDG
{
    class Jeu
    {
        static void Main(string[] args)
        {
            Sac_Jetons test = new Sac_Jetons("Jetons.txt");
            Joueur joueur_1 = new Joueur("Robert", test);
            Dictionnaire dico = new Dictionnaire("Francais.txt");
            joueur_1.Add_Mot("OOF");
            joueur_1.Add_Mot("ça fais mal ça...");


            Random r = new Random();
            int nombre_choisi = r.Next(0, joueur_1.Jeton_joueur.Count);
            Console.WriteLine(test.toString());
            Console.WriteLine();
            Console.WriteLine(test.Retire_Jeton().ToString());
            joueur_1.Jeton_joueur_ToString();
            //test.WriteFile("Fichier\\Jetons_Partie1.txt");
            //Sac_Jetons test1 = new Sac_Jetons("Fichier\\Jetons_Partie1.txt");
            //Console.WriteLine(test1.toString());

            //Console.WriteLine("--------------------");

            //Jeton ff = test.Retire_Jeton();
            //Console.WriteLine($"On essaye d'enlever le jeton : {ff.Nom_jeton}");
            //joueur_1.Add_main_Courante(ff);
            //Console.WriteLine(test.toString());
            //joueur_1.Jeton_joueur_ToString();
            //Console.WriteLine($"On retire un jeton de la main : {joueur_1.Jeton_joueur[nombre_choisi].Nom_jeton}");
            //joueur_1.Remove_Main_Courante(joueur_1.Jeton_joueur[nombre_choisi]);
            //joueur_1.Jeton_joueur_ToString();
            //joueur_1.Add_main_Courante(ff);
            //joueur_1.Jeton_joueur_ToString();

            //Console.WriteLine("--------------------");

            //Console.WriteLine(test.toString());

            Console.WriteLine(dico.toString());
            Console.WriteLine(dico.RechDico("test".ToUpper()));

        }
    }
}

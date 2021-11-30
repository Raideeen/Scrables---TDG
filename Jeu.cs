using System;

namespace Scrables___TDG
{
    class Jeu
    {
        static void Main(string[] args)
        {
            Sac_Jetons test = new Sac_Jetons("Jetons.txt");
            Joueur joueur_1 = new Joueur("Robert", test);
            joueur_1.Add_Mot("OOF");
            joueur_1.Add_Mot("ça fais mal ça...");



            //Console.WriteLine(test.toString());
            //Console.WriteLine();
            //Console.WriteLine(test.Retire_Jeton().ToString());
            //test.WriteFile("Fichier\\Jetons_Partie1.txt");
            //Sac_Jetons test1 = new Sac_Jetons("Fichier\\Jetons_Partie1.txt");
            //Console.WriteLine(test1.toString());
        }
    }
}

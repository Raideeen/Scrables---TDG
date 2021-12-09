using System;

namespace Scrables___TDG
{
    class Jeu
    {
        static void Main(string[] args)
        {
            #region test temporaire
            //Sac_Jetons test = new Sac_Jetons("Jetons.txt");
            //Joueur joueur_1 = new Joueur("Robert", test);
            //Dictionnaire dico = new Dictionnaire("Francais.txt");
            //joueur_1.Add_Mot("OOF");
            //joueur_1.Add_Mot("ça fais mal ça...");


            //Random r = new Random();
            //int nombre_choisi = r.Next(0, joueur_1.Jeton_joueur.Count);
            //Console.WriteLine(test.toString());
            //Console.WriteLine();
            //Console.WriteLine(test.Retire_Jeton().ToString());
            //joueur_1.Jeton_joueur_ToString();
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

            //Console.WriteLine(dico.toString());
            //Console.WriteLine(dico.RechDico("REELISAIT".ToUpper()));


            //// Get an array with the values of ConsoleColor enumeration members.
            //ConsoleColor[] colors = (ConsoleColor[])ConsoleColor.GetValues(typeof(ConsoleColor));
            //// Save the current background and foreground colors.
            //ConsoleColor currentBackground = Console.BackgroundColor;
            //ConsoleColor currentForeground = Console.ForegroundColor;

            //// Display all foreground colors except the one that matches the background.
            //Console.WriteLine("All the foreground colors except {0}, the background color:",
            //                  currentBackground);
            //foreach (var color in colors)
            //{
            //    if (color == currentBackground) continue;

            //    Console.ForegroundColor = color;
            //    Console.WriteLine("   The foreground color is {0}.", color);
            //}
            //Console.WriteLine();
            //// Restore the foreground color.
            //Console.ForegroundColor = currentForeground;

            //// Display each background color except the one that matches the current foreground color.
            //Console.WriteLine("All the background colors except {0}, the foreground color:",
            //                  currentForeground);
            //foreach (var color in colors)
            //{
            //    if (color == currentForeground) continue;

            //    Console.BackgroundColor = color;
            //    Console.WriteLine("   The background color is {0}.", color);
            //}

            //// Restore the original console colors.
            //Console.ResetColor();
            //Console.WriteLine("\nOriginal colors restored...");
            #endregion
            Dictionnaire dictionnaire = new Dictionnaire("Francais.txt");
            Sac_Jetons sac_jetons = new Sac_Jetons("Jetons.txt");
            Joueur Robert = new Joueur("Robert", sac_jetons, "test");
            Joueur[] joueurs = { Robert };
            Robert.Jeton_joueur_ToString();
            Plateau monplateau = new Plateau(dictionnaire, joueurs, "InstancePlateau.txt");
            //monplateau.AffichageMatriceStringBrut();
            monplateau.toStringCouleur();
            


            Console.WriteLine(monplateau.Test_Plateau("LUTER", 15, 3, 'h', Robert));

            Console.WriteLine(monplateau.Test_Plateau("VOLE", 1, 1, 'v', Robert));

            monplateau.toStringCouleur();
            monplateau.WriteFile("Instancetest.txt", false);
        }
    }
}

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
            #endregion
            Dictionnaire dictionnaire = new Dictionnaire("Francais.txt");
            Sac_Jetons sac_jetons = new Sac_Jetons("Jetons.txt");
            Joueur Robert = new Joueur("Robert", sac_jetons, "test");
            Joueur[] joueurs = { Robert };
            Robert.Jeton_joueur_ToString();
            Plateau monplateau = new Plateau(dictionnaire, joueurs, sac_jetons,"InstancePlateau.txt", "InstancePlateauScore.txt");
            //monplateau.AffichageMatriceStringBrut();
            monplateau.toStringCouleur();


            monplateau.AffichageMatriceStringBrut();
            Console.WriteLine(monplateau.Test_Plateau("LUTER", 15, 3, 'h', Robert));

            Console.WriteLine(monplateau.Test_Plateau("VOLE", 1, 1, 'v', Robert));
            
            monplateau.toStringCouleur();
            monplateau.WriteFile("Instancetest.txt","InstanceScoreTest.txt", false);

            //Rajouter un static List<string> qui récupère le nom de toutes les parties créée pour pouvoir sélectrionner les joueurs + l'instance du plateau correspondante à la bonne partie
            //Rajouter la possibilité de poser un joker. Changer dans le test_plateau.
            //Le joker peut-être automatiquement assigné à la position des lettres manquantes. Si imaginons pour luter il manque U E R, et que il possède 
            // E R dans sa main et un *, alors le programme demande "Il vous manque une lettre mais vous possédez un joker. Voulez-vous l'utiiser ?", si oui alors il complète directement à la position du U en consumant le *
        }


    }
}

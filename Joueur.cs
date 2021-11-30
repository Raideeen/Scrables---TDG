using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Scrables___TDG
{
    class Joueur
    {
        //Variable d'instance ou champ d'instance
        private string nom_joueur;
        private int score_joueur = 0;
        private Sac_Jetons sac_jeton;
        private List<string> mot_joueur = null;
        private List<string> jeton_joueur = new List<string>(7); //Les joueurs commencent avec une main courante de 7 jetons

        #region Constructeurs
        public Joueur(string nom_joueur, Sac_Jetons sac_jeton)
        {
            this.nom_joueur = nom_joueur;
            this.sac_jeton = sac_jeton;
            this.mot_joueur = new List<string>();
            for (int i = 0; i < 7; i++)
            {
                Jeton jeton_considere = sac_jeton.Retire_Jeton();
                jeton_joueur.Add(jeton_considere.Nom_jeton);
            }
            this.WriteFile($"{nom_joueur}.txt");
        }
        #endregion

        #region Propriétés
        #endregion

        #region Méthodes

        public void WriteFile(string fichier)
        {
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
                    if (i == mot_joueur.Count - 1) writer.Write($"{mot_joueur[i]}");
                    else writer.Write($"{mot_joueur[i]};");
                }
            }
            writer.WriteLine();
            for (int i = 0; i < jeton_joueur.Count; i++)
            {
                if (i == jeton_joueur.Count - 1) writer.Write($"{jeton_joueur[i]}");
                else writer.Write($"{jeton_joueur[i]};");
            }
            writer.Close();
        }
        public void Add_Mot(string mot)
        {
            mot_joueur.Add(mot);
            this.WriteFile($"{this.nom_joueur}.txt");
        }
        #endregion
    }
}

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
        private Joueur[] joueurs;
        private List<string> Nom_Parties = new List<string>();

        #region Constructeurs
        public Jeu(Dictionnaire mondico, Plateau monplateau, Sac_Jetons sac_jetons)
        {
            this.mondico = mondico + ".txt";

        }
        #endregion

        static void Main(string[] args)
        {
            
        }


    }
}

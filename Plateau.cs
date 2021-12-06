using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scrables___TDG
{
    class Plateau
    {
        //Variable d'instance ou champ d'instance
        private Dictionnaire mondico;
        private int[,] matrice_jeu = new int[15,15];

        int[,] matrice_jeuf = {{ 31,0,0,0,0,0,0,31,0,0,0,0,0,0,31},
                               { 0,30,0,0,0,0,0,0,0,0,0,0,0,30,0},
                               { 0,0,30,0,0,0,0,0,0,0,0,0,30,0,0},
                               { 0,0,0,30,0,0,0,0,0,0,0,30,0,0,0},
                               { 0,0,0,0,30,0,0,0,0,0,30,0,0,0,0},
                               { 0,0,0,0,0,29,0,0,0,29,0,0,0,0,0},
                               { 0,0,0,0,0,0,28,0,28,0,0,0,0,0,0}, //On commence là 
                               { 31,0,0,0,0,0,0,32,0,0,0,0,0,0,31},
                               { 0,0,0,0,0,0,28,0,28,0,0,0,0,0,0},
                               { 0,0,0,0,0,29,0,0,0,29,0,0,0,0,0},
                               { 0,0,0,0,30,0,0,0,0,0,30,0,0,0,0},
                               { 28,0,0,30,0,0,0,28,0,0,0,30,0,0,28},
                               { 0,0,30,0,0,0,28,0,28,0,0,0,30,0,0},
                               { 0,30,0,0,0,29,0,0,0,29,29,0,0,30,0},
                               { 31,0,0,28,0,0,0,31,0,0,0,28,0,0,31}};
        public Plateau(int[,] matrice_jeuf)
        {
            this.matrice_jeu = matrice_jeuf;
            
        }
        
    }
}

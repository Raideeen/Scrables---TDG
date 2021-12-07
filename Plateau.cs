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
        private bool nouvelle_partie;

        int[,] matrice_jeu_initiale = 
                               {
                               { 31,0,0,0,0,0,0,31,0,0,0,0,0,0,31},
                               { 0,30,0,0,0,0,0,0,0,0,0,0,0,30,0},
                               { 0,0,30,0,0,0,0,0,0,0,0,0,30,0,0},
                               { 0,0,0,30,0,0,0,0,0,0,0,30,0,0,0},
                               { 0,0,0,0,30,0,0,0,0,0,30,0,0,0,0},
                               { 0,0,0,0,0,29,0,0,0,29,0,0,0,0,0},
                               { 0,0,0,0,0,0,28,0,28,0,0,0,0,0,0}, 
                               { 31,0,0,0,0,0,0,32,0,0,0,0,0,0,31}, //On commence là 
                               { 0,0,0,0,0,0,28,0,28,0,0,0,0,0,0},
                               { 0,0,0,0,0,29,0,0,0,29,0,0,0,0,0},
                               { 0,0,0,0,30,0,0,0,0,0,30,0,0,0,0},
                               { 28,0,0,30,0,0,0,28,0,0,0,30,0,0,28},
                               { 0,0,30,0,0,0,28,0,28,0,0,0,30,0,0},
                               { 0,30,0,0,0,29,0,0,0,29,29,0,0,30,0},
                               { 31,0,0,28,0,0,0,31,0,0,0,28,0,0,31}};
        //public Plateau(bool nouvelle_partie, int[,] matrice_jeu_initiale, )
        //{
            
        //}
        
    }
}

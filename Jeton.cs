using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Scrables___TDG
{
    class Jeton
    {
        //Variable d'instance ou champ d'instance
        private string nom_jeton;
        private int valeur_jeton;
        private int nombre_jeton;
        

        #region Constructeurs
        public Jeton(string nom_jeton, int valeur_jeton, int nombre_jeton)
        {
            this.nom_jeton = nom_jeton;
            this.valeur_jeton = valeur_jeton;
            this.nombre_jeton = nombre_jeton;
        }
        #endregion

        #region Propriétés
        public string Nom_jeton
        {
            get { return this.nom_jeton; }
        }
        public int Valeur_jeton
        {
            get { return this.valeur_jeton; }
        }
        public int Nombre_jeton
        {
            get { return this.nombre_jeton; }
            set { this.nombre_jeton = value; }
        }
        #endregion

        #region Méthodes
        public string ToString()
        {
            string descriptif = "";
            if (this.nom_jeton == "*")
            {
                descriptif = $"Lettre: Joker Valeur: {this.valeur_jeton} Nombre: {this.nombre_jeton}\n";
            }
            else
            {
                descriptif = $"Lettre: {this.nom_jeton} Valeur: {this.valeur_jeton} Nombre: {this.nombre_jeton}\n";
            }
            return descriptif;
        }
        #endregion
    }
}

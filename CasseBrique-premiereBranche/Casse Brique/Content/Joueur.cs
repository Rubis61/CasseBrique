using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Casse_Brique.Content
{
    public class Joueur
    {
        public int Score { get; set; }
        public int NbrLife { get; set; }
        public string Pseudo { get; set; }

        public Joueur(string name)
        {
            Score = 0;
            NbrLife = 3;
            Pseudo = name;
        }
        
        public int CalculScoreBrique(Brique brique)
        {
            switch (brique.GetType().Name)
            {
                case "BriqueNormale"   : Score += 1;
                    break;
                case "BriqueDoubleCoup": Score += 5;
                    break;
                case "BriqueTripleCoup": Score += 10;
                    break;
            };
            return Score;
        }

        public int CalculScoreBonus(Bonus bonus)
        {
            switch (bonus.TypeBonus)
            {
                case TypeBonus.VitesseBalleRéduite   : Score += 10;
                    break;
                case TypeBonus.VitesseBalleAugmentée : Score += 10;
                    break;
                case TypeBonus.RaquetteAgrandie      : Score += 5;
                    break;
                case TypeBonus.RaquetteReduite       : Score += 5;
                    break;
                case TypeBonus.BalleInvincible       : Score += 20;
                    break;
                case TypeBonus.VieSupplementaire     : Score += 50;
                    break;
            };
            return Score;
        }

        public void EnleverUneVie()
        {
            NbrLife--;
            if (NbrLife < 0)
            {
                
            }
        }
    }
}

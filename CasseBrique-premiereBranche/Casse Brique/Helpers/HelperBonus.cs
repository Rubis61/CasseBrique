using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Casse_Brique.Content;

namespace Casse_Brique.Helpers
{
    public static class HelperBonus
    {
        private static Random rdm = new Random(DateTime.Now.Millisecond);
        public static List<Bonus> ListBonus = new List<Bonus>();
        public static Dictionary<string, int> ListBonusDict = new Dictionary<string, int>();
        private static int vitesseBonus = 2;

        public static void InitializeListBonus(Game1 game)
        {
            ListBonus.Add(new Bonus(game, vitesseBonus, 0, 1, TypeBonus.RaquetteAgrandie, 30));
            ListBonus.Add(new Bonus(game, vitesseBonus, 0, 1, TypeBonus.RaquetteReduite, 30));
            ListBonus.Add(new Bonus(game, vitesseBonus, 0, 1, TypeBonus.VitesseBalleAugmentée, 30));
            ListBonus.Add(new Bonus(game, vitesseBonus, 0, 1, TypeBonus.VitesseBalleRéduite, 30));
            ListBonus.Add(new Bonus(game, vitesseBonus, 0, 1, TypeBonus.BalleInvincible, 5));
            ListBonus.Add(new Bonus(game, vitesseBonus, 0, 1, TypeBonus.VieSupplementaire, 10));
            /*ListBonusDict.Add("++", 60);
            ListBonusDict.Add("+", 60);
            ListBonusDict.Add("--", 60);
            ListBonusDict.Add("-", 60);*/
        }

        public static Bonus GénérerBonusAléa()
        {
            return GénérerBonusAléa(ListBonus);
        }

        public static Bonus GénérerBonusAléa(List<Bonus> ListBonus)
        {
            List<Bonus> ListBonusAvecMemePourcentage;

            int chance = GénérerChance();

            ListBonusAvecMemePourcentage = GetAllBonusWithPourcentage(ListBonus, chance);

            if( ListBonusAvecMemePourcentage.Count == 0 ) return new Bonus(null, 0, 0, 0, TypeBonus.Aucun, 101);

            return ListBonusAvecMemePourcentage[rdm.Next(ListBonusAvecMemePourcentage.Count - 1)];
        }

        private static int GénérerChance()
        {
            return rdm.Next(100);
        }

        private static List<Bonus> GetAllBonusWithPourcentage(List<Bonus> ListBonus, int pourcentage)
        {
            List<Bonus> ListBonusAvecPourcentage = new List<Bonus>();
            foreach (var bonus in ListBonus)
	        {
                if(pourcentage < bonus.Pourcentage)
                {
                    ListBonusAvecPourcentage.Add(bonus);
                }
	        }

            return ListBonusAvecPourcentage;// ListBonus.Where(brique => pourcentage < brique.Pourcentage).ToList();
        }
    }
}
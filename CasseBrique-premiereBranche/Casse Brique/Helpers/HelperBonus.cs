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

        public static Bonus GénérerBonusAléa() { return GénérerBonusAléa(ListBonus); }

        public static Bonus GénérerBonusAléa(List<Bonus> ListBonus)
        {
            List<Bonus> ListBonusAvecMemePourcentage;

            int chance = GénérerChance();

            ListBonusAvecMemePourcentage = GetAllBonusWithPourcentage(ListBonus, chance);

            return ListBonusAvecMemePourcentage[rdm.Next(ListBonusAvecMemePourcentage.Count - 1)];
        }

        private static int GénérerChance()
        {
            return rdm.Next(100);
        }

        private static List<Bonus> GetAllBonusWithPourcentage(List<Bonus> ListBonus, int pourcentage)
        {
            return ListBonus.Where(brique => pourcentage < brique.Pourcentage).ToList();
        }
    }
}

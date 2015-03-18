using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Casse_Brique
{
    class BriqueInvisible : Brique
    {
        public BriqueInvisible(Game1 game, int vitesse, float dirX, float dirY)
        : base(game, vitesse, dirX, dirY)
        {
            IsInfinite = false;
            isActive = true;
            Vie = 1;
        }

    }
}

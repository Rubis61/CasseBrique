using Casse_Brique;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Casse_Brique
{
    class BriqueDoubleCoup : Brique
    {
        public BriqueDoubleCoup(Game1 game, int vitesse, float dirX, float dirY)
            : base(game, vitesse, dirX, dirY)
        {
            IsInfinite = false;
            Vie = 2;
        }
    }
}

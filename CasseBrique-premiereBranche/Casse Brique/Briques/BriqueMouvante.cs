using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Casse_Brique
{
    class BriqueMouvante : Brique
    {
        public BriqueMouvante(Game1 game, int vitesse, float dirX, float dirY)
        : base(game, vitesse, dirX, dirY)
        {
            IsInfinite = false;
            Vie = 1;
        }
    }
}

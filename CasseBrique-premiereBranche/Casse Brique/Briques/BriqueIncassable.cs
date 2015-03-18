using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;

namespace Casse_Brique
{
    class BriqueIncassable : Brique
    {
        public BriqueIncassable(Game1 game, int vitesse, float dirX, float dirY)
            : base(game, vitesse, dirX, dirY)
        {
            IsInfinite = true;
            Vie = 1;
        }
    }
}
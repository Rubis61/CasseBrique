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
    class BriqueVide : Brique
    {
        public BriqueVide(Game1 game, int vitesse, int dirX, int dirY)
            : base(game, vitesse, dirX, dirY)
        {
            IsInfinite = false;
            Vie = 1;
            isActive = false;
            _texture = null;
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gametime)
        {
            
        }
    }
}

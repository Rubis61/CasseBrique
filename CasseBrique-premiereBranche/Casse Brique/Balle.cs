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
    public class Balle : GameObject
    {
        public Balle(Game1 game, int vitesse, int dirX, int dirY)
            : base(game, vitesse, dirX, dirY)
        {

        }

        public override void Update(GameTime gametime, KeyboardState keyboardState)
        {
            int width = game.getWidth();
            int height = game.getHeight();

            if (_rectangle.X <= 0)
            {
                _dirX = 1;
            }
            else if ((_rectangle.X + _rectangle.Width >= width))
            {
                _dirX = -1;
            }
            if ( _rectangle.Y <= 0 )
            {
                _dirY = 1;
            }
            else if ((_rectangle.Y + _rectangle.Height) >= height)
            {
                _dirY = -1;
            }

            if (game.Raquette.getRectangle().Intersects(_rectangle))
            {
                game.Log = "Intersection !!";
                _dirY *= -1;
            }
            else game.Log = "";

            Rectangle collideWithBrique = game.murDeBrique.isCollisionWithBrique(_rectangle);

            if( !collideWithBrique.IsEmpty )
            {
                Vector2 milieuBalle = new Vector2(_rectangle.X + _rectangle.Width / 2, _rectangle.Y + _rectangle.Height / 2);

                if( milieuBalle.X < collideWithBrique.X )
                {
                    _dirX = -1;
                }
                else if( milieuBalle.X > collideWithBrique.X )
                {
                    _dirX = 1;
                }
                else if( milieuBalle.Y > collideWithBrique.Y )
                {
                    _dirY = 1;
                }
                else if( milieuBalle.Y < collideWithBrique.Y )
                {
                    _dirY = -1;
                }
            }

            base.Update(gametime, keyboardState);
        }
    }
}

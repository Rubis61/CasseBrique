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
    public class Raquette : GameObject
    {
        public Raquette(Game1 game, int vitesse, float dirX, float dirY)
            : base(game, vitesse, dirX, dirY)
        {
            
        }

        public override void Update(GameTime gametime, KeyboardState keyboardState)
        {
            bool leftDown = keyboardState.IsKeyDown(Keys.Left);
            bool rightDown = keyboardState.IsKeyDown(Keys.Right);
            int width = game.getWidth();

            if (leftDown && _rectangle.X > 0)
            {
                Direction.X = -1;
            }
            else if (rightDown && (_rectangle.X + _rectangle.Width < width))
            {
                Direction.X = 1;
            }
            else
            {
                Direction.X = 0;
            }

            base.Update(gametime, keyboardState);
        }

        public override void LoadContent(ContentManager content, string nom)
        {
            _texture = game.Content.Load<Texture2D>("barre");
        }
    }
}

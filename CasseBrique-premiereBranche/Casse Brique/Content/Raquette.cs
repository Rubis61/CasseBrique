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
        public static float MaxScale = 1.9f;
        public static float MinScale = 1;

        public Raquette(Game1 game, int vitesse, float dirX, float dirY)
            : base(game, vitesse, dirX, dirY)
        {
            Scale = new Vector2(1f);
        }

        public override void Update(GameTime gametime, KeyboardState keyboardState)
        {
            bool leftDown = keyboardState.IsKeyDown(Keys.Left);
            bool rightDown = keyboardState.IsKeyDown(Keys.Right);
            int width = game.getWidth();

            if (leftDown && Position.X > 0)
            {
                Direction.X = -1;
            }
            else if (rightDown && (Position.X + _rectangle.Width < width))
            {
                Direction.X = 1;
            }
            else
            {
                Direction.X = 0;
            }

            base.Update(gametime, keyboardState);
        }

        public void Agrandir()
        {
            Initialize(game.Raquette.Position.X, game.Raquette.Position.Y, game.Raquette.getRectangle().Width + 39, 28);
            //Initialize(_rectangle.X - (17 / 2), _rectangle.Y, (int)(_rectangle.Width * 1.1), 28);
            Position.X -= 19.5f;
            Scale.X = (float)Math.Round(Scale.X += 0.3f, 2);
        }

        public void Reduire()
        {
            Initialize(game.Raquette.Position.X, game.Raquette.Position.Y, game.Raquette.getRectangle().Width - 39, 28);
            //Initialize(_rectangle.X - (17 / 2), _rectangle.Y, (int)(_rectangle.Width / 1.1), 28);
            Position.X += 19.5f;
            Scale.X = (float)Math.Round(Scale.X -= 0.3f, 2);
        }

        public override void LoadContent(ContentManager content, string nom)
        {
            _texture = game.Content.Load<Texture2D>(nom);
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gametime)
        {
            spriteBatch.Draw(_texture, Position, null, Color.White, 0f, Vector2.Zero, Scale, SpriteEffects.None, 0f);
        }
    }
}
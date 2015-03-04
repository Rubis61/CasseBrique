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
using Casse_Brique.Content;

namespace Casse_Brique
{
    public class Brique : GameObject
    {
        public bool IsInfinite { get; set; }
        public int Vie { get; set; }
        public bool isActive { get; set; }
        public Bonus Bonus { get; set; }

        public Brique(Game1 game, int vitesse, float dirX, float dirY)
            : base(game, vitesse, dirX, dirY)
        {
            IsInfinite = false;
            Vie = 1;
            isActive = true;
        }

        public override void LoadContent(ContentManager content, string nom)
        {
            _texture = game.Content.Load<Texture2D>("kinder_bueno");
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gametime)
        {
            spriteBatch.Draw(_texture, Position, null, Color.White, 0f, Vector2.Zero, new Vector2(0.25f), SpriteEffects.None, 0f);
        }
    }
}
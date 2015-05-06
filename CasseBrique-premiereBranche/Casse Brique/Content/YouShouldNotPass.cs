using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace Casse_Brique.Content
{
    public class YouShouldNotPass : GameObject
    {
        public bool IsActif { get; set; }
        public TimeSpan TotalTime { get; set; }
        private TimeSpan timeElapsed;

        public YouShouldNotPass(Game1 game, TimeSpan totalTime)
            : base(game, 0, 0, 0)
        {
            IsActif = false;
            TotalTime = totalTime;
            timeElapsed = TimeSpan.Zero;
            Scale = new Vector2(2.05f, 0.5f);
        }

        public override void Update(GameTime gametime, KeyboardState keyboardState)
        {
            if (!IsActif) return;

            timeElapsed += gametime.ElapsedGameTime;

            if( timeElapsed > TotalTime )
            {
                IsActif = false;
                timeElapsed = TimeSpan.Zero;
            }
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gametime)
        {
            if (IsActif)
                spriteBatch.Draw(_texture, Position, new Rectangle(0, 0, game.getWidth(), _texture.Height), 
                                 Color.White, 0f, Vector2.Zero, Scale, SpriteEffects.None, 0f);
            //base.Draw(spriteBatch, gametime);
        }
    }
}
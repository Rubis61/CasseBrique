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

namespace Casse_Brique.Content
{
    public class Background
    {
        public Rectangle Rectangle { get; set; }
        public Texture2D Texture { get; set; }

        public Background(Game1 game)
        {
            Rectangle = new Rectangle(0, 0, game.getWidth(), game.getHeight());
        }

        public virtual void LoadContent(ContentManager content, string nom)
        {
            Texture = content.Load<Texture2D>(nom);
        }

        public virtual void unLoadContent()
        {
            Texture.Dispose();
        }

        public virtual void Draw(SpriteBatch spriteBatch, GameTime gametime)
        {
            //spriteBatch.Draw(_texture, new Rectangle((int)Position.X, (int)Position.Y, _rectangle.Width, _rectangle.Height), Color.White);
            spriteBatch.Draw(Texture, Rectangle, Color.White);
        }
    }
}
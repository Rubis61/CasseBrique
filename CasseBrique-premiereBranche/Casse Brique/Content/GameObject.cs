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
    public class GameObject
    {
        protected Rectangle _rectangle;
        protected Texture2D _texture;
        protected int _dirX;
        protected int _dirY;
        protected int vitesse;
        protected Game1 game;

        public string getPosition()
        {
            return " X : " + _rectangle.X + " Y : " + _rectangle.Y;
        }

        public Rectangle getRectangle() { return _rectangle; }
        public Texture2D getTexture() { return _texture; }

        public GameObject(Game1 game, int vitesse, int dirX, int dirY)
        {
            this.game = game;
            this.vitesse = vitesse;
            this._dirX = dirX;
            this._dirY = dirY;
        }

        public virtual void Initialize(int posX, int posY, int width, int height )
        {
            _rectangle = new Rectangle(posX, posY, width, height);
        }

        public void LoadContent(ContentManager content, string nom)
        {
            if( _texture != null ) _texture = content.Load<Texture2D>(nom);
        }

        public void unLoadContent()
        {
            _texture.Dispose();
            //_texture = null;
        }

        public virtual void Update(GameTime gametime, KeyboardState keyboardState)
        {
            _rectangle.X += vitesse * _dirX;
            _rectangle.Y += vitesse * _dirY;
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gametime)
        {
            spriteBatch.Draw(_texture, _rectangle, Color.White);
        }
    }
}

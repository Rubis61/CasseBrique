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
        protected float dirX;
        protected int dirY;
        //protected int vitesse;
        //protected Vector2 Position;
        protected Vector2 Direction;
        protected Game1 game;

        public float Vitesse { get; set; }

        public string getPosition()
        {
            return " X : " + _rectangle.X + " Y : " + _rectangle.Y;
        }

        public Vector2 getDirection() { return Direction; }

        public Rectangle getRectangle() { return _rectangle; }
        public Texture2D getTexture() { return _texture; }

        public GameObject(Game1 game, float vitesse, float dirX, float dirY)
        {
            this.game = game;
            Vitesse = vitesse;
            Direction = new Vector2(dirX, dirY);
            //this.__rectangle.X = _rectangle.X;
            //this.__rectangle.Y = _rectangle.Y;
        }

        public virtual void Initialize(int posX, int posY, int width, int height )
        {
            _rectangle = new Rectangle(posX, posY, width, height);
            //Position = new Vector2(posX, posY);
        }

        public virtual void LoadContent(ContentManager content, string nom)
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
            //_rectangle.X += (int)(vitesse * __rectangle.X);
            //Position += Vitesse * Direction;
            _rectangle.X += (int)(Vitesse * Direction.X);
            _rectangle.Y += (int)(Vitesse * Direction.Y);
            //_rectangle.Y += vitesse * __rectangle.Y;
        }

        public virtual void Draw(SpriteBatch spriteBatch, GameTime gametime)
        {
            spriteBatch.Draw(_texture, _rectangle, Color.White);
        }
    }
}
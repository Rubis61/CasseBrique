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
        public Rectangle _rectangle;
        public Vector2 Position;
        protected Texture2D _texture;
        //protected float dirX;
        //protected int dirY;
        //protected int vitesse;
        //protected Vector2 Position;
        public Vector2 Direction;
        protected Game1 game;
        public Vector2 Scale;

        public float Vitesse { get; set; }

        public string getPosition()
        {
            return " X : " + Position.X + " Y : " + Position.Y;
        }
        

        public Vector2 getDirection() { return Direction; }
        public float GetWidth() { return _texture.Width * Scale.X; }
        public float GetHeight() { return _texture.Height * Scale.Y; }

        public Rectangle getRectangle() { return new Rectangle((int)Position.X, (int)Position.Y, _rectangle.Width, _rectangle.Height); }
        public Texture2D getTexture() { return _texture; }

        public GameObject(Game1 game, float vitesse, float dirX, float dirY)
        {
            this.game = game;
            Vitesse = vitesse;
            Direction = new Vector2(dirX, dirY);
            //this.__rectangle.X = _rectangle.X;
            //this.__rectangle.Y = _rectangle.Y;
        }

        public virtual void Initialize(float posX, float posY, int width, int height )
        {
            _rectangle = new Rectangle((int)posX, (int)posY, width, height);
            Position = new Vector2(posX, posY);
            //Position = new Vector2(posX, posY);
        }

        public virtual void LoadContent(ContentManager content, string nom)
        {
            //if( _texture != null ) 
            _texture = content.Load<Texture2D>(nom);
        }

        public void unLoadContent()
        {
            _texture.Dispose();
            //_texture = null;
        }

        public virtual void Update(GameTime gametime, KeyboardState keyboardState)
        {
            _rectangle.X += (int)(Vitesse * Direction.X);
            _rectangle.Y += (int)(Vitesse * Direction.Y);
            Position.X += Vitesse * Direction.X;
            Position.Y += Vitesse * Direction.Y;
        }

        public virtual void Draw(SpriteBatch spriteBatch, GameTime gametime)
        {
            //spriteBatch.Draw(_texture, new Rectangle((int)Position.X, (int)Position.Y, _rectangle.Width, _rectangle.Height), Color.White);
            spriteBatch.Draw(_texture, Position, null, Color.White, 0f, Vector2.Zero, new Vector2(0.2f), SpriteEffects.None, 0f);
        }
    }
}
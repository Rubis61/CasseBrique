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
        public bool colisionWithRaquette { get; set; }
        public bool Aimanté { get; set; }
        public int MaxSpeed { get; set; }
        public int MinSpeed { get; set; }
        private float Boost = 1;
        private Vector2 normal;
        public bool IsInvincible { get; set; }

        public Balle(Game1 game, int vitesse, float dirX, float dirY)
            : base(game, vitesse, dirX, dirY)
        {
            MaxSpeed = 7;
            MinSpeed = 2;
            Boost = 1;
            Aimanté = false;
            IsInvincible = false;
        }

        public override void Update(GameTime gametime, KeyboardState keyboardState)
        {
            int width = game.getWidth();
            int height = game.getHeight();

            collideWithScreen(width, height);

            Rectangle futurePosition = new Rectangle(_rectangle.X, _rectangle.Y, _rectangle.Width, _rectangle.Height);
            for (int i = 1; i <= Vitesse; i++)
            {
                futurePosition.X += (int)(Vitesse * Direction.X);
                futurePosition.Y += (int)(Vitesse * Direction.Y);
                if (collideWithBriques(futurePosition)) break;
                else futurePosition = new Rectangle(_rectangle.X, _rectangle.Y, _rectangle.Width, _rectangle.Height); ;
            }

            //Direction.X *= Math.Abs(collideWithRaquette());
            collideWithRaquette();
            Boost = Math.Abs(Boost);

            //Direction.X = (Direction.X > 2) ? 2 : (Direction.X < -2) ? -2 : Direction.X;
            if(Aimanté == true && colisionWithRaquette == true)
            {
                Vitesse = 0;
                Position.X += game.Raquette.Vitesse * game.Raquette.getDirection().X;
                Position.Y = game.Raquette.Position.Y - _texture.Height + 1;
                _rectangle.X = (int)Position.X;
                _rectangle.Y = (int)Position.Y;
                Direction.Y = -1;
                //Position.X = game.Raquette.Position.X/2 - _rectangle.Width/2;
              //  Position.Y = game.Raquette.Position.Y - Game1.ESPACE_BALLE_RAQUETTE_INIT;
            }
            //Direction.X = Direction.X * Boost;
            //Direction.Y = Direction.Y * Boost;

            //Direction.X = (Direction.X > 2) ? 2 : (Direction.X < -2) ? -2 : Direction.X;
            //Direction.Y = (Direction.Y > 2) ? 2 : (Direction.Y < -2) ? -2 : Direction.Y;

            //_rectangle.X += (int)normal.X;
            //_rectangle.Y += (int)normal.Y;
            //Position.Y += (int)normal.Y;
            //Position.Y += (int)normal.Y;

            _rectangle.X = (int)Position.X;
            _rectangle.Y = (int)Position.Y;

            base.Update(gametime, keyboardState);

            //Position.X += normal.X;
            //_rectangle.X = (int)Position.X;
            //_rectangle.Y = (int)Position.Y;
        }

        private void collideWithScreen(int width, int height)
        {
            // Intersection avec les bords de l'écran
            if (Position.X <= 0) // bord gauche
            {
                Direction.X = 1;
            }
            else if ((Position.X + _rectangle.Width >= width)) // bord droit
            {
                Direction.X = -1;
            }
            if (Position.Y <= 0) // bord haut
            {
                Direction.Y = 1;
            }
            else if ((Position.Y + _rectangle.Height) >= height) // bord bas
            {
                Direction.Y = -1;
            }
        }

        public float collideWithRaquette()
        {
            //float boost = 1;

            if (game.Raquette.getRectangle().Intersects(_rectangle)) // Détection collision entre la balle et la raquette
            {
                Rectangle rectCollider = new Rectangle();
                rectCollider = Rectangle.Intersect(game.Raquette.getRectangle(), _rectangle);
                //Position.Y -= rectCollider.Height;

                colisionWithRaquette = true;
                game.Log = "Intersection !!";

                Raquette raquette = game.Raquette;
                Rectangle posRaquette = raquette.getRectangle();
                Texture2D textureRaquette = raquette.getTexture();

                /*
                int milieuRaquette_X = (posRaquette.X + textureRaquette.Width) / 2;
                int distanceEntrePostionDuBoostEtLeMilieuDeLaRaquette_X = textureRaquette.Width * 1 / 3;
                bool boostGauche = _rectangle.X <= (milieuRaquette_X - distanceEntrePostionDuBoostEtLeMilieuDeLaRaquette_X);
                bool boostDroit = _rectangle.X >= (textureRaquette.Width - distanceEntrePostionDuBoostEtLeMilieuDeLaRaquette_X);
                */

                float distanceDroiteGauche = raquette.getRectangle().Width;
                //float positionXballe = Position.X - (raquette.getRectangle().X - raquette.getRectangle().Width / 2);
                float positionXballe = Position.X - raquette.getRectangle().X;
                float pourcentage = positionXballe / distanceDroiteGauche;

                if (pourcentage < 0.33f)
                {
                    //normal = new Vector2(-0.196f, -0.981f);
                    normal = new Vector2(-0.6f, -0.981f);
                }
                else if (pourcentage > 0.66f)
                {
                    //normal = new Vector2(0.196f, -0.981f);
                    normal = new Vector2(0.6f, -0.981f);
                }
                else normal = new Vector2(1, 1);
                /*
                if (boostGauche)
                {
                    Boost = (Direction.X < 0) ? -2f : -1;
                }
                else if (boostDroit) 
                    Boost = (Direction.X > 0) ? 2f : 1;
                else 
                    Boost = 1;
                */
                Direction.Y *= -1;
            }
            else
            {
                game.Log = "";
                colisionWithRaquette = false;
            }

            return Boost;
        }

        private bool collideWithBriques(Rectangle rectangle)
        {
            Brique brique;
            Rectangle collideWithBrique = game.murDeBrique.isCollisionWithBrique(rectangle, out brique); // demande si il y a collision entre balle et une des briques du mur de brique ( rectangle null si aucune collision )

            if (!collideWithBrique.IsEmpty) // Collision entre la balle et une brique OK
            {
                // Si balle invincible return
                 if (IsInvincible && !brique.IsInfinite) return true;

                //Vector2 milieuBalle = new Vector2(Position.X + _rectangle.Width / 2, Position.Y + _rectangle.Height / 2);

                if (Direction.X == 1) // Direction de la balle vers la droite
                {
                    if (Direction.Y == -1) // La balle monte
                    {
                        if (collideWithBrique.Width < collideWithBrique.Height) // Collision bord gauche de la brique
                        {
                            Direction.X = -1; // projection vers la gauche
                            Position.X -= collideWithBrique.Width;
                        }
                        else if (collideWithBrique.Width > collideWithBrique.Height) // Collision bord bas
                        {
                            Direction.Y = 1; // projection vers le bas
                            //Position.Y = collideWithBrique.Y + collideWithBrique.Height;
                            Position.Y += collideWithBrique.Height;
                        }
                        else
                        {
                            //Direction.X = -1; // projection vers la gauche
                            Direction.Y = 1; // projection vers le bas
                            //Position.X -= collideWithBrique.Width;
                            Position.Y += collideWithBrique.Height;
                        }
                    }
                    else // La balle descend
                    {
                        if (collideWithBrique.Width > collideWithBrique.Height) // Collision bord haut
                        {
                            Direction.Y = -1; // projection vers le haut
                            //Position.Y = collideWithBrique.Y - GetHeight();
                            Position.Y -= collideWithBrique.Height;
                        }
                        else if (collideWithBrique.Width < collideWithBrique.Height) // Collision bord gauche
                        {
                            Direction.X = -1; // projection vers la gauche
                            //Position.X = collideWithBrique.X - GetWidth();
                            Position.X -= collideWithBrique.Width;
                        }
                        else
                        {
                            Direction.Y = -1; // projection vers le haut
                            //Direction.X = -1; // projection vers la gauche
                            Position.Y -= collideWithBrique.Height;
                            //Position.X -= collideWithBrique.Width;
                        }
                    }
                }
                else // Direction de la balle vers la gauche
                {
                    if (Direction.Y == -1) // La balle monte
                    {
                        if (collideWithBrique.Width < collideWithBrique.Height) // Collision bord droit
                        {
                            Direction.X = 1; // projection vers la droite
                            //Position.X = brique.Position.X + brique.GetWidth();
                            Position.X += collideWithBrique.Width;
                        }
                        else if (collideWithBrique.Width > collideWithBrique.Height) // Collision bord bas
                        {
                            Direction.Y = 1; // projection vers le bas
                            //Position.Y = brique.Position.Y + brique.GetHeight();
                            Position.Y += collideWithBrique.Height;
                        }
                        else
                        {
                            //Direction.X = 1; // projection vers la droite
                            Direction.Y = 1; // projection vers le bas
                            //Position.X += collideWithBrique.Width;
                            Position.Y += collideWithBrique.Height;
                        }
                    }
                    else // La balle descend
                    {
                        if (collideWithBrique.Width > collideWithBrique.Height) // Collision bord haut
                        {
                            Direction.Y = -1; // projection vers le haut
                            //Position.Y = brique.Position.Y - brique.GetHeight();
                            Position.Y -= collideWithBrique.Height;
                        }
                        else if (collideWithBrique.Width < collideWithBrique.Height)//collideWithBrique.Y < milieuBalle.Y) // Collision bord droit
                        {
                            Direction.X = 1; // projection vers la droite
                            //Position.X = brique.Position.X + brique.GetWidth();
                            Position.X += collideWithBrique.Width;
                        }
                        else
                        {
                            Direction.Y = -1; // projection vers le haut
                            //Direction.X = 1; // projection vers la droite
                            Position.Y -= collideWithBrique.Height;
                            //Position.X += collideWithBrique.Width;
                        }
                    }
                }

                return true;
            }

            return false;
        }

        public override void LoadContent(ContentManager content, string nom)
        {
            _texture = game.Content.Load<Texture2D>(nom);
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gametime)
        {
            spriteBatch.Draw(_texture, Position, null, Color.White, 0f, Vector2.Zero, new Vector2(0.75f), SpriteEffects.None, 0f);
        }
    }
}
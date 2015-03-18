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
        private float Boost = 1;
        private Vector2 normal;
        public bool IsInvincible { get; set; }

        public Balle(Game1 game, int vitesse, float dirX, float dirY)
            : base(game, vitesse, dirX, dirY)
        {
            Boost = 1;
            Aimanté = false;
            IsInvincible = false;
        }

        public override void Update(GameTime gametime, KeyboardState keyboardState)
        {
            int width = game.getWidth();
            int height = game.getHeight();

            collideWithScreen(width, height);

            collideWithBriques();

            //Direction.X *= Math.Abs(collideWithRaquette());
            collideWithRaquette();
            Boost = Math.Abs(Boost);

            Direction.X = (Direction.X > 2) ? 2 : (Direction.X < -2) ? -2 : Direction.X;
            if(Aimanté == true && colisionWithRaquette == true)
            {
                Vitesse = 0;
                _rectangle.X += (int)(game.Raquette.Vitesse * game.Raquette.getDirection().X);
                Position.X += (int)(game.Raquette.Vitesse * game.Raquette.getDirection().X);
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

            base.Update(gametime, keyboardState);
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
            if (game.Raquette.getRectangle().Intersects(_rectangle)) // Détection collision entre la balle et la raquete
            {
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
                float positionXballe = Position.X - (raquette.getRectangle().X - raquette.getRectangle().Width / 2);
                float pourcentage = positionXballe / distanceDroiteGauche;

                if(pourcentage < 0.33f)
                {
                    normal = new Vector2(-0.196f, -0.981f);
                }
                else if (pourcentage > 0.66f)
                {
                    normal = new Vector2(0.196f, -0.981f);
                }
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

        private void collideWithBriques()
        {
            Brique brique;
            Rectangle collideWithBrique = game.murDeBrique.isCollisionWithBrique(_rectangle, out brique); // demande si il y a collision entre balle et une des briques du mur de brique ( rectangle null si aucune collision )

            if (!collideWithBrique.IsEmpty) // Collision entre la balle et une brique OK
            {
                // Si balle invincible return
                if (IsInvincible) return;

                Vector2 milieuBalle = new Vector2(Position.X + _rectangle.Width / 2, Position.Y + _rectangle.Height / 2);

                if (Direction.X == 1) // Direction de la balle vers la droite
                {
                    if (Direction.Y == -1) // La balle monte
                    {
                        if (collideWithBrique.X > milieuBalle.X) // Collision bord gauche de la brique
                        {
                            Direction.X = -1; // projection vers la gauche
                        }
                        else if (collideWithBrique.Y + collideWithBrique.Height == brique.getRectangle().Y + brique.getRectangle().Height) // Collision bord bas
                        {
                            Direction.Y = 1; // projection vers le bas
                        }
                    }
                    else // La balle descend
                    {
                        if (collideWithBrique.Y > milieuBalle.Y) // Collision bord haut
                        {
                            Direction.Y = -1; // projection vers le haut
                        }
                        else if (collideWithBrique.X > milieuBalle.X) // Collision bord gauche
                        {
                            Direction.X = -1; // projection vers la gauche
                        }
                    }
                }
                else // Direction de la balle vers la gauche
                {
                    if (Direction.Y == -1) // La balle monte
                    {
                        if (collideWithBrique.X + collideWithBrique.Width == brique.getRectangle().X + brique.getRectangle().Width) // Collision bord droit
                        {
                            Direction.X = 1; // projection vers la droite
                        }
                        else if (collideWithBrique.Y + collideWithBrique.Height == brique.getRectangle().Y + brique.getRectangle().Height) // Collision bord bas
                        {
                            Direction.Y = 1; // projection vers le bas
                        }
                    }
                    else // La balle descend
                    {
                        if (collideWithBrique.Y > milieuBalle.Y) // Collision bord haut
                        {
                            Direction.Y = -1; // projection vers le haut
                        }
                        else if (collideWithBrique.X + collideWithBrique.Width == brique.getRectangle().X + brique.getRectangle().Width)//collideWithBrique.Y < milieuBalle.Y) // Collision bord droit
                        {
                            Direction.X = 1; // projection vers la droite
                        }
                    }
                }
            }
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
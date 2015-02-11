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
        public Balle(Game1 game, int vitesse, float dirX, float dirY)
            : base(game, vitesse, dirX, dirY)
        {

        }

        public override void Update(GameTime gametime, KeyboardState keyboardState)
        {
            int width = game.getWidth();
            int height = game.getHeight();

            collideWithScreen(width, height);

            collideWithBriques();

            //Direction.X *= Math.Abs(collideWithRaquette());
            Direction.X = Direction.X * Math.Abs(collideWithRaquette());

            Direction.X = (Direction.X > 2) ? 2 : (Direction.X < -2) ? -2 : Direction.X;

            base.Update(gametime, keyboardState);
        }

        private void collideWithScreen(int width, int height)
        {
            // Intersection avec les bords de l'écran
            if (_rectangle.X <= 0) // bord gauche
            {
                Direction.X = 1;
            }
            else if ((_rectangle.X + _rectangle.Width >= width)) // bord droit
            {
                Direction.X = -1;
            }
            if (_rectangle.Y <= 0) // bord haut
            {
                Direction.Y = 1;
            }
            else if ((_rectangle.Y + _rectangle.Height) >= height) // bord bas
            {
                Direction.Y = -1;
            }
        }

        private float collideWithRaquette()
        {
            float boost = 1;
            if (game.Raquette.getRectangle().Intersects(_rectangle)) // Détection collision entre la balle et la raquete
            {
                game.Log = "Intersection !!";

                Raquette raquette = game.Raquette;
                Rectangle posRaquette = raquette.getRectangle();
                Texture2D textureRaquette = raquette.getTexture();

                int milieuRaquette_X = (posRaquette.X + textureRaquette.Width) / 2;
                int distanceEntrePostionDuBoostEtLeMilieuDeLaRaquette_X = textureRaquette.Width * 1 / 4;
                bool boostGauche = _rectangle.X <= (milieuRaquette_X - distanceEntrePostionDuBoostEtLeMilieuDeLaRaquette_X);
                bool boostDroit = _rectangle.X >= (milieuRaquette_X + distanceEntrePostionDuBoostEtLeMilieuDeLaRaquette_X);

                if(boostGauche)
                {
                    boost = (Direction.X<0)? -2 : -1;
                }
                else if(boostDroit) boost = (Direction.X > 0) ? 2 : 1;

                Direction.Y *= -1;
            }
            else game.Log = "";

            return boost;
        }

        private void collideWithBriques()
        {
            Brique brique;
            Rectangle collideWithBrique = game.murDeBrique.isCollisionWithBrique(_rectangle, out brique); // demande si il y a collision entre balle et une des briques du mur de brique ( rectangle null si aucune collision )

            if (!collideWithBrique.IsEmpty) // Collision entre la balle et une brique OK
            {
                Vector2 milieuBalle = new Vector2(_rectangle.X + _rectangle.Width / 2, _rectangle.Y + _rectangle.Height / 2);

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
                    else // // La balle descend
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
            _texture = game.Content.Load<Texture2D>("balle");
        }
    }
}

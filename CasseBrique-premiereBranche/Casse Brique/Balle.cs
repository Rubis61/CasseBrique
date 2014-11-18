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
        public Balle(Game1 game, int vitesse, int dirX, int dirY)
            : base(game, vitesse, dirX, dirY)
        {

        }

        public override void Update(GameTime gametime, KeyboardState keyboardState)
        {
            int width = game.getWidth();
            int height = game.getHeight();

            // Intersection avec les bords de l'écran
            if (_rectangle.X <= 0) // bord gauche
            {
                _dirX = 1;
            }
            else if ((_rectangle.X + _rectangle.Width >= width)) // bord droit
            {
                _dirX = -1;
            }
            if (_rectangle.Y <= 0) // bord haut
            {
                _dirY = 1;
            }
            else if ((_rectangle.Y + _rectangle.Height) >= height) // bord bas
            {
                _dirY = -1;
            }

            if (game.Raquette.getRectangle().Intersects(_rectangle)) // Détection collision entre la balle et la raquete
            {
                game.Log = "Intersection !!";
                _dirY *= -1;
            }
            else game.Log = "";

            Rectangle collideWithBrique = game.murDeBrique.isCollisionWithBrique(_rectangle); // demande si il y a collision entre balle et une des briques du mur de brique ( rectangle null si aucune collision )

            if (!collideWithBrique.IsEmpty) // Collision entre la balle et une brique OK
            {
                Vector2 milieuBalle = new Vector2(_rectangle.X + _rectangle.Width / 2, _rectangle.Y + _rectangle.Height / 2);

                if (_dirX == 1) // Direction de la balle vers la droite
                {
                    if (_dirY == -1) // La balle monte
                    {
                        if (collideWithBrique.X > milieuBalle.X) // Collision bord gauche de la brique
                        {
                            _dirX = -1; // projection vers la gauche
                        }
                        else if (collideWithBrique.X < milieuBalle.X) // Collision bord bas
                        {
                            _dirY = 1; // projection vers le bas
                        }
                    }
                    else // La balle descend
                    {
                        if (collideWithBrique.Y > milieuBalle.Y) // Collision bord haut
                        {
                            _dirY = -1; // projection vers le haut
                        }
                        else if (collideWithBrique.X > milieuBalle.X) // Collision bord gauche
                        {
                            _dirX = -1; // projection vers la gauche
                        }
                    }
                }
                else // Direction de la balle vers la gauche
                {
                    if (_dirY == -1) // La balle monte
                    {
                        if (collideWithBrique.Y < milieuBalle.Y) // Collision bord droit
                        {
                            _dirX = 1; // projection vers la droite//
                        }
                        else if (collideWithBrique.X < milieuBalle.X) // Collision bord bas
                        {
                            _dirY = 1; // projection vers le bas
                        }
                    }
                    else // // La balle descend
                    {
                        if (collideWithBrique.Y > milieuBalle.Y) // Collision bord haut
                        {
                            _dirY = -1; // projection vers le haut
                        }
                        else if (collideWithBrique.Y < milieuBalle.Y) // Collision bord droit
                        {
                            _dirX = 1; // projection vers la droite
                        }
                    }
                }
                /*
                if( milieuBalle.X < collideWithBrique.X ) // Collision 
                {
                    _dirX = -1;
                }
                else if( milieuBalle.X > collideWithBrique.X )
                {
                    _dirX = 1;
                }
                else if( milieuBalle.Y > collideWithBrique.Y )
                {
                    _dirY = 1;
                }
                else if( milieuBalle.Y < collideWithBrique.Y )
                {
                    _dirY = -1;
                }*/
            }

            base.Update(gametime, keyboardState);
        }
    }
}

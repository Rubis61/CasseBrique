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
    public class MurDeBrique
    {
        public Rectangle rectConteneur { get; set; }
        public List<Brique> briques { get; set; }
        private const int distanceEntreBriques = 15;
        private Game1 game;

        public MurDeBrique(Game1 game)
        {
            this.game = game;
            briques = new List<Brique>();

            int largeurFenetre = game.getWidth();
            int hauteurFenetre = game.getHeight();

            rectConteneur = new Rectangle(50, 50, largeurFenetre-50, hauteurFenetre*2/3);
        }

        public void générerMurDeBriqueDeBase()
        {
            for (int i = 0; i < 19; i++)
            {
                briques.Add(new Brique(game, 0, 0, 0));
            }
        }

        public void initialiserBriques()
        {
            int posXlastBrique = 0;
            int posXcurrentBrique = rectConteneur.X;
            int largeurBrique = 64;//briques[0].getRectangle().Width;
            int hauteurBrique = 32;//briques[0].getRectangle().Height;
            int nbrMaxBriquesParLignes = rectConteneur.Width / (largeurBrique + distanceEntreBriques);

            int posY = rectConteneur.Y;
            int ligne = 0;
            for( int i=0 ; i<briques.Count ; i++ )
            {
                if (i != 0) posXlastBrique = briques[i - 1].getRectangle().X; // position de la dernière brique
                
                posXcurrentBrique = posXlastBrique + ((i!=0)?distanceEntreBriques:0) + briques[0].getRectangle().Width; // Calcul de la position de la brique actuelle grace à la prédédente
               
                if (i == nbrMaxBriquesParLignes) // cas maximum de brique dans une ligne atteint = saut d'une ligne avec espace entre
                { 
                    ligne++;
                    posY += hauteurBrique + distanceEntreBriques; // calcul prochaine position en Y
                    posXcurrentBrique = rectConteneur.X; // Retour à gauche
                }

                briques[i].Initialize(posXcurrentBrique, posY, 64, 32);
            }
        }

        public void loadContentBriques(ContentManager content)
        {
            foreach (var brique in briques)
            {
                brique.LoadContent(content, "brick016");
            }
        }

        public void drawBriques(SpriteBatch spriteBatch, GameTime gameTime)
        {
            foreach (var brique in briques)
            {
                brique.Draw(spriteBatch, gameTime);
            }
        }

        public Rectangle isCollisionWithBrique(Rectangle rectangle)
        {
            for( int i=0 ; i<briques.Count ; i++ )
            {
                Rectangle rectangleCollider = Rectangle.Intersect(briques[i].getRectangle(), rectangle);

                if( !rectangleCollider.IsEmpty )
                {
                    //briques[i].unLoadContent();
                    briques.RemoveAt(i);

                    return rectangleCollider;
                }
            }
            return Rectangle.Empty;
        }
    }
}

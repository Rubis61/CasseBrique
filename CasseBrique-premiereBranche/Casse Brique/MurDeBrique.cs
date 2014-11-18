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
        public Rectangle Rectangle { get; set; }
        public List<Brique> briques { get; set; }
        private Game1 game;

        public MurDeBrique(Game1 game)
        {
            this.game = game;
            briques = new List<Brique>();
        }

        public void générerMurDeBriqueDeBase()
        {
            for (int i = 0; i < 20; i++)
            {
                briques.Add(new Brique(game, 0, 0, 0));
            }
        }

        public void initialiserBriques() 
        {
            for( int i=0 ; i<briques.Count ; i++ )
            {
                briques[i].Initialize(50 + ((i*64)), 50, 64, 32);
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

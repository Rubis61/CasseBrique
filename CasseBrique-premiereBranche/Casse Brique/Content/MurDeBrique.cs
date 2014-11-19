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
        public List<Brique[]> ligneBriques { get; set; }
        private const int distanceEntreBriques = 0;//15;
        private Game1 game;

        public MurDeBrique(Game1 game)
        {
            this.game = game;
            //briques = new List<Brique>();
            ligneBriques = new List<Brique[]>();

            int largeurFenetre = game.getWidth();
            int hauteurFenetre = game.getHeight();

            rectConteneur = new Rectangle(0, 0, largeurFenetre, hauteurFenetre * 2 / 3);
        }

        public void générerMurDeBriqueDeBase()
        {
            for (int i = 0; i < 300; i++)
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
            /*
            int ligne = 0;
            for (int i = 0; i < briques.Count; i++)
            {
                if (i != 0)
                {
                    posXlastBrique = briques[i - 1].getRectangle().X; // position de la brique précédente

                    posXcurrentBrique = posXlastBrique + ((i != 0) ? distanceEntreBriques : 0) + briques[0].getRectangle().Width; // Calcul de la position de la brique actuelle grace à la prédédente
                }

                if (i%nbrMaxBriquesParLignes == 0 && i!=0) // cas maximum de brique dans une ligne atteint = saut d'une ligne avec espace entre
                {
                    ligne++;
                    posY += hauteurBrique + distanceEntreBriques; // calcul prochaine position en Y
                    posXcurrentBrique = rectConteneur.X; // Retour à gauche
                }

                briques[i].Initialize(posXcurrentBrique, posY, 64, 32);
            }*/
            for (int ligne = 0; ligne < ligneBriques.Count; ligne++)
            {
                for (int colonne = 0; colonne < 20; colonne++)
                {
                    if (colonne != 0)
                    {
                        posXlastBrique = ligneBriques[ligne][colonne - 1].getRectangle().X; // position de la brique précédente

                        posXcurrentBrique = posXlastBrique + ((colonne != 0) ? distanceEntreBriques : 0) + ligneBriques[ligne][0].getRectangle().Width; // Calcul de la position de la brique actuelle grace à la prédédente
                    }

                    ligneBriques[ligne][colonne].Initialize(posXcurrentBrique, posY, 64, 32);
                }
                posY += hauteurBrique + distanceEntreBriques; // calcul prochaine position en Y
            }
        }

        public void chargerBriques(int level)
        {
            string[][] map = MapReader.getMap(level);

            for( int ligne=0 ; ligne<map.Length ; ligne++ )
            {
                Brique[] tmpLigneBriques = new Brique[20];

                tmpLigneBriques = créerBriques(tmpLigneBriques);
                
                for( int colonne=0 ; map[ligne]!=null && colonne<map[ligne].Length ; colonne++ )
                {
                    if (map[ligne][colonne] == null) map[ligne][colonne] = "*"; //

                    Brique tmpBrique;
                    char[] tmpCharArray = map[ligne][colonne].ToCharArray();
                    char id = tmpCharArray[0];
                    convertirBrique(out tmpBrique, id);
                    tmpLigneBriques[colonne] = tmpBrique;
                }

                ligneBriques.Add(tmpLigneBriques);
            }
        }

        private Brique[] créerBriques(Brique[] briques)
        {
            for( int i=0 ; i<briques.Length ; i++ )
            {
                briques[i] = new Brique(game, 0, 0, 0);
            }

            return briques;
        }

        private void enleverNullMap(string[][] map)
        {
            for (int ligne = 0; ligne < map.Length; ligne++)
            {
                for (int colonne = 0; colonne < map[ligne].Length; colonne++)
                {
                    
                }
            }
        }

        private void convertirBrique(out Brique brique, char id)
        {
            switch (id)
            {
                case '0':
                    brique = null;
                    break;
                case '1':
                    brique = new BriqueNormale(game, 0, 0, 0);
                    break;
                default:
                    brique = null;
                    break;
            }
        }

        public void loadContentBriques(ContentManager content)
        {
            foreach (var ligneBrique in ligneBriques)
            {
                foreach (var brique in ligneBrique)
                {
                    brique.LoadContent(content, "brick016");
                }
            }
        }

        public void drawBriques(SpriteBatch spriteBatch, GameTime gameTime)
        {
            foreach (var ligneBrique in ligneBriques)
            {
                foreach (var brique in ligneBrique)
                {
                    brique.Draw(spriteBatch, gameTime);                    
                }
            }
        }

        public Rectangle isCollisionWithBrique(Rectangle rectangle, out Brique brique)
        {
            foreach (var ligneBrique in ligneBriques)
            {
                for (int i = 0; i < ligneBrique.Length; i++)
                {
                    if (!ligneBrique[i].isActive) break;

                    Rectangle rectangleCollider = Rectangle.Intersect(ligneBrique[i].getRectangle(), rectangle);

                    if (!rectangleCollider.IsEmpty)
                    {
                        brique = ligneBrique[i];
                        //briques[i].unLoadContent();
                        ligneBrique[i].isActive = false;

                        return rectangleCollider;
                    }
                }
            }
            
            brique = null;
            return Rectangle.Empty;
        }
    }
}

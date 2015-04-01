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
using Casse_Brique.Content;

namespace Casse_Brique
{
    public class MurDeBrique
    {
        public Rectangle rectConteneur { get; set; }
        public List<Brique> briques { get; set; }
        public List<Brique[]> ligneBriques { get; set; }
        private const int distanceEntreBriques = 0;//15;
        private Game1 game;
        private Bonus bonus; 

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
                posXcurrentBrique = rectConteneur.X;
                posY += hauteurBrique + distanceEntreBriques; // calcul prochaine position en Y
            }
        }

        public void chargerBriques(int level)
        {
            string[][] map = MapReader.getMap(level);

            for( int ligne=0 ; ligne<map.Length ; ligne++ )
            {
                Brique[] tmpLigneBriques = new Brique[20];

                if (map[ligne] == null) tmpLigneBriques = créerBriquesVides(tmpLigneBriques);
                else tmpLigneBriques = créerBriquesVides(tmpLigneBriques);
                // tmpLigneBriques = null;
                
                for( int colonne=0 ; map[ligne]!=null && colonne<map[ligne].Length ; colonne++ )
                {
                    if (map[ligne][colonne] == null) map[ligne][colonne] = "*"; // brique inconnue donc brique vide

                    Brique tmpBrique;
                    char[] tmpCharArray = map[ligne][colonne].ToCharArray();
                    char id = tmpCharArray[0];
                    // Générer random si c'est une brique bonus
                    convertirBrique(out tmpBrique, id);
                    
                    tmpLigneBriques[colonne] = tmpBrique;
                }

                //if( tmpLigneBriques == null ) tmpLigneBriques = créerBriquesVides(tmpLigneBriques);
                
                ligneBriques.Add(tmpLigneBriques);
            }
        }

        private Brique[] créerBriquesVides(Brique[] briques)
        {
            for (int i = 0; i < briques.Length; i++)
            {
                briques[i] = new BriqueVide(game, 0, 0, 0);
            }

            return briques;
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
                    // Transformer les null en briqueVide
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
                case '2':
                    brique = new BriqueDoubleCoup(game, 0, 0, 0);
                    break;
                case '3':
                    brique = new BriqueTripleCoup(game, 0, 0, 0);
                    break;
                case '4':
                    brique = new BriqueIncassable(game, 0, 0, 0);
                    break;
                default: // * pour null
                    brique = new BriqueVide(game, 0, 0, 0);
                    break;
            }
        }

        public void loadContentBriques(ContentManager content)
        {
            foreach (var ligneBrique in ligneBriques)
            {
                foreach (var brique in ligneBrique)
                {
                    switch (brique.GetType().Name)
                    {
                        case "BriqueNormale"    : brique.LoadContent(content, "twix");
                            break;
                        case "BriqueDoubleCoup" : brique.LoadContent(content, "balisto");
                            break;
                        case "BriqueTripleCoup" : brique.LoadContent(content, "lion");
                            break;
                        case "BriqueIncassable": brique.LoadContent(content, "kitkat");
                            break;
                    };

                }
            }
        }

        public void drawBriques(SpriteBatch spriteBatch, GameTime gameTime)
        {
            foreach (var ligneBrique in ligneBriques)
            {
                foreach (var brique in ligneBrique)
                {
                    if( brique.isActive ) brique.Draw(spriteBatch, gameTime);
                }
            }
        }

        public Rectangle isCollisionWithBrique(Rectangle rectangle, out Brique brique)
        {
            for (int ligne = 0; ligne < ligneBriques.Count; ligne++)
            {
                for (int colonne = 0; colonne < ligneBriques[ligne].Length; colonne++)
                {
                    if (ligneBriques[ligne][colonne].isActive) // La brique est active
                    {
                        Rectangle rectangleCollider = Rectangle.Intersect(ligneBriques[ligne][colonne].getRectangle(), rectangle);

                        if (!rectangleCollider.IsEmpty) // rectangle non null donc collision détectée
                        {
                            brique  = ligneBriques[ligne][colonne];
                            //ligneBriques[ligne][colonne].unLoadContent();
                            //ligneBriques[ligne][colonne].isActive = false;
                           
                            if(brique.Hit()) // Si la brique est détruite
                            {

                                game.joueur.CalculScoreBrique(brique);
                                ligneBriques[ligne][colonne] = new BriqueVide(game, 0, 0, 0);
                                Bonus bonus;
                                Helpers.HelperBonus.InitializeListBonus(game);
                                if ((bonus = Helpers.HelperBonus.GénérerBonusAléa()) != null)
                                {
                                    if (bonus.TypeBonus != TypeBonus.Aucun)
                                    {
                                        game.AjouterBonus(bonus, (int)(brique.Position.X + (brique.getTexture().Width * 0.2f / 2)),
                                                                                                     (int)(brique.Position.Y + (brique.getTexture().Height * 0.2f / 2)));
                                }             
                            }
                            }
                           
                            return rectangleCollider;
                        }
                    }
                }
            }
            
            brique = null;
            return Rectangle.Empty;
        }

        public int getNombreBriquesRestantes()
        {
            int nombre = 0;

            for (int ligne = 0; ligne < ligneBriques.Count; ligne++)
            {
                for (int colonne = 0; colonne < ligneBriques[ligne].Length; colonne++)
                {
                    if (ligneBriques[ligne][colonne].GetType().Name != "BriqueVide" &&
                        ligneBriques[ligne][colonne].GetType().Name != "BriqueIncassable" &&
                        ligneBriques[ligne][colonne].GetType().Name != "BriqueInvisibleIncassable") nombre++;
                }
            }
            return nombre;
        }
    }
}
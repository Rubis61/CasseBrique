using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Casse_Brique.Content
{
    public class Magasin
    {
        Dictionary<TypeBonus, int> myDictionnary;
        public Magasin()
        {
            myDictionnary = new Dictionary<TypeBonus, int>();
            myDictionnary.Add(TypeBonus.RaquetteAgrandie, 100);
            myDictionnary.Add(TypeBonus.VitesseBalleAugmentée, 100);
            myDictionnary.Add(TypeBonus.VitesseBalleRéduite, 100);
            myDictionnary.Add(TypeBonus.BalleInvincible, 300);
            myDictionnary.Add(TypeBonus.VieSupplementaire, 500);
        }

        public void AcheterUnBonus()
        {
           // switch()
        }
        public virtual void Update(GameTime gameTime, Joueur joueur, Game1 game, KeyboardState key, KeyboardState last, Balle balle)
        {

            if (key.IsKeyDown(Keys.NumPad0) && !last.IsKeyDown(Keys.NumPad0))
            {
                if(joueur.Score >= myDictionnary[TypeBonus.RaquetteAgrandie])
                {
                    //game.GraphicsDevice.²
                    Bonus.AgrandirLaRaquette(game);
                    joueur.Score -= 100;
                }
            }

            if (key.IsKeyDown(Keys.NumPad1) && !last.IsKeyDown(Keys.NumPad1))
            {
                if (joueur.Score >= myDictionnary[TypeBonus.VitesseBalleAugmentée])
                {
                    Bonus.AugmenterVitesseBalle(game);
                    joueur.Score -= 100;
                }
            }

            if (key.IsKeyDown(Keys.NumPad2) && !last.IsKeyDown(Keys.NumPad2))
            {
                if (joueur.Score >= myDictionnary[TypeBonus.VitesseBalleAugmentée])
                {
                    Bonus.RéduireVitesseBalle(game);
                    joueur.Score -= 100;
                }
            }

            if (key.IsKeyDown(Keys.NumPad3) && !last.IsKeyDown(Keys.NumPad3))
            {
                if (joueur.Score >= myDictionnary[TypeBonus.BalleInvincible])
                {
                    balle.IsInvincible = true;
                    joueur.Score -= 300;
                }
            }

            if (key.IsKeyDown(Keys.NumPad4) && !last.IsKeyDown(Keys.NumPad4))
            {
                if (joueur.Score >= myDictionnary[TypeBonus.VieSupplementaire])
                {
                    joueur.NbrLife++;
                    joueur.Score -= 500;
                }
            }
        }
    }
}

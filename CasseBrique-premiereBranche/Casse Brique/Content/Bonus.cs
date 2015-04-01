using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Casse_Brique.Content
{
    public enum TypeBonus { Aucun, RaquetteAgrandie, RaquetteReduite, VitesseBalleAugmentée, VitesseBalleRéduite, BalleInvincible, VieSupplementaire };

    public class Bonus : GameObject
    {
        Rectangle rectangle;
        Texture2D maTexture;
        ContentManager content;
        GraphicsDevice graphicsDevice;
        SpriteBatch spriteBatch;
        GameTime gametime;

        public TypeBonus TypeBonus { get; set; }

        public int Pourcentage { get; set; }
        //Game1 game;
        public Bonus(Game1 game, int vitesse, float dirX, float dirY, TypeBonus typeBonus, int pourcentage)
        : base(game, vitesse, dirX, dirY)
        {
            TypeBonus = typeBonus;
            Pourcentage = pourcentage;
        }
        public static void VieSupplementaire(Game1 game)
        {
            game.joueur.NbrLife++;
        }
        public static void AgrandirLaRaquette(Game1 game)
        {
            game.Raquette.Agrandir();
        }
        public static void RéduireLaRaquette(Game1 game)
        {
            //game.Raquette.Initialize(game.Raquette.getRectangle().X - (130 / 2), game.Raquette.getRectangle().Y, game.Raquette.getRectangle().Width / 2, 28);
            game.Raquette.Reduire();
        }
        public static void AugmenterVitesseBalle(Game1 game)
        {
            game.balle.Vitesse += 2;
        }
        public static void RéduireVitesseBalle(Game1 game)
        {
            game.balle.Vitesse -= 2;
        }
        public override void LoadContent(ContentManager content, string nom)
        {
            _texture = game.Content.Load<Texture2D>(nom);
        }
        public override void Initialize(int posX, int posY, int width, int height )
        {
            _rectangle = new Rectangle(posX, posY, (int)(width), (int)(height));
            //_rectangle = new Rectangle(posX, posY, (int)(width * 0.15625f), (int)(height * 0.15625f));
            Position = new Vector2(posX, posY);
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gametime)
        {
            spriteBatch.Draw(_texture, Position, null, Color.White, 0f, Vector2.Zero, new Vector2(1f), SpriteEffects.None, 0f);
        }
    }
}
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Casse_Brique.Content
{
    public class Bonus : GameObject
    {
        Rectangle rectangle;
        Texture2D maTexture;
        ContentManager content;
        GraphicsDevice graphicsDevice;
        SpriteBatch spriteBatch;
        GameTime gametime;
        //Game1 game;
        public Bonus(Game1 game, int vitesse, float dirX, float dirY)
        : base(game, vitesse, dirX, dirY)
        {

        }
        public void AgrandirLaRaquette()
        {
            game.Raquette.Initialize(game.Raquette.getRectangle().X - (130 / 2), game.Raquette.getRectangle().Y, game.Raquette.getRectangle().Width*2, 28);
        }
        public void RéduireLaRaquette()
        {
            game.Raquette.Initialize(game.Raquette.getRectangle().X - (130 / 2), game.Raquette.getRectangle().Y, game.Raquette.getRectangle().Width / 2, 28);
        }
        public void AugmenterVitesseBalle()
        {
            game.balle.Vitesse += 2;
        }
        public void RéduireVitesseBalle()
        {
            game.balle.Vitesse -= 2;
        }
        public override void LoadContent(ContentManager content, string nom)
        {
            _texture = game.Content.Load<Texture2D>("choux");
        }
        public override void Initialize(int posX, int posY, int width, int height )
        {
            _rectangle = new Rectangle(posX, posY, width, height);
        }
    }
}

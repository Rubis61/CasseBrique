#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using Casse_Brique.Content;
#endregion

// https://trello.com/b/BrEGQ4Y8/candy-casse-brique

namespace Casse_Brique
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        public static int ESPACE_BALLE_RAQUETTE_INIT = 40;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private bool isPaused = true;
        private bool isWin = false;

        const int height = 720;
        const int width = 1280;
        public int getHeight() { return height; }
        public int getWidth() { return width; }
        private KeyboardState lastKeyboardState;

        public Background Background { get; set; }
        public Raquette Raquette { get; private set; }
        public Balle balle;
        
        public List<Bonus> ListBonus { get; set; }
        
        public MurDeBrique murDeBrique { get; private set; }
        public Bonus bonus;
        SpriteFont font_position;
        SpriteFont font_log;
        public string Log { get; set; }

        public int nbrBriquesRestantes { get; set; }

        public Game1()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferHeight = height;
            graphics.PreferredBackBufferWidth = width;
            graphics.IsFullScreen = true;
            graphics.ApplyChanges();
        }

        public void AjouterBonus(Bonus bonus, int x, int y)
        {
            bonus.LoadContent(Content, "m&ms rouge");
            bonus.Initialize(x, y, bonus.getTexture().Width, bonus.getTexture().Height);
            ListBonus.Add(bonus);
        }

        public void AjouterBonus(Bonus bonus, Rectangle rect)
        {
            bonus.LoadContent(Content, "m&ms rouge");
            bonus.Initialize(rect.X, rect.Y, rect.Width, rect.Height);
            ListBonus.Add(bonus);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// 
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            Background = new Background(this);
            Raquette = new Raquette(this, 10, 0, 0);
            Raquette.Initialize((width/2)-(135/2)-3, height * 19 / 20, 130, 28);
            balle = new Balle(this, 4, 1, -1);
            balle.Initialize(width/2 - 24/2, Raquette.getRectangle().Y - ESPACE_BALLE_RAQUETTE_INIT, 25,25);
            murDeBrique = new MurDeBrique(this);
            //bonus = new Bonus(this, 0, 0, 0, TypeBonus.Aucun);
            ListBonus = new List<Bonus>();
            Log = "";

            murDeBrique.chargerBriques(1);
            murDeBrique.initialiserBriques();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            Background.LoadContent(Content, "paysage-bonbon");
            Raquette.LoadContent(Content, "Mars_chocolate_bar");
            
            font_position = Content.Load<SpriteFont>("position");
            font_log = Content.Load<SpriteFont>("position");
            balle.LoadContent(Content, "balle");
            murDeBrique.loadContentBriques(Content);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
            
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (keyboardState.IsKeyDown(Keys.F1) && !lastKeyboardState.IsKeyDown(Keys.F1))
            {
                Bonus.AgrandirLaRaquette(this);
            }
            if (keyboardState.IsKeyDown(Keys.F2) && !lastKeyboardState.IsKeyDown(Keys.F2))
            {
                Bonus.RéduireLaRaquette(this);
            }
            if (keyboardState.IsKeyDown(Keys.F3) && !lastKeyboardState.IsKeyDown(Keys.F3))
            {
                Bonus.RéduireVitesseBalle(this);
            }
            if (keyboardState.IsKeyDown(Keys.F4) && !lastKeyboardState.IsKeyDown(Keys.F4))
            {
                Bonus.AugmenterVitesseBalle(this);
            }
            if (keyboardState.IsKeyDown(Keys.F5) && !lastKeyboardState.IsKeyDown(Keys.F5))
            {
                balle.Aimanté = true;
            }
            if (keyboardState.IsKeyDown(Keys.F6) && !lastKeyboardState.IsKeyDown(Keys.F6))
            {
                balle.Aimanté = false;
                balle.Position.Y -= 5;
                balle._rectangle.Y -= 5;
                balle.Vitesse = 5;
            }
            if (keyboardState.IsKeyDown(Keys.F7) && !lastKeyboardState.IsKeyDown(Keys.F7))
            {
                balle.LoadContent(Content, "ferrero doré - copie");
                balle.IsInvincible = true;
                balle.Initialize((int)balle.Position.X, (int)balle.Position.Y, balle.getTexture().Width, balle.getTexture().Height);
            }
            if(keyboardState.IsKeyDown(Keys.Space) && !lastKeyboardState.IsKeyDown(Keys.Space))
            {
                isPaused = !isPaused;
            }
            
            if(!isPaused && !isWin)
            {
                Raquette.Update(gameTime, keyboardState);
                balle.Update(gameTime, keyboardState);

                foreach (var bonus in ListBonus)
                {
                    bonus.Update(gameTime, keyboardState);
                }
                CollideListBonusWithRaquette();
                nbrBriquesRestantes = murDeBrique.getNombreBriquesRestantes();
                if (nbrBriquesRestantes == 0)
                {
                    isWin = true;
                }
            }

            if (keyboardState.IsKeyDown(Keys.R))
            {
                Initialize();
            }

            lastKeyboardState = keyboardState;

            base.Update(gameTime);
        }

        private void CollideListBonusWithRaquette()
        {
            List<Bonus> ToRemove = new List<Bonus>();
            foreach (var bonus in ListBonus)
            {
                if (bonus.getRectangle().Intersects(Raquette.getRectangle()))
                {
                    // Applique le bonus
                    switch (bonus.TypeBonus)
                    {
                        case TypeBonus.Aucun:
                            break;
                        case TypeBonus.RaquetteAgrandie:
                            Bonus.AgrandirLaRaquette(this);
                            break;
                        case TypeBonus.RaquetteReduite:
                            Bonus.RéduireLaRaquette(this);
                            break;
                        case TypeBonus.VitesseBalleAugmentée:
                            Bonus.AugmenterVitesseBalle(this);
                            break;
                        case TypeBonus.VitesseBalleRéduite:
                            Bonus.RéduireVitesseBalle(this);
                            break;
                        case TypeBonus.BalleInvincible:
                            balle.IsInvincible = true;
                            balle.LoadContent(Content, "ferrero doré - copie");
                            balle.Initialize((int)balle.Position.X, (int)balle.Position.Y, balle.getTexture().Width, balle.getTexture().Height);
                            break;
                        default:
                            break;
                    }
                    ToRemove.Add(bonus);
                }
            }
            foreach (var bonus in ToRemove)
            {
                ListBonus.Remove(bonus);
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            
            // TODO: Add your drawing code here
            spriteBatch.Begin();
                Background.Draw(spriteBatch, gameTime);
                Raquette.Draw(spriteBatch, gameTime);
                balle.Draw(spriteBatch, gameTime);
                murDeBrique.drawBriques(spriteBatch, gameTime);
                spriteBatch.DrawString(font_position, ( nbrBriquesRestantes == 0 ? "Gagne !!" : nbrBriquesRestantes.ToString()), new Vector2(10, 10), Color.Red);
                spriteBatch.DrawString(font_log, Log, new Vector2(400, 10), Color.Blue);
                //bonus.Draw(spriteBatch, gameTime);
                foreach (var bonus in ListBonus)
                {
                    bonus.Draw(spriteBatch, gameTime);
                }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
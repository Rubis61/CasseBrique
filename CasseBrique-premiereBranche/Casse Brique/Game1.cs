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
        private static int ESPACE_BALLE_RAQUETTE_INIT = 40;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private bool isPaused = true;

        const int height = 720;
        const int width = 1280;
        public int getHeight() { return height; }
        public int getWidth() { return width; }
        private KeyboardState lastKeyboardState;

        public Raquette Raquette { get; private set; }
        public Balle balle;

        public MurDeBrique murDeBrique { get; private set; }
        public Bonus bonus;
        SpriteFont font_position;
        SpriteFont font_log;
        public string Log { get; set; }

        public Game1()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferHeight = height;
            graphics.PreferredBackBufferWidth = width;
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
            Raquette = new Raquette(this, 10, 0, 0);
            Raquette.Initialize((width/2)-(135/2)-3, height * 19 / 20, 130, 28);
            balle = new Balle(this, 4, 1, -1);
            balle.Initialize(width/2 - 24/2, Raquette.getRectangle().Y - ESPACE_BALLE_RAQUETTE_INIT,25,25);
            murDeBrique = new MurDeBrique(this);
            bonus = new Bonus(this, 0, 0, 0);
            Log = "";

            //murDeBrique.générerMurDeBriqueDeBase();
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
            Raquette.LoadContent(Content, "barre");
            
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

            if(keyboardState.IsKeyDown(Keys.F1) && !lastKeyboardState.IsKeyDown(Keys.F1))
            {
                bonus.AgrandirLaRaquette();
            }
            if (keyboardState.IsKeyDown(Keys.F2) && !lastKeyboardState.IsKeyDown(Keys.F2))
            {
                bonus.RéduireLaRaquette();
            }
            if (keyboardState.IsKeyDown(Keys.F3) && !lastKeyboardState.IsKeyDown(Keys.F3))
            {
                bonus.AugmenterVitesseBalle();
            }
            if (keyboardState.IsKeyDown(Keys.F4) && !lastKeyboardState.IsKeyDown(Keys.F4))
            {
                bonus.RéduireVitesseBalle();
            }
            if (keyboardState.IsKeyDown(Keys.F5) && !lastKeyboardState.IsKeyDown(Keys.F5))
            {
                balle.Aimanté = true;
            }
            if (keyboardState.IsKeyDown(Keys.F6) && !lastKeyboardState.IsKeyDown(Keys.F6))
            {
                balle.Aimanté = false;
                balle._rectangle.Y -= 5;
                balle.Vitesse = 5;
            }
            // TODO: Add your update logic here
            Raquette.Update(gameTime, keyboardState);
            balle.Update(gameTime, keyboardState);
            }

            if (keyboardState.IsKeyDown(Keys.R))
            {
                Initialize();
            }

            lastKeyboardState = keyboardState;

            base.Update(gameTime);
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
                Raquette.Draw(spriteBatch, gameTime);
                balle.Draw(spriteBatch, gameTime);
                murDeBrique.drawBriques(spriteBatch, gameTime);
                int nbr = murDeBrique.getNombreBriquesRestantes();
                spriteBatch.DrawString(font_position, ( nbr == 0 ? "Gagne !!" : nbr.ToString()), new Vector2(10, 10), Color.Red);
                spriteBatch.DrawString(font_log, Log, new Vector2(400, 10), Color.Blue);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
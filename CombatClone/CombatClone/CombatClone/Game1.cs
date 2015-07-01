using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace CombatClone
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        internal static Gui gui = new Gui();
        internal static SpawnManager spawnManager = new SpawnManager();

        bool startedGame;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = 640;
        }

        GamePadState gamePad;
        GamePadState prevGamePad;

        protected override void Initialize()
        {
            AssetManager.Load(Content);

            GameObjectManager.Add(new Player());

            if(GamePad.GetState(PlayerIndex.Two).IsConnected) GameObjectManager.Add(new TurretPlayer());

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void UnloadContent()
        {

        }


        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
            this.Exit();

            prevGamePad = gamePad;
            gamePad = GamePad.GetState(PlayerIndex.One);

            Globals.UpdateScreenOffset();

            if (!startedGame)
            {
                GameObjectManager.Update();
                spawnManager.Update();
                startedGame = true;
            }

            if (Globals.startScreen)
            {
                if (gamePad.IsButtonDown(Buttons.A) && !prevGamePad.IsButtonDown(Buttons.A))
                {
                    foreach (TextEffect t in GameObjectManager.gameObjects.Where(item => item is TextEffect)) t.destroy = true;
                    Globals.startScreen = false;
                    GameObjectManager.Add(new TextEffect(new Vector2(320, -100), "GET READY!", Color.White, new Vector2(320, 240), 0.05f, 128 * 3, 0));
                }
            }
            else
            {
                if (!Globals.paused)
                {
                    GameObjectManager.Update();
                    spawnManager.Update();
                }

                if (Globals.gameOver)
                {
                    if (gamePad.IsButtonDown(Buttons.A) && !prevGamePad.IsButtonDown(Buttons.A))
                    {
                        GameObjectManager.gameObjects.Clear();
                        GameObjectManager.Add(new Player());
                        if (GamePad.GetState(PlayerIndex.Two).IsConnected) GameObjectManager.Add(new TurretPlayer());
                        spawnManager = new SpawnManager();
                        GameObjectManager.Add(new TextEffect(new Vector2(320, -100), "GET READY!", Color.White, new Vector2(320, 240), 0.05f, 128 * 3, 0));
                    }
                }

                if (gamePad.IsButtonDown(Buttons.Start) && !prevGamePad.IsButtonDown(Buttons.Start) && !Globals.gameOver)
                {
                    Globals.paused = !Globals.paused;
                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(168, 110, Convert.ToInt16(72)));

            spriteBatch.Begin();
            spriteBatch.Draw(AssetManager.background, Globals.screenOffset, Color.White);
            spriteBatch.End();

            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend);

            foreach (GameObject g in GameObjectManager.gameObjects) g.DrawSprite(spriteBatch);

            spriteBatch.End();

            spriteBatch.Begin();
            gui.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

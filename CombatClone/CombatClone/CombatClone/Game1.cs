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

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = 640;
        }

        protected override void Initialize()
        {
            AssetManager.Load(Content);

            GameObjectManager.Add(new Player());
            
            GameObjectManager.Add(new PowerUp(new Vector2(500, 400), 1, false));
            GameObjectManager.Add(new PowerUp(new Vector2(300, 400), 0, true));

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

            Random random = new Random();
            if(GamePad.GetState(PlayerIndex.One).Buttons.A == ButtonState.Pressed && GameObjectManager.gameObjects.Count <= 1) GameObjectManager.Add(new ArmoredCar(new Vector2(500, 400)));
            GameObjectManager.Update();

            spawnManager.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

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

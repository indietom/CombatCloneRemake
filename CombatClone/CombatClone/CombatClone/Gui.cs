using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace CombatClone
{
    class Gui
    {
        Vector2 topBar;

        public void Update()
        {

        }

        public void DrawPlayerUi(SpriteBatch spriteBatch)
        {
            foreach (Player p in GameObjectManager.gameObjects.Where(item => item is Player))
            {
                for (int i = 0; i < p.Hp; i++)
                {
                    spriteBatch.Draw(AssetManager.spritesheet, topBar + new Vector2(10 + i*32, 10), new Rectangle(133, 1, 24, 24), Color.White);
                }
                spriteBatch.DrawString(AssetManager.bigFont, "SCORE: " + p.Score.ToString().PadLeft(8, '0'), topBar + new Vector2(10, 48), Color.LightGreen);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            DrawPlayerUi(spriteBatch);
        }
    }
}

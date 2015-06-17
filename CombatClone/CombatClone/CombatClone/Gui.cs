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

        float displayScore;
        float displayAmmo;

        public void Update()
        {

        }

        public void DrawPlayerUi(SpriteBatch spriteBatch)
        {
            foreach (Player p in GameObjectManager.gameObjects.Where(item => item is Player))
            {
                displayScore = Globals.Lerp(displayScore, p.Score, 0.1f);
                displayAmmo = Globals.Lerp(displayAmmo, p.CurrentAmmo, 0.05f);

                for (int i = 0; i < p.Hp; i++)
                {
                    spriteBatch.Draw(AssetManager.spritesheet, topBar + new Vector2(10 + i*32, 10), new Rectangle(133, 1, 24, 24), Color.White);
                }
                spriteBatch.DrawString(AssetManager.bigFont, "SCORE: " + Convert.ToInt32(displayScore).ToString().PadLeft(8, '0'), topBar + new Vector2(10, 48), Color.LightGreen);
                if (p.GunType != 0)
                {
                    spriteBatch.DrawString(AssetManager.bigFont, "AMMO: " + Convert.ToInt32(displayAmmo).ToString() + "/" + p.GetMaxAmmo(p.GunType), topBar + new Vector2(10, 70), Color.Gold);
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            DrawPlayerUi(spriteBatch);
        }
    }
}

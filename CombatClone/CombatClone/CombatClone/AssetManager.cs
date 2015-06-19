using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace CombatClone
{
    class AssetManager
    {
        public static Texture2D spritesheet, background;

        public static SpriteFont smallFont, bigFont;

        public static void Load(ContentManager content)
        {
            spritesheet = content.Load<Texture2D>("spritesheet");
            background = content.Load<Texture2D>("background");

            smallFont = content.Load<SpriteFont>("SmallFont");
            bigFont = content.Load<SpriteFont>("BigFont");
        }
    }
}

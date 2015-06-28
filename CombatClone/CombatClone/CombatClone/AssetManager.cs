using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace CombatClone
{
    class AssetManager
    {
        public static Texture2D spritesheet, background, startScreen;

        public static SpriteFont smallFont, bigFont;

        public static SoundEffect shootSound, explosionSound, hitSound, powerUpSound;

        public static void Load(ContentManager content)
        {
            spritesheet = content.Load<Texture2D>("spritesheet");
            background = content.Load<Texture2D>("background");
            startScreen = content.Load<Texture2D>("startScreen");

            smallFont = content.Load<SpriteFont>("SmallFont");
            bigFont = content.Load<SpriteFont>("BigFont");

            shootSound = content.Load<SoundEffect>("shot");
            explosionSound = content.Load<SoundEffect>("expolsion2");
            hitSound = content.Load<SoundEffect>("hit");
            powerUpSound = content.Load<SoundEffect>("powerUp");
        }
    }
}

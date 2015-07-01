using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace CombatClone
{
    class TextEffect : GameObject
    {
        byte type;

        short lifeTime;
        short maxLifeTime;

        string text;

        float waveCount;

        Vector2 target;

        public TextEffect(Vector2 pos2, string text2, Color color2, Vector2 target2, float speed2, short maxLifeTime2, byte type2)
        {
            Pos = pos2;

            text = text2;

            Color = color2;

            target = target2;

            Speed = speed2;

            Z = 1;
            Scale = 1;

            type = 0;

            type = type2;
            maxLifeTime = maxLifeTime2;
        }

        public override void Update()
        {
            switch (type)
            {
                case 0:
                    Pos = new Vector2(Globals.Lerp(Pos.X, target.X, Speed), Globals.Lerp(Pos.Y, target.Y, Speed)); 
                    break;
                case 1:
                    Pos += new Vector2((float)Math.Sin(20 * waveCount + 30), 0);
                    Rotation += (float)Math.Sin(20 * waveCount + 30);

                    waveCount += Speed;
                    break;
            }

            if (maxLifeTime != 0)
                destroy = (lifeTime >= maxLifeTime);

            lifeTime += 1;

            base.Update();
        }

        public override void DrawSprite(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(AssetManager.bigFont, text, Pos, Color, Globals.DegreesToRadian(Rotation), new Vector2(AssetManager.bigFont.MeasureString(text).X / 2, AssetManager.bigFont.MeasureString(text).Y / 2), Scale, SpriteEffects.None, Z); 
        }
    }
}

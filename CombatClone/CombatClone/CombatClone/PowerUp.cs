using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CombatClone
{
    class PowerUp : GameObject
    {
        byte type;

        bool special;

        short lifeTime;
        short maxLifeTime;

        float waveCount;

        public PowerUp(Vector2 pos2, byte type2, bool special2)
        {
            Pos = pos2;

            special = special2;
            type = type2;

            SpriteCoords = new Point(Frame(type, 24) + 198, Frame(Convert.ToInt16(special), 24) + 198);
            Size = new Point(24, 24);
            Scale = 1;
            Orgin = new Vector2(12, 12);
            Color = Color.White;

            maxLifeTime = 128 * 2;

            Z = 1f;
        }

        public void PickUpUpdate()
        {
            foreach (Player p in GameObjectManager.gameObjects.Where(item => item is Player))
            {
                if (p.Hitbox.Intersects(Hitbox))
                {
                    if (!special)
                    {
                        p.CurrentAmmo = p.GetMaxAmmo((byte)(type+1));
                        p.GunType = (byte)(type + 1);
                    }
                    else
                    {
                        if (type == 0)
                        {
                            p.Hp += 1;
                        }
                        if (type == 1)
                        {
                            p.InvisibleCount = 1;
                        }
                    }
                    destroy = true;
                }
            }
        }

        public override void Update()
        {
            Pos += new Vector2(0, (float)Math.Sin(20 * waveCount + 30));
            waveCount += 0.01f;

            Rotation += (float)Math.Sin(20 * waveCount + 30);

            PickUpUpdate();

            lifeTime += 1;

            if (lifeTime >= maxLifeTime)
            {
                Scale = Globals.Lerp(Scale, 0, 0.05f);
                if (Scale <= 0.1f) destroy = true;
            }

            base.Update();
        }

        public override void DrawSprite(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(AssetManager.spritesheet, Pos + Globals.screenOffset, new Rectangle(199, 166, 32, 32), Color.White, Globals.DegreesToRadian(-Rotation), new Vector2(16, 16), Scale, SpriteEffects.None, Z - 0.01f); 
            base.DrawSprite(spriteBatch);
        }
    }
}

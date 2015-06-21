using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CombatClone
{
    class Helicopter : Enemy
    {
        float rotorBladesRotation;

        bool followPlayer;

        public Helicopter(Vector2 pos2)
        {
            Pos = pos2;

            Hp = 3;

            RotateSpeed = 0.1f;
            MaxFireRate = 128 + 64;
            ProjectileSpeed = 7;
            ProjectileDamege = 1;

            Speed = 1;

            Orgin = new Vector2(32, 32);
            Size = new Point(64, 64);
            SpriteCoords = new Point(1, 265);
            Scale = 1;
            Color = Color.White;

            followPlayer = true;

            MaxDistance = 128*3;

            Z = 0.999f;

            Worth = 2000;
        }

        public override void Update()
        {
            Random random = new Random();

            rotorBladesRotation += 0.1f;

            if (followPlayer)
            {
                foreach (Player p in GameObjectManager.gameObjects.Where(item => item is Player))
                {
                    Pos = new Vector2(Globals.Lerp(Pos.X, (float)Math.Cos(rotorBladesRotation / 10) * 200 + p.Pos.X, 0.1f), Globals.Lerp(Pos.Y, (float)Math.Sin(rotorBladesRotation / 10) * 200 + p.Pos.Y, 0.1f));
                    Rotation = RotateTwoardsTarget(p.Pos);
                }

                FireRate += 1;

                ShootAngle = Rotation + random.Next(-8, 8);
            }
            else
            {

            }

            UpdateHealth();
            Shoot();

            base.Update();
        }

        public override void DrawSprite(SpriteBatch spriteBatch)
        {
            base.DrawSprite(spriteBatch);
            spriteBatch.Draw(AssetManager.spritesheet, Pos, new Rectangle(67, 265, 64, 64), Color, rotorBladesRotation, new Vector2(32, 32), 1, SpriteEffects.None, 1f);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CombatClone
{
    class Apc : Enemy
    {
        float turretRotation;

        Vector2 target;

        byte sqaudSize;

        short spawnEnemiesCount;
        short maxSpawnEnemiesCount;

        public Apc(Vector2 pos2)
        {
            Pos = pos2;

            Hp = 3;

            maxSpawnEnemiesCount = 256*2;

            ProjectileSpeed = 6;
            ProjectileDamege = 1;
            MaxFireRate = 128;
            BurstInterval = 4;
            BurstSize = 3;

            sqaudSize = 3;

            RotateSpeed = 0.03f;

            Speed = 0.5f;

            target = GenerateTarget;

            destroyOnTarget = true;

            Orgin = new Vector2(32, 16);
            Size = new Point(64, 32);
            SpriteCoords = new Point(1, 232);
            Scale = 1;
            Color = Color.White;

            Z = 0.5f;

            Worth = 1500;
        }

        public void SpawnEnemies()
        {
            Random random = new Random();

            spawnEnemiesCount += 1;

            for (int i = 0; i < sqaudSize; i++)
            {
                if (spawnEnemiesCount == maxSpawnEnemiesCount - i * 64)
                {
                    GameObjectManager.Add(new Infantry(Pos - new Vector2((float)Math.Cos(Globals.DegreesToRadian(Angle)) * 25, (float)Math.Sin(Globals.DegreesToRadian(Angle)) * 25), random));
                }
                if (spawnEnemiesCount >= maxSpawnEnemiesCount)
                {
                    sqaudSize = (byte)random.Next(3, 6);
                    spawnEnemiesCount = 0;
                }
            }
        }

        public override void Update()
        {
            Random random = new Random();

            UpdateHealth();

            if (Pos.X <= 640 && Pos.X >= 0 && Pos.Y >= 0 && Pos.Y <= 480)
            {
                SpawnEnemies();
            }

            Shoot();
            FireRate += 1;

            MoveFoward();
            Rotation = Angle;
            Angle = GetAimAngle(target);

            if (GetDistance(target) <= 8) destroy = true;

            foreach(Player p in GameObjectManager.gameObjects.Where(item => item is Player))
            {
                turretRotation = RotateTwoardsTarget(p.Pos);
            }

            ShootAngle = turretRotation + random.Next(-8, 9);

            ShootPos = new Vector2((float)Math.Cos(Globals.DegreesToRadian(turretRotation)) * 25, (float)Math.Sin(Globals.DegreesToRadian(turretRotation)) * 25);

            if (TouchedPlayer()) Hp = 0;

            base.Update();
        }

        public override void DrawSprite(SpriteBatch spriteBatch)
        {
            base.DrawSprite(spriteBatch);
            spriteBatch.Draw(AssetManager.spritesheet, Pos, new Rectangle(67, 232, 30, 16), Color, Globals.DegreesToRadian(turretRotation), new Vector2(8, 8), 1, SpriteEffects.None, 0.51f);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace CombatClone
{
    class Infantry : Enemy
    {
        short throwGranedeCount;
        short maxThrowGrandeCount;

        byte throwGranadeChance;
        byte amountOfGuts;

        bool crushed;
        bool active;

        public Infantry(Vector2 pos2, Random random)
        {
            Pos = pos2;

            maxThrowGrandeCount = 64;
            MaxFireRate = 96;
            BurstInterval = 16;
            BurstSize = 3;

            Hp = 1;
            OrginalSpeed = 1;
            Speed = OrginalSpeed;
            RotateSpeed = 0.05f;
            MaxDistance = random.Next(128, 128+64);

            ProjectileSpeed = 7;
            ProjectileType = 1;
            ProjectileDamege = 1;

            leaveCorpse = true;
            Size = new Point(16, 16);
            SpriteCoords = new Point(1, 133);
            Scale = 1;
            Color = Color.White;
            Orgin = new Vector2(8, 8);

            active = true;

            Worth = 500;

            Z = 0.01f;
        }

        public override void Update()
        {
            Random random = new Random();

            if (!crushed)
            {
                foreach (Projectile p in GameObjectManager.gameObjects.Where(item => item is Projectile))
                {
                    if (p.Hitbox.Intersects(Hitbox) && p.Type == 5) crushed = true;
                }
            }

            if (TouchedPlayer() && !crushed)
            {
                Hp = 0;
                Rotation = random.Next(360);
                foreach (Player p in GameObjectManager.gameObjects.Where(item => item is Player))
                {
                    p.Score += 1000;
                }

                amountOfGuts = (byte)random.Next(5, 11);

                for (int i = 0; i < amountOfGuts; i++)
                {
                    GameObjectManager.Add(new Particle(Pos + new Vector2(random.Next(-Size.X / 2, Size.X / 2), random.Next(-Size.Y / 2, Size.Y / 2)), new Point(8, 8), new Point(Frame(random.Next(3), 8) + 33, 168), random.Next(360), random.Next(1, 5), 0, 0, 0)); 
                }

                crushed = true;
            }

            if (Hp >= 1)
            {
                foreach (Apc a in GameObjectManager.gameObjects.Where(item => item is Apc))
                {
                    if (Hitbox.Intersects(a.Hitbox))
                        active = false;
                    else
                        active = true;
                }

                if (active)
                {
                    if (stoping)
                    {
                        Size = new Point(24, 16);
                        SpriteCoords = new Point(52, 133);

                        throwGranedeCount += 1;
                        if (throwGranedeCount >= maxThrowGrandeCount)
                        {
                            throwGranadeChance = (byte)random.Next(4);
                            if (throwGranadeChance == 1)
                            {
                                GameObjectManager.Add(new Projectile(Pos, Rotation, MaxDistance / 30, 2, 0, true));
                            }
                            throwGranedeCount = 0;
                        }
                    }
                    else
                    {
                        Size = new Point(16, 16);
                        SpriteCoords = new Point(1, 133);
                    }

                    Shoot();
                    UpdateHealth();
                    MoveFoward();
                    foreach (Player p in GameObjectManager.gameObjects.Where(item => item is Player))
                    {
                        Angle = RotateTwoardsTarget(p.Pos);
                    }
                    Rotation = Angle;
                    ShootAngle = Angle + random.Next(-8, 9);
                }
            }
            else
            {
                Size = new Point(16, 16);

                Z = 0;

                if (!crushed)
                    SpriteCoords = new Point(35, 133);
                else
                    SpriteCoords = new Point(1, 150);
            }

            base.Update();
        }
    }
}

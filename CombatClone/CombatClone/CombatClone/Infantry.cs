using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace CombatClone
{
    class Infantry : Enemy
    {
        short activeCount;

        bool crushed;
        bool active;
        
        public Infantry(Vector2 pos2, Random random)
        {
            Pos = pos2;

            MaxFireRate = 96;
            BurstInterval = 16;
            BurstSize = 3;

            Hp = 1;
            OrginalSpeed = 1;
            Speed = OrginalSpeed;
            RotateSpeed = 0.05f;
            MaxDistance = random.Next(64, 128);

            ProjectileSpeed = 7;
            ProjectileType = 1;
            ProjectileDamege = 1;

            leaveCorpse = true;
            Size = new Point(16, 16);
            SpriteCoords = new Point(1, 133);
            Scale = 1;
            Color = Color.White;
            Orgin = new Vector2(8, 8);

            Z = 0;
        }

        public override void Update()
        {
            Random random = new Random();

            if (TouchedPlayer() && !crushed)
            {
                Hp = 0;
                Rotation = random.Next(360);
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
                if (!crushed)
                    SpriteCoords = new Point(35, 133);
                else
                    SpriteCoords = new Point(1, 150);
            }
            base.Update();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace CombatClone
{
    class ArmoredCar : Enemy
    {
        public short dropMineCount;
        public short maxDropMineCount;

        Vector2 target;

        public ArmoredCar(Vector2 pos2)
        {
            Pos = pos2;

            maxDropMineCount = 128;

            Speed = 1;

            Hp = 2;

            destroyOnTarget = true;

            SpriteCoords = new Point(1, 199);
            Size = new Point(32, 32);
            Orgin = new Vector2(16, 16);
            Scale = 1;
            Color = Color.White;

            Worth = 1000;
        }

        public override void Update()
        {
            UpdateHealth();

            dropMineCount += 1;

            Rotation = Angle;
            Angle = RotateTwoardsTarget(target);

            MoveFoward();

            if (dropMineCount >= maxDropMineCount)
            {
                for (int i = 0; i < 2; i++)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        GameObjectManager.Add(new Projectile(Pos + new Vector2((float)Math.Cos(Globals.DegreesToRadian(-90*i)) * 10, (float)Math.Cos(Globals.DegreesToRadian(-90*j)) * 10), 0, 0, 3, 0, true));
                    }
                }

                dropMineCount = 0;
            }

            base.Update();
        }
    }
}

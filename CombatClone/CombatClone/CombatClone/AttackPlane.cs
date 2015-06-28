using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace CombatClone
{
    class AttackPlane : Enemy
    {
        public AttackPlane(Vector2 pos2, Random random)
        {
            Pos = pos2;

            Speed = random.Next(3, 6);

            Hp = 1;

            foreach(Player p in GameObjectManager.gameObjects.Where(item => item is Player))
            {
                Angle = GetAimAngle(p.Pos) + random.Next(-8, 9);
            }

            Z = 1;

            SpriteCoords = new Point(1, 100);
            Size = new Point(32, 32);
            Scale = 1;
            Color = Color.White;
            Orgin = new Vector2(16, 16);

            Worth = 100;
        }

        public override void Update()
        {
            UpdateHealth();

            AngleMath();
            Pos += Velocity;

            foreach (Player p in GameObjectManager.gameObjects.Where(item => item is Player))
            {
                if (TouchedPlayer())
                {
                    GameObjectManager.Add(new Expolsion(Pos + new Vector2(-32, -32), 65, true));
                    destroy = true;
                }

                if (GetDistance(p.Pos) >= 1500)
                {
                    destroy = true;
                }
            }

            Rotation = Angle;

            base.Update();
        }
    }
}

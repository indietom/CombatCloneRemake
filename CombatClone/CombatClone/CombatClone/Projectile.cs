using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace CombatClone
{
    class Projectile : GameObject
    {
        byte type;
        public byte Damege { get; private set; }

        public bool enemy;

        public Projectile(Vector2 pos2, float angle2, float speed2, byte type2, byte damege2, bool enemy2)
        {
            Pos = pos2;
            
            Speed = speed2;
            Angle = angle2;

            type = type2;
            AssignType();

            Damege = damege2;

            enemy = enemy2;

            Z = 0.99f;

            Scale = 1;
            Color = Color.White;
        }

        public override void Update()
        {
            AngleMath();
            Pos += Velocity;

            if (Pos.X >= 640 + Size.X || Pos.X < -Size.X * 2 || Pos.Y >= 480 + Size.Y || Pos.Y <= -Size.Y * 2) destroy = true;

            switch (type)
            {
                case 0:
                    Rotation += 10;
                    break;
                case 2:
                    Speed = Globals.Lerp(Speed, 0, 0.05f);
                    if (Speed <= 0.1f)
                    {
                        GameObjectManager.Add(new Expolsion(Pos + new Vector2(-32, -32), 65, true));
                        destroy = true;
                    }
                    break;
            }

            base.Update();
        }

        public void AssignType()
        {
            switch (type)
            {
                case 0:
                    SpriteCoords = new Point(67, 1);
                    Size = new Point(8, 8);
                    Orgin = new Vector2(4, 4);
                    break;
                case 1:
                    SpriteCoords = new Point(67, 10);
                    Size = new Point(4, 4);
                    Orgin = new Vector2(2, 2);
                    break;
                case 2:
                    SpriteCoords = new Point(43, 150);
                    Size = new Point(8, 8);
                    Orgin = new Vector2(4, 4);
                    break;
            }
        }
    }
}

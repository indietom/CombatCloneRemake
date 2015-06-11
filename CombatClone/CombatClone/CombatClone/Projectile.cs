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
        byte damege;

        public bool enemy;

        public Projectile(Vector2 pos2, float angle2, float speed2, byte type2, byte damege2, bool enemy2)
        {
            Pos = pos2;
            
            Speed = speed2;
            Angle = angle2;

            type = type2;
            AssignType();

            damege = damege2;

            enemy = enemy2;

            Z = 0.99f;

            Scale = 1;
            Color = Color.White;
        }

        public override void Update()
        {
            AngleMath();
            Pos += Velocity;

            switch (type)
            {
                case 0:
                    Rotation += 10;
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
            }
        }
    }
}

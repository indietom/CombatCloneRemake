using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace CombatClone
{
    class Particle : GameObject
    {
        byte movmentType;

        Vector2 target;

        short lifeTime;
        short maxLifeTime;
        short animationOffset;

        public Particle(Vector2 pos2, Point size2, Point spriteCoords2, float angle2, float speed2, short maxLifeTime2, short maxFrame2, short maxAnimationCount2)
        {
            Pos = pos2;

            SpriteCoords = spriteCoords2;
            Size = size2;
            Color = Color.White;
            Scale = 1;

            Angle = angle2;
            Speed = speed2;

            MaxFrame = maxFrame2;
            MaxAnimationCount = maxAnimationCount2;
            if (Size.X != 32)
            {
                animationOffset = (short)(SpriteCoords.X - 1);
            }

            maxLifeTime = maxLifeTime2;

            Orgin = new Vector2(Size.X / 2, Size.Y / 2);

            Z = 0.1f;
        }

        public override void Update()
        {
            switch (movmentType)
            {
                case 0:
                    AngleMath();
                    Pos += Velocity;

                    if (SpriteCoords.Y == 168)
                    {
                        Speed = Globals.Lerp(Speed, 0, 0.05f);
                        Rotation += Speed;
                    }
                    if (SpriteCoords.Y == 34)
                    {
                        Rotation = Angle;
                    }

                    break;
            }

            if (MaxFrame != 0)
            {
                Animate();
                AnimationCount += 1;
                SpriteCoords = new Point(Frame(CurrentFrame, Size.X) + animationOffset, SpriteCoords.Y);

                destroy = (CurrentFrame >= MaxFrame - 1) ? true : destroy;
            }
            else
            {
                lifeTime += 1;
                destroy = (lifeTime >= maxLifeTime) ? true : destroy;
            }

            if (SpriteCoords.Y == 168 || SpriteCoords.Y == 34) destroy = false;

            base.Update();
        }
    }
}

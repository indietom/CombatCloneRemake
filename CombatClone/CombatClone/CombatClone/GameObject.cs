using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace CombatClone
{
    abstract class GameObject
    {
        public Vector2 Pos { get; set; }
        public Vector2 Orgin { get; set; }
        public Vector2 Velocity { get; set; }

        public Point Size { get; set; }
        public Point SpriteCoords { get; set; }

        public float Angle { get; set; }
        public float Speed { get; set; }
        public float Scale { get; set; }
        public float Z { get; set; }
        public float Rotation { get; set; }

        public Color Color { get; set; }

        public short AnimationCount { get; set; }
        public short MaxAnimationCount { get; set; }
        public short MaxFrame { get; set; }
        public short MinFrame { get; set; }
        public short CurrentFrame { get; set; }
        

        public bool destroy;

        public Rectangle Hitbox
        {
            get { return new Rectangle((int)(Pos.X - Orgin.X), (int)(Pos.Y - Orgin.Y), Size.X * (int)Scale, Size.Y * (int)Scale); }
        }

        public void Animate()
        {
            if (AnimationCount >= MaxAnimationCount)
            {
                CurrentFrame = (CurrentFrame >= MaxFrame) ? (short)0 : (short)(CurrentFrame + 1);
                AnimationCount = 0;
            }
        }

        public int Frame(int cell)
        {
            return 32 * cell + cell + 1;
        }

        public int Frame(int cell, int size)
        {
            return size * cell + cell + 1;
        }

        public void AngleMath()
        {
            Velocity = new Vector2((float)Math.Cos(Globals.DegreesToRadian(Angle)) * Speed, (float)Math.Sin(Globals.DegreesToRadian(Angle)) * Speed);
        }

        public float GetAimAngle(Vector2 target)
        {
            return Globals.RadianToDegrees((float)Math.Atan2(target.Y - Pos.Y, target.X - Pos.X));
        }

        public float GetDistance(Vector2 target)
        {
            return (float)Math.Sqrt((Pos.X - target.X) * (Pos.X - target.X) + (Pos.Y  - target.Y) * (Pos.X - target.Y));  
        }

        public virtual void Update()
        {
            if (destroy) GameObjectManager.Remove(this);
        }

        public virtual void DrawSprite(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(AssetManager.spritesheet, Pos + Globals.screenOffset, new Rectangle(SpriteCoords.X, SpriteCoords.Y, Size.X, Size.Y), Color, Globals.DegreesToRadian(Rotation), Orgin, Scale, SpriteEffects.None, Z); 
        }
    }
}

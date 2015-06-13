using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace CombatClone
{
    class Player : GameObject
    {
        GamePadState gamePad;
        GamePadState prevGamePad;

        Vector2 crossHair;

        float friction;
        float turretRotation;

        short fireRate;
        short maxFireRate;

        byte gunType;

        sbyte hp;
        sbyte maxHp;

        bool inputActive;
        bool disabeld;
        bool deadCrew;

        public Player()
        {
            Random random = new Random();

            Pos = new Vector2(320, 240);

            friction = 0.95f;
            maxHp = 3;
            hp = maxHp;

            SpriteCoords = new Point(1, 1);
            Size = new Point(32, 32);
            Orgin = new Vector2(16, 16);

            Color = new Color(random.Next(100, 255), random.Next(100, 255), random.Next(100, 255));
            Scale = 1;

            inputActive = true;

            maxFireRate = 32;
        }

        public void Movment()
        {
            Speed *= friction;

            AngleMath();

            Pos += Velocity;
        }

        public void Input()
        {
            Random random = new Random();

            if (gamePad.ThumbSticks.Right.Y >= 0.2f || gamePad.ThumbSticks.Right.Y <= -0.2 || gamePad.ThumbSticks.Right.X >= 0.2f || gamePad.ThumbSticks.Right.X <= -0.2)
                turretRotation = (float)Math.Atan2(-gamePad.ThumbSticks.Right.Y, gamePad.ThumbSticks.Right.X);

            if (gamePad.ThumbSticks.Left.X >= 0.45f) Angle += 3;
            if (gamePad.ThumbSticks.Left.X <= -0.45f) Angle -= 3;

            if (gamePad.Triggers.Right >= 0.5f) Speed += 0.2f;
            if (gamePad.Triggers.Left >= 0.5f) Speed -= 0.1f;

            if ((gamePad.ThumbSticks.Right.X >= 0.7f || gamePad.ThumbSticks.Right.X <= -0.7f || gamePad.ThumbSticks.Right.Y >= 0.7f || gamePad.ThumbSticks.Right.Y <= -0.7f) && fireRate <= 0)
            {
                if (gunType == 0)
                {
                    GameObjectManager.Add(new Projectile(Pos + new Vector2((float)Math.Cos(turretRotation) * 10, (float)Math.Sin(turretRotation) * 10), Globals.RadianToDegrees(turretRotation)+random.Next(-8, 9), (float)Math.Abs(Speed) + 10, 0, 1, false));
                }
                fireRate = 1;
            }
        }

        public override void Update()
        {
            prevGamePad = gamePad;
            gamePad = GamePad.GetState(PlayerIndex.One, GamePadDeadZone.Circular);

            if (inputActive) Input();
            Movment();

            if (fireRate >= 1)
            {
                fireRate += 1;
                if (fireRate >= maxFireRate) fireRate = 0;
            }

            crossHair = new Vector2(Globals.Lerp(crossHair.X, ((float)Math.Cos(turretRotation) * 100), 0.1f), Globals.Lerp(crossHair.Y, ((float)Math.Sin(turretRotation) * 100), 0.1f));

            Rotation = Angle;

            base.Update();
        } 

        public override void DrawSprite(SpriteBatch spriteBatch)
        {
            if (hp >= 1)
            {
                spriteBatch.Draw(AssetManager.spritesheet, Pos, new Rectangle(34, 1, 28, 20), Color, turretRotation, new Vector2(9.5f, 10), 1, SpriteEffects.None, 0.99f);
            }
            spriteBatch.Draw(AssetManager.spritesheet, Pos + crossHair, new Rectangle(100, 1, 16, 16), Color.Red, Speed, new Vector2(8, 8), 1, SpriteEffects.None, 1);
            base.DrawSprite(spriteBatch);
        }
    }
}

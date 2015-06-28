using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace CombatClone
{
    class TurretPlayer : GameObject
    {
        float turretRotation;
        
        short fireRate;

        Vector2 crossHair;

        Color crossHairColor;

        bool draw;

        public TurretPlayer()
        {
            Random random = new Random();

            Z = 0.99f;

            SpriteCoords = new Point(1, 34);
            Size = new Point(30, 16);
            Scale = 0.7f;
            Color = Color.White;
            crossHairColor = Color.Green;

            Orgin = new Vector2(8, 8);
        }

        public void Input()
        {
            GamePadState gamePad = GamePad.GetState(PlayerIndex.Two);

            Random random = new Random();

            if (gamePad.ThumbSticks.Right.Y >= 0.2f || gamePad.ThumbSticks.Right.Y <= -0.2 || gamePad.ThumbSticks.Right.X >= 0.2f || gamePad.ThumbSticks.Right.X <= -0.2)
                turretRotation = (float)Math.Atan2(-gamePad.ThumbSticks.Right.Y, gamePad.ThumbSticks.Right.X);

            if ((gamePad.ThumbSticks.Right.X >= 0.7f || gamePad.ThumbSticks.Right.X <= -0.7f || gamePad.ThumbSticks.Right.Y >= 0.7f || gamePad.ThumbSticks.Right.Y <= -0.7f) && fireRate <= 0)
            {
                GameObjectManager.Add(new Projectile(Pos + new Vector2((float)Math.Cos(turretRotation) * 20, (float)Math.Sin(turretRotation) * 20), Globals.RadianToDegrees(turretRotation) + random.Next(-8, 9), (float)Math.Abs(Speed) + 10, 0, 1, false));
                fireRate = 1;
                AssetManager.shootSound.Play(1, -1, 0);
            }
        }

        public override void Update()
        {
            Rotation = Globals.RadianToDegrees(turretRotation);

            if (fireRate >= 1) fireRate += 1;
            if (fireRate >= 32) fireRate = 0;

            foreach (Player p in GameObjectManager.gameObjects.Where(item => item is Player))
            {
                Pos = p.Pos;

                if (draw)
                {
                    Input();
                }

                draw = (p.Hp >= 1);
            }

            crossHair = new Vector2(Globals.Lerp(crossHair.X, ((float)Math.Cos(turretRotation) * 70), 0.1f), Globals.Lerp(crossHair.Y, ((float)Math.Sin(turretRotation) * 70), 0.1f));

            base.Update();
        }

        public override void DrawSprite(SpriteBatch spriteBatch)
        {
            if (draw)
            {
                base.DrawSprite(spriteBatch);
                spriteBatch.Draw(AssetManager.spritesheet, Pos + crossHair + Globals.screenOffset, new Rectangle(100, 1, 16, 16), crossHairColor, 0, new Vector2(8, 8), 1, SpriteEffects.None, 1);
            }
        }
    }
}

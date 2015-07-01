using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CombatClone
{
    class Tank : Enemy
    {
        float turretRotation;

        public Tank(Vector2 pos2, Random random)
        {
            Pos = pos2;

            MaxFireRate = 64;
            ProjectileSpeed = 5;
            ProjectileDamege = 2;

            RotateSpeed = 0.05f;

            MaxDistance = random.Next(64, 129);

            OrginalSpeed = 0.4f;
            Speed = OrginalSpeed;

            Hp = 3;

            Size = new Point(32, 32);
            SpriteCoords = new Point(1, 1);
            Scale = 1;
            Color = Color.Red;
            Orgin = new Vector2(16, 16);

            Z = 0.2f;

            Worth = 1000;
        }

        public override void Update()
        {
            foreach (Player p in GameObjectManager.gameObjects.Where(item => item is Player))
            {
                turretRotation = RotateTwoardsTarget(p.Pos);
            }

            ShootPos = new Vector2((float)Math.Cos(Globals.DegreesToRadian(turretRotation)) * 10, (float)Math.Sin(Globals.DegreesToRadian(turretRotation)) * 10);

            Shoot();
            UpdateHealth();

            MoveFoward();

            if (TouchedPlayer())
            {
                Hp = 0;
            }

            if(!stoping) Angle = turretRotation;
            Rotation = Angle;
            ShootAngle = turretRotation;

            base.Update();
        }

        public override void DrawSprite(SpriteBatch spriteBatch)
        {
            base.DrawSprite(spriteBatch);
            spriteBatch.Draw(AssetManager.spritesheet, Pos + Globals.screenOffset, new Rectangle(34, 1, 28, 20), Color, Globals.DegreesToRadian(turretRotation), new Vector2(9.5f, 10), 1, SpriteEffects.None, 0.99f);
        }
    }
}

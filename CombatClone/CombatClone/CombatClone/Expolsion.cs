using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace CombatClone
{
    class Expolsion : GameObject
    {
        int animationOffset;

        byte size;

        public bool dangerous;
        public bool hasHurtPlayer;

        public Expolsion(Vector2 pos2, byte size2, bool dangerous2)
        {
            Pos = pos2;

            size = size2;
            AssignSize();

            Scale = 1;
            Color = Color.White;

            dangerous = dangerous2;

            Z = 1;

            AssetManager.explosionSound.Play();
        }

        public override void Update()
        {
            Animate();
            AnimationCount += 1;

            SpriteCoords = new Point(animationOffset + Frame(CurrentFrame, size), SpriteCoords.Y);

            foreach (Player p in GameObjectManager.gameObjects.Where(item => item is Player))
            {
                if (dangerous && !hasHurtPlayer && p.Hitbox.Intersects(Hitbox) && !p.invisible)
                {
                    p.Hp -= 1;
                    hasHurtPlayer = true;
                }
            }

            destroy = (CurrentFrame >= MaxFrame - 1) ? true : destroy;
            base.Update();
        }

        public void AssignSize()
        {
            switch (size)
            {
                case 65:
                    Size = new Point(65, 65);
                    SpriteCoords = new Point(133, 67);
                    MaxFrame = 8;
                    MaxAnimationCount = 4;
                    break;
            }
            if (size != 32) animationOffset = SpriteCoords.X - 1;
        }
    }
}

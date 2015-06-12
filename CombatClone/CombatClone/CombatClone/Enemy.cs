using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace CombatClone
{
    abstract class Enemy : GameObject
    {
        public Vector2 ShootPos { private get; set;}

        public short FireRate { get; set; }
        public short MaxFireRate { private get; set; }
        public short BurstInterval { private get; set; }

        public byte BurstSize { private get; set; }
        public byte ProjectileType { private get; set; }
        public byte ProjectileDamege { private get; set; }

        public sbyte Hp { get; set; }

        public float ProjectileSpeed { private get; set; }
        public float ShootAngle { private get; set; }

        // All abord the OOP nightmare train
        public Projectile ProjectilePrototype 
        { 
            get { return new Projectile(Pos + ShootPos, ShootAngle, ProjectileSpeed, ProjectileType, ProjectileDamege, true); } 
        }

        public void UpdateHealth()
        {
            foreach (Projectile p in GameObjectManager.gameObjects.Where(item => item is Projectile))
            {
                if (p.Hitbox.Intersects(Hitbox))
                {
                    Hp -= (sbyte)p.Damege;
                    p.destroy = true;
                }
            }
        }

        public void Shoot()
        {
            if (BurstSize <= 0)
            {
                if (FireRate >= MaxFireRate)
                {
                    GameObjectManager.Add(ProjectilePrototype);
                }
            }
            else
            {
                for (int i = 0; i < BurstSize; i++)
                {
                    if (FireRate == MaxFireRate - i * BurstInterval)
                    {
                        GameObjectManager.Add(ProjectilePrototype);
                    }
                }
                if (FireRate >= MaxFireRate)
                {
                    FireRate = 0;
                }
            }
        }
    }
}

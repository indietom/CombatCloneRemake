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
        public Vector2 Target { get; set; }

        public short FireRate { get; set; }
        public short MaxFireRate { private get; set; }
        public short BurstInterval { private get; set; }

        public byte BurstSize { private get; set; }
        public byte ProjectileType { private get; set; }
        public byte ProjectileDamege { private get; set; }

        public sbyte Hp { get; set; }

        public float ProjectileSpeed { private get; set; }
        public float ShootAngle { private get; set; }
        public float MaxDistance { get; set; }
        public float RotateSpeed { get; set; }
        public float OrginalSpeed { get; set; }

        public bool leaveCorpse;
        public bool stoping;
        public bool destroyOnTarget;

        // All abord the OOP nightmare train
        public Projectile ProjectilePrototype 
        { 
            get { return new Projectile(Pos + ShootPos, ShootAngle, ProjectileSpeed, ProjectileType, ProjectileDamege, true); } 
        }

        public bool TouchedPlayer()
        {
            foreach (Player p in GameObjectManager.gameObjects.Where(item => item is Player))
            {
                return p.Hitbox.Intersects(Hitbox); 
            }

            return false;
        }

        public Vector2 GenerateTarget
        {
            get
            {
                Random random = new Random();

                if (Pos.Y >= 480)
                {
                    return new Vector2(random.Next(640), -100);
                }
                else if (Pos.Y <= 0)
                {
                    return new Vector2(random.Next(640), 740);
                }
                else
                {
                    if (Pos.X >= 640)
                    {
                        return new Vector2(-100, random.Next(480));
                    }
                    else if (Pos.X <= 0)
                    {
                        return new Vector2(740, random.Next(480));
                    }
                }

                return Vector2.Zero;
            }
        }

        public float RotateTwoardsTarget(Vector2 aimAtTarget)
        {
            Target = new Vector2(Globals.Lerp(Target.X, aimAtTarget.X, RotateSpeed), Globals.Lerp(Target.Y, aimAtTarget.Y, RotateSpeed));
            return GetAimAngle(Target);
        }

        public void UpdateHealth()
        {
            Random random = new Random();

            if (destroyOnTarget)
            {
                if (GetDistance(Target) <= 1)
                {
                    destroy = true;
                }
            }

            foreach (Projectile p in GameObjectManager.gameObjects.Where(item => item is Projectile))
            {
                if (p.Hitbox.Intersects(Hitbox) && !p.enemy)
                {
                    Hp -= (sbyte)p.Damege;
                    p.destroy = true;
                }
            }

            foreach (Expolsion e in GameObjectManager.gameObjects.Where(item => item is Expolsion))
            {
                if (e.dangerous && e.Hitbox.Intersects(Hitbox))
                {
                    Hp = 0;
                }
            }

            if (Hp <= 0 && !leaveCorpse)
            {
                destroy = true;

                for (int i = 0; i < 5; i++)
                {
                    GameObjectManager.Add(new Expolsion(Pos + new Vector2(-32, -32) + new Vector2(random.Next(-Size.X, Size.X+1), random.Next(-Size.Y, Size.Y+1)), 65, false));
                }
            }
        }

        public void MoveFoward()
        {
            AngleMath();
            Pos += Velocity;
        }

        public void Shoot()
        {
            if (MaxDistance > 0)
            {
                foreach (Player p in GameObjectManager.gameObjects.Where(item => item is Player))
                {
                    if (GetDistance(p.Pos) < MaxDistance)
                    {
                        FireRate += 1;
                        Speed = Globals.Lerp(Speed, 0, 0.1f);
                        stoping = true;
                    }
                    else
                    {
                        FireRate = 0;
                        Speed = Globals.Lerp(Speed, OrginalSpeed, 0.1f);
                        stoping = false;
                    }
                }
            }
            if (BurstSize <= 0)
            {
                if (FireRate >= MaxFireRate)
                {
                    GameObjectManager.Add(ProjectilePrototype);
                    FireRate = 0;
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

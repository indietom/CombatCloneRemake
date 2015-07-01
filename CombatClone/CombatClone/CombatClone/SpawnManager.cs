using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace CombatClone
{
    class SpawnManager
    {       
        const byte PLANE = 0, INFANTRY = 1, TANK = 2, APC = 3, HELICOPTER = 4, ARMORED_CAR = 5;

        byte enemyLevel;
        byte currentLevel;
        byte chanceOfSpecial;

        short[] spawnEnemyCount = new short[Globals.amountOfEnemies];
        short[] maxSpawnEnemyCount = new short[Globals.amountOfEnemies];
        short[] orginalMaxSpawnEnemyCount = new short[Globals.amountOfEnemies];

        short nextLevelCount;
        short maxNextLevelCount;

        short powerUpSpawnCount;
        short maxPowerUpSpawnCount;

        public SpawnManager()
        {
            maxPowerUpSpawnCount = 128 * 7;
            maxNextLevelCount = 265;

            currentLevel = 0;

            for (int i = 0; i < Globals.amountOfEnemies; i++)
            {
                maxSpawnEnemyCount[i] = (short)(256 + (128 *2) * (i+1));
                orginalMaxSpawnEnemyCount[i] = maxSpawnEnemyCount[i];
            }
        }

        public void LevelUpdate()
        {
            maxNextLevelCount = (short)((256 * 2) + (currentLevel * 5));

            nextLevelCount += 10;
            if (nextLevelCount >= maxNextLevelCount)
            {
                currentLevel += 1;
                nextLevelCount = 0;
            }

            for (int i = 0; i < Globals.amountOfEnemies; i++)
            {
                if (i != HELICOPTER)
                {
                    if (currentLevel <= 20) maxSpawnEnemyCount[i] = (short)(orginalMaxSpawnEnemyCount[i] - currentLevel * 5);
                    else maxSpawnEnemyCount[i] = (short)(orginalMaxSpawnEnemyCount[i] - 20 * 2);
                }
            }

            maxSpawnEnemyCount[HELICOPTER] = 128 * 17;
        }

        public void AddEnemy(byte type)
        {
            Random random = new Random();

            Vector2 tmp = Vector2.Zero;

            bool cantSpawn = true;

            // while loop mid game, what could go wrong
            while (cantSpawn)
            {
                tmp = new Vector2(random.Next(-900, 900), random.Next(-800, 800));
                if (tmp.X >= 740 || tmp.X <= -150 || tmp.Y >= 680 || tmp.Y <= -150) cantSpawn = false;
            }

            switch (type)
            {
                case PLANE:
                    GameObjectManager.gameObjects.Add(new AttackPlane(tmp, random));
                    break;
                case INFANTRY:
                    GameObjectManager.gameObjects.Add(new Infantry(tmp, random));
                    break;
                case TANK:
                    GameObjectManager.gameObjects.Add(new Tank(tmp, random));
                    break;
                case APC:
                    GameObjectManager.gameObjects.Add(new Apc(tmp));
                    break;
                case HELICOPTER:
                    if (GameObjectManager.gameObjects.Where(item => item is Helicopter).Count() == 0)
                        GameObjectManager.gameObjects.Add(new Helicopter(tmp));
                    break;
                case ARMORED_CAR:
                    GameObjectManager.gameObjects.Add(new ArmoredCar(tmp));
                    break;
            }
        }

        public void EnemySpawnUpdate()
        {
            enemyLevel = (currentLevel >= Globals.amountOfEnemies-2) ? Globals.amountOfEnemies : (byte)(2+currentLevel);

            for (int i = 0; i < enemyLevel; i++)
            {
                spawnEnemyCount[i] += 1;
                if (spawnEnemyCount[i] >= maxSpawnEnemyCount[i])
                {
                    AddEnemy((byte)i);
                    spawnEnemyCount[i] = 0;
                }
            }
        }

        public void PowerUpSpawnUpdate()
        {
            Random random = new Random();

            powerUpSpawnCount += 1;

            if (powerUpSpawnCount >= maxPowerUpSpawnCount && GameObjectManager.gameObjects.Where(item => item is PowerUp).Count() <= 2)
            {
                chanceOfSpecial = (byte)random.Next(5);

                if (chanceOfSpecial == 3)
                {
                    GameObjectManager.Add(new PowerUp(new Vector2(random.Next(16, 640 - 16), random.Next(16, 480 - 16)), (byte)random.Next(2), true));
                }
                else
                {
                    GameObjectManager.Add(new PowerUp(new Vector2(random.Next(16, 640 - 16), random.Next(16, 480 - 16)), (byte)random.Next(5), false));
                }
                powerUpSpawnCount = 0;
            }
        }

        public void Update()
        {
            if (!Globals.gameOver)
            {
                LevelUpdate();
                EnemySpawnUpdate();
                PowerUpSpawnUpdate();
            }
        }
    }
}

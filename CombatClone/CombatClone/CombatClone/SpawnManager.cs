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

        short[] spawnEnemyCount = new short[Globals.amountOfEnemies];
        short[] maxSpawnEnemyCount = new short[Globals.amountOfEnemies];

        short nextLevelCount;
        short maxNextLevelCount;

        public SpawnManager()
        {
            for (int i = 0; i < Globals.amountOfEnemies; i++)
            {
                maxSpawnEnemyCount[i] = (short)(128 + 64 * i);
            }
        }

        public void LevelUpdate()
        {

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

            Console.WriteLine(tmp);

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
                    GameObjectManager.gameObjects.Add(new Helicopter(tmp));
                    break;
                case ARMORED_CAR:
                    GameObjectManager.gameObjects.Add(new ArmoredCar(tmp));
                    break;
            }
        }

        public void EnemySpawnUpdate()
        {
            enemyLevel = Globals.amountOfEnemies;


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

        public void Update()
        {
            LevelUpdate();
            EnemySpawnUpdate();
        }
    }
}

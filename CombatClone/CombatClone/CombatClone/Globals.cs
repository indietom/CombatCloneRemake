using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CombatClone
{
    class Globals
    {
        public static byte amountOfEnemies = 6;

        // what is enums
        public static bool startScreen = true;
        public static bool gameOver;
        public static bool paused;

        static int currentHighScore;

        public static int Highscore
        {
            get
            {
                foreach (Player p in GameObjectManager.gameObjects.Where(item => item is Player))
                {
                    if (p.Score > currentHighScore)
                        currentHighScore = p.Score;
                }

                return currentHighScore;
            }
        }

        public static float RadianToDegrees(float radian)
        {
            return radian * 180 / (float)Math.PI;
        }

        public static float DegreesToRadian(float degrees)
        {
            return degrees * (float)Math.PI / 180;
        }

        public static float Lerp(float s, float e, float t)
        {
            return s + t * (e - s);
        }
    }
}

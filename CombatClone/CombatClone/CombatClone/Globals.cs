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

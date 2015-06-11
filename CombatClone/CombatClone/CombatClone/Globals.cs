using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CombatClone
{
    class Globals
    {
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

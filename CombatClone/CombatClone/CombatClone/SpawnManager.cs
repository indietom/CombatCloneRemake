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

        short[] spawnEnemyCount = new short[Globals.amountOfEnemies];
        short[] maxSpawnEnemyCount = new short[Globals.amountOfEnemies];
    }
}

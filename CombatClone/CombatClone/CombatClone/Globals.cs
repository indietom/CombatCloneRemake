using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace CombatClone
{
    class Globals
    {
        public static byte amountOfEnemies = 6;

        public static Vector2 screenOffset = Vector2.Zero;

        // what is enums
        public static bool startScreen = true;
        public static bool gameOver;
        public static bool paused;

        static int currentHighScore;

        public static Highscore[] highscores = new Highscore[10];

        public static void UpdateHighScores(Highscore highscore)
        {
            int indexToReplace = 0;

            for (int i = 0; i < highscores.Length; i++)
            {
                if (highscore.score > highscores[i].score)
                {
                    indexToReplace = i;
                    break;
                }
            }

            for (int i = 0; i < highscores.Length; i++)
            {
                Highscore[] tmps = new Highscore[];


            }
        }

        public static void ShakeScreen(float intensity)
        {
            Random random = new Random();

            Vector2 shake = new Vector2(random.Next(-1, 2) * intensity, random.Next(-1, 2) * intensity);

            screenOffset += shake;
        }

        public static void UpdateScreenOffset()
        {
            Random random = new Random();

            screenOffset = new Vector2(Lerp(screenOffset.X, 0, 0.05f), Lerp(screenOffset.Y, 0, 0.05f));

            if (GameObjectManager.gameObjects.Where(item => item is Expolsion).Count() >= 3)
            {
                ShakeScreen(random.Next(5));
            }
        }

        public static int HighscoreSimple
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

    struct Highscore
    {
        public int score;
        public string name;

        public Highscore(int score2, string name2)
        {
            score = score2;
            name = name2;
        }
    }
}

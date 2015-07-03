using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace CombatClone
{
    class ControllTyper
    {
        char[] alphabet = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'V', 'X', 'Z', '%' };

        byte currentLetter;
        byte nextLetterDelay;

        GamePadState gamePad;
        GamePadState prevGamePad;

        public bool active;

        public Vector2 Pos { get; set; }

        public ControllTyper()
        {
            Pos = new Vector2(100, 100);
        }

        public void Input()
        {
            prevGamePad = gamePad;
            gamePad = GamePad.GetState(PlayerIndex.One);

            if (nextLetterDelay <= 0)
            {
                if (gamePad.ThumbSticks.Left.Y <= -0.5f)
                {
                    if (currentLetter != (byte)(alphabet.Length - 1))
                    {
                        currentLetter += 1;
                    }
                    else
                    {
                        currentLetter = 0;
                    }
                    nextLetterDelay = 1;
                }

                if (gamePad.ThumbSticks.Left.Y >= 0.5f)
                {
                    if (currentLetter != 0)
                    {
                        currentLetter -= 1;
                    }
                    else
                    {
                        currentLetter = (byte)(alphabet.Length - 1);
                    }
                   
                    nextLetterDelay = 1;
                }
            }

            if (gamePad.ThumbSticks.Left.Y == 0) nextLetterDelay = 0;
        }

        public char letter
        {
            get
            {
                if (gamePad.IsButtonDown(Buttons.A) && !prevGamePad.IsButtonDown(Buttons.A))
                {
                    return alphabet[currentLetter];
                }
                else
                {
                    return ' ';
                }
            }
        }

        public void Update()
        {
            if (active)
            {
                Input();

                if (nextLetterDelay >= 1) nextLetterDelay += 1;
                if (nextLetterDelay >= 16) nextLetterDelay = 0;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (active)
            {
                spriteBatch.Draw(AssetManager.spritesheet, Pos - new Vector2(0, 16), new Rectangle(232, 166, 16, 8), Color.White, 0, new Vector2(8, 4), 1, SpriteEffects.None, 0);
                spriteBatch.Draw(AssetManager.spritesheet, Pos - new Vector2(0, -16), new Rectangle(232, 166, 16, 8), Color.White, 0, new Vector2(8, 4), 1, SpriteEffects.FlipVertically, 0); 
                spriteBatch.DrawString(AssetManager.bigFont, alphabet[currentLetter].ToString(), Pos, Color.White, 0, new Vector2(AssetManager.bigFont.MeasureString(alphabet[currentLetter].ToString()).X / 2, AssetManager.bigFont.MeasureString(alphabet[currentLetter].ToString()).Y / 2), 1, SpriteEffects.None, 0);
            }
        }
    }
}

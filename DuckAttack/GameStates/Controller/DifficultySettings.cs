/*
MIT License

Copyright (c) 2020

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

namespace ExoGame2D.DuckAttack.GameStates.Controller
{
    public enum DifficultyEnum
    {
        Easy = 0,
        Medium = 1,
        Hard = 2
    }

    public static class DifficultySettings
    {
        public static DifficultyParameters Easy { get; private set; }
        public static DifficultyParameters Medium { get; private set; }
        public static DifficultyParameters Hard { get; private set; }

        public static DifficultyParameters CurrentDifficulty { get; private set; }

        static DifficultySettings()
        {
            Easy = new DifficultyParameters(true, 8, 0.0f, 8);
            Medium = new DifficultyParameters(true, 6, 0.2f, 10);
            Hard = new DifficultyParameters(false, 4, 0.4f, 12);

            CurrentDifficulty = Easy;
        }

        public static void SetDifficulty(DifficultyEnum difficulty)
        {
            switch (difficulty)
            {
                case DifficultyEnum.Easy:
                    CurrentDifficulty = Easy;
                    break;
                case DifficultyEnum.Medium:
                    CurrentDifficulty = Medium;
                    break;
                case DifficultyEnum.Hard:
                    CurrentDifficulty = Hard;
                    break;
            }
        }
    }
}

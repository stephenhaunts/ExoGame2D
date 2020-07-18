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

using System;
using System.Collections.Generic;

namespace ExoGame2D.DuckAttack.GameStates.Controller
{
    public static class ListShuffle
    {
        private static Random rng = new Random();

        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }

    public class LevelBuilder
    {
        public List<LevelDefinition> Levels { get; } = new List<LevelDefinition>();

        public LevelBuilder()
        {
            Level1();
            Level2();
            Level3();
            Level4();
            Level5();
            Level6();
            Level7();
            Level8();
            Level9();
        }

        private void Level1()
        {
            DuckDefinition duck1 = new DuckDefinition()
            {
                StartX = 150,
                HorizontalVelocity = 6.0f,
                VerticalVelocity = -4.0f,
                Flip = false
            };

            DuckDefinition duck2 = new DuckDefinition()
            {
                StartX = 550,
                HorizontalVelocity = 6.0f,
                VerticalVelocity = -4.0f,
                Flip = false
            };

            DuckDefinition duck3 = new DuckDefinition()
            {
                StartX = 1200,
                HorizontalVelocity = -6.0f,
                VerticalVelocity = -4.0f,
                Flip = true
            };

            DuckDefinition duck4 = new DuckDefinition()
            {
                StartX = 1600,
                HorizontalVelocity = -6.0f,
                VerticalVelocity = -4.0f,
                Flip = true
            };

            LevelDefinition level = new LevelDefinition(duck1, duck2, duck3, duck4)
            {
                Duck1StartTimerOffset = 2000,
                Duck2StartTimerOffset = 5000,
                Duck3StartTimerOffset = 8000,
                Duck4StartTimerOffset = 10000
            };


            Levels.Add(level);

            Levels.Shuffle();
        }

        private void Level2()
        {
            DuckDefinition duck1 = new DuckDefinition()
            {
                StartX = 150,
                HorizontalVelocity = 7.0f,
                VerticalVelocity = -5.0f,              
                Flip = false
            };

            DuckDefinition duck2 = new DuckDefinition()
            {
                StartX = 550,
                HorizontalVelocity = 7.0f,
                VerticalVelocity = -5.0f,
                Flip = false
            };

            DuckDefinition duck3 = new DuckDefinition()
            {
                StartX = 1200,
                HorizontalVelocity = -7.0f,
                VerticalVelocity = -5.0f,
                Flip = true
            };

            DuckDefinition duck4 = new DuckDefinition()
            {
                StartX = 1600,
                HorizontalVelocity = -7.0f,
                VerticalVelocity = -5.0f,
                Flip = true
            };

            LevelDefinition level = new LevelDefinition(duck1, duck2, duck3, duck4)
            {
                Duck1StartTimerOffset = 2000,
                Duck2StartTimerOffset = 4000,
                Duck3StartTimerOffset = 7000,
                Duck4StartTimerOffset = 9000
            };


            Levels.Add(level);

            Levels.Shuffle();
        }

        private void Level3()
        {
            DuckDefinition duck1 = new DuckDefinition()
            {
                StartX = 200,
                HorizontalVelocity = 7.0f,
                VerticalVelocity = -5.0f,
                Flip = false
            };

            DuckDefinition duck2 = new DuckDefinition()
            {
                StartX = 330,
                HorizontalVelocity = 7.0f,
                VerticalVelocity = -5.0f,
                Flip = false
            };

            DuckDefinition duck3 = new DuckDefinition()
            {
                StartX = 1000,
                HorizontalVelocity = -7.0f,
                VerticalVelocity = -5.0f,
                Flip = true
            };

            DuckDefinition duck4 = new DuckDefinition()
            {
                StartX = 1700,
                HorizontalVelocity = -7.0f,
                VerticalVelocity = -5.0f,
                Flip = true
            };

            LevelDefinition level = new LevelDefinition(duck1, duck2, duck3, duck4)
            {
                Duck1StartTimerOffset = 2000,
                Duck2StartTimerOffset = 5000,
                Duck3StartTimerOffset = 6000,
                Duck4StartTimerOffset = 8000
            };


            Levels.Add(level);

            Levels.Shuffle();
        }

        private void Level4()
        {
            DuckDefinition duck1 = new DuckDefinition()
            {
                StartX = 200,
                HorizontalVelocity = 8.0f,
                VerticalVelocity = -5.0f,
                Flip = false
            };

            DuckDefinition duck2 = new DuckDefinition()
            {
                StartX = 330,
                HorizontalVelocity = 8.0f,
                VerticalVelocity = -5.0f,
                Flip = false
            };

            DuckDefinition duck3 = new DuckDefinition()
            {
                StartX = 1000,
                HorizontalVelocity = -8.0f,
                VerticalVelocity = -5.0f,
                Flip = true
            };

            DuckDefinition duck4 = new DuckDefinition()
            {
                StartX = 1700,
                HorizontalVelocity = -8.0f,
                VerticalVelocity = -5.0f,
                Flip = true
            };

            LevelDefinition level = new LevelDefinition(duck1, duck2, duck3, duck4)
            {
                Duck1StartTimerOffset = 2000,
                Duck2StartTimerOffset = 3000,
                Duck3StartTimerOffset = 5000,
                Duck4StartTimerOffset = 7000
            };


            Levels.Add(level);

            Levels.Shuffle();
        }

        private void Level5()
        {
            DuckDefinition duck1 = new DuckDefinition()
            {
                StartX = 200,
                HorizontalVelocity = 8.0f,
                VerticalVelocity = -6.0f,
                Flip = false
            };

            DuckDefinition duck2 = new DuckDefinition()
            {
                StartX = 330,
                HorizontalVelocity = 8.0f,
                VerticalVelocity = -6.0f,
                Flip = false
            };

            DuckDefinition duck3 = new DuckDefinition()
            {
                StartX = 1000,
                HorizontalVelocity = -8.0f,
                VerticalVelocity = -6.0f,
                Flip = true
            };

            DuckDefinition duck4 = new DuckDefinition()
            {
                StartX = 1700,
                HorizontalVelocity = -8.0f,
                VerticalVelocity = -6.0f,
                Flip = true
            };

            LevelDefinition level = new LevelDefinition(duck1, duck2, duck3, duck4)
            {
                Duck1StartTimerOffset = 2000,
                Duck2StartTimerOffset = 3000,
                Duck3StartTimerOffset = 5000,
                Duck4StartTimerOffset = 7000
            };


            Levels.Add(level);

            Levels.Shuffle();
        }

        private void Level6()
        {
            DuckDefinition duck1 = new DuckDefinition()
            {
                StartX = 200,
                HorizontalVelocity = 8.5f,
                VerticalVelocity = -6.0f,
                Flip = false
            };

            DuckDefinition duck2 = new DuckDefinition()
            {
                StartX = 330,
                HorizontalVelocity = 8.5f,
                VerticalVelocity = -6.0f,
                Flip = false
            };

            DuckDefinition duck3 = new DuckDefinition()
            {
                StartX = 1000,
                HorizontalVelocity = -8.5f,
                VerticalVelocity = -6.0f,
                Flip = true
            };

            DuckDefinition duck4 = new DuckDefinition()
            {
                StartX = 1700,
                HorizontalVelocity = -8.5f,
                VerticalVelocity = -6.0f,
                Flip = true
            };

            LevelDefinition level = new LevelDefinition(duck1, duck2, duck3, duck4)
            {
                Duck1StartTimerOffset = 2000,
                Duck2StartTimerOffset = 3000,
                Duck3StartTimerOffset = 5000,
                Duck4StartTimerOffset = 6000
            };


            Levels.Add(level);

            Levels.Shuffle();
        }

        private void Level7()
        {
            DuckDefinition duck1 = new DuckDefinition()
            {
                StartX = 200,
                HorizontalVelocity = 8.5f,
                VerticalVelocity = -6.5f,
                Flip = false
            };

            DuckDefinition duck2 = new DuckDefinition()
            {
                StartX = 330,
                HorizontalVelocity = 8.5f,
                VerticalVelocity = -6.5f,
                Flip = false
            };

            DuckDefinition duck3 = new DuckDefinition()
            {
                StartX = 1000,
                HorizontalVelocity = -8.5f,
                VerticalVelocity = -6.5f,
                Flip = true
            };

            DuckDefinition duck4 = new DuckDefinition()
            {
                StartX = 1700,
                HorizontalVelocity = -8.5f,
                VerticalVelocity = -6.5f,
                Flip = true
            };

            LevelDefinition level = new LevelDefinition(duck1, duck2, duck3, duck4)
            {
                Duck1StartTimerOffset = 2000,
                Duck2StartTimerOffset = 3000,
                Duck3StartTimerOffset = 5000,
                Duck4StartTimerOffset = 6000
            };


            Levels.Add(level);

            Levels.Shuffle();
        }

        private void Level8()
        {
            DuckDefinition duck1 = new DuckDefinition()
            {
                StartX = 200,
                HorizontalVelocity = 9.5f,
                VerticalVelocity = -7.5f,
                Flip = false
            };

            DuckDefinition duck2 = new DuckDefinition()
            {
                StartX = 330,
                HorizontalVelocity = 9.5f,
                VerticalVelocity = -7.5f,
                Flip = false
            };

            DuckDefinition duck3 = new DuckDefinition()
            {
                StartX = 1000,
                HorizontalVelocity = -9.5f,
                VerticalVelocity = -7.5f,
                Flip = true
            };

            DuckDefinition duck4 = new DuckDefinition()
            {
                StartX = 1700,
                HorizontalVelocity = -9.5f,
                VerticalVelocity = -7.5f,
                Flip = true
            };

            LevelDefinition level = new LevelDefinition(duck1, duck2, duck3, duck4)
            {
                Duck1StartTimerOffset = 2000,
                Duck2StartTimerOffset = 3000,
                Duck3StartTimerOffset = 5000,
                Duck4StartTimerOffset = 6000
            };


            Levels.Add(level);

            Levels.Shuffle();
        }

        private void Level9()
        {
            DuckDefinition duck1 = new DuckDefinition()
            {
                StartX = 200,
                HorizontalVelocity = 10.0f,
                VerticalVelocity = -8.5f,
                Flip = false
            };

            DuckDefinition duck2 = new DuckDefinition()
            {
                StartX = 330,
                HorizontalVelocity = 10.0f,
                VerticalVelocity = -8.5f,
                Flip = false
            };

            DuckDefinition duck3 = new DuckDefinition()
            {
                StartX = 1000,
                HorizontalVelocity = 10.0f,
                VerticalVelocity = -8.5f,
                Flip = true
            };

            DuckDefinition duck4 = new DuckDefinition()
            {
                StartX = 1700,
                HorizontalVelocity = 10.0f,
                VerticalVelocity = -8.5f,
                Flip = true
            };

            LevelDefinition level = new LevelDefinition(duck1, duck2, duck3, duck4)
            {
                Duck1StartTimerOffset = 2000,
                Duck2StartTimerOffset = 3000,
                Duck3StartTimerOffset = 4000,
                Duck4StartTimerOffset = 5000
            };


            Levels.Add(level);

            Levels.Shuffle();
        }
    }
}

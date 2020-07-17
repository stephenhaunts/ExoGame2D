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
        }



        private void Level1()
        {
            DuckDefinition duck1 = new DuckDefinition()
            {
                StartX = 150,
                HorizontalVelocity = 7,
                VerticalVelocity = -5,              
                Flip = false
            };

            DuckDefinition duck2 = new DuckDefinition()
            {
                StartX = 550,
                HorizontalVelocity = 7,
                VerticalVelocity = -5,
                Flip = false
            };

            DuckDefinition duck3 = new DuckDefinition()
            {
                StartX = 1200,
                HorizontalVelocity = -7,
                VerticalVelocity = -5,
                Flip = true
            };

            DuckDefinition duck4 = new DuckDefinition()
            {
                StartX = 1600,
                HorizontalVelocity = -7,
                VerticalVelocity = -5,
                Flip = true
            };

            LevelDefinition level = new LevelDefinition(duck1, duck2, duck3, duck4);
            level.Duck1StartTimerOffset = 2000;
            level.Duck2StartTimerOffset = 5000;
            level.Duck3StartTimerOffset = 7000;
            level.Duck4StartTimerOffset = 9000;


            Levels.Add(level);

            Levels.Shuffle();
        }
    }
}

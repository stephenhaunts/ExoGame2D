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
    public class DuckDefinition
    {
        public int StartX { get; set; }
        public float HorizontalVelocity { get; set; }
        public float VerticalVelocity { get; set; }
        public bool Flip { get; set; }
    }

    public class LevelDefinition
    { 
        public List<DuckDefinition> Ducks = new List<DuckDefinition>();

        public int Duck1StartTimerOffset { get; set; }
        public int Duck2StartTimerOffset { get; set; }
        public int Duck3StartTimerOffset { get; set; }
        public int Duck4StartTimerOffset { get; set; }

        public LevelDefinition(DuckDefinition duck1, DuckDefinition duck2, DuckDefinition duck3, DuckDefinition duck4)
        {
            Ducks.Add(duck1);
            Ducks.Add(duck2);
            Ducks.Add(duck3);
            Ducks.Add(duck4);
        }
    }
}

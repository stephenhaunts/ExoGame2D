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
using System.Diagnostics;
using ExoGame2D.Interfaces;
using ExoGame2D.Renderers;
using Microsoft.Xna.Framework;

namespace ExoGame2D.DuckAttack.GameActors.Hud
{
    public class BillBoard : IRenderNode
    {
        private readonly FontRender _titles;
        private int _fontY;
        private int _fontX;
        private Color _fontColor;
        public string Name { get; set; }
        private Stopwatch _counter = new Stopwatch();

        private enum BillboardStateEnum
        {
            Drop = 0,
            Rise = 1
        }

        private BillboardStateEnum _state = BillboardStateEnum.Drop;

        public BillBoard(int xOffset, Color color)
        {
            Name = "billboardd";
            _fontY = -200;
            _fontX = xOffset;
            _fontColor = color;

            _titles = new FontRender("Titles");
            _titles.LoadContent("titlescreen");
            _titles.Location = new Vector2(_fontX, _fontY);
            _titles.Shadow = true; 
        }

        public void StartBillBoard(string text)
        {
            _titles.Text = text;
            _fontY = -200;
            _state = BillboardStateEnum.Drop;
        }

        public void Draw(GameTime gameTime)
        {
            Draw(gameTime, _fontColor);
        }

        public void Draw(GameTime gametime, Color tint)
        {
            _titles.Location = new Vector2(_fontX, _fontY);
            _titles.Draw(gametime, tint);      
        }
   
        public void Update(GameTime gametime)
        {
            switch (_state)
            {
                case BillboardStateEnum.Drop:
                    _fontY += 10;
                    if (_fontY >= 200)
                    {
                        _fontY = 200; 
                        _counter.Start();
                    }

                    if (_counter.ElapsedMilliseconds == 1500)
                    {
                        _state = BillboardStateEnum.Rise;
                        _counter.Stop();
                        _counter.Reset();
                    }
                    break;

                case BillboardStateEnum.Rise:
                    _fontY -= 10;
                    if (_fontY <= -200)
                    {
                        _fontY = -200;                     
                    }
                    break;
            }
           
        }

        public ISprite GetSprite()
        {
            throw new NotImplementedException();
        }

        public bool IsAssetOfType(Type type)
        {
            return _titles.GetType().IsSubclassOf(type);
        }
    }
}

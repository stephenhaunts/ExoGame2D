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
using ExoGame2D.Interfaces;
using ExoGame2D.Renderers;
using Microsoft.Xna.Framework;

namespace ExoGame2D.DuckAttack.GameActors.Hud
{
    public class DuckIndicator : IRenderNode
    {
        public string Name { get; set; }
        private ISprite _duckhud;
        private ISprite _duckmiss;
        private ISprite _duckshot;

        private ISprite _currentSprite;

        public DuckIndicatorStateEnum State;
        private int _x;
        private int _y;

        public DuckIndicator(string name, int x)
        {
            Name = name;
            _duckhud = new Sprite();
            _duckhud.LoadContent("duckhud");

            _duckmiss = new Sprite();
            _duckmiss.LoadContent("duckhudmiss");

            _duckshot = new Sprite();
            _duckshot.LoadContent("duckhudshot");

            _currentSprite = _duckhud;
            State = DuckIndicatorStateEnum.None;

            _x = x;
            _y = 950;
            _duckhud.X = _x;
            _duckhud.Y = _y;

            _duckmiss.X = _x;
            _duckmiss.Y = _y;

            _duckshot.X = _x;
            _duckshot.Y = _y;
        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(GameTime gameTime)
        {
            Draw(gameTime, Color.White);
        }

        public void Draw(GameTime gameTime, Color tint)
        {
            _currentSprite.Draw(gameTime, tint);
        }

        public ISprite GetSprite()
        {
            return _currentSprite;
        }

        public bool IsAssetOfType(Type type)
        {
            return _currentSprite.IsAssetOfType(type);
        }
    }
}

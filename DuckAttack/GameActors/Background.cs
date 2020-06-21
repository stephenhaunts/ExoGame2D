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

namespace ExoGame2D.DuckAttack.GameActors
{
    public class Background : IRenderNode
    {
        private ISprite _background;
        public string Name { get; set; }

        public Background()
        {
            Name = "background";
            _background = new Sprite();
            _background.LoadContent("background");
        }

        public ISprite GetSprite()
        {
            return _background;
        }

        public void Draw(GameTime gameTime)
        {
            _background.Draw(gameTime); 
        }

        public void Draw(GameTime gameTime, Color tint)
        {
            throw new NotImplementedException();
        }

        public void Update(GameTime gameTime)
        {
           
        }

        public bool IsAssetOfType(Type type)
        {
            return _background.IsAssetOfType(type);
        }
    }
}

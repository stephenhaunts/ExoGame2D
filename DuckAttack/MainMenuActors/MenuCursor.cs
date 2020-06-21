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

namespace ExoGame2D.DuckAttack.MainMenuActors
{
    public class MenuCursor : IRenderNode
    {
        private ISprite _crosshair;

        public string Name { get; set; }


        public MenuCursor()
        {
            Name = "crosshair";
            _crosshair = new Sprite();
            _crosshair.LoadContent("crosshair");

            SoundEffectPlayer.LoadSoundEffect("gunsound");
        }

        public void Draw(GameTime gameTime)
        {         
            _crosshair.Draw(gameTime, Color.White);
        }

        public void Draw(GameTime gameTime, Color tint)
        {
            throw new NotImplementedException();
        }

        public ISprite GetSprite()
        {
            return _crosshair;
        }

        public void Update(GameTime gameTime)
        {
            var mouse = Engine.ScreenToWorld(new Vector2(InputHelper.MousePosition.X, InputHelper.MousePosition.Y));
            _crosshair.Location = new Vector2(mouse.X - _crosshair.Width / 2, mouse.Y - _crosshair.Height / 2);
        }

        public bool IsAssetOfType(Type type)
        {
            return _crosshair.IsAssetOfType(type);
        }
    }
}

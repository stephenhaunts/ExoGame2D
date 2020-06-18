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
using GameEngine;
using GameEngine.Interfaces;
using GameEngine.Renderers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace GameEngineTest.GameActors
{
    public class Crosshair : IRenderNode
    {
        private ISprite _crosshair;

        public string Name { get; set; }


        public Crosshair()
        {
            Name = "crosshair";
            _crosshair = new Sprite();
            _crosshair.LoadContent("crosshair");

            SoundEffectPlayer.LoadSoundEffect("gunsound");
        }

        public void Draw(GameTime gameTime)
        {
            //var mouse = Engine.ScreenToWorld(new Vector2(InputHelper.MousePosition.X, InputHelper.MousePosition.Y));

            //var source = new Vector2(0, (int)mouse.Y);
            //var dest = new Vector2((int)Engine.ScaledViewPort.X, (int)mouse.Y);

            //var source2 = new Vector2(mouse.X, 0);
            //var dest2 = new Vector2(mouse.X, Engine.ScaledViewPort.Y + 100);

            //Engine.SpriteBatch.DrawLine(source, dest, Color.GreenYellow, 1);
            //Engine.SpriteBatch.DrawLine(source2, dest2, Color.GreenYellow, 1);

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

            if (InputHelper.MouseLeftButtonPressed())
            {
               // SoundEffectPlayer.PlayOneShot("gunsound");
                Channels.PostMessage("soundeffects", new SoundEffectMessage() { SoundEffectToPlay = "gunsound" });
            }
        }

        public bool IsAssetOfType(Type type)
        {
            return _crosshair.IsAssetOfType(type);
        }
    }
}

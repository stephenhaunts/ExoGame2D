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
using ExoGameEngine.Interfaces;
using Microsoft.Xna.Framework;
using ExoGameEngine.Renderers;
using Microsoft.Xna.Framework.Graphics;

namespace ExoGameEngine.UI
{
    public class Button : UIControlBase, IRenderNode
    {       
        public string Text { get; set; }     
        public Color TextColor { get; set; }
        public Color MouseOverTextColor { get; set; }
        private SpriteFont _font;
        private IButtonHandler _handler;

        public Button(string name, IButtonHandler handler)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (handler == null)
            {
                throw new ArgumentNullException(nameof(handler));
            }

            _handler = handler;
            Name = name;
            Width = 50;
            Height = 20;
            Location = new Vector2(100, 100);
            Enabled = true;
            Visible = true;
            Text = "My Button";
            Color = Color.LightGray;
            OutlineColor = Color.Gray;
            MouseOverColor = Color.DimGray;
            TextColor = Color.Black;
            MouseOverTextColor = Color.DarkKhaki;

            DrawWindowChrome = true;
            _font = Engine.Content.Load<SpriteFont>("default");         
        }

        public void Update(GameTime gameTime)
        {
            Rectangle bounds = new Rectangle((int)Location.X, (int)Location.Y, Width, Height);

            var mouse = Engine.ScreenToWorld(new Vector2(InputHelper.MousePosition.X, InputHelper.MousePosition.Y));
            Rectangle mouseCursor = new Rectangle((int)mouse.X, (int)mouse.Y, 1, 1);

            if (bounds.Intersects(mouseCursor))
            {
                MouseOver = true;

                _handler.OnMouseOver(this);

                if (InputHelper.MouseLeftButtonPressed())
                {
                    _handler.OnMouseClick(this);
                }
            }
            else
            {
                MouseOver = false;
            }

           
        }

        public void Draw(GameTime gameTime)
        {
            Draw(gameTime, Color.White);
        }

        public void Draw(GameTime gameTime, Color tint)
        {
            if (!Visible)
            {
                return;
            }

            if (DrawWindowChrome)
            {
                if (MouseOver)
                {
                    Engine.SpriteBatch.FillRectangle(Location.X, Location.Y, Width, Height, MouseOverColor);
                }
                else
                {
                    Engine.SpriteBatch.FillRectangle(Location.X, Location.Y, Width, Height, Color);
                }

                Engine.SpriteBatch.DrawRectangle(Location, new Vector2(Width, Height), OutlineColor, 2);
            }

            if (MouseOver)
            {
                Rectangle bounds = new Rectangle((int)Location.X, (int)Location.Y, Width, Height);
                DrawString(_font, Text, bounds, AlignmentEnum.Center, MouseOverTextColor);
            }
            else
            {
                Rectangle bounds = new Rectangle((int)Location.X, (int)Location.Y, Width, Height);
                DrawString(_font, Text, bounds, AlignmentEnum.Center, TextColor);
            }
        }

        public ISprite GetSprite()
        {
            throw new NotImplementedException();
        }

        public bool IsAssetOfType(Type type)
        {
            return GetType() == type;
        }        
    }
}

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

using ExoGameEngine.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ExoGameEngine.UI
{
    public class UIControlBase : IUIControl
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public Vector2 Location { get; set; }
        public bool Enabled { get; set; }
        public bool Visible { get; set; }
        public Color Color { get; set; }
        public Color OutlineColor { get; set; }
        public Color MouseOverColor { get; set; }
        public bool DrawWindowChrome { get; set; }
        public bool MouseOver { get; set; }
        public string Name { get; set; }
        protected Texture2D _controlTexture { get; set; }
        protected string _controlTextureName;

        public string ControlTexture
        {
            get
            {
                return _controlTextureName;
            }
            set
            {
                _controlTexture = Engine.Content.Load<Texture2D>(value);
                _controlTextureName = value;
            }
        }

        protected void DrawString(SpriteFont font, string text, Rectangle bounds, AlignmentEnum align, Color color)
        {
            Vector2 size = font.MeasureString(text);
            Point pos = bounds.Center;
            Vector2 origin = size * 0.5f;

            if (align.HasFlag(AlignmentEnum.Left))
            {
                origin.X += bounds.Width / 2 - size.X / 2;
            }

            if (align.HasFlag(AlignmentEnum.Right))
            {
                origin.X -= bounds.Width / 2 - size.X / 2;
            }

            if (align.HasFlag(AlignmentEnum.Top))
            {
                origin.Y += bounds.Height / 2 - size.Y / 2;
            }

            if (align.HasFlag(AlignmentEnum.Bottom))
            {
                origin.Y -= bounds.Height / 2 - size.Y / 2;
            }

            Engine.SpriteBatch.DrawString(font, text, new Vector2(pos.X, pos.Y), color, 0, origin, 1, SpriteEffects.None, 0);
        }
    }
}

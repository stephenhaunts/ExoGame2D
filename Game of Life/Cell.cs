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
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ExoGame2D.GameOfLife
{
    public class Cell
    {
        private Point _position { get; set; }
        private Rectangle _boundingBox { get; set; }

        public bool IsAlive { get; set; }
        public int Color { get; set; }

        public Cell(Point position)
        {
            _position = position;
            _boundingBox = new Rectangle(_position.X * GameLoop.SingleCellSize, _position.Y * GameLoop.SingleCellSize, GameLoop.SingleCellSize, GameLoop.SingleCellSize);
            Color = 255;
            IsAlive = false;
        }

        public void Update()
        {

            var point = Engine.ScreenToWorld(new Vector2(InputHelper.MousePosition.X, InputHelper.MousePosition.Y));

            if (_boundingBox.Contains(new Point((int)point.X, (int)point.Y)))
            {
                if (InputHelper.MouseLeftButtonHeld())
                {
                    IsAlive = true;               
                }
                else if (InputHelper.MouseRightButtonHeld())
                {
                    IsAlive = false;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (IsAlive)
            {
                spriteBatch.Draw(GameLoop.Pixel, _boundingBox, new Color(0, Color, 0));
            }
        }
    }
}

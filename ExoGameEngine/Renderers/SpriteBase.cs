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
using Microsoft.Xna.Framework.Content;
using System;

namespace ExoGame2D.Renderers
{
    public class SpriteBase
    {
        protected Texture2D _currentTexture;
        private Vector2 _location = new Vector2(0, 0);
        private Vector2 _velocity = new Vector2(0, 0);
        protected readonly ContentManager _contentManager;
        private readonly SpriteBatch _spriteBatch;
        private float _x;
        private float _y;
        private float _velocityX;
        private float _velocityY;

        public bool IsVisible { get; set; }
        public bool IsEnabled { get; set; }

        public bool RenderBoundingBox { get; set; }

        public string Name { get; set; }

        public float X
        {
            get => _x;
            set
            {
                _x = value;
                _location.X = value;
            }
        }

        public float Y
        {
            get => _y;
            set
            {
                _y = value;
                _location.Y = value;
            }
        }

        public float VX
        {
            get => _velocityX;
            set
            {
                _velocityX = value;
                _velocity.X = value;
            }
        }

        public float VY
        {
            get => _velocityY;
            set
            {
                _velocityY = value;
                _velocity.Y = value;
            }
        }

        public Vector2 Location
        {
            get => _location;
            set
            {
                _location = value;
                X = _location.X;
                Y = _location.Y;
            }
        }

        public Vector2 Velocity
        {
            get => _velocity;
            set
            {
                _velocity = value;
                _velocityX = _velocity.X;
                _velocityY = _velocity.Y;
            }
        }

        public int Width => _currentTexture.Width;

        public int Height => _currentTexture.Height;

        public Rectangle Dimensions => _currentTexture.Bounds;

        public Rectangle BoundingBox => new Rectangle((int)X, (int)Y, _currentTexture.Width, _currentTexture.Height);

        public bool Flip { get; set; } = false;

        public SpriteBase()
        {
            _contentManager = Engine.Content;
            _spriteBatch = Engine.SpriteBatch;

            IsEnabled = true;
            IsVisible = true;
        }

        public void LoadContent(string textureName)
        {
            if (string.IsNullOrEmpty(textureName))
            {
                throw new ArgumentNullException(nameof(textureName));
            }

            _currentTexture = _contentManager.Load<Texture2D>(textureName);
        }

        protected void Update(GameTime gameTime)
        {
            if (!IsEnabled)
            {
                return;
            }

            X += VX * (gameTime.ElapsedGameTime.Milliseconds / 16);
            Y += VY * (gameTime.ElapsedGameTime.Milliseconds / 16);
        }

        protected void Draw(GameTime gameTime)
        {
            Draw(gameTime, Color.White);
        }

        protected void Draw(GameTime gameTime, Color tint)
        {
            if (_currentTexture == null)
            {
                return;
            }

            if (!IsVisible)
            {
                return;
            }

            SpriteEffects effect = Flip ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
                
            _spriteBatch.Draw(_currentTexture, _location,
                new Rectangle(0, 0, _currentTexture.Width, _currentTexture.Height), tint, 0,
                new Vector2(0, 0), 1.0f, effect, 0.0f);

            if (RenderBoundingBox)
            {
                _spriteBatch.DrawRectangle(BoundingBox, Color.Yellow, 3);
            }
        }

        protected  bool PerPixelCollision(Sprite sprite1, Sprite sprite2)
        {
            // Get Color data of each Texture
            Color[] bitsA = new Color[sprite1.Width * sprite1.Height];
            sprite1._currentTexture.GetData(bitsA);
            Color[] bitsB = new Color[sprite2.Width * sprite2.Height];
            sprite2._currentTexture.GetData(bitsB);

            // Calculate the intersecting rectangle
            int x1 = Math.Max(sprite1.BoundingBox.X, sprite2.BoundingBox.X);
            int x2 = Math.Min(sprite1.BoundingBox.X + sprite1.BoundingBox.Width, sprite2.BoundingBox.X + sprite2.BoundingBox.Width);

            int y1 = Math.Max(sprite1.BoundingBox.Y, sprite2.BoundingBox.Y);
            int y2 = Math.Min(sprite1.BoundingBox.Y + sprite1.BoundingBox.Height, sprite2.BoundingBox.Y + sprite2.BoundingBox.Height);

            // For each single pixel in the intersecting rectangle
            for (int y = y1; y < y2; ++y)
            {
                for (int x = x1; x < x2; ++x)
                {
                    // Get the color from each texture
                    Color a = bitsA[(x - sprite1.BoundingBox.X) + (y - sprite1.BoundingBox.Y) * sprite1.Width];
                    Color b = bitsB[(x - sprite2.BoundingBox.X) + (y - sprite2.BoundingBox.Y) * sprite2.Width];

                    if (a.A != 0 && b.A != 0) // If both colors are not transparent (the alpha channel is not 0), then there is a collision
                    {
                        return true;
                    }
                }
            }
            // If no collision occurred by now, we're clear.
            return false;
        }

        protected bool PerPixelCollision(Sprite sprite1, AnimatedSprite sprite2)
        {
            // Get Color data of each Texture
            Color[] bitsA = new Color[sprite1.Width * sprite1.Height];
            sprite1._currentTexture.GetData(bitsA);
            Color[] bitsB = new Color[sprite2.Width * sprite2.Height];
            sprite2._currentTexture.GetData(bitsB);

            // Calculate the intersecting rectangle
            int x1 = Math.Max(sprite1.BoundingBox.X, sprite2.BoundingBox.X);
            int x2 = Math.Min(sprite1.BoundingBox.X + sprite1.BoundingBox.Width, sprite2.BoundingBox.X + sprite2.BoundingBox.Width);

            int y1 = Math.Max(sprite1.BoundingBox.Y, sprite2.BoundingBox.Y);
            int y2 = Math.Min(sprite1.BoundingBox.Y + sprite1.BoundingBox.Height, sprite2.BoundingBox.Y + sprite2.BoundingBox.Height);

            // For each single pixel in the intersecting rectangle
            for (int y = y1; y < y2; ++y)
            {
                for (int x = x1; x < x2; ++x)
                {
                    // Get the color from each texture
                    Color a = bitsA[(x - sprite1.BoundingBox.X) + (y - sprite1.BoundingBox.Y) * sprite1.Width];
                    Color b = bitsB[(x - sprite2.BoundingBox.X) + (y - sprite2.BoundingBox.Y) * sprite2.Width];

                    if (a.A != 0 && b.A != 0) // If both colors are not transparent (the alpha channel is not 0), then there is a collision
                    {
                        return true;
                    }
                }
            }
            // If no collision occurred by now, we're clear.
            return false;
        }


    }
}

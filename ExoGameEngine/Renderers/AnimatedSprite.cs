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
using ExoGame2D.Interfaces;
using System;
using System.Collections.Generic;

namespace ExoGame2D.Renderers
{
    public class AnimatedSprite : SpriteBase, ISprite
    {
        public AnimationTypeEnum AnimationType { get; set; }
        private List<Texture2D> _animationFrames = new List<Texture2D>();
        public AnimationPlayingStateEnum PlayState { get; private set; }
        public int AnimationSpeed { get; set; }
        private int _currentFrame = 0;
        private bool _pingPong = false;
        private int _internalTimer = 0;

        public AnimatedSprite() : base()
        {
            AnimationType = AnimationTypeEnum.Linear;
            PlayState = AnimationPlayingStateEnum.Stopped;
            AnimationSpeed = 10;
        }

        public void LoadFrameTexture(string textureName)
        {
            if (string.IsNullOrEmpty(textureName))
            {
                throw new ArgumentNullException(nameof(textureName));
            }

            _animationFrames.Add(_contentManager.Load<Texture2D>(textureName));

            _currentFrame = 0;
            _currentTexture = _animationFrames[_currentFrame];
        }

        public void ResetAnimation()
        {
            _currentFrame = 0;
            _currentTexture = _animationFrames[_currentFrame];
            _pingPong = false;
        }

        public void NextFrame()
        {
            switch (AnimationType)
            {
                case AnimationTypeEnum.Linear:
                    if (_currentFrame < _animationFrames.Count - 1)
                    {
                        _currentFrame++;
                        _currentTexture = _animationFrames[_currentFrame];
                    }
                    else
                    {
                        _currentFrame = 0;
                        _currentTexture = _animationFrames[_currentFrame];
                    }
                    break;

                case AnimationTypeEnum.PingPong:
                    switch (_pingPong)
                    {
                        case false:
                            if (_currentFrame < _animationFrames.Count - 1)
                            {
                                _currentFrame++;
                                _currentTexture = _animationFrames[_currentFrame];
                            }
                            else
                            {
                                _pingPong = true;
                            }
                            break;
                        case true:
                            if (_currentFrame > 0)
                            {
                                _currentFrame--;
                                _currentTexture = _animationFrames[_currentFrame];
                            }
                            else
                            {
                                _pingPong = false;
                            }
                            break;
                    }                 
                    break;
            }          
        }

        public bool CollidesWith(ISprite sprite)
        {
            return PerPixelCollision((Sprite)sprite, this);
        }

        public void Play()
        {
            PlayState = AnimationPlayingStateEnum.Playing;
        }

        public void Stop()
        {
            PlayState = AnimationPlayingStateEnum.Stopped;
        }    

        public new void Update(GameTime gameTime)
        {
            if (PlayState == AnimationPlayingStateEnum.Playing)
            {
                _internalTimer++;

                if (_internalTimer >= AnimationSpeed)
                {
                    NextFrame();
                    _internalTimer = 0;
                }
            }

            base.Update(gameTime);
        }

        public new void Draw(GameTime gameTime)
        {
            base.Draw(gameTime, Color.White);
        }

        public new void Draw(GameTime gameTime, Color tint)
        {          
            base.Draw(gameTime, tint);
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

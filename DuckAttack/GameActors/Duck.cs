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
using ExoGame2D;
using ExoGame2D.Interfaces;
using ExoGame2D.Renderers;
using ExoGame2D.SceneManagement;
using ExoGame2D.DuckAttack.Messages;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace ExoGame2D.DuckAttack.GameActors
{
    public enum DuckStateEnum
    {
        Start = 0,
        Fying,
        Dead,
        FlyAway
    }

    public class Duck : IRenderNode
    {
        private AnimatedSprite _duck;
        private Sprite _duckDeath;
        public Rectangle Crosshair { get; set; }
        public string Name { get; set; }
        public DuckStateEnum State { get; set; }
        private Stopwatch _duckClock = new Stopwatch();

        public Duck(string name, int x, int y)
        {
            Name = name;

            _duckDeath = new Sprite("DuckDeath");
            _duckDeath.LoadContent("DuckDeath");

            _duck = new AnimatedSprite()
            {
                RenderBoundingBox = false,
                Name = name
            };

            _duck.LoadFrameTexture("DuckFrame1");
            _duck.LoadFrameTexture("DuckFrame2");
            _duck.LoadFrameTexture("DuckFrame3");

            _duck.Velocity = new Vector2(7, 5);
            _duck.Flip = false;
            _duck.Location = new Vector2(x, y);
            _duck.IsVisible = true;
            _duck.IsEnabled = true;
            _duck.AnimationType = AnimationTypeEnum.PingPong;
            _duck.AnimationSpeed = 5;
            _duck.Play();
            State = DuckStateEnum.Start;

            Channels.AddNewChannel("score");

            SoundEffectPlayer.LoadSoundEffect("scream");
        }


        public Duck(string name, int x, bool flip, int vx, int vy)
        {
            Name = name;

            _duckDeath = new Sprite("DuckDeath");
            _duckDeath.LoadContent("DuckDeath");

            _duck = new AnimatedSprite()
            {
                RenderBoundingBox = false,
                Name = name
            };

            _duck.LoadFrameTexture("DuckFrame1");
            _duck.LoadFrameTexture("DuckFrame2");
            _duck.LoadFrameTexture("DuckFrame3");

            _duck.Velocity = new Vector2(vx, vy);
            _duck.Flip = flip;
            _duck.Location = new Vector2(x, 900);
            _duck.IsVisible = true;
            _duck.IsEnabled = true;
            _duck.AnimationType = AnimationTypeEnum.PingPong;
            _duck.AnimationSpeed = 5;
            _duck.Play();
            State = DuckStateEnum.Start;

            Channels.AddNewChannel("score");

            SoundEffectPlayer.LoadSoundEffect("scream");
        }

        public ISprite GetSprite()
        {
            return _duck;
        }

        public void Update(GameTime gameTime)
        {
            switch (State)
            {
                case DuckStateEnum.Start:
                    break;

                case DuckStateEnum.Fying:
                    _duck.Update(gameTime);
                    _duckClock.Start();
                    if (_duckClock.ElapsedMilliseconds > 1000)
                    {
                        BoundDuckOffWalls();
                    }

                    if (_duckClock.ElapsedMilliseconds > 6000)
                    {
                        State = DuckStateEnum.FlyAway;
                    }

                    // Shoot the duck
                    if ((InputHelper.MouseLeftButtonPressed()) && (CollisionManager.IsCollision("crosshair", Name)))
                    {
                        // Post message to the score board.
                        ScoreMessage message = new ScoreMessage() { ScoreIncrement = 10 };
                        Channels.PostMessage("score", message);

                        Channels.PostMessage("soundeffects", new SoundEffectMessage() { SoundEffectToPlay = "scream" });
                        State = DuckStateEnum.Dead;
                    }
                    break;

                case DuckStateEnum.Dead:
                    // Engine.CurrentScene.RemoveSpriteFromLayer(RenderLayerEnum.LAYER2, this);
                    _duckDeath.X = _duck.X;
                    _duckDeath.Y = _duck.Y;
                      
                    break;

                case DuckStateEnum.FlyAway:
                    _duck.Update(gameTime);

                    // Shoot the duck
                    if ((InputHelper.MouseLeftButtonPressed()) && (CollisionManager.IsCollision("crosshair", Name)))
                    {                      
                        // Post message to the score board.
                        ScoreMessage message = new ScoreMessage() { ScoreIncrement = 10 };
                        Channels.PostMessage("score", message);

                        Channels.PostMessage("soundeffects", new SoundEffectMessage() { SoundEffectToPlay = "scream" });
                        State = DuckStateEnum.Dead;
                    }
                    break;

            }
            
        }

        private void BoundDuckOffWalls()
        {
            if (_duck.X + _duck.Width > Engine.ScaledViewPort.X)
            {
                _duck.VX = -_duck.VX;
                _duck.Flip = true;
            }

            if (_duck.X < 0)
            {
                _duck.VX = -_duck.VX;
                _duck.Flip = false;
            }

            if (_duck.Y + (_duck.Height) > Engine.ScaledViewPort.Y - 200)
            {
                _duck.VY = -_duck.VY;
            }

            if (_duck.Y < 0)
            {
                _duck.VY = -_duck.VY;
            }
        }

        public void Draw(GameTime gameTime)
        {
            switch (State)
            {
                case DuckStateEnum.Start:
                    break;

                case DuckStateEnum.Fying:
                    if (CollisionManager.IsCollision("crosshair", Name))
                    {
                        _duck.Draw(gameTime, new Color(100, 100, 100, 255));
                    }
                    else
                    {
                        _duck.Draw(gameTime, new Color(255, 255, 255, 255));
                    }
                    break;

                case DuckStateEnum.FlyAway:
                    break;

                case DuckStateEnum.Dead:
                    _duckDeath.Draw(gameTime, new Color(255, 255, 255, 255));
                    break;
            }


        }

        public void Draw(GameTime gameTime, Color tint)
        {
            throw new NotImplementedException();
        }

        public bool IsAssetOfType(Type type)
        {
            return _duck.IsAssetOfType(type);
        }
    }
}

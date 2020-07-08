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
using ExoGame2D.DuckAttack.Messages;
using Microsoft.Xna.Framework;
using System.Diagnostics;
using ExoGame2D.DuckAttack.GameActors.Hud;

namespace ExoGame2D.DuckAttack.GameActors
{
    public class Duck : IRenderNode
    {
        private AnimatedSprite _duck;
        private Sprite _duckDeath;
        private Sprite _duckDive;

        public Rectangle Crosshair { get; set; }
        public string Name { get; set; }
        public DuckStateEnum State { get; set; }
        private Stopwatch _duckClock = new Stopwatch();
        private int _deathCounter = 0;
        private int _duckFlashCounter = 0;

        public Duck(string name, int x, int y)
        {
            Name = name;

            _duckDeath = new Sprite("DuckDeath");
            _duckDeath.LoadContent("DuckDeath");

            _duckDive = new Sprite("DuckDive");
            _duckDive.LoadContent("DuckDive");

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
          
            SoundEffectPlayer.LoadSoundEffect("death");
            SoundEffectPlayer.LoadSoundEffect("quaks");
            SoundEffectPlayer.LoadSoundEffect("falling");
            SoundEffectPlayer.LoadSoundEffect("flapping");
        }


        public Duck(string name, int x, bool flip, int vx, int vy)
        {
            Name = name;

            _duckDeath = new Sprite("DuckDeath");
            _duckDeath.LoadContent("DuckDeath");

            _duckDive = new Sprite("DuckDive");
            _duckDive.LoadContent("DuckDive");

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

            SoundEffectPlayer.LoadSoundEffect("death");
            SoundEffectPlayer.LoadSoundEffect("quaks");
            SoundEffectPlayer.LoadSoundEffect("falling");
            SoundEffectPlayer.LoadSoundEffect("flapping");
        }

        public ISprite GetSprite()
        {
            return _duck;
        }


        private bool _playDuckSound = false;
        private bool _playFallingSound = false;

        public void Update(GameTime gameTime)
        {
            switch (State)
            {
                case DuckStateEnum.Start:
                    break;

                case DuckStateEnum.Fying:
                    _duck.Update(gameTime);
                    _duckClock.Start();

                    if (!_playDuckSound)
                    {
                        Channels.PostMessage("soundeffects", new SoundEffectMessage() { SoundEffectToPlay = "quaks" });
                        Channels.PostMessage("soundeffects", new SoundEffectMessage() { SoundEffectToPlay = "flapping" });

                        _playDuckSound = true;
                    }

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
                        if (Hud.Hud.NumShotsLeft > 0)
                        {
                            // Post message to the score board.
                            ScoreMessage message = new ScoreMessage() { ScoreIncrement = 10 };
                            Channels.PostMessage("score", message);

                            DuckHitMessage duckHitMessage = new DuckHitMessage() { State = DuckIndicatorStateEnum.Hit };
                            Channels.PostMessage("duckhit", duckHitMessage);

                            Channels.PostMessage("soundeffects", new SoundEffectMessage() { SoundEffectToPlay = "death" });
                            State = DuckStateEnum.Dead;
                        }
                    }
                    break;

                case DuckStateEnum.Dead:
                    _duckDeath.X = _duck.X;
                    _duckDeath.Y = _duck.Y;

                    _duckDive.X = _duck.X;
                    _duckDive.Y = _duck.Y;

                    break;

                case DuckStateEnum.FlyAway:
                    _duck.Update(gameTime);

                    if (DuckOutOfBounds(_duck))
                    {
                        State = DuckStateEnum.Finished;
                        DuckHitMessage duckHitMessage = new DuckHitMessage() { State = DuckIndicatorStateEnum.Miss };
                        Channels.PostMessage("duckhit", duckHitMessage);
                    }

                    // Shoot the duck
                    if ((InputHelper.MouseLeftButtonPressed()) && (CollisionManager.IsCollision("crosshair", Name)))
                    {
                        if (Hud.Hud.NumShotsLeft > 0)
                        {
                            // Post message to the score board.
                            ScoreMessage message = new ScoreMessage() { ScoreIncrement = 10 };
                            Channels.PostMessage("score", message);

                            DuckHitMessage duckHitMessage = new DuckHitMessage() { State = DuckIndicatorStateEnum.Hit };
                            Channels.PostMessage("duckhit", duckHitMessage);

                            Channels.PostMessage("soundeffects", new SoundEffectMessage() { SoundEffectToPlay = "death" });
                            State = DuckStateEnum.Dead;
                        }
                    }
                    break;

                case DuckStateEnum.Dive:
                    _duckDive.Update(gameTime);
                    _duckDive.Velocity = new Vector2(0, 15);

                    if (!_playFallingSound)
                    {
                        Channels.PostMessage("soundeffects", new SoundEffectMessage() { SoundEffectToPlay = "falling" });
                        _playFallingSound = true;
                    }

                    if (DuckOutOfBounds(_duckDive))
                    {
                        State = DuckStateEnum.Finished;
                    }
                    break;

                case DuckStateEnum.Finished:
                    
                    break;

            }
        }

        private bool DuckOutOfBounds(ISprite sprite)
        {
            if (sprite.X + sprite.Width > Engine.ScaledViewPort.X + 100)
            {
                return true;
            }

            if (sprite.X < -100)
            {
                return true;
            }

            if (sprite.Y + (sprite.Height) > Engine.ScaledViewPort.Y )
            {
                return true;
            }

            if (sprite.Y < -100)
            {
                return true;
            }

            return false;
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
                    if (CollisionManager.IsCollision("crosshair", Name))
                    {
                        _duck.Draw(gameTime, new Color(100, 100, 100, 255));
                    }
                    else
                    {
                        _duck.Draw(gameTime, new Color(255, 255, 255, 255));
                    }
                    break;

                case DuckStateEnum.Dead:
                    _deathCounter++;

                    if (_deathCounter < 5)
                    {
                        _duckDeath.Draw(gameTime, new Color(255, 255, 255, 255));
                    }
                    else
                    {
                        _duckDeath.Draw(gameTime, new Color(150, 150, 150, 255));
                    }

                    if (_deathCounter >= 10)
                    {
                        _deathCounter = 0;
                        _duckFlashCounter++;
                    }

                    if (_duckFlashCounter == 5)
                    {
                        State = DuckStateEnum.Dive;
                    }
                    break;

                case DuckStateEnum.Dive:
                    _duckDive.Draw(gameTime, new Color(255, 255, 255, 255));
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

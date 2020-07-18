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
using ExoGame2D.SceneManagement;
using ExoGame2D.DuckAttack.GameActors;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using ExoGame2D.DuckAttack.GameActors.Hud;
using ExoGame2D.DuckAttack.GameStates.Controller;

namespace ExoGame2D.DuckAttack.GameStates
{
    public class PlayingGameState : IGameState
    {
        private Scene _scene = new Scene();
        private Duck _duck;
        private Duck _duck2;
        private Duck _duck3;
        private Duck _duck4;
        private Background _background;
        private Crosshair _crosshair;
        private FrameCounter _frameCounter = new FrameCounter();
        private FontRender _fps;
        private readonly ScoreBoard _score;
        private Stopwatch _gameClock = new Stopwatch();
        private LevelController _levelController;
        private Hud _hud;
        private bool Round1Triggered = false;
        private bool Round2Triggered = false;
        private bool Round3Triggered = false;
        private int _hudResetOffsetCounter = 0;
        private BillBoard _billboard = new BillBoard(500, Color.Yellow);


        public PlayingGameState()
        {
            _crosshair = new Crosshair();

            _background = new Background();
            _fps = new FontRender("fps counter");
            _fps.LoadContent("default");
            _fps.Location = new Vector2(40, Engine.ScaledViewPort.Y - 50);

            _score = new ScoreBoard("score board");
            _hud = new Hud("Hud");
            _levelController = new LevelController(new LevelBuilder().Levels);

            NextLevel();



            _scene.AddSpriteToLayer(RenderLayerEnum.LAYER4, _hud);
            _scene.AddSpriteToLayer(RenderLayerEnum.LAYER1, _background);
            _scene.AddSpriteToLayer(RenderLayerEnum.LAYER4, _fps);
            _scene.AddSpriteToLayer(RenderLayerEnum.LAYER4, _score);
            _scene.AddSpriteToLayer(RenderLayerEnum.LAYER4, _billboard);
            _scene.AddSpriteToLayer(RenderLayerEnum.LAYER5, _crosshair);

            Channels.AddNewChannel("score");
            Channels.AddNewChannel("duckhit");
            Channels.AddNewChannel("gunfired");

            _billboard.StartBillBoard("Level " + _levelController.LevelNumber);
        }

        private void NextLevel()
        {
            var level = _levelController.CurrentLevel;


            if (_hud.NumberOfDucksShot == 12)
            {
                _hud.ResetDuckCounter();
                _levelController.NextLevel();
                level = _levelController.CurrentLevel;
                _billboard.StartBillBoard("Level " + _levelController.LevelNumber);
            }

            level.Ducks.Shuffle();

            _duck  = new Duck("duck", level.Ducks[0].StartX, level.Ducks[0].Flip, level.Ducks[0].HorizontalVelocity, level.Ducks[0].VerticalVelocity);
            _duck2 = new Duck("duck2", level.Ducks[1].StartX, level.Ducks[1].Flip, level.Ducks[1].HorizontalVelocity, level.Ducks[1].VerticalVelocity);
            _duck3 = new Duck("duck3", level.Ducks[2].StartX, level.Ducks[2].Flip, level.Ducks[2].HorizontalVelocity, level.Ducks[2].VerticalVelocity);
            _duck4 = new Duck("duck4", level.Ducks[3].StartX, level.Ducks[3].Flip, level.Ducks[3].HorizontalVelocity, level.Ducks[3].VerticalVelocity);

            _scene.RemoveSpriteFromLayer(RenderLayerEnum.LAYER2, _duck);
            _scene.RemoveSpriteFromLayer(RenderLayerEnum.LAYER2, _duck2);
            _scene.RemoveSpriteFromLayer(RenderLayerEnum.LAYER2, _duck3);
            _scene.RemoveSpriteFromLayer(RenderLayerEnum.LAYER2, _duck4);

            CollisionManager.RemoveSpriteToCollisionManager(_duck.Name);
            CollisionManager.RemoveSpriteToCollisionManager(_duck2.Name);
            CollisionManager.RemoveSpriteToCollisionManager(_duck3.Name);
            CollisionManager.RemoveSpriteToCollisionManager(_duck4.Name);

            _scene.AddSpriteToLayer(RenderLayerEnum.LAYER2, _duck);
            _scene.AddSpriteToLayer(RenderLayerEnum.LAYER2, _duck2);
            _scene.AddSpriteToLayer(RenderLayerEnum.LAYER2, _duck3);
            _scene.AddSpriteToLayer(RenderLayerEnum.LAYER2, _duck4);

            _gameClock.Reset();
            _gameClock.Start();

            Round1Triggered = false;
            Round2Triggered = false;
            Round3Triggered = false;

            _hud.ResetGun();
        }

        public void Remove()
        {
            CollisionManager.RemoveAll();
        }

        public void Draw(GameTime gametime)
        {
            Draw(gametime, Color.White);
        }

        public void Draw(GameTime gametime, Color tint)
        {
            _frameCounter.Update((float)gametime.ElapsedGameTime.TotalSeconds);

            Engine.BeginRender(_scene);

            _scene.RenderScene(gametime);

            _fps.Text = "FPS - " + Math.Round(_frameCounter.AverageFramesPerSecond);

            Engine.EndRender();
        }

        public void Update(GameTime gametime)
        {
            if (InputHelper.KeyPressed(Keys.Escape))
            {
                Engine.GameState.CurrentState.Remove();
                Engine.GameState.Register("Playing", new PlayingGameState());

                Engine.GameState.Register("MainMenu", new MainMenu());
                Engine.GameState.ChangeState("MainMenu");
            }

            if ((_hud.NumberOfDucksShot == 4) && (!Round1Triggered))
            {
                _hudResetOffsetCounter++;

                if (_hudResetOffsetCounter == 50)
                {
                    NextLevel();
                    Round1Triggered = true;
                    _hudResetOffsetCounter = 0;
                }
            }

            if ((_hud.NumberOfDucksShot == 8) && (!Round2Triggered))
            {
                _hudResetOffsetCounter++;
                if (_hudResetOffsetCounter == 50)
                {
                    NextLevel();
                    Round2Triggered = true;
                    _hudResetOffsetCounter = 0;
                }
            }

            if ((_hud.NumberOfDucksShot == 12) && (!Round3Triggered))
            {
                _hudResetOffsetCounter++;

                if (_hudResetOffsetCounter == 50)
                {
                    NextLevel();
                    Round3Triggered = true;
                    _hudResetOffsetCounter = 0;
                }
            }

            var level = _levelController.CurrentLevel;

            if (_gameClock.ElapsedMilliseconds > level.Duck1StartTimerOffset)
            {
                if (_duck.State == DuckStateEnum.Start)
                {
                    _duck.State = DuckStateEnum.Fying;
                }
            }

            if (_gameClock.ElapsedMilliseconds > level.Duck2StartTimerOffset)
            {
                if (_duck2.State == DuckStateEnum.Start)
                {
                    _duck2.State = DuckStateEnum.Fying;
                }
            }

            if (_gameClock.ElapsedMilliseconds > level.Duck3StartTimerOffset)
            {
                if (_duck3.State == DuckStateEnum.Start)
                {
                    _duck3.State = DuckStateEnum.Fying;
                }
            }

            if (_gameClock.ElapsedMilliseconds > level.Duck4StartTimerOffset)
            {
                if (_duck4.State == DuckStateEnum.Start)
                {
                    _duck4.State = DuckStateEnum.Fying;
                }
            }

            _scene.UpdateGameLogic(gametime);
        }
    }
}

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
using ExoGameEngine.Renderers;
using ExoGameEngine.SceneManagement;
using ExoGameEngine.DuckAttack.GameActors;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace ExoGameEngine.DuckAttack.GameStates
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

        public PlayingGameState()
        {
            _crosshair = new Crosshair();
            _duck = new Duck("duck", 10, 10);
            _duck2 = new Duck("duck2", 800, 600);
            _duck3 = new Duck("duck3", 1000, 300);
            _duck4 = new Duck("duck4", 150, 600);

            _background = new Background();
            _fps = new FontRender("fps counter");
            _fps.LoadContent("default");
            _fps.Location = new Vector2(40, Engine.ScaledViewPort.Y - 50);

            _score = new ScoreBoard("score board");
            _scene.AddSpriteToLayer(RenderLayerEnum.LAYER1, _background);
            _scene.AddSpriteToLayer(RenderLayerEnum.LAYER2, _duck);
            _scene.AddSpriteToLayer(RenderLayerEnum.LAYER2, _duck2);
            _scene.AddSpriteToLayer(RenderLayerEnum.LAYER2, _duck3);
            _scene.AddSpriteToLayer(RenderLayerEnum.LAYER2, _duck4);
            _scene.AddSpriteToLayer(RenderLayerEnum.LAYER4, _fps);
            _scene.AddSpriteToLayer(RenderLayerEnum.LAYER4, _score);
            _scene.AddSpriteToLayer(RenderLayerEnum.LAYER5, _crosshair);
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
                
            _scene.UpdateGameLogic(gametime);
        }
    }
}

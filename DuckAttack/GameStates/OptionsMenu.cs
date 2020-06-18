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
using GameEngine;
using GameEngine.Interfaces;
using GameEngine.Renderers;
using GameEngine.SceneManagement;
using GameEngine.UI;
using GameEngineTest.GameActors;
using GameEngineTest.MainMenuActors;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GameEngineTest.GameStates
{
    public class OptionsMenu : IGameState
    {
        private readonly Scene _scene = new Scene();
        
        private readonly Background _background;
        private readonly MenuCursor _crosshair;
        private readonly FrameCounter _frameCounter = new FrameCounter();
        private readonly FontRender _titles;
        private int _fontY;

        private UIContainer _container;
        private IRenderNode _backToMainMenu;

        public OptionsMenu()
        {
            _crosshair = new MenuCursor();
            _background = new Background();

            _titles = new FontRender("Titles");
            _titles.LoadContent("titlescreen");
            _titles.Location = new Vector2(150, _fontY);
            _titles.Shadow = true;


            _container = new UIContainer("OptionsMenu");

            int startYPosition = 600;

            _backToMainMenu = new Button("ExitToMainMenu", new ExitToMainMenuButtonHandler())
            {
                Width = 400,
                Height = 70,
                Location = new Vector2(750, startYPosition),
                Text = "<-- Exit to Main Menu",
                DrawWindowChrome = true
            };        

            _container.AddControl(_backToMainMenu);
          
            _scene.AddSpriteToLayer(RenderLayerEnum.LAYER1, _background);
            _scene.AddSpriteToLayer(RenderLayerEnum.LAYER2, _titles);
            _scene.AddSpriteToLayer(RenderLayerEnum.LAYER5, _crosshair);
            _scene.AddSpriteToLayer(RenderLayerEnum.LAYER4, _container);
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

            _titles.Text = "Duck Attack";

            _titles.Location = new Vector2(150, 200);

            _scene.RenderScene(gametime);

            Engine.EndRender();
        }

        public void Update(GameTime gametime)
        {
            //if (InputHelper.MouseLeftButtonPressed())
            //{
            //    Engine.GameState.CurrentState.Remove();
            //    Engine.GameState.Register("Playing", new PlayingGameState());
            //    Engine.GameState.ChangeState("Playing");
            //}

            if (InputHelper.KeyPressed(Keys.Escape))
            {
                Engine.Exit();
            }

            _scene.UpdateGameLogic(gametime);
        }
    }
}

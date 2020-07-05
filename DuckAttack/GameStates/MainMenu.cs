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
using ExoGame2D.Interfaces;
using ExoGame2D.Renderers;
using ExoGame2D.SceneManagement;
using ExoGame2D.UI;
using ExoGame2D.DuckAttack.GameActors;
using ExoGame2D.DuckAttack.MainMenuActors;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace ExoGame2D.DuckAttack.GameStates
{
    public class MainMenu : IGameState
    {
        private readonly Scene _scene = new Scene();
        
        private readonly Background _background;
        private readonly MenuCursor _crosshair;
        private readonly FrameCounter _frameCounter = new FrameCounter();
        private readonly FontRender _titles;
        private int _fontY;

        private UIContainer _container;
        private IRenderNode _playGameButton;
        private IRenderNode _optionsButton;
        private IRenderNode _exitButton;

        public MainMenu()
        {
            _crosshair = new MenuCursor();
            _background = new Background();

            MusicPlayer.LoadMusic("banjo");

            _fontY = -200;
            _titles = new FontRender("Titles");
            _titles.LoadContent("titlescreen");
            _titles.Location = new Vector2(150, _fontY);
            _titles.Shadow = true;


            _container = new UIContainer("MainMenu");

            int startYPosition = 450;

            _playGameButton = new Button("PlayGameButton", new NewGameButtonHandler())
            {
                Width = 500,
                Height = 70,
                Location = new Vector2(700, startYPosition),
                Text = "Play Duck Attack",
                DrawWindowChrome = true,
                ControlTexture = "ButtonBackground"
            };

            startYPosition += 80;

            _optionsButton = new Button("OptionsGameButton", new OptionsButtonHandler())
            {
                Width = 500,
                Height = 70,
                Location = new Vector2(700, startYPosition),
                Text = "Options",
                DrawWindowChrome = true,
                ControlTexture = "ButtonBackground"
            };

            startYPosition += 80;

            _exitButton = new Button("ExitGameButton", new ExitButtonHandler())
            {
                Width = 500,
                Height = 70,
                Location = new Vector2(700, startYPosition),
                Text = "Exit Game",
                DrawWindowChrome = true,
                ControlTexture = "ButtonBackground"
            };

            _container.AddControl(_playGameButton);
            _container.AddControl(_optionsButton);
            _container.AddControl(_exitButton);

            _scene.AddSpriteToLayer(RenderLayerEnum.LAYER1, _background);
            _scene.AddSpriteToLayer(RenderLayerEnum.LAYER2, _titles);
            _scene.AddSpriteToLayer(RenderLayerEnum.LAYER5, _crosshair);
            _scene.AddSpriteToLayer(RenderLayerEnum.LAYER4, _container);

            MusicPlayer.Play("banjo");
            MusicPlayer.Looped = true;
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

            _fontY += 10;
            if (_fontY >= 200)
            {
                _fontY = 200;
            }

            _titles.Location = new Vector2(150, _fontY);

            _scene.RenderScene(gametime);

            Engine.EndRender();
        }

        public void Update(GameTime gametime)
        {
            if (InputHelper.KeyPressed(Keys.Escape))
            {
                Engine.Exit();
            }

            _scene.UpdateGameLogic(gametime);
        }
    }
}

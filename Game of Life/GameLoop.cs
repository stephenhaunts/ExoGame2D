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
using ExoGame2D.SceneManagement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ExoGame2D.GameOfLife
{
    public class GameLoop : Game
    {
        public const bool ALLOW_CELL_DEGENERATION = true;
        public const int GenerationsPerSecond = 10; 
        public const int SingleCellSize = 10;
        private const int NumberCellsHorizontal = 200;
        private const int NumberCellsVertical = 49 * 2;
        public static bool Paused = true;
        public static Texture2D Pixel;
        private Grid grid;
        private readonly Scene _scene;

        public GameLoop()
        {
            Engine.InitializeEngine(this, 1920, 1080);
            IsMouseVisible = true;
            _scene = new Scene();       
        }

        protected override void Initialize()
        {
            base.Initialize();
            grid = new Grid(NumberCellsHorizontal, NumberCellsVertical) { Name = "Grid" };
            _scene.AddSpriteToLayer(RenderLayerEnum.LAYER1, grid);
        }

        protected override void LoadContent()
        {
            Window.AllowUserResizing = true;
            Pixel = new Texture2D(ExoGame2D.Engine.SpriteBatch.GraphicsDevice, 1, 1);
            Pixel.SetData(new[] { Color.Gray });    
        }

        protected override void Update(GameTime gameTime)
        {
            InputHelper.Update();

            if (InputHelper.KeyPressed(Keys.Escape))
            {
                Exit();         
            }

            if (InputHelper.KeyPressed(Keys.F))
            {
                Engine.FullScreen = !Engine.FullScreen;
            }

            if (InputHelper.KeyPressed(Keys.Space))
            {
                Paused = !Paused;
            }

            if (InputHelper.KeyPressed(Keys.Back))
            {
                grid.ClearGrid();
            }

            grid.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            Engine.BeginRender(_scene);

            _scene.RenderScene(gameTime);

            Engine.EndRender();

            base.Draw(gameTime);
        }
    }
}
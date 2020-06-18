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
using ExoGameEngine.SceneManagement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ExoGameEngine
{
    public static class Engine
    {
        public static GraphicsDeviceManager Graphics;

        /// <summary>
        /// The width and height of the game world, in game units.
        /// </summary>
        private static Point _worldSize;

        /// <summary>
        /// The width and height of the window, in pixels.
        /// </summary>
        public static Point WindowSize;

        /// <summary>
        /// A matrix used for scaling the game world so that it fits inside the window.
        /// </summary>
        public static Matrix SpriteScale;

        public static SpriteBatch SpriteBatch;

        private static Game _game;

        public static Vector2 _scaledViewPort;

        public static Matrix _spriteScalingFactor;

        public static GameStateManager GameState { get; } = new GameStateManager();

        public static void InitializeEngine(Game game, int windowX, int windowY)
        {
            _game = game;
            Graphics = new GraphicsDeviceManager(game);
            game.Content.RootDirectory = "Content";

            game.IsMouseVisible = true;

            WindowSize = new Point(windowX, windowY);
            WorldSize = new Point(1920, 1080);
            FullScreen = false;

            SpriteBatch = new SpriteBatch(_game.GraphicsDevice);
        }

        public static void InitializeEngine(Game game)
        {
            InitializeEngine(game, 1920, 1080);
        }

        public static ContentManager Content => _game.Content;

        public static Scene CurrentScene { get; private set; }

        public static Point WorldSize
        {
            get => _worldSize;
            set
            {
                _worldSize = value;
                Graphics.PreferredBackBufferWidth = WorldSize.X;
                Graphics.PreferredBackBufferHeight = WorldSize.Y;
            }
        }

        public static void Exit()
        {
            _game.Exit();
        }

        public static Matrix SpriteScalingFactor => Matrix.CreateScale((float)_game.GraphicsDevice.Viewport.Width / WorldSize.X, (float)_game.GraphicsDevice.Viewport.Height / WorldSize.Y, 1);

        public static Vector2 ScaledViewPort => ScreenToWorld(new Vector2(Graphics.GraphicsDevice.Viewport.Width, Graphics.GraphicsDevice.Viewport.Height));

        public static Vector2 ScreenToWorld(Vector2 screenPosition)
        {
            Vector2 viewportTopLeft = new Vector2(_game.GraphicsDevice.Viewport.X, _game.GraphicsDevice.Viewport.Y);
            float screenToWorldScale = WorldSize.X / (float)_game.GraphicsDevice.Viewport.Width;

            return (screenPosition - viewportTopLeft) * screenToWorldScale;
        }

        public static void BeginRender(Scene currentScene)
        {
            CurrentScene = currentScene ?? throw new ArgumentNullException(nameof(currentScene));
            SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, SpriteScalingFactor);
        }

        public static void EndRender()
        {
            SpriteBatch.End();
        }

        /// <summary>
        /// Scales the window to the desired size, and calculates how the game world should be scaled to fit inside that window.
        /// </summary>
        public static void ApplyResolutionSettings(bool fullScreen)
        {
            // make the game full-screen or not
            Graphics.IsFullScreen = fullScreen;

            // get the size of the screen to use: either the window size or the full screen size
            Point screenSize = fullScreen
                ? new Point(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height)
                : WindowSize;

            // scale the window to the desired size
            Graphics.PreferredBackBufferWidth = screenSize.X;
            Graphics.PreferredBackBufferHeight = screenSize.Y;

            Graphics.ApplyChanges();

            // calculate and set the viewport to use
            _game.GraphicsDevice.Viewport = CalculateViewport(screenSize);

            // calculate how the graphics should be scaled, so that the game world fits inside the window
            SpriteScale = Matrix.CreateScale((float)_game.GraphicsDevice.Viewport.Width / WorldSize.X, (float)_game.GraphicsDevice.Viewport.Height / WorldSize.Y, 1);
        }

        /// <summary>
        /// Calculates and returns the viewport to use, so that the game world fits on the screen while preserving its aspect ratio.
        /// </summary>
        /// <param name="windowSize">The size of the screen on which the world should be drawn.</param>
        /// <returns>A Viewport object that will show the game world as large as possible while preserving its aspect ratio.</returns>
        public static Viewport CalculateViewport(Point windowSize)
        {
            // create a Viewport object
            Viewport viewport = new Viewport();

            // calculate the two aspect ratios
            float gameAspectRatio = (float)WorldSize.X / WorldSize.Y;
            float windowAspectRatio = (float)windowSize.X / windowSize.Y;

            // if the window is relatively wide, use the full window height
            if (windowAspectRatio > gameAspectRatio)
            {
                viewport.Width = (int)(windowSize.Y * gameAspectRatio);
                viewport.Height = windowSize.Y;
            }
            // if the window is relatively high, use the full window width
            else
            {
                viewport.Width = windowSize.X;
                viewport.Height = (int)(windowSize.X / gameAspectRatio);
            }

            // calculate and store the top-left corner of the viewport
            viewport.X = (windowSize.X - viewport.Width) / 2;
            viewport.Y = (windowSize.Y - viewport.Height) / 2;

            return viewport;
        }

        public static bool FullScreen
        {
            get => Graphics.IsFullScreen;
            set => ApplyResolutionSettings(value);
        }
    }
}

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
using Microsoft.Xna.Framework.Input;

namespace GameEngine
{
    /// <summary>
    /// A class that manages mouse and keyboard input.
    /// </summary>
    public static class InputHelper
    {
        // the current and previous mouse state
        private static MouseState currentMouseState, previousMouseState;

        // the current and previous keyboard state
        private static KeyboardState currentKeyboardState, previousKeyboardState;

        /// <summary>
        /// Updates this InputHelper object for one frame of the game loop.
        /// This method retrievse the current the mouse and keyboard state, and stores the previous states as a backup.
        /// </summary>
        public static void Update()
        {
            previousMouseState = currentMouseState;
            previousKeyboardState = currentKeyboardState;
            currentMouseState = Mouse.GetState();
            currentKeyboardState = Keyboard.GetState();
        }

        /// <summary>
        /// Gets the current position of the mouse, relative to the top-left corner of the screen.
        /// </summary>
        public static Vector2 MousePosition => new Vector2(currentMouseState.X, currentMouseState.Y);

        /// <summary>
        /// Checks and returns whether the player has started pressing the left mouse button in the last frame of the game loop.
        /// </summary>
        /// <returns>true if the left mouse button is now pressed and was not yet pressed in the previous frame; false otherwise.</returns>
        public static bool MouseLeftButtonPressed()
        {
            return currentMouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released;
        }

        /// <summary>
        /// Checks and returns whether the player has started pressing a certain keyboard key in the last frame of the game loop.
        /// </summary>
        /// <param name="k">The key to check.</param>
        /// <returns>true if the given key is now pressed and was not yet pressed in the previous frame; false otherwise.</returns>
        public static bool KeyPressed(Keys k)
        {
            return currentKeyboardState.IsKeyDown(k) && previousKeyboardState.IsKeyUp(k);
        }
    }
}
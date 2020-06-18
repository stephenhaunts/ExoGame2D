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
using System.Collections.Generic;
using System.Globalization;
using ExoGameEngine.Interfaces;
using Microsoft.Xna.Framework;

namespace ExoGameEngine
{
    public class GameStateManager
    {
        private readonly Dictionary<string, IGameState> _registeredGameStates = new Dictionary<string, IGameState>();

        public GameStateManager()
        {
        }

        public IGameState CurrentState { get; private set; } = null;
        public IGameState PreviousState { get; private set; } = null;


        public void Register(string name, IGameState gameStateHandler)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (gameStateHandler == null)
            {
                throw new ArgumentNullException(nameof(gameStateHandler));
            }

            if (_registeredGameStates.ContainsKey(name.ToUpper(CultureInfo.InvariantCulture)))
            {
                _registeredGameStates.Remove(name.ToUpper(CultureInfo.InvariantCulture));
            }

            _registeredGameStates.Add(name.ToUpper(CultureInfo.InvariantCulture), gameStateHandler);
        }

        public void RemoveState(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (!_registeredGameStates.ContainsKey(name.ToUpper(CultureInfo.InvariantCulture)))
            {
                throw new InvalidOperationException("The gamestate manager does not contain an entry for <" + name + ">");
            }

            _registeredGameStates.Remove(name.ToUpper(CultureInfo.InvariantCulture));
        }

        public void ChangeState (string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (!_registeredGameStates.ContainsKey(name.ToUpper(CultureInfo.InvariantCulture)))
            {
                throw new InvalidOperationException("The gamestate manager does not contain an entry for <" + name + ">");
            }

            PreviousState = CurrentState;
            CurrentState = _registeredGameStates[name.ToUpper(CultureInfo.InvariantCulture)];
        }
    }
}

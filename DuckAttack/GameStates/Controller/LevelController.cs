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

namespace ExoGame2D.DuckAttack.GameStates.Controller
{
    public class LevelController
    {
        private List<LevelDefinition> _levels = new List<LevelDefinition>();
        private int _currentLevelIndex = 0;

        public LevelDefinition CurrentLevel
        {
            get
            {
                return _levels[_currentLevelIndex];
            }
        }

        public LevelController(List<LevelDefinition> levels)
        {
            _levels = levels;
            _currentLevelIndex = 0;
        }

        public void ResetLevels()
        {
            _currentLevelIndex = 0;
        }

        public void NextLevel()
        {
            _currentLevelIndex++;

            if (_currentLevelIndex >= _levels.Count)
            {
                _currentLevelIndex = _levels.Count-1;
            }
        }

        public void AddLevel(LevelDefinition level)
        {
            if (level == null)
            {
                throw new ArgumentNullException(nameof(level));
            }

            _levels.Add(level);
        }
    }
}

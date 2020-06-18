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
using System.Collections.Generic;
using ExoGameEngine.Interfaces;

namespace ExoGameEngine
{
    public class CollisionManager
    {
        private static readonly Dictionary<string, ISprite> _spriteList = new Dictionary<string, ISprite>();

        public CollisionManager()
        {
        }

        public static void AddSpriteToCollisionManager(ISprite sprite, string name)
        {
            if (!_spriteList.ContainsKey(name))
            {
                _spriteList.Add(name, sprite);
            }
        }

        public static bool IsCollision(string source, string destination)
        {
            ISprite sourceSprite = _spriteList[source];
            ISprite destinationSprite = _spriteList[destination];

            return sourceSprite.BoundingBox.Intersects(destinationSprite.BoundingBox);
        }

        public static void RemoveAll()
        {
            _spriteList.Clear();
        }
    }
}

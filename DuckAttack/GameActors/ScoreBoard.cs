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
using ExoGame2D;
using ExoGame2D.Interfaces;
using ExoGame2D.Renderers;
using ExoGame2D.DuckAttack.Messages;
using Microsoft.Xna.Framework;

namespace ExoGame2D.DuckAttack.GameActors
{
    public class ScoreBoard : IRenderNode
    {
        private FontRender _scoreboard;
        private int _score;

        public ScoreBoard(string name)
        {
            Name = name;
            _scoreboard = new FontRender(name);
            _scoreboard.LoadContent("headsup");
            _scoreboard.Location = new Vector2(40, 40);
            _score = 0;
            _scoreboard.Shadow = true;
        }

        public string Name { get; set; }

        public void Draw(GameTime gameTime)
        {
            _scoreboard.Draw(gameTime, Color.White);
        }

        public void Draw(GameTime gameTime, Color tint)
        {
            _scoreboard.Draw(gameTime, tint);
        }

        public ISprite GetSprite()
        {
            throw new NotImplementedException();
        }

        public bool IsAssetOfType(Type type)
        {
            return _scoreboard.GetType().IsSubclassOf(type);
        }

        public void Update(GameTime gameTime)
        {
            if (Channels.Exists("score"))
            {
                var message = (ScoreMessage)Channels.RetrieveLatestMessage("score");

                if (message != null)
                {
                    _score += message.ScoreIncrement;
                }
            }

            _scoreboard.Text = "Score = " + _score;
        }
    }
}

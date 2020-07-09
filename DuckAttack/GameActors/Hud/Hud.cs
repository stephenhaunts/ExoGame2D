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
using ExoGame2D.DuckAttack.Messages;
using ExoGame2D.Interfaces;
using Microsoft.Xna.Framework;

namespace ExoGame2D.DuckAttack.GameActors.Hud
{

    public class Hud : IRenderNode
    {
        private const int MAX_NUMBER_DUCKS = 12;
        private DuckIndicator[] _duckIndicator = new DuckIndicator[MAX_NUMBER_DUCKS];
        private static BulletIndicator[] _bulletIndicator = new BulletIndicator[8];
        private int _numberDucksShot = 0;

        private const int MAX_SHOTS = 4;
        public static int NumShotsLeft = 4;

        public Hud(string name)
        {
            Name = name;

            int x_offset = 1050;

            for (int i = 0; i < MAX_NUMBER_DUCKS; i++)
            {
                _duckIndicator[i] = new DuckIndicator("DuckIndicator_" + i, x_offset);

                x_offset += 30;
            }

            int bullet_x_offset = 550;

            for (int i = 0; i < MAX_SHOTS; i++)
            {
                _bulletIndicator[i] = new BulletIndicator("BulletIndicator_" + i, bullet_x_offset);

                bullet_x_offset += 30;
            }

            ResetHud();
        }

        public void ResetHud()
        {
            NumShotsLeft = MAX_SHOTS;
            _numberDucksShot = 0;

            for (int i = 0; i < MAX_SHOTS; i++)
            {
                _bulletIndicator[i].State = BulletIndicatorStateEnum.NotFired;
            }

            for (int i = 0; i < MAX_NUMBER_DUCKS; i++)
            {
                _duckIndicator[i].State = DuckIndicatorStateEnum.None;
            }
        }

        public void ResetGun()
        {
            NumShotsLeft = MAX_SHOTS;
            
            for (int i = 0; i < MAX_SHOTS; i++)
            {
                _bulletIndicator[i].State = BulletIndicatorStateEnum.NotFired;
            }
        }

        public void RegisterHit()
        {
            _duckIndicator[_numberDucksShot].State = DuckIndicatorStateEnum.Hit;
            _numberDucksShot++;
        }

        public void RegisterMiss()
        {
            _duckIndicator[_numberDucksShot].State = DuckIndicatorStateEnum.Miss;
            _numberDucksShot++;
        }

        public static void ShootBullet()
        {
            if (NumShotsLeft > 0)
            {
                _bulletIndicator[NumShotsLeft - 1].State = BulletIndicatorStateEnum.Fired;
                NumShotsLeft--;
            }
        }

        public string Name { get; set; }

        public void Update(GameTime gameTime)
        {
            if (Channels.Exists("duckhit"))
            {
                var message = (DuckHitMessage)Channels.RetrieveLatestMessage("duckhit");

                if (message != null)
                {
                    switch(message.State)
                    {
                        case DuckIndicatorStateEnum.Hit:
                            RegisterHit();
                            break;

                        case DuckIndicatorStateEnum.Miss:
                            RegisterMiss();
                            break;
                    }
                }
            }

            if (Channels.Exists("gunfired"))
            {
                var message = (BulletFiredMessage)Channels.RetrieveLatestMessage("gunfired");
                if (message != null)
                {
                    ShootBullet();
                }
            }
        }


        public void Draw(GameTime gameTime)
        {
            Draw(gameTime, Color.White);
        }

        public void Draw(GameTime gameTime, Color tint)
        {
            for (int i = 0; i < MAX_NUMBER_DUCKS; i++)
            {
                _duckIndicator[i].Draw(gameTime, Color.White);
            }

            for (int i = 0; i < MAX_SHOTS; i++)
            {
                _bulletIndicator[i].Draw(gameTime, Color.White);
            }
        }

        public ISprite GetSprite()
        {
            throw new NotImplementedException();
        }

        public bool IsAssetOfType(Type type)
        {
            return _duckIndicator[0].GetType().IsSubclassOf(type);
        }
    }
}

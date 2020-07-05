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
using Microsoft.Xna.Framework.Media;

namespace ExoGame2D
{

    public static class MusicPlayer
    {
        private static Dictionary<string, Song> _music = new Dictionary<string, Song>();
        public static bool PlayMusic { get; set; } = true;
        public static MusicPlayStateEnum State = MusicPlayStateEnum.Stopped;

        public static bool Looped
        {
            get
            {
                return MediaPlayer.IsRepeating;
            }
            set
            {
                MediaPlayer.IsRepeating = value;
            }
        }

        public static void LoadMusic(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            string lowerCaseName = name.ToLower(CultureInfo.InvariantCulture);

            if (_music.ContainsKey(lowerCaseName))
            {
                return;
            }
           
            _music.Add(lowerCaseName, Engine.Content.Load<Song>(lowerCaseName));
        }

        public static void RemoveMusic(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            string lowerCaseName = name.ToLower(CultureInfo.InvariantCulture);

            if (!_music.ContainsKey(lowerCaseName))
            {
                throw new InvalidOperationException("The music file <" + lowerCaseName + "> doesn't exists.");
            }

            _music.Remove(name);
        }

        public static void Play(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            string lowerCaseName = name.ToLower(CultureInfo.InvariantCulture);

            if (!_music.ContainsKey(lowerCaseName))
            {
                throw new InvalidOperationException("The music file <" + lowerCaseName + "> doesn't exists.");
            }

            if (PlayMusic)
            {
                if (State == MusicPlayStateEnum.Playing)
                {
                    return;
                }

                State = MusicPlayStateEnum.Playing;
                MediaPlayer.Play(_music[lowerCaseName]);
            }
        }

        public static void Stop()
        {
            if (PlayMusic)
            {
                if (State == MusicPlayStateEnum.Stopped)
                {
                    return;
                }
                
                State = MusicPlayStateEnum.Stopped;
                MediaPlayer.Stop();
            }
        }

        public static void Resume()
        {
            if (PlayMusic)
            {
                if (State == MusicPlayStateEnum.Playing)
                {
                    return;
                }

                State = MusicPlayStateEnum.Playing;
                MediaPlayer.Resume();
            }
        }

        public static void Pause()
        {
            if (PlayMusic)
            {
                if (State == MusicPlayStateEnum.Paused)
                {
                    return;
                }

                State = MusicPlayStateEnum.Paused;
                MediaPlayer.Pause();
            }
        }
    }

}

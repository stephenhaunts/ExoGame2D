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
using Microsoft.Xna.Framework.Audio;

namespace ExoGameEngine
{
    public class SoundEffectMessage : IChannelMessage
    {
        public string SoundEffectToPlay { get; set; }
    }

    public static class SoundEffectPlayer
    {
        private static Dictionary<string, SoundEffect> _soundEffects = new Dictionary<string, SoundEffect>();
        public static bool PlaySoundEffects { get; set; } = true;

        public static void LoadSoundEffect(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            string lowerCaseName = name.ToLower(CultureInfo.InvariantCulture);

            if (_soundEffects.ContainsKey(lowerCaseName))
            {
                return;
            }

            _soundEffects.Add(lowerCaseName, Engine.Content.Load<SoundEffect>(lowerCaseName));
        }

        public static void RemoveSoundEffect(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            string lowerCaseName = name.ToLower(CultureInfo.InvariantCulture);

            if (!_soundEffects.ContainsKey(lowerCaseName))
            {
                throw new InvalidOperationException("The sound effect <" + lowerCaseName + "> doesn't exists.");
            }

            _soundEffects.Remove(name);
        }

        public static void PlayOneShot(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            string lowerCaseName = name.ToLower(CultureInfo.InvariantCulture);

            if (!_soundEffects.ContainsKey(lowerCaseName))
            {
                throw new InvalidOperationException("The sound effect <" + lowerCaseName + "> doesn't exists.");
            }

            if (PlaySoundEffects)
            {
                _soundEffects[name].CreateInstance().Play();
            }
        }

        public static void ProcessSoundEvents()
        {
            if (!Channels.Exists("soundeffects"))
            {
                Channels.AddNewChannel("soundeffects");
            }

            if (Channels.Exists("soundeffects"))
            {
                var message = (SoundEffectMessage)Channels.RetrieveLatestMessage("soundeffects");

                if (message != null)
                {
                    PlayOneShot(message.SoundEffectToPlay.ToLower(CultureInfo.InvariantCulture));
                }
            }
        }

    }
}

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
using GameEngine.Interfaces;

namespace GameEngine
{
    public class Channels
    {
        private static readonly Dictionary<string, Queue<IChannelMessage>> _channels = new Dictionary<string, Queue<IChannelMessage>>();

        public Channels()
        {
        }

        public static void AddNewChannel(string channelName)
        {
            if (string.IsNullOrEmpty(channelName))
            {
                throw new ArgumentNullException(nameof(channelName));
            }

            if (_channels.ContainsKey(channelName.ToLower(CultureInfo.InvariantCulture)))
            {
                return;
            }

            _channels.Add(channelName, new Queue<IChannelMessage>());
        }

        public static void RemoveChannel(string channelName)
        {
            if (string.IsNullOrEmpty(channelName))
            {
                throw new ArgumentNullException(nameof(channelName));
            }

            if (!_channels.ContainsKey(channelName.ToLower(CultureInfo.InvariantCulture)))
            {
                throw new InvalidOperationException("The channel <" + channelName + "> does not exist.");
            }

            if (_channels[channelName.ToLower(CultureInfo.InvariantCulture)].Count > 0)
            {
                throw new InvalidOperationException("The channel <" + channelName.ToLower(CultureInfo.InvariantCulture) + "> contains messages.");
            }

            _channels.Remove(channelName.ToLower(CultureInfo.InvariantCulture));
        }

        public static bool Exists(string channelName)
        {
            if (string.IsNullOrEmpty(channelName))
            {
                throw new ArgumentNullException(nameof(channelName));
            }

            if (_channels.ContainsKey(channelName.ToLower(CultureInfo.InvariantCulture)))
            {
                return true;
            }

            return false;
        }

        public static void ClearChannel(string channelName)
        {
            if (string.IsNullOrEmpty(channelName))
            {
                throw new ArgumentNullException(nameof(channelName));
            }

            if (!_channels.ContainsKey(channelName.ToLower(CultureInfo.InvariantCulture)))
            {
                throw new InvalidOperationException("The channel <" + channelName.ToLower(CultureInfo.InvariantCulture) + "> does not exist.");
            }

            _channels[channelName.ToLower(CultureInfo.InvariantCulture)].Clear();
        }

        public static void PostMessage(string channelName, IChannelMessage message)
        {
            if (string.IsNullOrEmpty(channelName))
            {
                throw new ArgumentNullException(nameof(channelName));
            }

            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            if (!_channels.ContainsKey(channelName.ToLower(CultureInfo.InvariantCulture)))
            {
                throw new InvalidOperationException("The channel <" + channelName.ToLower(CultureInfo.InvariantCulture) + "> does not exist.");
            }

            _channels[channelName.ToLower(CultureInfo.InvariantCulture)].Enqueue(message);
        }

        public static IChannelMessage RetrieveLatestMessage(string channelName)
        {
            if (string.IsNullOrEmpty(channelName))
            {
                throw new ArgumentNullException(nameof(channelName));
            }

            if (!_channels.ContainsKey(channelName.ToLower(CultureInfo.InvariantCulture)))
            {
                throw new InvalidOperationException("The channel <" + channelName.ToLower(CultureInfo.InvariantCulture) + "> does not exist.");
            }

            if (_channels[channelName.ToLower(CultureInfo.InvariantCulture)].Count == 0)
            {
                return null;
            }

            return _channels[channelName.ToLower(CultureInfo.InvariantCulture)].Dequeue();
        }
    }
}

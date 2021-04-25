#region LICENCE

// /*
//         The MIT License (MIT)
// 
//         Copyright (c) 2021 Johnathon M
// 
//         Permission is hereby granted, free of charge, to any person obtaining a copy
//         of this software and associated documentation files (the "Software"), to deal
//         in the Software without restriction, including without limitation the rights
//         to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//         copies of the Software, and to permit persons to whom the Software is
//         furnished to do so, subject to the following conditions:
// 
//         The above copyright notice and this permission notice shall be included in
//         all copies or substantial portions of the Software.
// 
//         THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//         IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//         FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//         AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//         LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//         OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
//         THE SOFTWARE.
// 
// */

#endregion

using System.Drawing;
using System.Linq;
using TileIconifier.Core.Utilities;

namespace TileIconifier.Core.Custom
{
    public class NewCustomShortcutFormCache
    {
        private byte[] _currentIconBytes;

        private Image _iconCache;
        private byte[] _newIconBytes;
        public string ShortcutName { get; set; }
        public ShortcutUser AllOrCurrentUser { get; set; }

        public void SetIconBytes(byte[] bytes)
        {
            _currentIconBytes = _currentIconBytes ?? bytes;
            _newIconBytes = bytes;
        }

        public Image GetIcon()
        {
            var iconBytesChanged = (_currentIconBytes != null && _newIconBytes != null &&
                                    !_currentIconBytes.SequenceEqual(_newIconBytes)) ||
                                   _currentIconBytes != null && _newIconBytes == null;

            if (_iconCache != null && !iconBytesChanged) return _iconCache;

            _currentIconBytes = _newIconBytes?.ToArray();
            _iconCache?.Dispose();
            _iconCache = ImageUtils.ByteArrayToImage(_currentIconBytes);
            return _iconCache;
        }
    }
}
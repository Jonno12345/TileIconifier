#region LICENCE

// /*
//         The MIT License (MIT)
// 
//         Copyright (c) 2016 Johnathon M
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

using System;
using System.Linq;

namespace TileIconifier.Shortcut
{
    public class ShortcutIconParameters : IEquatable<ShortcutIconParameters>
    {
        public string BackgroundColor;
        public string ForegroundText;
        public byte[] MediumImageBytes;

        public string ShowNameOnSquare150X150Logo;
        public byte[] SmallImageBytes;

        public bool Equals(ShortcutIconParameters other)
        {
            if (ReferenceEquals(this, other))
                return true;

            return BackgroundColor == other.BackgroundColor
                   && ForegroundText == other.ForegroundText
                   && ShowNameOnSquare150X150Logo == other.ShowNameOnSquare150X150Logo
                   && MediumImageBytesEqual(other)
                   && SmallImageBytesEqual(other);
        }

        public bool MediumImageBytesEqual(ShortcutIconParameters other)
        {
            var mediumImageEqual = (MediumImageBytes == other.MediumImageBytes) ||
                                   other.MediumImageBytes != null && MediumImageBytes != null &&
                                   MediumImageBytes.SequenceEqual(other.MediumImageBytes);
            return mediumImageEqual;
        }

        public bool SmallImageBytesEqual(ShortcutIconParameters other)
        {
            var smallImageEqual = (SmallImageBytes == other.SmallImageBytes) ||
                                  other.MediumImageBytes != null && MediumImageBytes != null &&
                                  SmallImageBytes.SequenceEqual(other.SmallImageBytes);
            return smallImageEqual;
        }

        public ShortcutIconParameters Clone()
        {
            return new ShortcutIconParameters
            {
                BackgroundColor = BackgroundColor,
                ForegroundText = ForegroundText,
                ShowNameOnSquare150X150Logo = ShowNameOnSquare150X150Logo,
                SmallImageBytes = SmallImageBytes,
                MediumImageBytes = MediumImageBytes
            };
        }
    }
}
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
using System.Drawing;

namespace TileIconifier.Core.Shortcut.State
{
    public class ShortcutItemState : IEquatable<ShortcutItemState>
    {
        private string _showNameOnSquare150X150Logo;

        public ShortcutItemState()
        {
            MediumImage = new ShortcutItemImage(ShortcutConstantsAndEnums.MediumShortcutOutputSize);
            SmallImage = new ShortcutItemImage(ShortcutConstantsAndEnums.SmallShortcutOutputSize);
        }

        public bool UseSystemAccentColor { get; set; }
        public string BackgroundColor { get; set; }
        public string ForegroundText { get; set; }
        public ShortcutItemImage MediumImage { get; set; }
        public ShortcutItemImage SmallImage { get; set; }


        public bool ShowNameOnSquare150X150Logo
        {
            get { return _showNameOnSquare150X150Logo == "on"; }
            set { _showNameOnSquare150X150Logo = value ? "on" : "off"; }
        }

        public bool Equals(ShortcutItemState other)
        {
            if (ReferenceEquals(this, other))
                return true;

            return BackgroundColor == other.BackgroundColor
                   && ForegroundText == other.ForegroundText
                   && ShowNameOnSquare150X150Logo == other.ShowNameOnSquare150X150Logo
                   && MediumImage.Equals(other.MediumImage)
                   && SmallImage.Equals(other.SmallImage)
                   && UseSystemAccentColor == other.UseSystemAccentColor;
        }

        public bool MediumImageBytesEqual(ShortcutItemState other)
        {
            return MediumImage.Equals(other.MediumImage);
        }

        public bool SmallImageBytesEqual(ShortcutItemState other)
        {
            return SmallImage.Equals(other.SmallImage);
        }

        public ShortcutItemState Clone()
        {
            return new ShortcutItemState
            {
                BackgroundColor = BackgroundColor,
                ForegroundText = ForegroundText,
                ShowNameOnSquare150X150Logo = ShowNameOnSquare150X150Logo,
                UseSystemAccentColor = UseSystemAccentColor,
                MediumImage = MediumImage != null
                    ? new ShortcutItemImage(new Size(MediumImage.Width, MediumImage.Height))
                    {
                        Bytes = MediumImage.Bytes,
                        X = MediumImage.X,
                        Y = MediumImage.Y,
                        Path = MediumImage.Path
                    }
                    : new ShortcutItemImage(ShortcutConstantsAndEnums.MediumShortcutOutputSize),
                SmallImage = SmallImage != null
                    ? new ShortcutItemImage(new Size(SmallImage.Width, SmallImage.Height))
                    {
                        Bytes = SmallImage.Bytes,
                        X = SmallImage.X,
                        Y = SmallImage.Y,
                        Path = SmallImage.Path
                    }
                    : new ShortcutItemImage(ShortcutConstantsAndEnums.SmallShortcutOutputSize)
            };
        }
    }
}
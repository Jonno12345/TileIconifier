using System;
using System.Drawing;
using TileIconifier.Utilities;

namespace TileIconifier.Shortcut
{
    public class ShortcutIconParameters : IEquatable<ShortcutIconParameters>
    {
        public string BackgroundColor;
        public string ForegroundText;
        public Bitmap MediumImage;

        public string ShowNameOnSquare150X150Logo;
        public Bitmap SmallImage;

        public bool Equals(ShortcutIconParameters other)
        {
            if (this == other)
                return true;

            return BackgroundColor == other.BackgroundColor
                   && ForegroundText == other.ForegroundText
                   && ShowNameOnSquare150X150Logo == other.ShowNameOnSquare150X150Logo
                   && ImageUtils.BitmapsAreEqual(MediumImage, other.MediumImage)
                   && ImageUtils.BitmapsAreEqual(SmallImage, other.SmallImage);
        }

        public ShortcutIconParameters Clone()
        {
            return new ShortcutIconParameters
            {
                BackgroundColor = BackgroundColor,
                ForegroundText = ForegroundText,
                ShowNameOnSquare150X150Logo = ShowNameOnSquare150X150Logo,
                SmallImage = SmallImage,
                MediumImage = MediumImage
            };
        }
    }
}
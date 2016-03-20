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
                other.MediumImageBytes != null && MediumImageBytes != null && MediumImageBytes.SequenceEqual(other.MediumImageBytes);
            return mediumImageEqual;
        }

        public bool SmallImageBytesEqual(ShortcutIconParameters other)
        {
            var smallImageEqual = (SmallImageBytes == other.SmallImageBytes) || 
                other.MediumImageBytes != null && MediumImageBytes != null && SmallImageBytes.SequenceEqual(other.SmallImageBytes);
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
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TileIconifier.Utilities;

namespace TileIconifier
{
    public class ShortcutIconParameters : IEquatable<ShortcutIconParameters>
    {
        public Bitmap SmallImage;
        public Bitmap MediumImage;

        //Deleteme
        public ShortcutItem Shortcut;
        
        public string BackgroundColor;
        public string ForegroundText;
        public string ShowNameOnSquare150x150Logo;

        public ShortcutIconParameters Clone()
        {
            return new ShortcutIconParameters()
            {
                BackgroundColor = this.BackgroundColor,
                ForegroundText = this.ForegroundText,
                ShowNameOnSquare150x150Logo = this.ShowNameOnSquare150x150Logo,
                SmallImage = this.SmallImage,
                MediumImage = this.MediumImage
            };
        }

        public bool Equals(ShortcutIconParameters other)
        {
            if (this == other)
                return true;

            return (BackgroundColor == other.BackgroundColor
                && ForegroundText == other.ForegroundText
                && ShowNameOnSquare150x150Logo == other.ShowNameOnSquare150x150Logo
                && ImageUtilities.BitmapsAreEqual(MediumImage, other.MediumImage)
                && ImageUtilities.BitmapsAreEqual(SmallImage, other.SmallImage));
        }
    }
}

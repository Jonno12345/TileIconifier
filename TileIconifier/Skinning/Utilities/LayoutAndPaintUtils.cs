using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TileIconifier.Skinning.Utilities
{
    class LayoutAndPaintUtils
    {
        /// <summary>
        ///     Scale a point using the specified scaling factor.
        /// </summary>
        /// <param name="pt"><see cref="Point"/> to scale</param>
        /// <param name="scale"><see cref="SizeF"/> specifiying the scaling factor for each axis of the point.</param>
        public static void ScalePoint(ref Point pt, SizeF scale)
        {
            var ptF = (PointF)pt;

            ptF.X *= scale.Width;
            ptF.Y *= scale.Height;

            pt = Point.Round(ptF);
        }

        /// <summary>
        ///     Returns a <see cref="Rectangle"/> whose size was increased or decreased from the specified 
        ///     <see cref="Rectangle"/> based on the specified <see cref="Padding"/>.
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="pad"></param>
        /// <returns></returns>
        public static Rectangle InflateRectangle(Rectangle rect, Padding pad)
        {
            Rectangle r = Rectangle.FromLTRB(
                    rect.Left + pad.Left,
                    rect.Top + pad.Top,
                    rect.Right - pad.Right,
                    rect.Bottom - pad.Bottom);
            return r;
        }

        /// <summary>
        ///     Returns a <see cref="TextFormatFlags"/> value equivalent to the specified <see cref="ContentAlignment"/>.
        /// </summary>
        /// <param name="contentAlign"></param>        
        public static TextFormatFlags ConvertToTextFormatFlags(ContentAlignment contentAlign)
        {
            TextFormatFlags flags = new TextFormatFlags();

            //Top
            if (contentAlign == ContentAlignment.TopLeft || contentAlign == ContentAlignment.TopCenter || contentAlign == ContentAlignment.TopRight)
                flags |= TextFormatFlags.Top;

            //Middle
            if (contentAlign == ContentAlignment.MiddleLeft || contentAlign == ContentAlignment.MiddleCenter || contentAlign == ContentAlignment.MiddleRight)
                flags |= TextFormatFlags.VerticalCenter;

            //Bottom
            if (contentAlign == ContentAlignment.BottomLeft || contentAlign == ContentAlignment.BottomCenter || contentAlign == ContentAlignment.BottomRight)
                flags |= TextFormatFlags.Bottom;

            //Left
            if (contentAlign == ContentAlignment.BottomLeft || contentAlign == ContentAlignment.MiddleLeft || contentAlign == ContentAlignment.TopLeft)
                flags |= TextFormatFlags.Left;

            //Center
            if (contentAlign == ContentAlignment.BottomCenter || contentAlign == ContentAlignment.MiddleCenter || contentAlign == ContentAlignment.TopCenter)
                flags |= TextFormatFlags.HorizontalCenter;

            //Right
            if (contentAlign == ContentAlignment.BottomRight || contentAlign == ContentAlignment.MiddleRight || contentAlign == ContentAlignment.TopRight)
                flags |= TextFormatFlags.Right;

            return flags;
        }

        /// <summary>
        ///     Returns a <see cref="TextFormatFlags"/> value equivalent to the specified <see cref="HorizontalAlignment"/>.
        /// </summary>
        public static TextFormatFlags ConvertToTextFormatFlags(HorizontalAlignment horiAlign)
        {
            switch (horiAlign)
            {
                case HorizontalAlignment.Left:
                    return TextFormatFlags.Left;

                case HorizontalAlignment.Center:
                    return TextFormatFlags.HorizontalCenter;

                case HorizontalAlignment.Right:
                    return TextFormatFlags.Right;

                default:
                    throw new ArgumentException("Unsupported horizontal alignement.");
            }
        }
    }
}

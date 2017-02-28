using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TileIconifier.Utilities
{
    internal static class ButtonUtils
    {

        /// <summary>
        /// Returns a new rectangle similar to an existing one, but deflated based on the specified padding.
        /// </summary>
        /// <param name="pRect"></param>
        /// <param name="pPad"></param>
        /// <returns></returns>
        internal static Rectangle CreatePaddedRectangle(Rectangle pRect, Padding pPad)
        {
            Rectangle rect = Rectangle.FromLTRB(
                    pRect.Left + pPad.Left,
                    pRect.Top + pPad.Top,
                    pRect.Right - pPad.Right,
                    pRect.Bottom - pPad.Bottom);
            return rect;
        }

        /// <summary>
        /// Converts a ContentAlignement value into a TextFormatFlags.
        /// </summary>
        /// <param name="pContentAlign"></param>
        /// <returns></returns>
        internal static TextFormatFlags ConvertToTextFormatFlags(ContentAlignment pContentAlign)
        {
            TextFormatFlags flags = TextFormatFlags.Default;

            //Top
            if (pContentAlign == ContentAlignment.TopLeft | pContentAlign == ContentAlignment.TopCenter | pContentAlign == ContentAlignment.TopRight)            
                flags = flags | TextFormatFlags.Top;
            
            //Middle
            if (pContentAlign == ContentAlignment.MiddleLeft | pContentAlign == ContentAlignment.MiddleCenter | pContentAlign == ContentAlignment.MiddleRight)            
                flags = flags | TextFormatFlags.VerticalCenter;            

            //Bottom
            if (pContentAlign == ContentAlignment.BottomLeft | pContentAlign == ContentAlignment.BottomCenter | pContentAlign == ContentAlignment.BottomRight)
                flags = flags | TextFormatFlags.Bottom;            

            //Left
            if (pContentAlign == ContentAlignment.BottomLeft | pContentAlign == ContentAlignment.MiddleLeft | pContentAlign == ContentAlignment.TopLeft)            
                flags = flags | TextFormatFlags.Left;
           
            //Center
            if (pContentAlign == ContentAlignment.BottomCenter | pContentAlign == ContentAlignment.MiddleCenter | pContentAlign == ContentAlignment.TopCenter)            
                flags = flags | TextFormatFlags.HorizontalCenter;
           
            //Right
            if (pContentAlign == ContentAlignment.BottomRight | pContentAlign == ContentAlignment.MiddleRight | pContentAlign == ContentAlignment.TopRight)            
                flags = flags | TextFormatFlags.Right;            

            return flags;
        }

        /// <summary>
        /// Returns a rectangle where the text can be drawn on a RadioButton or a CheckBox.
        /// </summary>
        /// <param name="pCheckAreaSize"></param>
        /// <param name="pClientRect"></param>
        /// <param name="pSpacing">Spacing between the glyph and the text.</param>
        /// <returns></returns>
        internal static Rectangle GetGlyphButtonTextRect(Size pCheckAreaSize, Rectangle pClientRect, int pSpacing)
        {
            Point textRectLocation = new Point(pClientRect.X + pCheckAreaSize.Width + pSpacing, pClientRect.Y);
            Size textRectSize = new Size(pClientRect.Width - pCheckAreaSize.Width - pSpacing, pClientRect.Height);
            Rectangle textRect = new Rectangle(textRectLocation, textRectSize);

            return textRect;
        }
    }
}

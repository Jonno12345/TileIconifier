using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TileIconifier.Controls;

namespace TileIconifier.Utilities
{
    internal static class ButtonUtils
    {
        internal static readonly TextFormatFlags BaseTextFormatFlags = TextFormatFlags.WordBreak | TextFormatFlags.TextBoxControl;
        
        private static Rectangle CreatePaddedRectangle(Rectangle pRect, Padding pPad)
        {
            Rectangle rect = Rectangle.FromLTRB(
                    pRect.Left + pPad.Left,
                    pRect.Top + pPad.Top,
                    pRect.Right - pPad.Right,
                    pRect.Bottom - pPad.Bottom);
            return rect;
        }
        
        private static Size GetCheckBoxGlyphSize(Graphics pGraphics, FlatStyle pFlatStyle)
        {
            float flScaleX = pGraphics.DpiX / 96F;
            float flScaleY = pGraphics.DpiY / 96F;

            switch (pFlatStyle)
            {
                case FlatStyle.Flat:
                case FlatStyle.Popup:
                    //In the .Net 4.6 Reference Source, the size of the checkmark is a 
                    //constant called "flatCheckSize" in a class called CheckBoxBaseAdapter.
                    return new Size((int)(11 * flScaleX), (int)(11 * flScaleY));
                default:
                    //We don't bother with states here. We just assume 
                    //that all states have the same size.
                    return CheckBoxRenderer.GetGlyphSize(pGraphics, System.Windows.Forms.VisualStyles.CheckBoxState.CheckedNormal);
            }
        }
                
        private static Size GetRadioButtonGlyphSize(Graphics pGraphics, FlatStyle pFlatStyle)
        {
            float flScaleX = pGraphics.DpiX / 96F;
            float flScaleY = pGraphics.DpiY / 96F;

            switch (pFlatStyle)
            {
                case FlatStyle.Flat:
                case FlatStyle.Popup:
                    //In the .Net 4.6 Reference Source, the size of the checkmark is a 
                    //constant called "flatCheckSize" in a class called RadioButtonFlatAdapter.
                    return new Size((int)(12 * flScaleX), (int)(12 * flScaleY));
                default:
                    //We don't bother with states here. We just assume 
                    //that all states have the same size.
                    return RadioButtonRenderer.GetGlyphSize(pGraphics, System.Windows.Forms.VisualStyles.RadioButtonState.CheckedNormal);
            }
        }

        private static Rectangle CreatePushButtonTextRectangle(Control pControl)
        {
            return CreatePaddedRectangle(pControl.ClientRectangle, pControl.Padding);
        }

        private static Rectangle CreateGlyphButtonTextRectangle(Control pControl, Size pGlyphSize)
        {
            //Spacing between the edge of the glyph and the outer edge of the check area.
            //1px padding + 1px for the glyph border.
            const int inGLYPH_ADDITIONNAL_SPACE = 2;
            //Some mysterious spacing on the left and right sides of the text.
            const int inTEXT_LATTERAL_PADDING = 1;

            Rectangle contentRect = CreatePaddedRectangle(pControl.ClientRectangle, pControl.Padding);
            Size checkAreaSize = new Size(pGlyphSize.Width + inGLYPH_ADDITIONNAL_SPACE, pGlyphSize.Height + inGLYPH_ADDITIONNAL_SPACE);
            Point textRectLocation;
            if (pControl.RightToLeft != RightToLeft.Yes)
            {
                textRectLocation = new Point(contentRect.X + checkAreaSize.Width + inTEXT_LATTERAL_PADDING, contentRect.Y);
            }
            else
            {
                textRectLocation = new Point(contentRect.X + inTEXT_LATTERAL_PADDING);
            }
            Size textRectSize = new Size(contentRect.Width - checkAreaSize.Width - 2 * inTEXT_LATTERAL_PADDING, contentRect.Height);
            Rectangle textRect = new Rectangle(textRectLocation, textRectSize);

            return textRect;
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
            if (pContentAlign == ContentAlignment.TopLeft || pContentAlign == ContentAlignment.TopCenter || pContentAlign == ContentAlignment.TopRight)            
                flags = flags | TextFormatFlags.Top;
            
            //Middle
            if (pContentAlign == ContentAlignment.MiddleLeft || pContentAlign == ContentAlignment.MiddleCenter || pContentAlign == ContentAlignment.MiddleRight)            
                flags = flags | TextFormatFlags.VerticalCenter;            

            //Bottom
            if (pContentAlign == ContentAlignment.BottomLeft || pContentAlign == ContentAlignment.BottomCenter || pContentAlign == ContentAlignment.BottomRight)
                flags = flags | TextFormatFlags.Bottom;            

            //Left
            if (pContentAlign == ContentAlignment.BottomLeft || pContentAlign == ContentAlignment.MiddleLeft || pContentAlign == ContentAlignment.TopLeft)            
                flags = flags | TextFormatFlags.Left;
           
            //Center
            if (pContentAlign == ContentAlignment.BottomCenter || pContentAlign == ContentAlignment.MiddleCenter || pContentAlign == ContentAlignment.TopCenter)            
                flags = flags | TextFormatFlags.HorizontalCenter;
           
            //Right
            if (pContentAlign == ContentAlignment.BottomRight || pContentAlign == ContentAlignment.MiddleRight || pContentAlign == ContentAlignment.TopRight)            
                flags = flags | TextFormatFlags.Right;            

            return flags;
        }
                
        /// <summary>
        /// Returns a rectangle where the text can be drawn on a push button.
        /// </summary>
        /// <param name="pButton"></param>
        /// <returns></returns>
        internal static Rectangle GetPushButtonTextRectangle(Button pButton)
        {
            return CreatePushButtonTextRectangle(pButton);
        }

        /// <summary>
        /// Returns a rectangle where the text can be drawn on a push button.
        /// </summary>
        /// <param name="pRadioButton"></param>
        /// <returns></returns>
        internal static Rectangle GetPushButtonTextRectangle(RadioButton pRadioButton)
        {
            return CreatePushButtonTextRectangle(pRadioButton);
        }

        /// <summary>
        /// Returns a rectangle where the text can be drawn on a push button.
        /// </summary>
        /// <param name="pCheckBox"></param>
        /// <returns></returns>
        /// 
        internal static Rectangle GetPushButtonTextRectangle(CheckBox pCheckBox)
        {
            return CreatePushButtonTextRectangle(pCheckBox);
        }

        /// <summary>
        /// Returns a rectangle where the text can be drawn on a radio button.
        /// </summary>
        /// <param name="pRadioButton"></param>
        /// <param name="pGraphics"></param>
        /// <returns></returns>
        internal static Rectangle GetRadioButtonTextRectangle(RadioButton pRadioButton, Graphics pGraphics)
        {
            return CreateGlyphButtonTextRectangle(pRadioButton, GetRadioButtonGlyphSize(pGraphics, pRadioButton.FlatStyle));
        }

        /// <summary>
        /// Returns a rectangle where the text can be drawn on a check box.
        /// </summary>
        /// <param name="pCheckBox"></param>
        /// <param name="pGraphics"></param>
        /// <returns></returns>
        internal static Rectangle GetCheckBoxTextRectangle(CheckBox pCheckBox, Graphics pGraphics)
        {
            return CreateGlyphButtonTextRectangle(pCheckBox, GetCheckBoxGlyphSize(pGraphics, pCheckBox.FlatStyle));
        }
    }
}

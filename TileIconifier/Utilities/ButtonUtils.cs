using System.Drawing;
using System.Windows.Forms;

namespace TileIconifier.Utilities
{
    internal static class ButtonUtils
    {
        internal static readonly TextFormatFlags BaseTextFormatFlags = TextFormatFlags.WordBreak | TextFormatFlags.TextBoxControl;
        
        private static Rectangle CreatePaddedRectangle(Rectangle rect, Padding pad)
        {
            Rectangle r = Rectangle.FromLTRB(
                    rect.Left + pad.Left,
                    rect.Top + pad.Top,
                    rect.Right - pad.Right,
                    rect.Bottom - pad.Bottom);
            return r;
        }
        
        private static Size GetCheckBoxGlyphSize(Graphics graphics, FlatStyle flatStyle)
        {
            float scaleX = graphics.DpiX / 96F;
            float scaleY = graphics.DpiY / 96F;

            switch (flatStyle)
            {
                case FlatStyle.Flat:
                case FlatStyle.Popup:
                    //In the .Net 4.6 Reference Source, the size of the checkmark is a 
                    //constant called "flatCheckSize" in a class called CheckBoxBaseAdapter.
                    return new Size((int)(11 * scaleX), (int)(11 * scaleY));
                default:
                    //We don't bother with states here. We just assume 
                    //that all states have the same size.
                    return CheckBoxRenderer.GetGlyphSize(graphics, System.Windows.Forms.VisualStyles.CheckBoxState.CheckedNormal);
            }
        }
                
        private static Size GetRadioButtonGlyphSize(Graphics graphics, FlatStyle flatStyle)
        {
            float scaleX = graphics.DpiX / 96F;
            float scaleY = graphics.DpiY / 96F;

            switch (flatStyle)
            {
                case FlatStyle.Flat:
                case FlatStyle.Popup:
                    //In the .Net 4.6 Reference Source, the size of the checkmark is a 
                    //constant called "flatCheckSize" in a class called RadioButtonFlatAdapter.
                    return new Size((int)(12 * scaleX), (int)(12 * scaleY));
                default:
                    //We don't bother with states here. We just assume 
                    //that all states have the same size.
                    return RadioButtonRenderer.GetGlyphSize(graphics, System.Windows.Forms.VisualStyles.RadioButtonState.CheckedNormal);
            }
        }

        private static Rectangle CreatePushButtonTextRectangle(Control control)
        {
            return CreatePaddedRectangle(control.ClientRectangle, control.Padding);
        }

        private static Rectangle CreateGlyphButtonTextRectangle(Control control, Size glyphSize)
        {
            //Spacing between the edge of the glyph and the outer edge of the check area.
            //1px padding + 1px for the glyph border.
            const int GLYPH_ADDITIONNAL_SPACE = 2;
            //Some mysterious spacing on the left and right sides of the text.
            const int TEXT_LATTERAL_PADDING = 1;

            Rectangle contentRect = CreatePaddedRectangle(control.ClientRectangle, control.Padding);
            Size checkAreaSize = new Size(glyphSize.Width + GLYPH_ADDITIONNAL_SPACE, glyphSize.Height + GLYPH_ADDITIONNAL_SPACE);
            Point textRectLocation;
            if (control.RightToLeft != RightToLeft.Yes)
            {
                textRectLocation = new Point(contentRect.X + checkAreaSize.Width + TEXT_LATTERAL_PADDING, contentRect.Y);
            }
            else
            {
                textRectLocation = new Point(contentRect.X + TEXT_LATTERAL_PADDING);
            }
            Size textRectSize = new Size(contentRect.Width - checkAreaSize.Width - 2 * TEXT_LATTERAL_PADDING, contentRect.Height);
            Rectangle textRect = new Rectangle(textRectLocation, textRectSize);

            return textRect;
        }
                
        /// <summary>
        /// Converts a ContentAlignement value into a TextFormatFlags.
        /// </summary>
        /// <param name="contentAlign"></param>
        /// <returns></returns>
        private static TextFormatFlags ConvertToTextFormatFlags(ContentAlignment contentAlign)
        {
            TextFormatFlags flags = new TextFormatFlags();

            //Top
            if (contentAlign == ContentAlignment.TopLeft || contentAlign == ContentAlignment.TopCenter || contentAlign == ContentAlignment.TopRight)            
                flags = flags | TextFormatFlags.Top;
            
            //Middle
            if (contentAlign == ContentAlignment.MiddleLeft || contentAlign == ContentAlignment.MiddleCenter || contentAlign == ContentAlignment.MiddleRight)            
                flags = flags | TextFormatFlags.VerticalCenter;            

            //Bottom
            if (contentAlign == ContentAlignment.BottomLeft || contentAlign == ContentAlignment.BottomCenter || contentAlign == ContentAlignment.BottomRight)
                flags = flags | TextFormatFlags.Bottom;            

            //Left
            if (contentAlign == ContentAlignment.BottomLeft || contentAlign == ContentAlignment.MiddleLeft || contentAlign == ContentAlignment.TopLeft)            
                flags = flags | TextFormatFlags.Left;
           
            //Center
            if (contentAlign == ContentAlignment.BottomCenter || contentAlign == ContentAlignment.MiddleCenter || contentAlign == ContentAlignment.TopCenter)            
                flags = flags | TextFormatFlags.HorizontalCenter;
           
            //Right
            if (contentAlign == ContentAlignment.BottomRight || contentAlign == ContentAlignment.MiddleRight || contentAlign == ContentAlignment.TopRight)            
                flags = flags | TextFormatFlags.Right;            

            return flags;
        }

        /// <summary>
        /// Returns a <see cref="TextFormatFlags"/> for the specified button.
        /// </summary>
        /// <param name="btn"></param>
        /// <param name="translatedContentAlign">A <see cref="ContentAlignment"/> value that specifies the 
        /// text alignement for the control. Note that this value must be provided already translated for
        /// right-to-left, if desired</param>
        /// <param name="showKeyboardCue"></param>
        /// <returns></returns>
        public static TextFormatFlags CreateTextFormatFlags(ButtonBase btn, ContentAlignment translatedContentAlign, bool showKeyboardCue)
        {
            var flags = BaseTextFormatFlags | ConvertToTextFormatFlags(translatedContentAlign);

            if (btn.RightToLeft == RightToLeft.Yes)
            {
                flags |= TextFormatFlags.RightToLeft;
            }

            if (!btn.UseMnemonic)
            {
                //Show the ampersand
                flags |= TextFormatFlags.NoPrefix;
            }
            else if (!showKeyboardCue)
            {
                //Hide the cue
                flags |= TextFormatFlags.HidePrefix;
            }

            return flags;
        }
                
        /// <summary>
        /// Returns a rectangle where the text can be drawn on a push button.
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        public static Rectangle GetPushButtonTextRectangle(ButtonBase button)
        {
            return CreatePushButtonTextRectangle(button);
        }

        /// <summary>
        /// Returns a rectangle where the text can be drawn on a radio button.
        /// </summary>
        /// <param name="radioButton"></param>
        /// <param name="graphics"></param>
        /// <returns></returns>
        public static Rectangle GetRadioButtonTextRectangle(RadioButton radioButton, Graphics graphics)
        {
            return CreateGlyphButtonTextRectangle(radioButton, GetRadioButtonGlyphSize(graphics, radioButton.FlatStyle));
        }

        /// <summary>
        /// Returns a rectangle where the text can be drawn on a check box.
        /// </summary>
        /// <param name="checkBox"></param>
        /// <param name="graphics"></param>
        /// <returns></returns>
        public static Rectangle GetCheckBoxTextRectangle(CheckBox checkBox, Graphics graphics)
        {
            return CreateGlyphButtonTextRectangle(checkBox, GetCheckBoxGlyphSize(graphics, checkBox.FlatStyle));
        }
    }
}

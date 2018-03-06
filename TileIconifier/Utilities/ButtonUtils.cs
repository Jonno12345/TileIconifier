using System;
using System.Drawing;
using System.Windows.Forms;
using TileIconifier.Core.Utilities;

namespace TileIconifier.Utilities
{
    internal static class ButtonUtils
    {
        private static readonly TextFormatFlags BaseTextFormatFlags = TextFormatFlags.WordBreak | TextFormatFlags.TextBoxControl;
        
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
            return LayoutAndPaintUtils.InflateRectangle(control.ClientRectangle, control.Padding);
        }

        private static Rectangle CreateGlyphButtonTextRectangle(Control control, Size glyphSize)
        {
            //Spacing between the edge of the glyph and the outer edge of the check area.
            //1px padding + 1px for the glyph border.
            const int GLYPH_ADDITIONNAL_SPACE = 2;
            //Some mysterious spacing on the left and right sides of the text.
            const int TEXT_LATTERAL_PADDING = 1;

            Rectangle contentRect = LayoutAndPaintUtils.InflateRectangle(control.ClientRectangle, control.Padding);
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
            var flags = BaseTextFormatFlags | LayoutAndPaintUtils.ConvertToTextFormatFlags(translatedContentAlign);

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

        /// <summary>
        ///     Set the Image of the specified buttons to the specified images, scaled to the specified logical size.
        /// </summary>
        /// <param name="buttons"></param>
        /// <param name="images"></param>
        /// <param name="logicalMaxSize"></param>
        internal static void SetScaledImage(ButtonBase[] buttons, Image[] images, Size logicalMaxSize)
        {
            if (buttons == null)
            {
                throw new ArgumentNullException(nameof(buttons));
            }
            if (images == null)
            {
                throw new ArgumentNullException(nameof(images));
            }
            if (buttons.Length < 1 || images.Length < 1 || buttons.Length != images.Length)
            {
                throw new ArgumentException("The amount of specified buttons must be equal to the amount of specified images. Furthermore, both must be greater than 0.");
            }
            //When the app targets .net 4.7 or higher, use Control.LogicalToDeviceUnit() instead
            //of calculating the scaling factor ourselves.
            float scaleX;
            float scaleY;
            using (var g = buttons[0].CreateGraphics())
            {
                scaleX = g.DpiX / 96F;
                scaleY = g.DpiY / 96F;
            }
            var imgWidth = (int)Math.Round(logicalMaxSize.Width * scaleX);
            var imgHeight = (int)Math.Round(logicalMaxSize.Height * scaleY);
            //
            for (var i = 0; i < buttons.Length; i++)
            {
                buttons[i].Image = ImageUtils.ScaleImage(images[i], imgWidth, imgHeight);
            }
        }
    }
}

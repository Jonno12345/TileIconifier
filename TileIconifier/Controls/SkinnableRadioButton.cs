using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using TileIconifier.Skinning.Skins;
using TileIconifier.Utilities;

namespace TileIconifier.Controls
{
    class SkinnableRadioButton : RadioButton, ISkinnableCheckableButton
    {
        private bool basePainting;

        public override string Text
        {
            get
            {
                //Lies to the base class by telling it there is no text to draw
                //so that we can draw it ourselves.
                if (basePainting && !Enabled)
                    return "";
                else
                    return base.Text;
            }
            set { base.Text = value; }
        }

        private Color disabledForeColor = SystemColors.GrayText;
        /// <summary>
        /// Gets or sets the foreground color of the button when it is disabled.
        /// </summary>
        [DefaultValue(typeof(Color), nameof(SystemColors.GrayText))]
        public Color DisabledForeColor
        {
            get { return disabledForeColor; }
            set
            {
                if (disabledForeColor != value)
                {
                    disabledForeColor = value;
                    Invalidate();
                }
            }
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            basePainting = true;
            base.OnPaint(pevent);
            basePainting = false;

            //We paint the disabled text on top of the base class drawing using 
            //the ForeColorDisabled color that we have implemented ourselves.
            //Incomplete implementation: We don't consider the TextImageRelation property (yet).
            
            if (!Enabled)
            {
                TextFormatFlags flags;
                Rectangle textRect;

                //Create flags
                flags = ButtonUtils.CreateTextFormatFlags(this, RtlTranslateContent(TextAlign), ShowKeyboardCues);

                //Create rectangle
                if (Appearance == Appearance.Button)
                {
                    textRect = ButtonUtils.GetPushButtonTextRectangle(this);
                }
                else
                {                    
                    textRect = ButtonUtils.GetRadioButtonTextRectangle(this, pevent.Graphics);
                }

                //Draw
                TextRenderer.DrawText(pevent.Graphics, Text, Font, textRect, DisabledForeColor, flags);
            }
        }

        public void ApplySkin(BaseSkin skin)
        {
            if (Appearance == Appearance.Button)
            {
                FlatStyle = skin.ButtonFlatStyle;
                ForeColor = skin.ButtonForeColor;
                BackColor = skin.ButtonBackColor;
                DisabledForeColor = skin.ButtonDisabledForeColor;
                FlatAppearance.BorderColor = skin.ButtonFlatBorderColor;
            }
            else
            {
                //We treat checkboxes and radiobutton like labels (their Fore/Back colors
                //are ambiant) so there is no need to apply skin properties manually. The
                //only exceptions are FlatStyle and DisabledForeColor, which are not ambiant 
                //and specific to ISkinnableButton in the case of FlatStyle.
                FlatStyle = skin.ButtonFlatStyle;
                DisabledForeColor = skin.DisabledForeColor;
            }
        }
    }
}

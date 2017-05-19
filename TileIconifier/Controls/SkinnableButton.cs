using System.Windows.Forms;
using System.Drawing;
using TileIconifier.Utilities;
using System.ComponentModel;
using TileIconifier.Skinning.Skins;

namespace TileIconifier.Controls
{
    class SkinnableButton : Button, ISkinnableButton
    {
        private bool basePainting = false;  

        public override string Text
        {
            get
            {
                //Lies to the base class by telling it there is no text to draw.                
                if (basePainting && !Enabled)
                    return "";
                else
                    return base.Text;
            }
            set { base.Text = value; }
        }

        public override Color BackColor
        {
            get { return base.BackColor; }
            set
            {
                base.BackColor = value;
                //We can't override the OnBackColorChanged method for this
                //because it is only called when the BackColor property 
                //value itself is changed.
                if (BackColor == DefaultBackColor)
                    UseVisualStyleBackColor = true;
            }
        }

        public void ApplySkin(BaseSkin skin)
        {
            FlatStyle = skin.ButtonFlatStyle;
            ForeColor = skin.ButtonForeColor;
            BackColor = skin.ButtonBackColor;
            DisabledForeColor = skin.ButtonDisabledForeColor;
            FlatAppearance.BorderColor = skin.ButtonFlatBorderColor;
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
                    Invalidate(ButtonUtils.GetPushButtonTextRectangle(this));
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
                Rectangle contentRect = ButtonUtils.GetPushButtonTextRectangle(this);
                TextFormatFlags flags = ButtonUtils.CreateTextFormatFlags(this, RtlTranslateContent(TextAlign), ShowKeyboardCues);

                TextRenderer.DrawText(pevent.Graphics, Text, Font, contentRect, DisabledForeColor, flags);
            }                
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using TileIconifier.Utilities;

namespace TileIconifier.Controls
{
    class SkinnableButton : Button, ISkinnableButton
    {
        private bool boBasePainting = false;  

        public override string Text
        {
            get
            {
                //Lies to the base class by telling it there is no text to draw.                
                if (boBasePainting && !Enabled)
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
                //value itself is changed. However, the UseVisualStyleBackColor
                //property is always set to false when the BackColor property setter
                //is ran, even when the property value is not really changed,
                //so we need to to the same.
                if (BackColor == DefaultBackColor)
                    UseVisualStyleBackColor = true;
            }
        }

        private Color disabledForeColor = SystemColors.GrayText;
        /// <summary>
        /// Gets or sets the foreground color of the button when it is disabled.
        /// </summary>
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
            boBasePainting = true;
            base.OnPaint(pevent);
            boBasePainting = false;

            //We paint the disabled text on top of the base class drawing using 
            //the ForeColorDisabled color that we have implemented ourselves.
            //Rudimentary implementation. Some properties may be ignored.
            if (!Enabled)
            {
                Rectangle contentRect = ButtonUtils.GetPushButtonTextRectangle(this);
                TextFormatFlags flags;
                flags = ButtonUtils.BaseTextFormatFlags | ButtonUtils.ConvertToTextFormatFlags(RtlTranslateContent(TextAlign));
                if (RightToLeft == RightToLeft.Yes)
                {
                    flags |= TextFormatFlags.RightToLeft;
                }

                TextRenderer.DrawText(pevent.Graphics, Text, Font, contentRect, DisabledForeColor, flags);
            }                
        }
    }
}

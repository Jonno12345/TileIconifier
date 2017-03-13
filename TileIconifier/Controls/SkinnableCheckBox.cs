using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TileIconifier.Forms;
using System.Windows.Forms.VisualStyles;
using TileIconifier.Utilities;
using System.ComponentModel;

namespace TileIconifier.Controls
{
    class SkinnableCheckBox : CheckBox, ISkinnableCheckableButton
    {
        private bool boBasePainting;

        public override string Text
        {
            get
            {
                //Lies to the base class by telling it there is no text to draw
                //so that we can draw it ourselves.
                if (boBasePainting && !Enabled)
                    return "";
                else
                    return base.Text;
            }
            set { base.Text = value; }
        }

        private Color disabledForeColor;
        /// <summary>
        /// Gets or sets the foreground color of the button when it is disabled.
        /// </summary>
        [DefaultValue(typeof(Color), nameof(SystemColors.GrayText))]
        public Color DisabledForeColor
        {
            get
            {
                //If the checkbox has the appearance of a checkbox, we treat its
                //text like a label. Therefore, we want this property to be ambiant.
                if (disabledForeColor.IsEmpty)
                {
                    if (Appearance == Appearance.Button)
                    {
                        return SystemColors.GrayText;
                    }
                    else
                    {
                        SkinnableForm frm = TopLevelControl as SkinnableForm;
                        if (frm != null && frm.FormSkin != null)
                            return frm.FormSkin.DisabledForeColor;
                        else
                            return SystemColors.GrayText;
                    }
                }
                else
                {
                    return disabledForeColor;
                }
            }
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
            boBasePainting = true;
            base.OnPaint(pevent);
            boBasePainting = false;

            //We paint the disabled text on top of the base class drawing using 
            //the ForeColorDisabled color that we have implemented ourselves.
            //Rudimentary implementation. Some properties are ignored.
            
            if (!Enabled)
            {
                TextFormatFlags flags;
                Rectangle textRect;

                flags = ButtonUtils.BaseTextFormatFlags | ButtonUtils.ConvertToTextFormatFlags(RtlTranslateContent(TextAlign));
                if (RightToLeft == RightToLeft.Yes)
                {
                    flags |= TextFormatFlags.RightToLeft;
                }
                if (Appearance == Appearance.Button)
                {
                    textRect = ButtonUtils.GetPushButtonTextRectangle(this);
                }
                else
                {
                    textRect = ButtonUtils.GetCheckBoxTextRectangle(this, pevent.Graphics);
                }

                TextRenderer.DrawText(pevent.Graphics, Text, Font, textRect, DisabledForeColor, flags);
            }                
        }
    }
}

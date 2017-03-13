using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TileIconifier.Forms;
using System.Windows.Forms.VisualStyles;
using System.Drawing;
using TileIconifier.Utilities;

namespace TileIconifier.Controls
{
    class SkinnableRadioButton : RadioButton, ISkinnableCheckableButton
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

        private Color foreColorDisabled;
        /// <summary>
        /// Gets or set the foreground color of the button when it is disabled.
        /// </summary>
        public Color DisabledForeColor
        {
            get
            {
                //If the checkbox has the appearance of a checkbox, we treat its
                //text like a label. Therefore, we want this property to be Ambiant. UNTESTED
                if (foreColorDisabled.IsEmpty && Appearance != Appearance.Button)
                {
                    SkinnableForm frm = TopLevelControl as SkinnableForm;
                    if (frm != null && frm.FormSkin != null)
                        return frm.FormSkin.DisabledForeColor;
                    else
                        return foreColorDisabled;
                }
                else
                    return foreColorDisabled;
            }

            set
            {
                if (foreColorDisabled != value)
                    foreColorDisabled = value;
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
                    textRect = ButtonUtils.GetRadioButtonTextRectangle(this, pevent.Graphics);
                }

                TextRenderer.DrawText(pevent.Graphics, Text, Font, textRect, DisabledForeColor, flags);
            }
        }        
    }
}

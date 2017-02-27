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

namespace TileIconifier.Controls
{
    class SkinnableCheckBox : CheckBox
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
        public Color ForeColorDisabled
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
            //Very rudimentary implementation. Properties like TextAlign are ignored.
            if (!Enabled)
            {
                const int inGlyphPadding = 1;
                const int inCheckAreaAndTextAreaSpacing = 2;

                Size checkAreaSize = GetCheckSize(pevent) + new Size(inGlyphPadding, inGlyphPadding);
                Rectangle textRect = ButtonUtils.GetGlyphButtonTextRect(checkAreaSize, ClientRectangle, inCheckAreaAndTextAreaSpacing);
                TextFormatFlags flags = ButtonUtils.ConvertToTextFormatFlags(TextAlign);             

                TextRenderer.DrawText(pevent.Graphics, Text, Font, textRect, ForeColorDisabled, flags);
            }                
        }

        private Size GetCheckSize(PaintEventArgs pevent)
        {
            switch (FlatStyle)
            {
                case FlatStyle.Flat:
                case FlatStyle.Popup:                
                    //According to the .Net 4.6 Reference Source, the size of the checkmark is a 
                    //constant called "flatCheckSize" in a class called CheckBoxBaseAdapter.
                    return new Size(11, 11);
                default:
                    //We don't bother with states here. We just assume 
                    //that all states have the same size.
                    return CheckBoxRenderer.GetGlyphSize(pevent.Graphics, CheckBoxState.CheckedNormal);     
            }             
        }
    }
}

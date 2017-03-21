using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TileIconifier.Controls
{
    class SkinnableComboBox : ComboBox
    {
        private const TextFormatFlags DEFAULT_TEXT_FLAGS = TextFormatFlags.VerticalCenter;        

        private Font glyphFont = new Font("Marlett", 10);

        private bool CanCustomDraw
        {
            get
            {
                if (FlatStyle == FlatStyle.Flat && DropDownStyle == ComboBoxStyle.DropDownList)
                    return true;
                else
                    return false;
            }
        }

        private TextFormatFlags TextFlags
        {
            get
            {
                TextFormatFlags flags = DEFAULT_TEXT_FLAGS;
                if (RightToLeft == RightToLeft.Yes)
                    flags = flags | TextFormatFlags.RightToLeft | TextFormatFlags.Right;

                return flags;
            }
        }

        //There is no event for when this properties is changed,
        //so we need reimplement it to allow us to do stuff when it is changed.
        public new FlatStyle FlatStyle
        {
            get { return base.FlatStyle; }
            set
            {
                base.FlatStyle = value;
                ConfigureDrawingProperties(true);
            }
        }
        
        private Color flatButtonBackColor = SystemColors.Control;
        [DefaultValue(typeof(Color), nameof(SystemColors.Control))]
        public Color FlatButtonBackColor
        {
            get { return flatButtonBackColor; }
            set
            {
                if (flatButtonBackColor != value)
                {
                    flatButtonBackColor = value;
                    if (CanCustomDraw)
                    {
                        Invalidate();
                    }
                }
            }
        }
                
        private Color flatButtonForeColor = SystemColors.ControlText;
        [DefaultValue(typeof(Color), nameof(SystemColors.ControlText))]
        public Color FlatButtonForeColor
        {
            get { return flatButtonForeColor; }
            set
            {
                if (flatButtonForeColor != value)
                {
                    flatButtonForeColor = value;
                    if (CanCustomDraw)
                    {
                        Invalidate();
                    }
                }
            }
        }
        
        private Color flatButtonDisabledForeColor = SystemColors.GrayText;
        [DefaultValue(typeof(Color), nameof(SystemColors.GrayText))]
        public Color FlatButtonDisabledForeColor
        {
            get { return flatButtonDisabledForeColor; }
            set
            {
                if (flatButtonDisabledForeColor != value)
                {
                    flatButtonDisabledForeColor = value;
                    if (CanCustomDraw)
                    {
                        Invalidate();
                    }
                }
            }
        }
        
        private Color flatButtonBorderColor = SystemColors.ControlDark;
        [DefaultValue(typeof(Color), nameof(SystemColors.ControlDark))]
        public Color FlatButtonBorderColor
        {
            get { return flatButtonBorderColor; }
            set
            {
                if (flatButtonBorderColor != value)
                {
                    flatButtonBorderColor = value;
                    if (CanCustomDraw)
                    {
                        Invalidate();
                    }
                }
            }
        }
        
        private Color flatButtonBorderFocusedColor = SystemColors.Highlight;
        [DefaultValue(typeof(Color), nameof(SystemColors.Highlight))]
        public Color FlatButtonBorderFocusedColor
        {
            get { return flatButtonBorderFocusedColor; }
            set
            {
                if (flatButtonBorderFocusedColor != value)
                {
                    flatButtonBorderFocusedColor = value;
                    if (CanCustomDraw)
                    {
                        Invalidate();
                    }
                }
            }
        }

        private void ConfigureDrawingProperties(bool needToRestoreDefaultProperties)
        {
            if (CanCustomDraw)
            {
                SetStyle(ControlStyles.UserPaint, true);
                DrawMode = DrawMode.OwnerDrawFixed;
            }
            else if (needToRestoreDefaultProperties)
            {
                //These values could change in a future version of Winforms.
                SetStyle(ControlStyles.UserPaint, false);
                DrawMode = DrawMode.Normal;
            }
        }

        protected override void OnDropDownStyleChanged(EventArgs e)
        {
            base.OnDropDownStyleChanged(e);

            ConfigureDrawingProperties(true);
        }

        protected override void OnPaint(PaintEventArgs e)
        {  
            if (CanCustomDraw)
            {
                Rectangle bounds = ClientRectangle;
                int glyphAreaWidth = SystemInformation.HorizontalScrollBarThumbWidth;

                //Border
                Color borderColor = (Focused) ? FlatButtonBorderFocusedColor : FlatButtonBorderColor;
                //Compensation needed when drawing a rectangle with GDI+
                bounds.Width--;
                bounds.Height--;

                using (var p = new Pen(borderColor))
                    e.Graphics.DrawRectangle(p, bounds);

                //Background
                //Removes the 1 pixel GDI+ compensation.
                bounds.Width++;
                bounds.Height++;
                //Skrinks the rectangle to fit within the borders we have just drawn.
                bounds.Inflate(-1, -1);

                using (var b = new SolidBrush(FlatButtonBackColor))
                    e.Graphics.FillRectangle(b, bounds);

                //Selected item text
                Color textColor = (Enabled) ? FlatButtonForeColor : FlatButtonDisabledForeColor;
                //We need to calculate the text bounds even when we don't draw text, 
                //because we use that rectangle for the glyphRect.
                //Same thing for the textColor.
                if (RightToLeft == RightToLeft.Yes)
                {
                    bounds.X += glyphAreaWidth;
                }
                bounds.Width -= glyphAreaWidth;
                if (SelectedIndex >= 0 && SelectedIndex < Items.Count)
                {
                    string text = GetItemText(Items[SelectedIndex]);
                    TextRenderer.DrawText(e.Graphics, text, Font, bounds, textColor, TextFlags);
                }

                //Glyph button
                Rectangle buttonRect = new Rectangle();
                buttonRect.Width = glyphAreaWidth;
                buttonRect.Height = bounds.Height;
                buttonRect.Y = bounds.Y;
                TextFormatFlags glyphFlags = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter;
                if (RightToLeft == RightToLeft.Yes)
                {
                    buttonRect.X = bounds.X - glyphAreaWidth;
                }
                else
                {
                    buttonRect.X = bounds.X + bounds.Width;
                }
                TextRenderer.DrawText(e.Graphics, "u", glyphFont, buttonRect, textColor, glyphFlags);
            }

            //The call to base.OnPaint must be after our custom drawing, in order to be
            //consistent with standard Winform controls and raise the Paint event after
            //the base drawing.
            base.OnPaint(e);
        }

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            //This method is only called when items are custom drawn and 
            //items always look the same regardless of the DropDownStyle anyway,
            //so there is no need to check if CanCustomDraw.

            e.DrawBackground();
            e.DrawFocusRectangle();

            int index = e.Index;
            if (index >= 0 && index < Items.Count)
            {
                string itemText = GetItemText(Items[index]);
                Color colTextColor = (e.State.HasFlag(DrawItemState.Selected)) ? SystemColors.HighlightText : ForeColor;                

                TextRenderer.DrawText(e.Graphics, itemText, Font, e.Bounds, colTextColor, TextFlags);
            }

            base.OnDrawItem(e);            
        }
    }
}

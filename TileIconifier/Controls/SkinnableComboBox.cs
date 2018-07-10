using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using TileIconifier.Skinning.Skins;

namespace TileIconifier.Controls
{
    class SkinnableComboBox : ComboBox, ISkinnableControl
    {
        private const TextFormatFlags DEFAULT_TEXT_FLAGS = TextFormatFlags.VerticalCenter;        
        
        //Fonts are very expensive to create and this one is only used
        //with the Flat FlatStyle, so it's worth loading it lazyly.
        private readonly Lazy<Font> _glyphFont = new Lazy<Font>(() => new Font("Marlett", 10));

        /// <summary>
        ///     Indicates whether or not we should draw the control ourselves.
        /// </summary>
        private bool HandleDrawing => FlatStyle == FlatStyle.Flat && DropDownStyle == ComboBoxStyle.DropDownList;

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
        //so we need to reimplement it to allow us to do stuff when it is changed.
        //Not ideal since this could be bypassed. Will think about this...
        public new FlatStyle FlatStyle
        {
            get { return base.FlatStyle; }
            set
            {
                base.FlatStyle = value;
                ConfigureDrawingProperties();
            }
        }
        
        private Color _flatButtonBackColor = SystemColors.Control;
        [DefaultValue(typeof(Color), nameof(SystemColors.Control))]
        public Color FlatButtonBackColor
        {
            get { return _flatButtonBackColor; }
            set
            {
                if (_flatButtonBackColor != value)
                {
                    _flatButtonBackColor = value;
                    if (HandleDrawing)
                    {
                        Invalidate();
                    }
                }
            }
        }
                
        private Color _flatButtonForeColor = SystemColors.ControlText;
        [DefaultValue(typeof(Color), nameof(SystemColors.ControlText))]
        public Color FlatButtonForeColor
        {
            get { return _flatButtonForeColor; }
            set
            {
                if (_flatButtonForeColor != value)
                {
                    _flatButtonForeColor = value;
                    if (HandleDrawing && Enabled)
                    {
                        Invalidate();
                    }
                }
            }
        }
        
        private Color _flatButtonDisabledForeColor = SystemColors.GrayText;
        [DefaultValue(typeof(Color), nameof(SystemColors.GrayText))]
        public Color FlatButtonDisabledForeColor
        {
            get { return _flatButtonDisabledForeColor; }
            set
            {
                if (_flatButtonDisabledForeColor != value)
                {
                    _flatButtonDisabledForeColor = value;
                    if (HandleDrawing && !Enabled)
                    {
                        Invalidate();
                    }
                }
            }
        }
        
        private Color _flatButtonBorderColor = SystemColors.ControlDark;
        [DefaultValue(typeof(Color), nameof(SystemColors.ControlDark))]
        public Color FlatButtonBorderColor
        {
            get { return _flatButtonBorderColor; }
            set
            {
                if (_flatButtonBorderColor != value)
                {
                    _flatButtonBorderColor = value;
                    if (HandleDrawing && !Focused)
                    {
                        Invalidate();
                    }
                }
            }
        }
        
        private Color _flatButtonBorderFocusedColor = SystemColors.Highlight;
        [DefaultValue(typeof(Color), nameof(SystemColors.Highlight))]
        public Color FlatButtonBorderFocusedColor
        {
            get { return _flatButtonBorderFocusedColor; }
            set
            {
                if (_flatButtonBorderFocusedColor == value)
                {
                    return;
                }
                _flatButtonBorderFocusedColor = value;
                if (HandleDrawing && Focused)
                {
                    Invalidate();
                }
            }
        }

        private void ConfigureDrawingProperties()
        {
            if (HandleDrawing)
            {
                SetStyle(ControlStyles.UserPaint, true);
                DrawMode = DrawMode.OwnerDrawFixed;
                DoubleBuffered = true;
            }
            else
            {
                //These values could change in a future version of Winforms.
                SetStyle(ControlStyles.UserPaint, false);
                DrawMode = DrawMode.Normal;
                DoubleBuffered = false;
            }
        }

        protected override void OnDropDownStyleChanged(EventArgs e)
        {
            base.OnDropDownStyleChanged(e);

            ConfigureDrawingProperties();
        }

        protected override void OnPaint(PaintEventArgs e)
        {  
            if (HandleDrawing)
            {
                Rectangle bounds = ClientRectangle;
                int glyphAreaWidth = SystemInformation.HorizontalScrollBarThumbWidth;

                //Border
                Color borderColor = Focused ? FlatButtonBorderFocusedColor : FlatButtonBorderColor;                
                ControlPaint.DrawBorder(e.Graphics, bounds, borderColor, ButtonBorderStyle.Solid);

                //Background                
                //Skrinks the rectangle to fit within the borders we have just drawn.
                bounds.Inflate(-1, -1);

                using (var b = new SolidBrush(FlatButtonBackColor))
                    e.Graphics.FillRectangle(b, bounds);

                //Selected item text
                Color textColor = Enabled ? FlatButtonForeColor : FlatButtonDisabledForeColor;
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
                Rectangle buttonRect = new Rectangle
                {
                    Width = glyphAreaWidth,
                    Height = bounds.Height,
                    Y = bounds.Y
                };
                TextFormatFlags glyphFlags = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter;
                if (RightToLeft == RightToLeft.Yes)
                {
                    buttonRect.X = bounds.X - glyphAreaWidth;
                }
                else
                {
                    buttonRect.X = bounds.X + bounds.Width;
                }
                TextRenderer.DrawText(e.Graphics, "u", _glyphFont.Value, buttonRect, textColor, glyphFlags);
            }

            //The call to base.OnPaint must be after our custom drawing, in order to be
            //consistent with standard Winform controls and raise the Paint event after
            //the base drawing.
            base.OnPaint(e);
        }

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            //This method is only called when items are owner drawn
            //so there is no need to check if HandleDrawing.

            e.DrawBackground();
            e.DrawFocusRectangle();

            int index = e.Index;
            if (index >= 0 && index < Items.Count)
            {
                string itemText = GetItemText(Items[index]);
                Color colTextColor = e.State.HasFlag(DrawItemState.Selected) ? SystemColors.HighlightText : ForeColor;                

                TextRenderer.DrawText(e.Graphics, itemText, Font, e.Bounds, colTextColor, TextFlags);
            }

            base.OnDrawItem(e);            
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && _glyphFont.IsValueCreated)
            {
                _glyphFont.Value.Dispose();
            }

            base.Dispose(disposing);
        }

        public void ApplySkin(BaseSkin skin)
        {
            FlatStyle = skin.ComboBoxFlatStyle;
            BackColor = skin.ComboBoxBackColor;
            ForeColor = skin.ComboBoxForeColor;
            FlatButtonBackColor = skin.ComboBoxButtonBackColor;
            FlatButtonForeColor = skin.ComboboxButtonForeColor;
            FlatButtonDisabledForeColor = skin.ComboBoxDisabledForeColor;
            FlatButtonBorderColor = skin.ComboBoxButtonBorderColor;
            FlatButtonBorderFocusedColor = skin.ComboBoxButtonBorderFocusedColor;
        }
    }
}

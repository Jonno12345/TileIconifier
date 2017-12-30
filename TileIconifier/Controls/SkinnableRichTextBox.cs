using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using TileIconifier.Skinning.Skins;
using TileIconifier.Utilities;

namespace TileIconifier.Controls
{
    class SkinnableRichTextBox : RichTextBox, ISkinnableTextBox
    {
        private const string VSCLASS_EDIT = "Edit";

        //Lazy loaded instance of the VisualStyleRenderer used to draw the border. To retrieve it,
        //use the GetVisualStyleRenderer Method.
        private VisualStyleRenderer _vsRenderer;

        #region "Properties"
        private Color backColor = SystemColors.Window;
        [DefaultValue(typeof(Color), nameof(SystemColors.Window))]
        public new Color BackColor
        {
            get { return backColor; }
            set
            {
                backColor = value;
                if (!ReadOnly)
                {
                    base.BackColor = value;
                }
            }
        }        

        private Color readOnlyBackColor = SystemColors.Control;
        [DefaultValue(typeof(Color), nameof(SystemColors.Control))]
        public Color ReadOnlyBackColor
        {
            get { return readOnlyBackColor; }
            set
            {
                readOnlyBackColor = value;
                if (ReadOnly)
                {
                    base.BackColor = ReadOnlyBackColor;
                }
            }
        }

        private Color _borderColor = SystemColors.WindowFrame;
        [DefaultValue(typeof(Color), nameof(SystemColors.WindowFrame))]
        public Color BorderColor
        {
            get { return _borderColor; }
            set
            {
                if (_borderColor != value)
                {
                    _borderColor = value;
                    if (BorderStyle == BorderStyle.FixedSingle &&
                        ((Enabled && !Focused) ||
                        (Focused && BorderFocusedColor.IsEmpty) ||
                        (!Enabled && BorderDisabledColor.IsEmpty)))
                    {
                        InvalidateNonClient();
                    }
                }
            }
        }

        private Color _borderFocusedColor = Color.Empty;
        [DefaultValue(typeof(Color), "")]
        public Color BorderFocusedColor
        {
            get { return _borderFocusedColor; }
            set
            {
                if (_borderFocusedColor != value)
                {
                    _borderFocusedColor = value;
                    if (Focused && BorderStyle == BorderStyle.FixedSingle)
                    {
                        InvalidateNonClient();
                    }
                }
            }
        }

        private Color _borderDisabledColor = Color.Empty;
        [DefaultValue(typeof(Color), "")]
        public Color BorderDisabledColor
        {
            get { return _borderDisabledColor; }
            set
            {
                if (_borderDisabledColor != value)
                {
                    _borderDisabledColor = value;
                    if (!Enabled && BorderStyle == BorderStyle.FixedSingle)
                    {
                        InvalidateNonClient();
                    }
                }
            }
        }
        #endregion

        private VisualStyleRenderer GetVisualStyleRenderer(VisualStyleElement vsElement)
        {
            if (_vsRenderer == null)
            {
                _vsRenderer = new VisualStyleRenderer(vsElement);
            }
            else
            {
                _vsRenderer.SetParameters(vsElement);
            }
            return _vsRenderer;
        }

        private void InvalidateNonClient()
        {
            LayoutAndPaintUtils.InvalidateNonClient(this);
        }

        protected override void OnReadOnlyChanged(EventArgs e)
        {
            base.OnReadOnlyChanged(e);

            //We use the base class property to change the actual color. 
            //This classe's BackColor property stores the not-read-only-BackColor 
            //value independently from the actual (current) Background color.
            if (ReadOnly)
            {
                base.BackColor = ReadOnlyBackColor;
            }
            else
            {
                base.BackColor = BackColor;
            }
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            if ((BorderStyle == BorderStyle.Fixed3D && VisualStyleRenderer.IsSupported) || BorderStyle == BorderStyle.FixedSingle)
            {
                InvalidateNonClient();
            }

            base.OnSizeChanged(e);
        }

        protected override void OnEnter(EventArgs e)
        {
            if ((BorderStyle == BorderStyle.Fixed3D && VisualStyleRenderer.IsSupported) || BorderStyle == BorderStyle.FixedSingle)
            {
                InvalidateNonClient();
            }

            base.OnEnter(e);
        }

        protected override void OnLeave(EventArgs e)
        {
            if ((BorderStyle == BorderStyle.Fixed3D && VisualStyleRenderer.IsSupported) || BorderStyle == BorderStyle.FixedSingle)
            {
                InvalidateNonClient();
            }

            base.OnLeave(e);
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (m.Msg == NativeMethods.WM_NCPAINT)
            {
                if (BorderStyle == BorderStyle.Fixed3D && VisualStyleRenderer.IsSupported)
                {
                    PaintVisuallyStyledBorder(m.HWnd, m.WParam);
                }
                else if (BorderStyle == BorderStyle.FixedSingle)
                {
                    PaintCustomBorder(m.HWnd, m.WParam);
                }
            }
        }        

        private void PaintVisuallyStyledBorder(IntPtr hWnd, IntPtr hRgn)
        {
            var state = !Enabled ? 4 : Focused ? 3 : 1;            

            var vsElement = VisualStyleElement.CreateElement(VSCLASS_EDIT, 6, state);
            if (VisualStyleRenderer.IsElementDefined(vsElement))
            {
                var vsRenderer = GetVisualStyleRenderer(vsElement);
                using (var ncg = new NonClientGraphics(hWnd, hRgn))
                {
                    var g = ncg.Graphics;
                    if (g == null)
                    {
                        return;
                    }
                    using (new GraphicsClippedToBorder(g, this, BorderStyle))
                    {
                        vsRenderer.DrawBackground(g, new Rectangle(new Point(0), Size));
                    }
                }
            }
        }

        private void PaintCustomBorder(IntPtr hWnd, IntPtr hRgn)
        {
            Color borderColor;
            if (!Enabled && !BorderDisabledColor.IsEmpty)
            {
                borderColor = BorderDisabledColor;
            }
            else if (Focused && !BorderFocusedColor.IsEmpty)
            {
                borderColor = BorderFocusedColor;
            }
            else
            {
                //Since this control does not support a single border out of the box, we draw the regular border
                //even if it's color is not different from the default.
                borderColor = BorderColor;
            }            

            using (var ncg = new NonClientGraphics(hWnd, hRgn))
            {
                var g = ncg.Graphics;
                if (g == null)
                {
                    return;
                }
                var borderBounds = new Rectangle(new Point(0), Size);
                ControlPaint.DrawBorder(g, borderBounds, borderColor, ButtonBorderStyle.Solid);
                //As per the Microsoft documentation for this control, the Single BorderStyle is not supported
                //and the 3D border is always used. As a workaround, we paint the inner border with the same
                //color as the back color to simulate a thinner border. I have not tried it, but I believe that 
                //a better solution would be to handle the WM_NCCALC message, but that would be more complicated...
                var borderSize = SystemInformation.BorderSize;
                borderBounds.Inflate(-borderSize.Width, -borderSize.Height);
                ControlPaint.DrawBorder(g, borderBounds, base.BackColor, ButtonBorderStyle.Solid);
            }
        }

        public void ApplySkin(BaseSkin skin)
        {
            BorderStyle = skin.TextBoxBorderStyle;
            BackColor = skin.TextBoxBackColor;
            ReadOnlyBackColor = skin.TextBoxReadOnlyBackColor;
            BorderColor = skin.TextBoxBorderColor;
            BorderFocusedColor = skin.TextBoxBorderFocusedColor;
            BorderDisabledColor = skin.TextBoxBorderDisabledColor;
            ForeColor = skin.TextBoxForeColor;
        }
    }
}

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using TileIconifier.Skinning.Skins;
using TileIconifier.Skinning.Utilities;

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

        /// <summary>
        /// Doesn't do anything yet!
        /// </summary>
        [DefaultValue(typeof(Color), "")]
        public Color BorderColor
        {
            get { return Color.Empty; }
            set
            {
                
            }
        }

        /// <summary>
        /// Doesn't do anything yet!
        /// </summary>
        [DefaultValue(typeof(Color), "")]
        public Color BorderFocusedColor
        {
            get { return Color.Empty; }
            set
            {
                
            }
        }

        /// <summary>
        /// Doesn't do anything yet!
        /// </summary>
        [DefaultValue(typeof(Color), "")]
        public Color BorderDisabledColor
        {
            get { return Color.Empty; }
            set
            {
                
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
            //to do
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

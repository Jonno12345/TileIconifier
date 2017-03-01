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
    class SkinnableListView : ListView
    {
        [DefaultValue(true)]
        private bool headersUseVisualStyleColors = true;
        public bool HeadersUseVisualStyleColors
        {
            get
            {
                return headersUseVisualStyleColors;
            }
            set
            {
                headersUseVisualStyleColors = value;
                OwnerDraw = !value;
            }
        }

        private Color headerBackColor = SystemColors.Control;
        [DefaultValue(typeof(Color), "Control")]
        public Color HeaderBackColor
        {
            get
            {
                return headerBackColor;
            }
            set
            {
                if (headerBackColor != value)
                {
                    headerBackColor = value;
                    if (HeadersUseVisualStyleColors)
                    {
                        Invalidate();
                    }
                }                
            }
        }

        private Color headerForeColor = SystemColors.ControlText;
        [DefaultValue(typeof(Color), "ControlText")]
        public Color HeaderForeColor
        {
            get
            {
                return headerForeColor;
            }
            set
            {
                if (headerForeColor != value)
                {
                    headerForeColor = value;
                    if (HeadersUseVisualStyleColors)
                    {
                        Invalidate();
                    }
                }
            }
        }
        protected override void OnDrawColumnHeader(DrawListViewColumnHeaderEventArgs e)
        {
            base.OnDrawColumnHeader(e);
            
            if (!HeadersUseVisualStyleColors)
            {
                using (SolidBrush b = new SolidBrush(HeaderBackColor))
                    e.Graphics.FillRectangle(b, e.Bounds);

                TextFormatFlags flags = 
                    TextFormatFlags.Default |
                    TextFormatFlags.VerticalCenter |
                    TextFormatFlags.EndEllipsis |
                    ConvertToTextFormatFlags(e.Header.TextAlign);

                TextRenderer.DrawText(e.Graphics, e.Header.Text, Font, e.Bounds, HeaderForeColor, flags);
            }
        }

        protected override void OnDrawItem(DrawListViewItemEventArgs e)
        {
            base.OnDrawItem(e);

            e.DrawDefault = true;
        }

        protected override void OnDrawSubItem(DrawListViewSubItemEventArgs e)
        {
            base.OnDrawSubItem(e);

            //Maybe not needed since we already set DrawDefault to true in OnDrawItem. To verify one day...
            e.DrawDefault = true;           
        }

        private TextFormatFlags ConvertToTextFormatFlags(HorizontalAlignment pHoriAlign)
        {     
            switch (pHoriAlign)
            {
                case HorizontalAlignment.Left:
                    return TextFormatFlags.Left;
                case HorizontalAlignment.Center:
                    return TextFormatFlags.HorizontalCenter;
                case HorizontalAlignment.Right:
                    return TextFormatFlags.Right;
                default:
                    throw new ArgumentException("Unsupported horizontal alignement.");
            }                
        }
    }
}

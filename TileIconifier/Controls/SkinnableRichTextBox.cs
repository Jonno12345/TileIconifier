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
    class SkinnableRichTextBox : RichTextBox, ISkinnableTextBox
    {
        #region "Properties"
        private Color backColor = SystemColors.Window;
        [DefaultValue(typeof(Color), nameof(SystemColors.Window))]
        public new Color BackColor
        {
            get { return backColor; }
            set
            {
                if (backColor != value)
                {
                    backColor = value;
                    if (!ReadOnly)
                    {
                        base.BackColor = value;
                    }
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
                if (readOnlyBackColor != value)
                {
                    readOnlyBackColor = value;
                    if (ReadOnly)
                    {
                        base.BackColor = ReadOnlyBackColor;
                    }
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
    }
}

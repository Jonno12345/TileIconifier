using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TileIconifier.Controls
{
    interface ISkinnableTextBox : ISkinnableControl
    {
        BorderStyle BorderStyle { get; set; }
        Color BackColor { get; set; }
        Color ReadOnlyBackColor { get; set; }
        Color ForeColor { get; set; }                
        Color BorderColor { get; set; }
        Color BorderFocusedColor { get; set; }
        Color BorderDisabledColor { get; set; }
    }
}

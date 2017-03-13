using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TileIconifier.Controls
{
    interface ISkinnableButton : ISkinnableControl
    {
        //Appearance
        Color ForeColor { get; set; }
        Color BackColor { get; set; }
        Color DisabledForeColor { get; set; }
        FlatStyle FlatStyle { get; set; }
        FlatButtonAppearance FlatAppearance { get; }

        //Layout
        ContentAlignment TextAlign { get; set; }
    }
}

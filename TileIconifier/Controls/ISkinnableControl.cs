using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TileIconifier.Controls
{
    interface ISkinnableControl
    {
        Rectangle ClientRectangle { get; }
        Padding Padding { get; set; }
        RightToLeft RightToLeft { get; set; }
    }
}

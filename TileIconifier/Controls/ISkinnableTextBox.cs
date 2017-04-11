using System.Drawing;
using System.Windows.Forms;

namespace TileIconifier.Controls
{
    interface ISkinnableTextBox : ISkinnableControl
    {
        BorderStyle BorderStyle { get; set; }
        Color ReadOnlyBackColor { get; set; }
        Color BorderColor { get; set; }
        Color BorderFocusedColor { get; set; }
        Color BorderDisabledColor { get; set; }
    }
}

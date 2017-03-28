using System.Drawing;
using System.Windows.Forms;

namespace TileIconifier.Controls
{
    interface ISkinnableButton : ISkinnableControl
    {
        //Appearance
        Color DisabledForeColor { get; set; }
        FlatStyle FlatStyle { get; set; }
        FlatButtonAppearance FlatAppearance { get; }        
    }
}

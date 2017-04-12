using System.Windows.Forms;

namespace TileIconifier.Controls
{
    interface ISkinnableCheckableButton : ISkinnableButton
    {
        Appearance Appearance { get; set; }
    }
}

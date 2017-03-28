using System.Drawing;
using TileIconifier.Skinning.Skins;

namespace TileIconifier.Controls
{
    interface ISkinnableControl
    {
        Color ForeColor { get; set; }
        Color BackColor { get; set; }

        void ApplySkin(BaseSkin skin);
    }
}

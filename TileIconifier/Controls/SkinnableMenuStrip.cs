using System.Windows.Forms;
using TileIconifier.Skinning.Skins;

namespace TileIconifier.Controls
{
    class SkinnableMenuStrip : MenuStrip, ISkinnableControl
    {
        public void ApplySkin(BaseSkin skin)
        {
            Renderer = skin.ToolStripRenderer;
        }
    }
}

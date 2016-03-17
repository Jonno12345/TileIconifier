using System.Drawing;

namespace TileIconifier.Custom
{
    internal class NewCustomShortcutFormCache
    {
        public Image Icon { get; set; }
        public string ShortcutName { get; set; }
        public ShortcutUser AllOrCurrentUser { get; set; }
    }
}
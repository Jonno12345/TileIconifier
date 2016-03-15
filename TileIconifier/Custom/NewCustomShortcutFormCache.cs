using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TileIconifier.Custom
{
    class NewCustomShortcutFormCache
    {
        public Image Icon { get; set; }
        public string ShortcutName { get; set; }
        public ShortcutUser AllOrCurrentUser { get; set; }
    }
}

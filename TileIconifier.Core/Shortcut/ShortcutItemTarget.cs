using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TileIconifier.Core.Shortcut
{
    [Serializable]
    public class ShortcutItemTarget
    {
        public string FilePath { get; set; }
        public string Arguments { get; set; }
        public string IconLocation { get; set; }
    }
}

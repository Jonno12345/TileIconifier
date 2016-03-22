using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TileIconifier.Utilities
{
    public static class ColorUtils
    {
        public static string ColorToHex(System.Drawing.Color c)
        {
            return "#" + c.R.ToString("X2") + c.G.ToString("X2") + c.B.ToString("X2");
        }

        public static Color HexToColor(string h)
        {
            return ColorTranslator.FromHtml(h);
        }
    }
}

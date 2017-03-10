#region LICENCE

// /*
//         The MIT License (MIT)
// 
//         Copyright (c) 2016 Johnathon M
// 
//         Permission is hereby granted, free of charge, to any person obtaining a copy
//         of this software and associated documentation files (the "Software"), to deal
//         in the Software without restriction, including without limitation the rights
//         to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//         copies of the Software, and to permit persons to whom the Software is
//         furnished to do so, subject to the following conditions:
// 
//         The above copyright notice and this permission notice shall be included in
//         all copies or substantial portions of the Software.
// 
//         THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//         IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//         FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//         AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//         LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//         OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
//         THE SOFTWARE.
// 
// */

#endregion

using System;
using System.Drawing;

namespace TileIconifier.Core.Utilities
{
    public static class ColorUtils
    {
        public static string ColorToHex(Color c)
        {
            return "#" + c.R.ToString("X2") + c.G.ToString("X2") + c.B.ToString("X2");
        }

        public static Color HexToColor(string h)
        {
            try
            {
                return ColorTranslator.FromHtml(h);
            }
            catch
            {
                return Color.Empty;
            }
        }

        public static Color HexOrNameToColor(string h)
        {
            var tryColorFromName = Color.FromName(h);
            return tryColorFromName.IsKnownColor ? tryColorFromName : HexToColor(h);
        }

        public static Color BlendColors(Color pColor1, int pCol1Weight, Color pColor2, int pCol2Weight)
        {
            int a1 = pColor1.A;
            int r1 = pColor1.R;
            int g1 = pColor1.G;
            int b1 = pColor1.B;

            int a2 = pColor2.A;
            int r2 = pColor2.R;
            int g2 = pColor2.G;
            int b2 = pColor2.B;

            int inTotalIntensity = pCol1Weight + pCol2Weight;

            if (inTotalIntensity <= 0)
                throw new ArgumentException("The total color intensity must be greater than 0.");

            int a3 = (a1 * pCol1Weight + a2 * pCol2Weight) / inTotalIntensity;
            int r3 = (r1 * pCol1Weight + r2 * pCol2Weight) / inTotalIntensity;
            int g3 = (g1 * pCol1Weight + g2 * pCol2Weight) / inTotalIntensity;
            int b3 = (b1 * pCol1Weight + b2 * pCol2Weight) / inTotalIntensity;

            return Color.FromArgb(a3, r3, g3, b3);
        }
    }
}
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

using System.Drawing;
using System.IO;
using TileIconifier.Core.Properties;
using TileIconifier.Core.Utilities;

namespace TileIconifier.Core.Custom.Chrome
{
    public class ChromeApp
    {
        public string IconPath { get; set; }
        public string AppId { get; set; }
        public string AppName { get; set; }

        public string ChromeAppExecutionArgument =>
            $@"--profile-directory=Default --app-id={AppId}";

        public byte[] IconAsBytes
        {
            get
            {
                if (IconPath == null) return ImageUtils.ImageToByteArray(Resources.Google_Chrome_icon);

                try
                {
                    using (var readImage = new FileStream(IconPath, FileMode.Open, FileAccess.Read, FileShare.Read))
                        return ImageUtils.ImageToByteArray(new Bitmap(readImage));
                }
                catch
                {
                    // ignored
                }
                return ImageUtils.ImageToByteArray(Resources.Google_Chrome_icon);
            }
        }
    }
}
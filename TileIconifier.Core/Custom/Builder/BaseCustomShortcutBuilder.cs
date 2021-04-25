#region LICENCE

// /*
//         The MIT License (MIT)
// 
//         Copyright (c) 2021 Johnathon M
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
using System.Text;
using TileIconifier.Core.Properties;
using TileIconifier.Core.Utilities;

namespace TileIconifier.Core.Custom.Builder
{
    public abstract class BaseCustomShortcutBuilder
    {
        protected BaseCustomShortcutBuilder(GenerateCustomShortcutParams generateParameters)
        {
            Parameters = generateParameters;
        }

        protected abstract CustomShortcutType ShortcutType { get; }

        public GenerateCustomShortcutParams Parameters { get; protected set; }

        public virtual CustomShortcut GenerateCustomShortcut(string shortcutName)
        {
            var customShortcut =
                new CustomShortcut(shortcutName,
                    Parameters.ShortcutTarget,
                    Parameters.ShortcutArguments,
                    ShortcutType,
                    Parameters.WindowType,
                    Parameters.ShortcutRootPath,
                    Parameters.IconPath);

            if (string.IsNullOrEmpty(Parameters.IconPath))
                Build(customShortcut, Parameters.Image);
            else
                Build(customShortcut);

            return customShortcut;
        }


        protected void Build(CustomShortcut customShortcut, Image iconImage = null)
        {
            if (iconImage != null)
                GenerateNewIcon(customShortcut, iconImage);

            customShortcut.VbsFilePath = customShortcut.VbsFolderPath + customShortcut.ShortcutName + ".vbs";

            var targetDir = "";
            try
            {
                targetDir = $@"{new FileInfo(customShortcut.TargetPath).Directory?.FullName}\".EscapeVba();
            }
            catch
            {
                //ignore
            }

            File.WriteAllText(customShortcut.VbsFilePath,
                string.Format(Resources.CustomShortcutVbsTemplate,
                    customShortcut.ShortcutName.EscapeVba(),
                    customShortcut.ShortcutItem.ShortcutFileInfo.FullName.EscapeVba(),
                    customShortcut.TargetPath.QuoteWrap().EscapeVba(),
                    customShortcut.TargetArguments.EscapeVba(),
                    ShortcutType,
                    (int) customShortcut.WindowType,
                    targetDir
                    ), Encoding.Unicode);

            ShortcutUtils.CreateLnkFile(customShortcut.ShortcutItem.ShortcutFileInfo.FullName,
                customShortcut.VbsFilePath,
                customShortcut.ShortcutName + " shortcut created by TileIconifier",
                iconPath: customShortcut.BasicShortcutIcon,
                workingDirectory: customShortcut.WorkingFolder
                );
        }

        private void GenerateNewIcon(CustomShortcut customShortcut, Image basicIconImage)
        {
            customShortcut.BasicShortcutIcon = customShortcut.VbsFolderPath + customShortcut.ShortcutName + ".ico";
            try
            {
                using (var iconWriter = new FileStream(customShortcut.BasicShortcutIcon, FileMode.Create))
                {
                    ImageUtils.ConvertToIcon(basicIconImage, iconWriter);
                }
            }
            catch
            {
                customShortcut.BasicShortcutIcon = null;
            }
        }
    }
}
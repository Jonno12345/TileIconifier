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

using TileIconifier.Core.Utilities;

namespace TileIconifier.Core.Custom.Builder
{
    public class UriCustomShortcutBuilder : BaseCustomShortcutBuilder
    {
        public UriCustomShortcutBuilder(GenerateCustomShortcutParams generateParameters) : base(generateParameters)
        {
        }

        protected override CustomShortcutType ShortcutType { get; } = CustomShortcutType.Uri;

        public override CustomShortcut GenerateCustomShortcut(string shortcutName)
        {
            var customShortcut =
                new CustomShortcut(shortcutName,
                    Parameters.ShortcutTarget,
                    string.Empty,
                    ShortcutType,
                    Parameters.WindowType,
                    Parameters.ShortcutRootPath,
                    Parameters.IconPath);


            var urlGenerationPath = $"{customShortcut.VbsFolderPath}{shortcutName}.url";
            ShortcutUtils.CreateUrlFile(urlGenerationPath, Parameters.ShortcutTarget);

            customShortcut.TargetPath = urlGenerationPath;

            if (string.IsNullOrEmpty(Parameters.IconPath))
                Build(customShortcut, Parameters.Image);
            else
                Build(customShortcut);

            return customShortcut;
        }
    }
}
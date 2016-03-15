using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using TileIconifier.Utilities;
using TileIconifier.Properties;
using System.Windows.Forms;
using System.Drawing;

namespace TileIconifier.Custom
{
    class CustomShortcut : ListViewItem
    {
        public ShortcutItem ShortcutItem { get; set; }

        private string VbsFilePath { get; set; }
        private string VbsFolderPath { get; set; }

        private string ShortcutName { get; set; }
        private string TargetPath { get; set; }
        private string TargetArguments { get; set; }

        private CustomShortcutType ShortcutType { get; set; }

        private string _basicShortcutIcon;
        private string BasicShortcutIcon
        {
            get
            {
                if (!string.IsNullOrEmpty(_basicShortcutIcon))
                    return _basicShortcutIcon;
                return TargetPath;
            }
            set
            {
                _basicShortcutIcon = value;
            }
        }

        public CustomShortcut(
            string shortcutName,
            string shortcutPath,
            string targetPath,
            string targetArguments,
            CustomShortcutType shortcutType,
            string basicShortcutIcon = null,
            string vbsFilePath = null,
            string vbsFolderPath = null)
        {
            ShortcutName = shortcutName.CleanInvalidFilenameChars();
            ShortcutItem = new ShortcutItem(shortcutPath);
            TargetPath = targetPath;
            TargetArguments = targetArguments;
            ShortcutType = shortcutType;
            BasicShortcutIcon = basicShortcutIcon;
            VbsFilePath = vbsFilePath;
            VbsFolderPath = vbsFolderPath;

            Text = ShortcutName;
            SubItems.Add(ShortcutType.ToString());
            SubItems.Add(ShortcutItem.ShortcutUser.ToString());

        }

        public CustomShortcut(
            string shortcutName,
            string targetPath,
            string targetArguments,
            CustomShortcutType shortcutType,
            string shortcutRootFolder,
            string basicIconToUse = null)
            {


            var vbsFolderPath = DirectoryUtils.GetUniqueDirName(CustomShortcutConstants.CUSTOM_SHORTCUT_VBS_PATH + shortcutName) + "\\";
            var shortcutPath = string.Format("{0}{1}\\{2}.lnk", shortcutRootFolder, new DirectoryInfo(vbsFolderPath).Name, shortcutName);



            ShortcutName = shortcutName.CleanInvalidFilenameChars();
            ShortcutItem = new ShortcutItem(shortcutPath);
            TargetPath = targetPath;
            TargetArguments = targetArguments;
            ShortcutType = shortcutType;
            VbsFolderPath = vbsFolderPath;

            Directory.CreateDirectory(VbsFolderPath);

            if (basicIconToUse != null && File.Exists(basicIconToUse))
            {
                string newBasicIconToUsePath = vbsFolderPath + Path.GetFileName(basicIconToUse);
                try
                {
                    File.Copy(basicIconToUse, newBasicIconToUsePath);
                    BasicShortcutIcon = newBasicIconToUsePath;
                }
                catch { }
            }
            Text = ShortcutName;
            SubItems.Add(ShortcutType.ToString());
        }


        /// <summary>
        /// Create a custom shortcut and save its icon alongside it
        /// </summary>
        /// <param name="basicIconTouse"></param>
        public void BuildCustomShortcut(Image basicIconTouse)
        {
            BasicShortcutIcon = VbsFolderPath + ShortcutName + ".ico";
            try {
                using (StreamWriter iconWriter = new StreamWriter(BasicShortcutIcon))
                {
                    using (Icon ico = Icon.FromHandle(((Bitmap)basicIconTouse).GetHicon()))
                    {
                        ico.Save(iconWriter.BaseStream);
                    }
                }
            } catch { basicIconTouse = null; }

            BuildCustomShortcut();
        }

        public void BuildCustomShortcut()
        {
            VbsFilePath = VbsFolderPath + ShortcutName + ".vbs";

            File.WriteAllText(VbsFilePath,
                string.Format(Resources.CustomShortcutVbsTemplate,
                    ShortcutName.EscapeVba(),
                    ShortcutItem.ShortcutFileInfo.FullName.EscapeVba(),
                    TargetPath.QuoteWrap().EscapeVba(),
                    TargetArguments.EscapeVba(),
                    ShortcutType
                ));

            ShortcutUtils.CreateLnkFile(
                shortcutPath: ShortcutItem.ShortcutFileInfo.FullName,
                targetPath: VbsFilePath,
                description: ShortcutName + " shortcut created by TileIconifier",
                iconPath: BasicShortcutIcon
                );
        }

        public void Delete()
        {
            if(ShortcutItem.ShortcutFileInfo.Directory.Exists)
                ShortcutItem.ShortcutFileInfo.Directory.Delete(true);

            if (Directory.Exists(VbsFolderPath))
                Directory.Delete(VbsFolderPath, true);
        }

        /// <summary>
        /// Parses data from a VBS file and returns a custom shortcut
        /// </summary>
        /// <exception cref="InvalidCustomShortcutException">If a property Regex could not be matched</exception>
        public static CustomShortcut Load(string vbsFilePath)
        {
            if (!File.Exists(vbsFilePath))
                throw new FileNotFoundException(vbsFilePath);

            var vbsFileContents = File.ReadAllText(vbsFilePath);

            var regexMatch = Regex.Match(vbsFileContents,
                "'Custom Shortcut Type = \"(.*)\".*'Shortcut Name = \"(.*)\".*'Shortcut Path = \"(.*)\".*targetPath = \"(.*)\".*targetArguments = \"(.*)\"\r\n.*",
                RegexOptions.Singleline);

            if (!regexMatch.Success)
                throw new InvalidCustomShortcutException();

            return new CustomShortcut(
                shortcutName: (regexMatch.Groups[2].Value).UnescapeVba(),
                shortcutPath: (regexMatch.Groups[3].Value).UnescapeVba(),
                targetPath: (regexMatch.Groups[4].Value).UnescapeVba(),
                targetArguments: (regexMatch.Groups[5].Value).UnescapeVba(),
                shortcutType: (CustomShortcutType)Enum.Parse(typeof(CustomShortcutType), regexMatch.Groups[1].Value),
                vbsFilePath: vbsFilePath,
                vbsFolderPath: new FileInfo(vbsFilePath).Directory.FullName + "\\");
        }
    }
}

using System;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using TileIconifier.Properties;
using TileIconifier.Shortcut;
using TileIconifier.Utilities;

namespace TileIconifier.Custom
{
    internal class CustomShortcut : ListViewItem
    {
        private string _basicShortcutIcon;

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
            string basicIconToUse = null,
            string workingFolder = null)
        {
            var vbsFolderPath =
                DirectoryUtils.GetUniqueDirName(CustomShortcutGetters.CustomShortcutVbsPath + shortcutName) + "\\";
            var shortcutPath = string.Format("{0}{1}\\{2}.lnk", shortcutRootFolder,
                new DirectoryInfo(vbsFolderPath).Name, shortcutName);


            ShortcutName = shortcutName.CleanInvalidFilenameChars();
            ShortcutItem = new ShortcutItem(shortcutPath);
            TargetPath = targetPath;
            TargetArguments = targetArguments;
            ShortcutType = shortcutType;
            VbsFolderPath = vbsFolderPath;
            WorkingFolder = workingFolder;

            Directory.CreateDirectory(VbsFolderPath);

            if (basicIconToUse != null && File.Exists(basicIconToUse)) BasicShortcutIcon = basicIconToUse;

            Text = ShortcutName;
            SubItems.Add(ShortcutType.ToString());
        }

        public ShortcutItem ShortcutItem { get; set; }
        private string VbsFilePath { get; set; }
        private string VbsFolderPath { get; }
        private string ShortcutName { get; }
        private string TargetPath { get; }
        private string TargetArguments { get; }
        private string WorkingFolder { get; }
        private CustomShortcutType ShortcutType { get; }

        private string BasicShortcutIcon
        {
            get { return !string.IsNullOrEmpty(_basicShortcutIcon) ? _basicShortcutIcon : TargetPath; }
            set { _basicShortcutIcon = value; }
        }

        /// <summary>
        ///     Create a custom shortcut and save its icon alongside it
        /// </summary>
        /// <param name="basicIconTouse"></param>
        public void BuildCustomShortcut(Image basicIconTouse)
        {
            BasicShortcutIcon = VbsFolderPath + ShortcutName + ".ico";
            try
            {
                using (var iconWriter = new StreamWriter(BasicShortcutIcon))
                {
                    using (var ico = Icon.FromHandle(((Bitmap)basicIconTouse).GetHicon()))
                    {
                        ico.Save(iconWriter.BaseStream);
                    }
                }
            }
            catch
            {
                BasicShortcutIcon = null;
            }

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

            ShortcutUtils.CreateLnkFile(ShortcutItem.ShortcutFileInfo.FullName, VbsFilePath,
                ShortcutName + " shortcut created by TileIconifier",
                iconPath: BasicShortcutIcon,
                workingDirectory: WorkingFolder
                );
        }

        public void Delete()
        {
            if (ShortcutItem.ShortcutFileInfo.Directory != null && ShortcutItem.ShortcutFileInfo.Directory.Exists)
                try
                {
                    ShortcutItem.ShortcutFileInfo.Directory.Delete(true);
                }
                catch
                {
                    // ignored
                }

            if (Directory.Exists(VbsFolderPath))
                try
                {
                    Directory.Delete(VbsFolderPath, true);
                }
                catch
                {
                    // ignored
                }
        }

        /// <summary>
        ///     Parses data from a VBS file and returns a custom shortcut
        /// </summary>
        /// <exception cref="InvalidCustomShortcutException">If a property Regex could not be matched</exception>
        public static CustomShortcut Load(string vbsFilePath)
        {
            if (!File.Exists(vbsFilePath))
                throw new FileNotFoundException(vbsFilePath);

            var vbsFileContents = File.ReadAllText(vbsFilePath);

            var regexMatch = Regex.Match(vbsFileContents,
                "'Custom Shortcut Type = \"(.*)\".*'Shortcut Name = \"(.*)\".*'Shortcut Path = \"(.*)\".*targetPath = \"(.*)\".*targetArguments = \"(.*)\"\r?\n.*",
                RegexOptions.Singleline);

            if (!regexMatch.Success)
                throw new InvalidCustomShortcutException();

            var directoryInfo = new FileInfo(vbsFilePath).Directory;
            if (directoryInfo != null)
                return new CustomShortcut((regexMatch.Groups[2].Value).UnescapeVba(),
                    (regexMatch.Groups[3].Value).UnescapeVba(), (regexMatch.Groups[4].Value).UnescapeVba(),
                    (regexMatch.Groups[5].Value).UnescapeVba(),
                    (CustomShortcutType)Enum.Parse(typeof(CustomShortcutType), regexMatch.Groups[1].Value, true),
                    vbsFilePath: vbsFilePath,
                    vbsFolderPath: directoryInfo.FullName + "\\");

            throw new DirectoryNotFoundException();
        }
    }
}
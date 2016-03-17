using System;
using System.IO;
using System.Xml.Linq;
using TileIconifier.Shortcut;

namespace TileIconifier.TileIconify
{
    class TileIcon
    {
        readonly ShortcutItem _shortcutItem;

        public TileIcon(ShortcutItem shortcutItem)
        {
            _shortcutItem = shortcutItem;
        }

        public void RunIconify()
        {
            BuildFilesAndFolders();
            SaveIcon();
            RebuildLnkInStartMenu();
        }

        public void DeIconify()
        {
            DeleteFilesAndFolders();
            RebuildLnkInStartMenu();
        }

        private void BuildFilesAndFolders()
        {
            var xNamespace = XNamespace.Get("http://www.w3.org/2001/XMLSchema-instance");

            var xDoc = new XDocument(
                new XElement("Application",
                    new XAttribute(XNamespace.Xmlns + "xsi", xNamespace),
                    new XElement("VisualElements",
                        new XAttribute("ShowNameOnSquare150x150Logo", (_shortcutItem.ShowNameOnSquare150X150Logo ? "on" : "off")),
                        new XAttribute("Square150x150Logo", _shortcutItem.RelativeMediumIconPath),
                        new XAttribute("Square70x70Logo", _shortcutItem.RelativeSmallIconPath),
                        new XAttribute("ForegroundText", _shortcutItem.ForegroundText),
                        new XAttribute("BackgroundColor", _shortcutItem.BackgroundColor)
                        )));
            
            if (!Directory.Exists(_shortcutItem.VisualElementsPath))
                Directory.CreateDirectory(_shortcutItem.VisualElementsPath);

            xDoc.Save(_shortcutItem.VisualElementManifestPath);
        }

        private void DeleteFilesAndFolders()
        {
            if (Directory.Exists(_shortcutItem.VisualElementsPath))
                Directory.Delete(_shortcutItem.VisualElementsPath, true);

            if (File.Exists(string.Format("{0}\\{1}.VisualElementsManifest.xml", _shortcutItem.TargetFolderPath, Path.GetFileNameWithoutExtension(_shortcutItem.TargetFilePath))))
                File.Delete(string.Format("{0}\\{1}.VisualElementsManifest.xml", _shortcutItem.TargetFolderPath, Path.GetFileNameWithoutExtension(_shortcutItem.TargetFilePath)));
        }


        private void SaveIcon()
        {

            using (var fs = new FileStream(_shortcutItem.FullMediumIconPath, FileMode.Create))
            {
                _shortcutItem.MediumImage.Save(fs, System.Drawing.Imaging.ImageFormat.Png);
            }
            using (var fs = new FileStream(_shortcutItem.FullSmallIconPath, FileMode.Create))
            {
                _shortcutItem.SmallImage.Save(fs, System.Drawing.Imaging.ImageFormat.Png);
            }
        }

        private void RebuildLnkInStartMenu()
        {
            File.SetLastWriteTime(_shortcutItem.ShortcutFileInfo.FullName, DateTime.Now);
        }
    }

}

using System;
using System.IO;
using System.Xml.Linq;
using TileIconifier.Shortcut;

namespace TileIconifier.TileIconify
{
    internal class TileIcon
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
                        new XAttribute("ShowNameOnSquare150x150Logo", _shortcutItem.ShowNameOnSquare150X150Logo ? "on" : "off"),
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

            if (File.Exists(
                $"{_shortcutItem.TargetFolderPath}\\{Path.GetFileNameWithoutExtension(_shortcutItem.TargetFilePath)}.VisualElementsManifest.xml"))
                File.Delete(
                    $"{_shortcutItem.TargetFolderPath}\\{Path.GetFileNameWithoutExtension(_shortcutItem.TargetFilePath)}.VisualElementsManifest.xml");
        }


        private void SaveIcon()
        {

            using (var fs = new FileStream(_shortcutItem.FullMediumIconPath, FileMode.Create))
            {
                fs.Write(_shortcutItem.MediumImageBytes,0, _shortcutItem.MediumImageBytes.Length);
            }
            using (var fs = new FileStream(_shortcutItem.FullSmallIconPath, FileMode.Create))
            {
                fs.Write(_shortcutItem.SmallImageBytes, 0, _shortcutItem.SmallImageBytes.Length);

            }
        }

        private void RebuildLnkInStartMenu()
        {
            File.SetLastWriteTime(_shortcutItem.ShortcutFileInfo.FullName, DateTime.Now);
        }
    }

}

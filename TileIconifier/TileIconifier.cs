using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TileIconifier.Utilities;
using TsudaKageyu;

namespace TileIconifier
{
    class TileIcon
    {
        private TileIconParameters _parameters;

        public TileIcon(TileIconParameters Parameters)
        {
            _parameters = Parameters;
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
            string xmlContents = string.Format(@"<Application xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance'>
  <VisualElements
      ShowNameOnSquare150x150Logo='{0}'
      Square150x150Logo='{1}'
      Square70x70Logo='{2}'
      ForegroundText='{3}'
      BackgroundColor='{4}'/>
</Application>", _parameters.ShowNameOnSquare150x150Logo ? "on" : "off", _parameters.Shortcut.RelativeMediumIconPath, _parameters.Shortcut.RelativeSmallIconPath, _parameters.FgText, _parameters.BgColour);

            if (!Directory.Exists(_parameters.Shortcut.VisualElementsPath))
                Directory.CreateDirectory(_parameters.Shortcut.VisualElementsPath);

            File.WriteAllText(_parameters.Shortcut.VisualElementManifestPath, xmlContents);
        }

        private void DeleteFilesAndFolders()
        {
            if (Directory.Exists(_parameters.Shortcut.VisualElementsPath))
                Directory.Delete(_parameters.Shortcut.VisualElementsPath,true);

            if (File.Exists(string.Format("{0}\\{1}.VisualElementsManifest.xml", _parameters.Shortcut.ExeFolderPath, Path.GetFileNameWithoutExtension(_parameters.Shortcut.ExeFilePath))))
                File.Delete(string.Format("{0}\\{1}.VisualElementsManifest.xml", _parameters.Shortcut.ExeFolderPath, Path.GetFileNameWithoutExtension(_parameters.Shortcut.ExeFilePath)));
        }


        private void SaveIcon()
        {
            
            using (var fs = new FileStream(_parameters.Shortcut.FullMediumIconPath, FileMode.Create))
            {
                _parameters.Shortcut.MediumImage.Save(fs, System.Drawing.Imaging.ImageFormat.Png);
            }
            using (var fs = new FileStream(_parameters.Shortcut.FullSmallIconPath, FileMode.Create))
            {
                _parameters.Shortcut.SmallImage.Save(fs, System.Drawing.Imaging.ImageFormat.Png);
            }
        }


        private void RebuildLnkInStartMenu()
        {
            File.SetLastWriteTime(_parameters.Shortcut.ShortcutFileInfo.FullName, DateTime.Now);
        }
    }

    public class TileIconParameters
    {
        public ShortcutItem Shortcut;
        public string BgColour;
        public string FgText;
        public bool ShowNameOnSquare150x150Logo;
    }
}

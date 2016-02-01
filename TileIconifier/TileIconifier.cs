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

        Icon icon;




        public TileIcon(TileIconParameters Parameters)
        {
            _parameters = Parameters;
        }

        public void RunIconify()
        {
            GetLogo();
            BuildFilesAndFolders();
            SaveIcon();
            RebuildLnkInStartMenu();
        }

        public void DeIconify()
        {
            DeleteFilesAndFolders();
            RebuildLnkInStartMenu();
        }

        private void GetLogo()
        {
            IconExtractor IconExtraction = new IconExtractor(_parameters.Shortcut.ExeFilePath);
            var Icons = IconExtraction.GetAllIcons();

            var IconSelection = new IconSelector(Icons);

            do
            {
                IconSelection.ShowDialog();
            } while (IconSelection.SelectionIndex == -1);

            if (IconSelection.SelectionIndex == -2)
                throw new UserCancellationException();

            var SplitIcons = IconUtil.Split(Icons[IconSelection.SelectionIndex]);

            icon = SplitIcons.OrderByDescending(k => k.Width)
                                        .ThenByDescending(k => k.Height)
                                        .First();
        }

        private void BuildFilesAndFolders()
        {
            string xmlContents = string.Format(@"<Application xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance'>
  <VisualElements
      ShowNameOnSquare150x150Logo='{0}'
      Square150x150Logo='VisualElements\Logo.png'
      Square70x70Logo='VisualElements\Logo.png'
      ForegroundText='{1}'
      BackgroundColor='{2}'/>
</Application>", _parameters.ShowNameOnSquare150x150Logo ? "on" : "off", _parameters.FgText, _parameters.BgColour);

            if (!Directory.Exists(_parameters.Shortcut.VisualElementsPath))
                Directory.CreateDirectory(_parameters.Shortcut.VisualElementsPath);

            File.WriteAllText(string.Format("{0}\\{1}.VisualElementsManifest.xml", _parameters.Shortcut.ExeFolderPath, Path.GetFileNameWithoutExtension(_parameters.Shortcut.ExeFilePath)), xmlContents);
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
            using (var bmp = Bitmap.FromHicon(icon.Handle))
            {
                using (var fs = new FileStream(_parameters.Shortcut.VisualElementsPath + "Logo.png", FileMode.Create))
                {
                    bmp.Save(fs, System.Drawing.Imaging.ImageFormat.Png);
                }
            }
        }


        private void RebuildLnkInStartMenu()
        {
            File.SetLastWriteTime(_parameters.Shortcut.ShortcutFileInfo.FullName, DateTime.Now);


            //Seems setting last write time is enough, old code moving it back and forth

            //var tempPath = string.Format("{0}{1}\\{2}\\", Path.GetTempPath(), "TileIconifier", Path.GetFileNameWithoutExtension(exeFilePath));
            //if (!Directory.Exists(tempPath))
            //    Directory.CreateDirectory(tempPath);

            //var newLnkLocation = tempPath + Path.GetFileName(lnkFilePath);

            //File.Move(lnkFilePath, newLnkLocation);
            //Thread.Sleep(3000);
            //File.Move(newLnkLocation, lnkFilePath);
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

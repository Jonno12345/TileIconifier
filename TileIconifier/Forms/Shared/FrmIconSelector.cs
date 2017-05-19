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
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using TileIconifier.Core;
using TileIconifier.Core.IconExtractor;
using TileIconifier.Core.Utilities;
using TileIconifier.Properties;
using TileIconifier.Utilities;
using static System.String;

namespace TileIconifier.Forms.Shared
{
    public partial class FrmIconSelector : SkinnableForm
    {
        private readonly List<string> _commonIconDlls = new List<string>
        {
            @"%windir%\system32\shell32.dll",
            @"%windir%\System32\wmploc.DLL",
            @"%windir%\system32\setupapi.dll",
            @"%windir%\system32\ddores.dll",
            @"%windir%\System32\ieframe.dll",
            @"%windir%\system32\netshell.dll",
            @"%windir%\System32\imageres.dll",
            @"%windir%\System32\pifmgr.dll",
            @"%windir%\System32\moricons.dll",
            @"%windir%\System32\mmcndmgr.dll",
            @"%windir%\System32\compstui.dll",
            @"%windir%\system32\accessibilitycpl.dll",
            @"%windir%\explorer.exe",
            @"%windir%\system32\gameux.dll",
            @"%windir%\system32\mmres.dll",
            @"%windir%\system32\mstscax.dll",
            @"%windir%\System32\netcenter.dll",
            @"%windir%\System32\networkexplorer.dll",
            @"%windir%\system32\networkmap.dll",
            @"%windir%\System32\pnidui.dll",
            @"%windir%\system32\SensorsCpl.dll",
            @"%windir%\system32\xpsrchvw.exe ",
            @"%windir%\system32\UIHub.dll",
            @"%windir%\system32\vpc.exe",
            @"%windir%\system32\wmp.dll",
            @"%windir%\system32\wpdshext.dll",
            @"%windir%\system32\wucltux.dll"
        };

        private readonly List<string> _iconExtractorFileTypes = new List<string>
        {
            ".ico",
            ".exe",
            ".dll"
        };

        private readonly List<string> _supportedImageFileTypes = new List<string>
        {
            ".jpeg",
            ".jpg",
            ".png",
            ".bmp"
        };

        private Icon[] _icons;

        public byte[] ReturnedBitmapBytes;
        public string ReturnedImagePath;

        private FrmIconSelector(string targetPath)
        {
            InitializeComponent();
            SetUpOpenFileDialog();
            SetUpCommonDllComboBox();
            SetUpTargetPath(targetPath);
            BuildListView();            
        }

        public static IconSelectorResult GetImage(IWin32Window owner, string defaultPathForIconExtraction = "")
        {
            var iconSelector = new FrmIconSelector(defaultPathForIconExtraction);
            iconSelector.ShowDialog(owner);
            if (iconSelector.ReturnedBitmapBytes == null)
            {
                throw new UserCancellationException();
            }

            return new IconSelectorResult
            {
                ImageBytes = iconSelector.ReturnedBitmapBytes,
                ImagePath = iconSelector.ReturnedImagePath
            };
        }

        private void SetUpOpenFileDialog()
        {
            //Image Files (*.bmp, *.jpg, *.png)|*.bmp;*.jpg;*.png|Programs, ICO's and Libraries (*.exe, *.dll, *.ico)|*.exe;*.dll;*.ico
            opnFile.Filter =
                $@"Image Files|*{
                    Join(";*", _supportedImageFileTypes)
                    }|Programs, ICO's and Libraries|*{
                    Join(";*", _iconExtractorFileTypes)
                    }";
        }

        private bool PathTypeIsForIconExtractor(string path)
        {
            return
                _iconExtractorFileTypes.Any(
                    s => string.Equals(Path.GetExtension(path), s, StringComparison.InvariantCultureIgnoreCase));
        }

        private bool PathTypeIsAnImageFile(string path)
        {
            return
                _supportedImageFileTypes.Any(
                    s => string.Equals(Path.GetExtension(path), s, StringComparison.InvariantCultureIgnoreCase));
        }

        private void SetUpTargetPath(string targetPath)
        {
            if (PathTypeIsForIconExtractor(targetPath))
            {
                txtPathToExtractFrom.Text = targetPath;
                radIconFromTarget.Checked = true;
            }
            else if (PathTypeIsAnImageFile(targetPath))
            {
                txtImagePath.Text = targetPath;
                radUseCustomImage.Checked = true;
            }
        }

        private void SetUpCommonDllComboBox()
        {
            foreach (var commonIconDll in _commonIconDlls.ToList())
            {
                _commonIconDlls.Remove(commonIconDll);
                if (File.Exists(Environment.ExpandEnvironmentVariables(commonIconDll)))
                {
                    _commonIconDlls.Add(Environment.ExpandEnvironmentVariables(commonIconDll));
                }
            }
            _commonIconDlls.Insert(0, "");

            cmbCommonIconDlls.DisplayMember = "Key";
            cmbCommonIconDlls.ValueMember = "Value";
            cmbCommonIconDlls.DataSource =
                _commonIconDlls.Select(s => new KeyValuePair<string, string>(Path.GetFileName(s), s))
                    .OrderBy(s => s.Key)
                    .ToList();
        }

        private void BuildListView()
        {
            //reset the list view items
            lvwIcons.Items.Clear();

            //validate the path exists
            var targetPath = txtPathToExtractFrom.Text;
            if (!File.Exists(targetPath))
            {
                return;
            }

            lvwIcons.BeginUpdate();
            try
            {
                //icon files don't need extraction
                if (string.Equals(Path.GetExtension(targetPath), ".ico", StringComparison.InvariantCultureIgnoreCase))
                {
                    _icons = new[] {new Icon(targetPath)};
                }
                else
                {
                    var iconExtraction = new IconExtractor(targetPath);
                    _icons = iconExtraction.GetAllIcons();
                }

                foreach (var i in _icons)
                {
                    var splitIcons = IconUtil.Split(i);

                    var largestIcon = splitIcons.OrderByDescending(k => k.Width)
                        .ThenByDescending(k => Math.Max(k.Height, k.Width))
                        .First();

                    var item = new IconListViewItem {Bitmap = IconUtil.ToBitmap(largestIcon)};
                    i.Dispose();
                    largestIcon.Dispose();

                    lvwIcons.Items.Add(item);
                }
            }
            catch
            {
                // ignored
            }
            finally
            {
                lvwIcons.EndUpdate();
            }
        }

        private void lvwIcons_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            var item = e.Item as IconListViewItem;

            // Draw item

            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            e.Graphics.CompositingQuality = CompositingQuality.HighQuality;

            if (e.Item.Selected)
                //If the skin ever gets a highlight color property specific 
                //to listview (something like ListViewSelectedBackColor), 
                //it is the one that we should use here instead of the generic highlight color.
                using (var b = new SolidBrush(FormSkin.HighlightBackColor))
                    e.Graphics.FillRectangle(b, e.Bounds);            

            var w = (int) Math.Ceiling(lvwIcons.TileSize.Width*0.8);
            var h = (int) Math.Ceiling(lvwIcons.TileSize.Height*0.8);

            var x = e.Bounds.X + (e.Bounds.Width - w)/2;
            var y = e.Bounds.Y + (e.Bounds.Height - h)/2;
            var dstRect = new Rectangle(x, y, w, h);
            if (item != null)
            {
                var srcRect = new Rectangle(Point.Empty, item.Bitmap.Size);

                e.Graphics.DrawImage(item.Bitmap, dstRect, srcRect, GraphicsUnit.Pixel);
            }
            
            //Draw a border that uses a blend of the ForeColor and the BackColor to
            //ensure that it is good looking and visible with any skin.
            var borderColor = ColorUtils.BlendColors(FormSkin.ListViewForeColor, 1, FormSkin.ListViewBackColor, 10);
            var borderRect = e.Bounds;
            borderRect.Width--;
            borderRect.Height--;

            using (var p = new Pen(borderColor))
                e.Graphics.DrawRectangle(p, e.Bounds);
        }        

        private void lvwIcons_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnOk_Click(this, null);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                if (radIconFromTarget.Checked)
                {
                    if (lvwIcons.SelectedItems.Count != 1)
                    {
                        FormUtils.ShowMessage(this, Strings.PleaseSelectAnIcon, Strings.PleaseSelectAnIcon, MessageBoxButtons.OK,
                            MessageBoxIcon.Exclamation);
                        return;
                    }
                    ReturnedBitmapBytes = GetLogoBytes();
                    ReturnedImagePath = txtPathToExtractFrom.Text;
                }
                else
                {
                    var imagePath = txtImagePath.Text;
                    if (!File.Exists(imagePath))
                    {
                        throw new FileNotFoundException(Strings.FileCouldNotBeFound, imagePath);
                    }
                    ReturnedBitmapBytes = ImageUtils.LoadFileToByteArray(imagePath);
                    ReturnedImagePath = imagePath;
                }
            }
            catch (FileNotFoundException ex)
            {
                FormUtils.ShowMessage(this, $"{ex.Message}: {ex.FileName}", $"{Strings.FileCouldNotBeFound}", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
            }

            Close();
        }

        private byte[] GetLogoBytes()
        {
            var item = lvwIcons.SelectedItems[0] as IconListViewItem;

            return ImageUtils.ImageToByteArray(item?.Bitmap);
        }

        private void lvwIcons_SelectedIndexChanged(object sender, EventArgs e)
        {
            radIconFromTarget.Checked = true;
        }

        private void lvwIcons_MouseClick(object sender, MouseEventArgs e)
        {
            radIconFromTarget.Checked = true;
        }

        private void btnBrowseCustomImage_Click(object sender, EventArgs e)
        {
            SetOpenFileDialogPaths(
                IsNullOrEmpty(txtImagePath.Text)
                    ? Environment.ExpandEnvironmentVariables(@"%USERPROFILE%\Pictures\")
                    : txtImagePath.Text);

            opnFile.FilterIndex = 1;
            if (opnFile.ShowDialog(this) != DialogResult.OK)
            {
                return;
            }

            HandlePathSelection(opnFile.FileName);
        }

        private void btnBrowseIconPath_Click(object sender, EventArgs e)
        {
            SetOpenFileDialogPaths(txtPathToExtractFrom.Text);

            opnFile.FilterIndex = 2;
            if (opnFile.ShowDialog(this) != DialogResult.OK)
            {
                return;
            }

            HandlePathSelection(opnFile.FileName);
        }

        private void SetOpenFileDialogPaths(string filePath)
        {
            //attempt to default the paths of the browse dialog - ignore if fails
            try
            {
                opnFile.InitialDirectory = new FileInfo(filePath).Directory?.FullName;
                opnFile.FileName = Path.GetFileName(filePath);
            }
            catch
            {
                // ignored
            }
        }

        private void HandlePathSelection(string fileName)
        {
            if (PathTypeIsForIconExtractor(fileName))
            {
                txtPathToExtractFrom.Text = opnFile.FileName;
                radIconFromTarget.Checked = true;
                BuildListView();
            }
            else if (PathTypeIsAnImageFile(fileName))
            {
                txtImagePath.Text = opnFile.FileName;
                radUseCustomImage.Checked = true;
            }
        }

        private void cmbCommonIconDlls_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCommonIconDlls.SelectedIndex >= 0)
            {
                txtPathToExtractFrom.Text = cmbCommonIconDlls.SelectedValue.ToString();
            }
            BuildListView();
        }

        private void txtImagePath_TextChanged(object sender, EventArgs e)
        {
            if (!File.Exists(txtImagePath.Text))
            {
                if (pctPreview.Image == null)
                {
                    return;
                }
                try
                {
                    pctPreview.Image.Dispose();
                }
                catch
                {
                    // ignored
                }
                pctPreview.Image = null;
                return;
            }

            try
            {
                pctPreview.Image = ImageUtils.LoadFileToBitmap(txtImagePath.Text);
            }
            catch
            {
                FormUtils.ShowMessage(this, Strings.ErrorLoadingImageFile);
            }
        }

        private class IconListViewItem : ListViewItem
        {
            public Bitmap Bitmap { get; set; }
        }
    }

    public class IconSelectorResult
    {
        public byte[] ImageBytes { get; set; }
        public string ImagePath { get; set; }
    }
}
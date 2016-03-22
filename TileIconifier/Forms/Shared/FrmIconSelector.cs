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
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using TileIconifier.IconExtractor;
using TileIconifier.Utilities;

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

        private Icon[] _icons;
        //public Bitmap ReturnedBitmap;
        public byte[] ReturnedBitmapBytes;

        public FrmIconSelector(string targetPath)
        {
            InitializeComponent();
            SetUpCommonDllComboBox();
            txtPathToExtractFrom.Text = targetPath;
            BuildListView();
        }

        private void SetUpCommonDllComboBox()
        {
            foreach (var commonIconDll in _commonIconDlls.ToList())
            {
                _commonIconDlls.Remove(commonIconDll);
                if (File.Exists(Environment.ExpandEnvironmentVariables(commonIconDll)))
                    _commonIconDlls.Add(Environment.ExpandEnvironmentVariables(commonIconDll));
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
                return;

            lvwIcons.BeginUpdate();
            try
            {
                var iconExtraction = new IconExtractor.IconExtractor(targetPath);
                _icons = iconExtraction.GetAllIcons();

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
            e.Graphics.Clip = new Region(e.Bounds);

            e.Graphics.FillRectangle(
                e.Item.Selected
                    ? new SolidBrush(CurrentBaseSkin.HighlightColor)
                    : new SolidBrush(CurrentBaseSkin.BackColor), e.Bounds);

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

            e.Graphics.Clip = new Region();
            e.Graphics.DrawRectangle(SystemPens.ControlLight, e.Bounds);
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
                    //GetLogo()
                    ReturnedBitmapBytes = GetLogoBytes();
                }
                else
                {
                    var imagePath = txtImagePath.Text;
                    if (!File.Exists(imagePath))
                        throw new FileNotFoundException();

                    ReturnedBitmapBytes = ImageUtils.LoadBitmapToByteArray(imagePath);
                    //ReturnedBitmap = ImageUtils.LoadIconifiedBitmap(imagePath);
                }
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show(@"File could not be found: " + ex.FileName, @"File not found!", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
            }
            catch
            {
                // ignored
            }

            Close();
        }

        //private void GetLogo()
        //{
        //    var splitIcons = IconUtil.Split(_icons[lvwIcons.SelectedItems[0].Index]);

        //    ReturnedBitmap = Bitmap.FromHicon(splitIcons.OrderByDescending(k => k.Width)
        //        .ThenByDescending(k => k.Height)
        //        .First().Handle);
        //}

        private byte[] GetLogoBytes()
        {
            var splitIcons = IconUtil.Split(_icons[lvwIcons.SelectedItems[0].Index]);
            byte[] byteArray;
            using (var stream = new MemoryStream())
            {
                using (var bitmapLoad = Bitmap.FromHicon(splitIcons.OrderByDescending(k => k.Width)
                    .ThenByDescending(k => k.Height)
                    .First().Handle))
                {
                    bitmapLoad.Save(stream, ImageFormat.Png);
                    stream.Close();

                    byteArray = stream.ToArray();
                }
            }
            return byteArray;
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
                string.IsNullOrEmpty(txtImagePath.Text)
                    ? Environment.ExpandEnvironmentVariables(@"%USERPROFILE%\Pictures\")
                    : txtImagePath.Text);

            opnFile.FilterIndex = 1;
            if (opnFile.ShowDialog(this) != DialogResult.OK)
                return;

            HandlePathSelection(opnFile.FileName);
        }

        private void btnBrowseIconPath_Click(object sender, EventArgs e)
        {
            SetOpenFileDialogPaths(txtPathToExtractFrom.Text);

            opnFile.FilterIndex = 2;
            if (opnFile.ShowDialog(this) != DialogResult.OK)
                return;

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
            //need clean this up... actually using the filters from the file selector would be best.
            if (string.Equals(Path.GetExtension(fileName), ".exe", StringComparison.InvariantCultureIgnoreCase) ||
                string.Equals(Path.GetExtension(fileName), ".dll", StringComparison.InvariantCultureIgnoreCase))
            {
                txtPathToExtractFrom.Text = opnFile.FileName;
                radIconFromTarget.Checked = true;
                BuildListView();
            }
            else if (string.Equals(Path.GetExtension(fileName), ".jpg", StringComparison.InvariantCultureIgnoreCase) ||
                     string.Equals(Path.GetExtension(fileName), ".png", StringComparison.InvariantCultureIgnoreCase) ||
                     string.Equals(Path.GetExtension(fileName), ".bmp", StringComparison.InvariantCultureIgnoreCase) ||
                     string.Equals(Path.GetExtension(fileName), ".ico", StringComparison.InvariantCultureIgnoreCase))
            {
                txtImagePath.Text = opnFile.FileName;
                radUseCustomImage.Checked = true;
            }
        }

        private void cmbCommonIconDlls_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCommonIconDlls.SelectedIndex >= 0)
                txtPathToExtractFrom.Text = cmbCommonIconDlls.SelectedValue.ToString();
            BuildListView();
        }

        private class IconListViewItem : ListViewItem
        {
            public Bitmap Bitmap { get; set; }
        }
    }
}
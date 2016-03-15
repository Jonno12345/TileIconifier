using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TsudaKageyu;

namespace TileIconifier.Forms
{
    public partial class frmIconSelector : Form
    {
        public Bitmap ReturnedBitmap;
        private Icon[] icons;

        private List<string> commonIconDlls = new List<string>() { @"%windir%\system32\shell32.dll",
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
@"%windir%\system32\wucltux.dll"};


        private class IconListViewItem : ListViewItem
        {
            public Bitmap Bitmap { get; set; }
        }

        public frmIconSelector(string targetPath)
        {
            InitializeComponent();
            SetUpCommonDllComboBox();
            txtPathToExtractFrom.Text = targetPath;
            BuildListView();
        }

        private void SetUpCommonDllComboBox()
        {
            foreach (var commonIconDll in commonIconDlls.ToList())
            {
                commonIconDlls.Remove(commonIconDll);
                if (File.Exists(Environment.ExpandEnvironmentVariables(commonIconDll)))
                    commonIconDlls.Add(Environment.ExpandEnvironmentVariables(commonIconDll));
            }
            commonIconDlls.Insert(0, "");

            cmbCommonIconDlls.DisplayMember = "Key";
            cmbCommonIconDlls.ValueMember = "Value";
            cmbCommonIconDlls.DataSource = commonIconDlls.Select(s => new KeyValuePair<string, string>(Path.GetFileName(s), s)).OrderBy(s => s.Key).ToList();

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
                IconExtractor IconExtraction = new IconExtractor(targetPath);
                icons = IconExtraction.GetAllIcons();

                foreach (var i in icons)
                {
                    var SplitIcons = IconUtil.Split(i);

                    var LargestIcon = SplitIcons.OrderByDescending(k => k.Width)
                    .ThenByDescending(k => Math.Max(k.Height, k.Width))
                    .First();

                    var item = new IconListViewItem();
                    item.Bitmap = IconUtil.ToBitmap(LargestIcon);
                    i.Dispose();
                    LargestIcon.Dispose();

                    lvwIcons.Items.Add(item);
                }
            }
            catch { }
            finally { lvwIcons.EndUpdate(); }
        }

        private void lvwIcons_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            var item = e.Item as IconListViewItem;

            // Draw item

            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            e.Graphics.CompositingQuality = CompositingQuality.HighQuality;
            e.Graphics.Clip = new Region(e.Bounds);

            if (e.Item.Selected)
                e.Graphics.FillRectangle(SystemBrushes.MenuHighlight, e.Bounds);
            else
                e.Graphics.FillRectangle(SystemBrushes.Control, e.Bounds);

            int w = (int)Math.Ceiling(lvwIcons.TileSize.Width * 0.8) ;// Math.Min(lvwIcons.TileSize.Width, item.Bitmap.Width);
            int h = (int)Math.Ceiling(lvwIcons.TileSize.Height * 0.8);// Math.Min(lvwIcons.TileSize.Height, item.Bitmap.Height);

            int x = e.Bounds.X + (e.Bounds.Width - w) / 2;
            int y = e.Bounds.Y + (e.Bounds.Height - h) / 2;
            var dstRect = new Rectangle(x, y, w, h);
            var srcRect = new Rectangle(Point.Empty, item.Bitmap.Size);


            e.Graphics.DrawImage(item.Bitmap, dstRect, srcRect, GraphicsUnit.Pixel);

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
                    GetLogo();
                }
                else
                {
                    var imagePath = txtImagePath.Text;
                    if (!File.Exists(imagePath))
                        throw new FileNotFoundException();

                    FileStream readImage = new FileStream(imagePath, FileMode.Open, FileAccess.Read, FileShare.Read);
                    ReturnedBitmap = new Bitmap(readImage);
                }
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show("File could not be found: " + ex.FileName, "File not found!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch { }

            Close();
        }

        private void GetLogo()
        {
            var SplitIcons = IconUtil.Split(icons[lvwIcons.SelectedItems[0].Index]);

            ReturnedBitmap = Bitmap.FromHicon(SplitIcons.OrderByDescending(k => k.Width)
            .ThenByDescending(k => k.Height)
            .First().Handle);

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
                string.IsNullOrEmpty(txtImagePath.Text) ?
                Environment.ExpandEnvironmentVariables(@"%USERPROFILE%\Pictures\") :
                txtImagePath.Text);

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
                opnFile.InitialDirectory = new FileInfo(filePath).Directory.FullName;
                opnFile.FileName = Path.GetFileName(filePath);
            }
            catch { }
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
    }
}

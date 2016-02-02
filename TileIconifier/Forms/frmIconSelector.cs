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
        private string _exePath;
        private Icon[] icons;

        private class IconListViewItem : ListViewItem
        {
            public Bitmap Bitmap { get; set; }
        }

        public frmIconSelector(string exePath)
        {
            InitializeComponent();
            _exePath = exePath;
            BuildListView();
        }

        private void BuildListView()
        {
            lvwIcons.BeginUpdate();

            IconExtractor IconExtraction = new IconExtractor(_exePath);
            icons = IconExtraction.GetAllIcons();


            foreach (var i in icons)
            {
                var item = new IconListViewItem();
                var size = i.Size;
                var bits = IconUtil.GetBitCount(i);
                //item.ToolTipText = String.Format("{0}x{1}, {2} bits", size.Width, size.Height, bits);
                item.Bitmap = IconUtil.ToBitmap(i);
                i.Dispose();

                lvwIcons.Items.Add(item);
            }

            lvwIcons.EndUpdate();
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
                e.Graphics.FillRectangle(SystemBrushes.Window, e.Bounds);

            int w = Math.Min(lvwIcons.TileSize.Width, item.Bitmap.Width);
            int h = Math.Min(lvwIcons.TileSize.Height, item.Bitmap.Height);

            int x = e.Bounds.X + (e.Bounds.Width - w) / 2;
            int y = e.Bounds.Y + (e.Bounds.Height - h) / 2;
            var dstRect = new Rectangle(x, y, w, h);
            var srcRect = new Rectangle(Point.Empty, item.Bitmap.Size);


            e.Graphics.DrawImage(item.Bitmap, dstRect, srcRect, GraphicsUnit.Pixel);

            var textRect = new Rectangle(
                e.Bounds.Left, e.Bounds.Bottom - Font.Height - 4,
                e.Bounds.Width, Font.Height + 2);
            TextRenderer.DrawText(e.Graphics, item.ToolTipText, Font, textRect, ForeColor);

            e.Graphics.Clip = new Region();
            e.Graphics.DrawRectangle(SystemPens.ControlLight, e.Bounds);
        }

        private void lvwIcons_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnOk_Click(this, null);
        }

        private void IconSelector_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            if (opnImageFile.ShowDialog() != DialogResult.OK)
                return;

            txtImagePath.Text = opnImageFile.FileName;
            radUseCustomImage.Checked = true;

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                if (radIconFromExe.Checked)
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
            radIconFromExe.Checked = true;
        }

        private void lvwIcons_MouseClick(object sender, MouseEventArgs e)
        {
            radIconFromExe.Checked = true;
        }
    }
}

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
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace TileIconifier.Controls.IconifierPanel.PictureBox
{
    public partial class PannablePictureBoxControlPanel : UserControl
    {
        private enum ImageZoomAdjustement
        {
            None, Enlarge, Shrink
        }

        private ImageZoomAdjustement _lastClickType;

        public PannablePictureBoxControlPanel()
        {
            InitializeComponent();
            tmrScrollDelay.Interval = SystemInformation.DoubleClickTime;
            btnEnlarge.Tag = ImageZoomAdjustement.Enlarge;
            btnShrink.Tag = ImageZoomAdjustement.Shrink;
        }

        [Browsable(false)]
        public PannablePictureBox PannablePictureBox => panPct;

        [Localizable(true)]
        public string HeaderText
        {
            get { return lblHeader.Text; }
            set { lblHeader.Text = value; }
        }
        
        public Size PannablePictureBoxSize
        {
            get { return panPct.Size; }
            set { panPct.Size = value; }
        }        

        public event EventHandler ChangeImageClick;

        public void UpdateTrackBarAndZoom()
        {
            trkZoom.Value = (int) Math.Round(PannablePictureBox.GetZoomPercentage(), 1);
            UpdateZoomPercentage();
        }

        public void UpdateControls()
        {
            if (PannablePictureBox?.PannablePictureBoxImage.Image == null)
            {
                DisableControls();
                return;
            }
            EnableControls();
            UpdateTrackBarAndZoom();
        }        

        private void ZoomButton_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            var ctrl = ((Control)sender);
            var adjustement = (ImageZoomAdjustement)ctrl.Tag;

            DoAdjustement(adjustement);
            _lastClickType = adjustement;
            tmrScrollDelay.Start();
        }

        private void ZoomButton_MouseUp(object sender, MouseEventArgs e)
        {
            tmrScrollDelay.Stop();
            tmrZoom.Stop();
        }        

        private void tmrScrollDelay_Tick(object sender, EventArgs e)
        {
            tmrScrollDelay.Stop();
            DoAdjustement(_lastClickType);
            tmrZoom.Start();
        }

        private void tmrZoom_Tick(object sender, EventArgs e)
        {
            DoAdjustement(_lastClickType);
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            PannablePictureBox.ResetImage();
        }

        private void btnOpenImage_Click(object sender, EventArgs e)
        {
            ChangeImageClick?.Invoke(this, null);
        }

        private void btnAlign_Click(object sender, EventArgs e)
        {
            var alignForm = new AlignImageForm();
            alignForm.PannablePictureBox = PannablePictureBox;
            alignForm.Location = MousePosition;
            alignForm.Show(this);
        }
        
        private void trkZoom_Scroll(object sender, EventArgs e)
        {
            PannablePictureBox.SetZoom(trkZoom.Value);
            UpdateZoomPercentage();
        }

        private void EnableControls()
        {
            trkZoom.Enabled = true;
            btnAlign.Enabled = true;
            btnEnlarge.Enabled = true;
            btnReset.Enabled = true;
            btnShrink.Enabled = true;
        }

        private void DisableControls()
        {
            trkZoom.Value = 1;
            trkZoom.Enabled = false;
            lblPercent.Text = @"---%";
            btnAlign.Enabled = false;
            btnEnlarge.Enabled = false;
            btnReset.Enabled = false;
            btnShrink.Enabled = false;
        }

        private void DoAdjustement(ImageZoomAdjustement adjustementType)
        {
            switch(adjustementType)
            {
                case ImageZoomAdjustement.Enlarge:
                    PannablePictureBox.EnlargeImage();
                    break;
                case ImageZoomAdjustement.Shrink:
                    PannablePictureBox.ShrinkImage();
                    break;
                case ImageZoomAdjustement.None:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(adjustementType), adjustementType, null);
            }
        }

        private void UpdateZoomPercentage()
        {
            lblPercent.Text = $@"{PannablePictureBox.GetZoomPercentage().ToString("F")}%";
        }
    }
}
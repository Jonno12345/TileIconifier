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
using System.Windows.Forms;

namespace TileIconifier.Controls.PannablePictureBox
{
    public partial class PannablePictureBoxControlPanel : UserControl
    {
        public PannablePictureBoxControlPanel()
        {
            InitializeComponent();
        }

        public PannablePictureBox PannablePictureBoxControl { get; private set; }

        public void SetPannablePictureBoxControl(PannablePictureBox value)
        {
            PannablePictureBoxControl = value;
            PannablePictureBoxControl.OnPannablePictureImagePropertyChange += (o, args) => UpdateControls();
        }

        public event EventHandler ChangeImageClick;

        private void PannablePictureBoxControlPanel_Load(object sender, EventArgs e)
        {
            var shrinkToolTip = new ToolTip();
            shrinkToolTip.SetToolTip(btnShrink, "Zoom Out");
            var enlargeToolTip = new ToolTip();
            enlargeToolTip.SetToolTip(btnEnlarge, "Zoom In");
            var resetToolTip = new ToolTip();
            resetToolTip.SetToolTip(btnReset, "Reset");
            var changeImageToolTip = new ToolTip();
            changeImageToolTip.SetToolTip(btnOpenImage, "Change Image");
            var centerImageToolTip = new ToolTip();
            centerImageToolTip.SetToolTip(btnCenter, "Center Image");
        }

        private void btnEnlarge_MouseDown(object sender, MouseEventArgs e)
        {
            tmrEnlarge_Tick(this, null);
            tmrEnlarge.Start();
        }

        private void btnEnlarge_MouseUp(object sender, MouseEventArgs e)
        {
            tmrEnlarge.Stop();
        }

        private void tmrEnlarge_Tick(object sender, EventArgs e)
        {
            PannablePictureBoxControl.EnlargeImage();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            PannablePictureBoxControl.ResetImage();
        }

        private void tmrShrink_Tick(object sender, EventArgs e)
        {
            PannablePictureBoxControl.ShrinkImage();
        }

        private void btnShrink_MouseDown(object sender, MouseEventArgs e)
        {
            tmrShrink_Tick(this, null);
            tmrShrink.Start();
        }

        private void btnShrink_MouseUp(object sender, MouseEventArgs e)
        {
            tmrShrink.Stop();
        }

        private void btnOpenImage_Click(object sender, EventArgs e)
        {
            ChangeImageClick?.Invoke(this, null);
        }

        private void btnCenter_Click(object sender, EventArgs e)
        {
            PannablePictureBoxControl.CenterImage();
        }

        private void trkZoom_Scroll(object sender, EventArgs e)
        {
            PannablePictureBoxControl.SetZoom(trkZoom.Value);
            UpdateZoomPercentage();
        }

        public void UpdateTrackBarAndZoom()
        {
            trkZoom.Value = (int) Math.Round(PannablePictureBoxControl.GetZoomPercentage(), 1);
            UpdateZoomPercentage();
        }

        public void UpdateControls()
        {
            if (PannablePictureBoxControl?.PannablePictureBoxImage.Image == null)
            {
                DisableControls();
                return;
            }
            EnableControls();
            UpdateTrackBarAndZoom();
        }

        private void EnableControls()
        {
            trkZoom.Enabled = true;
            btnCenter.Enabled = true;
            btnEnlarge.Enabled = true;
            btnReset.Enabled = true;
            btnShrink.Enabled = true;
        }

        private void DisableControls()
        {
            trkZoom.Value = 1;
            trkZoom.Enabled = false;
            lblPercent.Text = @"---%";
            btnCenter.Enabled = false;
            btnEnlarge.Enabled = false;
            btnReset.Enabled = false;
            btnShrink.Enabled = false;
        }

        private void UpdateZoomPercentage()
        {
            lblPercent.Text = PannablePictureBoxControl.GetZoomPercentage().ToString("F") + @"%";
        }
    }
}
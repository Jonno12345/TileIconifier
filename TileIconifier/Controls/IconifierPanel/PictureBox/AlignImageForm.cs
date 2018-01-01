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
using TileIconifier.Properties;
using TileIconifier.Utilities;

namespace TileIconifier.Controls.IconifierPanel.PictureBox
{
    public partial class AlignImageForm : Form
    {
        private int BUTTON_ICON_LOGICAL_SIZE = 24;

        public AlignImageForm()
        {
            InitializeComponent();
            SetButtonImages();

            //Setup the tags that are used to determine which command is associated
            //with each button that perform a continuous adjustement.
            btnNudgeUp.Tag = PannableImageContinuousAdjustement.NudgeUp;
            btnNudgeLeft.Tag = PannableImageContinuousAdjustement.NudgeLeft;
            btnNudgeRight.Tag = PannableImageContinuousAdjustement.NudgeRight;
            btnNudgeDown.Tag = PannableImageContinuousAdjustement.NudgeDown;
        }        

        private PannablePictureBox _pannablePictureBox = null;
        [DefaultValue(null)]
        public PannablePictureBox PannablePictureBox
        {
            get { return _pannablePictureBox; }
            set
            {
                if (_pannablePictureBox != value)
                {
                    //Remove the event handle attached to the old PannablePictureBox
                    if (_pannablePictureBox != null)
                    {
                        _pannablePictureBox.OnPannablePictureImagePropertyChange -= PannablePictureBox_OnPannablePictureImagePropertyChange;
                    }
                    if (value == null)
                    {
                        tlpBody.Enabled = false;                        
                    }
                    else
                    {
                        tlpBody.Enabled = true;
                        value.OnPannablePictureImagePropertyChange += PannablePictureBox_OnPannablePictureImagePropertyChange;
                    }
                    _pannablePictureBox = value;
                    SetPositionText(); //Must be done AFTER the field is set
                }
            }
        }

        private void PannablePictureBox_OnPannablePictureImagePropertyChange(object sender, EventArgs e)
        {
            SetPositionText();
        }



        private void btnLeft_Click(object sender, EventArgs e)
        {
            PannablePictureBox?.AlignLeft();
        }

        private void btnXMiddle_Click(object sender, EventArgs e)
        {
            PannablePictureBox?.AlignXMiddle();
        }

        private void btnRight_Click(object sender, EventArgs e)
        {
            PannablePictureBox?.AlignRight();
        }

        private void btnTop_Click(object sender, EventArgs e)
        {
            PannablePictureBox?.AlignTop();
        }

        private void btnYMiddle_Click(object sender, EventArgs e)
        {
            PannablePictureBox?.AlignYMiddle();
        }

        private void btnBottom_Click(object sender, EventArgs e)
        {
            PannablePictureBox?.AlignBottom();
        }

        private void btnCenter_Click(object sender, EventArgs e)
        {
            PannablePictureBox?.CenterImage();
        }

        private void NudgeButton_MouseDown(object sender, MouseEventArgs e)
        {
            var ctrl = (Control)sender;
            var adjustement = (PannableImageContinuousAdjustement)ctrl.Tag;
            PannablePictureBox?.BeginContinuousAdjustment(adjustement);
        }

        private void NudgeButton_MouseUp(object sender, MouseEventArgs e)
        {
            PannablePictureBox?.EndContinuousAdjustment();
        }

        protected override void OnDeactivate(EventArgs e)
        {
            base.OnDeactivate(e);

            Close();
        }

        private void SetPositionText()
        {
            if (PannablePictureBox != null)
            {
                var panImage = PannablePictureBox.PannablePictureBoxImage;
                var x = panImage.X;
                //Move the origin at the bottom of picture box since that's probably what most users expect.
                var y = PannablePictureBox.OutputSize.Height - panImage.Y - panImage.Height;

                lblXValue.Text = x.ToString();
                lblYValue.Text = y.ToString();
            }
            else
            {
                lblXValue.Text = string.Empty;
                lblYValue.Text = string.Empty;
            }
        }

        private void SetButtonImages()
        {
            Button[] btns =
            {
                btnLeft,
                btnXMiddle,
                btnRight,
                btnTop,
                btnYMiddle,
                btnBottom,
                btnNudgeUp,
                btnNudgeLeft,
                btnCenter,
                btnNudgeRight,
                btnNudgeDown
            };
            Image[] imgs = 
            {
                Resources.AlignLeft,
                Resources.AlignXMiddle,
                Resources.AlignRight,
                Resources.AlignTop,
                Resources.AlignYMiddle,
                Resources.AlignBottom,
                Resources.NudgeUp,
                Resources.NudgeLeft,
                Resources.AlignCenter,
                Resources.NudgeRight,
                Resources.NudgeDown
            };
            ButtonUtils.SetScaledImage(btns, imgs, new Size(BUTTON_ICON_LOGICAL_SIZE, BUTTON_ICON_LOGICAL_SIZE));
        }
    }
}

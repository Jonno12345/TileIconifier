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
using System.Windows.Forms;

namespace TileIconifier.Controls.IconifierPanel.PictureBox
{
    public partial class AlignImageForm : Form
    {
        private ImageAlignAdjustement _lastClickType;

        public event EventHandler<AlignImageEventArgs> ImageLocationChanged;

        public AlignImageForm()
        {
            InitializeComponent();

            Location = Cursor.Position;

            //Unlike what its name implies, the "DoubleClickTime" system setting
            //is not only used for double clicks. It more generally describes for
            //how long a mouse button needs to be pressed before the click is
            //considered as more than just a simple "click".
            tmrScrollDelay.Interval = SystemInformation.DoubleClickTime;

            //Setup the tags, which are used to determine which action
            //is associated with each button.
            btnLeft.Tag = ImageAlignAdjustement.LeftAlign;
            btnXMiddle.Tag = ImageAlignAdjustement.XAlign;
            btnRight.Tag = ImageAlignAdjustement.RightAlign;
            btnTop.Tag = ImageAlignAdjustement.TopAlign;
            btnYMiddle.Tag = ImageAlignAdjustement.YAlign;
            btnBottom.Tag = ImageAlignAdjustement.BottomAlign;
            btnNudgeUp.Tag = ImageAlignAdjustement.NudgeUp;
            btnNudgeLeft.Tag = ImageAlignAdjustement.NudgeLeft;
            btnCenter.Tag = ImageAlignAdjustement.Center;
            btnNudgeRight.Tag = ImageAlignAdjustement.NudgeRight;
            btnNudgeDown.Tag = ImageAlignAdjustement.NudgeDown;
        }        

        private PannablePictureBox _pannablePictureBox = null;
        [DefaultValue(null)]
        public PannablePictureBox PannablePictureBox
        {
            get { return _pannablePictureBox; }
            set
            {
                _pannablePictureBox = value;
                //Controls are disabled if no PannablePictureBox is set
                tlpBody.Enabled = (value != null);
                SetPositionText();
            }
        }

        protected override void OnDeactivate(EventArgs e)
        {
            base.OnDeactivate(e);

            Close();
        }

        private void AlignButton_Click(object sender, EventArgs e)
        {
            var ctrl = (Control)sender;
            var adjustement = (ImageAlignAdjustement)ctrl.Tag;

            DoAdjustement(adjustement);
        }

        private void PanButton_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            var ctrl = (Control)sender;
            var adjustement = (ImageAlignAdjustement)ctrl.Tag;

            _lastClickType = adjustement;
            //First adjustement immediatly when the button is down, before the delay is considered.
            DoAdjustement(adjustement);
            tmrScrollDelay.Start();
        }

        private void PanButton_MouseUp(object sender, MouseEventArgs e)
        {
            //Don't forget to stop tmrDelay to prevent it from starting tmrNudge if the delay is not reached yet.
            tmrScrollDelay.Stop();
            tmrNudge.Stop();
        }

        private void TmrScrollDelay_Tick(object sender, EventArgs e)
        {
            tmrScrollDelay.Stop();
            //Do an adjustement right now
            DoAdjustement(_lastClickType);
            //Start the continuous adjustement
            tmrNudge.Start();
        }

        private void TmrNudge_Tick(object sender, EventArgs e)
        {
            DoAdjustement(_lastClickType);
        }

        private void DoAdjustement(ImageAlignAdjustement adjustementType)
        {
            if (PannablePictureBox != null)
            {
                switch (adjustementType)
                {
                    case ImageAlignAdjustement.LeftAlign:
                        PannablePictureBox.AlignLeft();
                        break;
                    case ImageAlignAdjustement.BottomAlign:
                        PannablePictureBox.AlignBottom();
                        break;
                    case ImageAlignAdjustement.RightAlign:
                        PannablePictureBox.AlignRight();
                        break;
                    case ImageAlignAdjustement.TopAlign:
                        PannablePictureBox.AlignTop();
                        break;
                    case ImageAlignAdjustement.XAlign:
                        PannablePictureBox.AlignXMiddle();
                        break;
                    case ImageAlignAdjustement.YAlign:
                        PannablePictureBox.AlignYMiddle();
                        break;
                    case ImageAlignAdjustement.NudgeUp:
                        PannablePictureBox.Nudge(y: -1);
                        break;
                    case ImageAlignAdjustement.NudgeDown:
                        PannablePictureBox.Nudge(y: 1);
                        break;
                    case ImageAlignAdjustement.NudgeLeft:
                        PannablePictureBox.Nudge(-1);
                        break;
                    case ImageAlignAdjustement.NudgeRight:
                        PannablePictureBox.Nudge(1);
                        break;
                    case ImageAlignAdjustement.Center:
                        PannablePictureBox.CenterImage();
                        break;
                    case ImageAlignAdjustement.None:
                        return;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(adjustementType), adjustementType, null);
                }

                SetPositionText();

                ImageLocationChanged?.Invoke(this, new AlignImageEventArgs(adjustementType));
            }            
        }

        private void SetPositionText()
        {
            if (PannablePictureBox != null)
            {
                var panImage = PannablePictureBox.PannablePictureBoxImage;
                var x = panImage.X;
                //Move the origin at the bottom of picture box since that's probably what most users expect.
                //Should use client size here.
                var y = PannablePictureBox.Height - panImage.Y - panImage.Height;

                lblXValue.Text = x.ToString();
                lblYValue.Text = y.ToString();
            }
            else
            {
                lblXValue.Text = string.Empty;
                lblYValue.Text = string.Empty;
            }
        }
    }

    public enum ImageAlignAdjustement
    {
        None = 0,
        LeftAlign = 1,
        RightAlign = 2,
        TopAlign = 3,
        BottomAlign = 4,
        XAlign = 5,
        YAlign = 6,
        NudgeUp = 7,
        NudgeDown = 8,
        NudgeLeft = 9,
        NudgeRight = 10,
        Center = 11
    }

    public class AlignImageEventArgs : EventArgs
    {
        public ImageAlignAdjustement AlignButtonClicked { get; set; }

        public AlignImageEventArgs(ImageAlignAdjustement alignButtonClick)
        {
            AlignButtonClicked = alignButtonClick;
        }
    }
}

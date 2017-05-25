using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TileIconifier.Controls.IconifierPanel.PictureBox
{
    public partial class AlignImageForm : Form
    {
        private PannableImageAdjustement _lastClickType;

        public event EventHandler<AlignImageEventArgs> ImageLocationChanged;

        public AlignImageForm()
        {
            InitializeComponent();

            Location = Cursor.Position;

            //Unlike what its name implies, the "DoubleClickTime" system setting
            //is not only used for double clicks. It more generally describes for
            //how long a mouse button needs to be pressed before the click is
            //considered as more than just a simple "click".
            tmrDelay.Interval = SystemInformation.DoubleClickTime;

            //Setup the tags, which are used to determine which action
            //is associated with each button.
            btnLeft.Tag = PannableImageAdjustement.LeftAlign;
            btnXMiddle.Tag = PannableImageAdjustement.XAlign;
            btnRight.Tag = PannableImageAdjustement.RightAlign;
            btnTop.Tag = PannableImageAdjustement.TopAlign;
            btnYMiddle.Tag = PannableImageAdjustement.YAlign;
            btnBottom.Tag = PannableImageAdjustement.BottomAlign;
            btnNudgeUp.Tag = PannableImageAdjustement.NudgeUp;
            btnNudgeLeft.Tag = PannableImageAdjustement.NudgeLeft;
            btnCenter.Tag = PannableImageAdjustement.Center;
            btnNudgeRight.Tag = PannableImageAdjustement.NudgeRight;
            btnNudgeDown.Tag = PannableImageAdjustement.NudgeDown;
        }        

        private PannablePictureBox _pannablePictureBox = null;
        [DefaultValue(null)]
        public PannablePictureBox PannablePictureBox
        {
            get { return _pannablePictureBox; }
            set
            {
                var newEnabled = (value != null);
                if ((_pannablePictureBox /*old enabled value*/ != null) != newEnabled)
                {
                    tlpBody.Enabled = newEnabled;
                }
                _pannablePictureBox = value;
                SetPositionText();
            }
        }

        protected override void OnDeactivate(EventArgs e)
        {
            Close();

            base.OnDeactivate(e);
        }

        private void AlignButton_Click(object sender, EventArgs e)
        {
            var item = (Control)sender;
            var adjustementType = (PannableImageAdjustement)item.Tag;

            DoAdjustement(adjustementType);
        }

        private void PanButton_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            var item = (Control)sender;
            var adjustementType = (PannableImageAdjustement)item.Tag;

            _lastClickType = adjustementType;
            //First adjustement immediatly when the button is down, before the delay is considered.
            DoAdjustement(adjustementType);
            tmrDelay.Start();
        }

        private void PanButton_MouseUp(object sender, MouseEventArgs e)
        {
            //Don't forget to stop tmrDelay to prevent it from starting tmrNudge if the delay is not reached yet.
            tmrDelay.Stop();
            tmrNudge.Stop();
        }

        private void TmrDelay_Tick(object sender, EventArgs e)
        {
            tmrDelay.Stop();
            //Do an adjustement right now
            DoAdjustement(_lastClickType);
            //Start the continuous adjustement
            tmrNudge.Start();
        }

        private void TmrNudge_Tick(object sender, EventArgs e)
        {
            DoAdjustement(_lastClickType);
        }

        private void DoAdjustement(PannableImageAdjustement adjustementType)
        {
            if (PannablePictureBox != null)
            {
                switch (adjustementType)
                {
                    case PannableImageAdjustement.LeftAlign:
                        PannablePictureBox.AlignLeft();
                        break;
                    case PannableImageAdjustement.BottomAlign:
                        PannablePictureBox.AlignBottom();
                        break;
                    case PannableImageAdjustement.RightAlign:
                        PannablePictureBox.AlignRight();
                        break;
                    case PannableImageAdjustement.TopAlign:
                        PannablePictureBox.AlignTop();
                        break;
                    case PannableImageAdjustement.XAlign:
                        PannablePictureBox.AlignXMiddle();
                        break;
                    case PannableImageAdjustement.YAlign:
                        PannablePictureBox.AlignYMiddle();
                        break;
                    case PannableImageAdjustement.NudgeUp:
                        PannablePictureBox.Nudge(y: -1);
                        break;
                    case PannableImageAdjustement.NudgeDown:
                        PannablePictureBox.Nudge(y: 1);
                        break;
                    case PannableImageAdjustement.NudgeLeft:
                        PannablePictureBox.Nudge(-1);
                        break;
                    case PannableImageAdjustement.NudgeRight:
                        PannablePictureBox.Nudge(1);
                        break;
                    case PannableImageAdjustement.Center:
                        PannablePictureBox.CenterImage();
                        break;
                    case PannableImageAdjustement.None:
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

    public enum PannableImageAdjustement
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
        public PannableImageAdjustement AlignButtonClicked { get; set; }

        public AlignImageEventArgs(PannableImageAdjustement alignButtonClick)
        {
            AlignButtonClicked = alignButtonClick;
        }
    }
}

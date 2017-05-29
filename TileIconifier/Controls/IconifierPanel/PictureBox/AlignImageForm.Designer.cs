namespace TileIconifier.Controls.IconifierPanel.PictureBox
{
    partial class AlignImageForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AlignImageForm));
            this.tlpBody = new System.Windows.Forms.TableLayoutPanel();
            this.ilsCommandIcons = new System.Windows.Forms.ImageList(this.components);
            this.tlpLocation = new System.Windows.Forms.TableLayoutPanel();
            this.lblX = new System.Windows.Forms.Label();
            this.lblXValue = new System.Windows.Forms.Label();
            this.lblY = new System.Windows.Forms.Label();
            this.lblYValue = new System.Windows.Forms.Label();
            this.tmrScrollDelay = new System.Windows.Forms.Timer(this.components);
            this.tmrNudge = new System.Windows.Forms.Timer(this.components);
            this.ttpCommands = new System.Windows.Forms.ToolTip(this.components);
            this.btnLeft = new TileIconifier.Controls.SkinnableButton();
            this.btnXMiddle = new TileIconifier.Controls.SkinnableButton();
            this.btnRight = new TileIconifier.Controls.SkinnableButton();
            this.btnTop = new TileIconifier.Controls.SkinnableButton();
            this.btnYMiddle = new TileIconifier.Controls.SkinnableButton();
            this.btnBottom = new TileIconifier.Controls.SkinnableButton();
            this.btnNudgeUp = new TileIconifier.Controls.SkinnableButton();
            this.btnNudgeLeft = new TileIconifier.Controls.SkinnableButton();
            this.btnCenter = new TileIconifier.Controls.SkinnableButton();
            this.btnNudgeRight = new TileIconifier.Controls.SkinnableButton();
            this.btnNudgeDown = new TileIconifier.Controls.SkinnableButton();
            this.tlpBody.SuspendLayout();
            this.tlpLocation.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpBody
            // 
            resources.ApplyResources(this.tlpBody, "tlpBody");
            this.tlpBody.Controls.Add(this.btnLeft, 0, 0);
            this.tlpBody.Controls.Add(this.btnXMiddle, 1, 0);
            this.tlpBody.Controls.Add(this.btnRight, 2, 0);
            this.tlpBody.Controls.Add(this.btnTop, 0, 1);
            this.tlpBody.Controls.Add(this.btnYMiddle, 1, 1);
            this.tlpBody.Controls.Add(this.btnBottom, 2, 1);
            this.tlpBody.Controls.Add(this.btnNudgeUp, 1, 3);
            this.tlpBody.Controls.Add(this.btnNudgeLeft, 0, 4);
            this.tlpBody.Controls.Add(this.btnCenter, 1, 4);
            this.tlpBody.Controls.Add(this.btnNudgeRight, 2, 4);
            this.tlpBody.Controls.Add(this.btnNudgeDown, 1, 5);
            this.tlpBody.Controls.Add(this.tlpLocation, 0, 7);
            this.tlpBody.Name = "tlpBody";
            // 
            // ilsCommandIcons
            // 
            this.ilsCommandIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilsCommandIcons.ImageStream")));
            this.ilsCommandIcons.TransparentColor = System.Drawing.Color.Transparent;
            this.ilsCommandIcons.Images.SetKeyName(0, "AlignBottom.png");
            this.ilsCommandIcons.Images.SetKeyName(1, "AlignCenter.png");
            this.ilsCommandIcons.Images.SetKeyName(2, "AlignLeft.png");
            this.ilsCommandIcons.Images.SetKeyName(3, "AlignRight.png");
            this.ilsCommandIcons.Images.SetKeyName(4, "AlignTop.png");
            this.ilsCommandIcons.Images.SetKeyName(5, "AlignXMiddle.png");
            this.ilsCommandIcons.Images.SetKeyName(6, "AlignYMiddle.png");
            this.ilsCommandIcons.Images.SetKeyName(7, "NudgeDown.png");
            this.ilsCommandIcons.Images.SetKeyName(8, "NudgeLeft.png");
            this.ilsCommandIcons.Images.SetKeyName(9, "NudgeRight.png");
            this.ilsCommandIcons.Images.SetKeyName(10, "NudgeUp.png");
            // 
            // tlpLocation
            // 
            resources.ApplyResources(this.tlpLocation, "tlpLocation");
            this.tlpBody.SetColumnSpan(this.tlpLocation, 3);
            this.tlpLocation.Controls.Add(this.lblX, 0, 0);
            this.tlpLocation.Controls.Add(this.lblXValue, 1, 0);
            this.tlpLocation.Controls.Add(this.lblY, 2, 0);
            this.tlpLocation.Controls.Add(this.lblYValue, 3, 0);
            this.tlpLocation.Name = "tlpLocation";
            // 
            // lblX
            // 
            resources.ApplyResources(this.lblX, "lblX");
            this.lblX.Name = "lblX";
            // 
            // lblXValue
            // 
            resources.ApplyResources(this.lblXValue, "lblXValue");
            this.lblXValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblXValue.Name = "lblXValue";
            // 
            // lblY
            // 
            resources.ApplyResources(this.lblY, "lblY");
            this.lblY.Name = "lblY";
            // 
            // lblYValue
            // 
            resources.ApplyResources(this.lblYValue, "lblYValue");
            this.lblYValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblYValue.Name = "lblYValue";
            // 
            // tmrScrollDelay
            // 
            this.tmrScrollDelay.Tick += new System.EventHandler(this.TmrScrollDelay_Tick);
            // 
            // tmrNudge
            // 
            this.tmrNudge.Interval = 50;
            this.tmrNudge.Tick += new System.EventHandler(this.TmrNudge_Tick);
            // 
            // btnLeft
            // 
            resources.ApplyResources(this.btnLeft, "btnLeft");
            this.btnLeft.ImageList = this.ilsCommandIcons;
            this.btnLeft.Name = "btnLeft";
            this.ttpCommands.SetToolTip(this.btnLeft, resources.GetString("btnLeft.ToolTip"));
            this.btnLeft.UseVisualStyleBackColor = true;
            this.btnLeft.Click += new System.EventHandler(this.AlignButton_Click);
            // 
            // btnXMiddle
            // 
            resources.ApplyResources(this.btnXMiddle, "btnXMiddle");
            this.btnXMiddle.ImageList = this.ilsCommandIcons;
            this.btnXMiddle.Name = "btnXMiddle";
            this.ttpCommands.SetToolTip(this.btnXMiddle, resources.GetString("btnXMiddle.ToolTip"));
            this.btnXMiddle.UseVisualStyleBackColor = true;
            this.btnXMiddle.Click += new System.EventHandler(this.AlignButton_Click);
            // 
            // btnRight
            // 
            resources.ApplyResources(this.btnRight, "btnRight");
            this.btnRight.ImageList = this.ilsCommandIcons;
            this.btnRight.Name = "btnRight";
            this.ttpCommands.SetToolTip(this.btnRight, resources.GetString("btnRight.ToolTip"));
            this.btnRight.UseVisualStyleBackColor = true;
            this.btnRight.Click += new System.EventHandler(this.AlignButton_Click);
            // 
            // btnTop
            // 
            resources.ApplyResources(this.btnTop, "btnTop");
            this.btnTop.ImageList = this.ilsCommandIcons;
            this.btnTop.Name = "btnTop";
            this.ttpCommands.SetToolTip(this.btnTop, resources.GetString("btnTop.ToolTip"));
            this.btnTop.UseVisualStyleBackColor = true;
            this.btnTop.Click += new System.EventHandler(this.AlignButton_Click);
            // 
            // btnYMiddle
            // 
            resources.ApplyResources(this.btnYMiddle, "btnYMiddle");
            this.btnYMiddle.ImageList = this.ilsCommandIcons;
            this.btnYMiddle.Name = "btnYMiddle";
            this.ttpCommands.SetToolTip(this.btnYMiddle, resources.GetString("btnYMiddle.ToolTip"));
            this.btnYMiddle.UseVisualStyleBackColor = true;
            this.btnYMiddle.Click += new System.EventHandler(this.AlignButton_Click);
            // 
            // btnBottom
            // 
            resources.ApplyResources(this.btnBottom, "btnBottom");
            this.btnBottom.ImageList = this.ilsCommandIcons;
            this.btnBottom.Name = "btnBottom";
            this.ttpCommands.SetToolTip(this.btnBottom, resources.GetString("btnBottom.ToolTip"));
            this.btnBottom.UseVisualStyleBackColor = true;
            this.btnBottom.Click += new System.EventHandler(this.AlignButton_Click);
            // 
            // btnNudgeUp
            // 
            resources.ApplyResources(this.btnNudgeUp, "btnNudgeUp");
            this.btnNudgeUp.ImageList = this.ilsCommandIcons;
            this.btnNudgeUp.Name = "btnNudgeUp";
            this.btnNudgeUp.UseVisualStyleBackColor = true;
            this.btnNudgeUp.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PanButton_MouseDown);
            this.btnNudgeUp.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PanButton_MouseUp);
            // 
            // btnNudgeLeft
            // 
            resources.ApplyResources(this.btnNudgeLeft, "btnNudgeLeft");
            this.btnNudgeLeft.ImageList = this.ilsCommandIcons;
            this.btnNudgeLeft.Name = "btnNudgeLeft";
            this.btnNudgeLeft.UseVisualStyleBackColor = true;
            this.btnNudgeLeft.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PanButton_MouseDown);
            this.btnNudgeLeft.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PanButton_MouseUp);
            // 
            // btnCenter
            // 
            resources.ApplyResources(this.btnCenter, "btnCenter");
            this.btnCenter.ImageList = this.ilsCommandIcons;
            this.btnCenter.Name = "btnCenter";
            this.btnCenter.UseVisualStyleBackColor = true;
            this.btnCenter.Click += new System.EventHandler(this.AlignButton_Click);
            // 
            // btnNudgeRight
            // 
            resources.ApplyResources(this.btnNudgeRight, "btnNudgeRight");
            this.btnNudgeRight.ImageList = this.ilsCommandIcons;
            this.btnNudgeRight.Name = "btnNudgeRight";
            this.btnNudgeRight.UseVisualStyleBackColor = true;
            this.btnNudgeRight.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PanButton_MouseDown);
            this.btnNudgeRight.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PanButton_MouseUp);
            // 
            // btnNudgeDown
            // 
            resources.ApplyResources(this.btnNudgeDown, "btnNudgeDown");
            this.btnNudgeDown.ImageList = this.ilsCommandIcons;
            this.btnNudgeDown.Name = "btnNudgeDown";
            this.btnNudgeDown.UseVisualStyleBackColor = true;
            this.btnNudgeDown.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PanButton_MouseDown);
            this.btnNudgeDown.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PanButton_MouseUp);
            // 
            // AlignImageForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ControlBox = false;
            this.Controls.Add(this.tlpBody);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AlignImageForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.tlpBody.ResumeLayout(false);
            this.tlpBody.PerformLayout();
            this.tlpLocation.ResumeLayout(false);
            this.tlpLocation.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpBody;
        private SkinnableButton btnLeft;
        private System.Windows.Forms.ImageList ilsCommandIcons;
        private SkinnableButton btnXMiddle;
        private SkinnableButton btnRight;
        private SkinnableButton btnTop;
        private SkinnableButton btnYMiddle;
        private SkinnableButton btnBottom;
        private SkinnableButton btnNudgeUp;
        private SkinnableButton btnNudgeLeft;
        private SkinnableButton btnCenter;
        private SkinnableButton btnNudgeRight;
        private SkinnableButton btnNudgeDown;
        private System.Windows.Forms.TableLayoutPanel tlpLocation;
        private System.Windows.Forms.Label lblX;
        private System.Windows.Forms.Label lblXValue;
        private System.Windows.Forms.Label lblY;
        private System.Windows.Forms.Label lblYValue;
        private System.Windows.Forms.Timer tmrScrollDelay;
        private System.Windows.Forms.Timer tmrNudge;
        private System.Windows.Forms.ToolTip ttpCommands;
    }
}
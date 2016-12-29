using TileIconifier.Controls.IconifierPanel.PictureBox;

namespace TileIconifier.Controls.IconifierPanel
{
    partial class TileIconifierPanel
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TileIconifierPanel));
            this.pnlImages = new System.Windows.Forms.Panel();
            this.lblSmallIcon = new System.Windows.Forms.Label();
            this.lblMediumIcon = new System.Windows.Forms.Label();
            this.panPctSmallIcon = new PannablePictureBox();
            this.panPctMediumIcon = new PannablePictureBox();
            this.chkUseSameImg = new System.Windows.Forms.CheckBox();
            this.pannablePictureBoxControlPanelMedium = new PannablePictureBoxControlPanel();
            this.pannablePictureBoxControlPanelSmall = new PannablePictureBoxControlPanel();
            this.btnReset = new System.Windows.Forms.Button();
            this.lblUnsaved = new System.Windows.Forms.Label();
            this.colorPanel = new TileIconifier.Controls.IconifierPanel.ColorPanel();
            this.pnlImages.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlImages
            // 
            resources.ApplyResources(this.pnlImages, "pnlImages");
            this.pnlImages.Controls.Add(this.lblSmallIcon);
            this.pnlImages.Controls.Add(this.lblMediumIcon);
            this.pnlImages.Controls.Add(this.panPctSmallIcon);
            this.pnlImages.Controls.Add(this.panPctMediumIcon);
            this.pnlImages.Controls.Add(this.chkUseSameImg);
            this.pnlImages.Controls.Add(this.pannablePictureBoxControlPanelMedium);
            this.pnlImages.Controls.Add(this.pannablePictureBoxControlPanelSmall);
            this.pnlImages.Name = "pnlImages";
            // 
            // lblSmallIcon
            // 
            resources.ApplyResources(this.lblSmallIcon, "lblSmallIcon");
            this.lblSmallIcon.Name = "lblSmallIcon";
            // 
            // lblMediumIcon
            // 
            resources.ApplyResources(this.lblMediumIcon, "lblMediumIcon");
            this.lblMediumIcon.Name = "lblMediumIcon";
            // 
            // panPctSmallIcon
            // 
            this.panPctSmallIcon.AssociatedSize = new System.Drawing.Size(0, 0);
            this.panPctSmallIcon.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.panPctSmallIcon, "panPctSmallIcon");
            this.panPctSmallIcon.Name = "panPctSmallIcon";
            this.panPctSmallIcon.Click += new System.EventHandler(this.panPctSmallIcon_Click);
            this.panPctSmallIcon.DoubleClick += new System.EventHandler(this.panPctSmallIcon_DoubleClick);
            // 
            // panPctMediumIcon
            // 
            this.panPctMediumIcon.AssociatedSize = new System.Drawing.Size(0, 0);
            this.panPctMediumIcon.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.panPctMediumIcon, "panPctMediumIcon");
            this.panPctMediumIcon.Name = "panPctMediumIcon";
            this.panPctMediumIcon.Click += new System.EventHandler(this.panPctMediumIcon_Click);
            this.panPctMediumIcon.DoubleClick += new System.EventHandler(this.panPctMediumIcon_DoubleClick);
            // 
            // chkUseSameImg
            // 
            resources.ApplyResources(this.chkUseSameImg, "chkUseSameImg");
            this.chkUseSameImg.Checked = true;
            this.chkUseSameImg.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkUseSameImg.Name = "chkUseSameImg";
            this.chkUseSameImg.UseVisualStyleBackColor = true;
            // 
            // pannablePictureBoxControlPanelMedium
            // 
            resources.ApplyResources(this.pannablePictureBoxControlPanelMedium, "pannablePictureBoxControlPanelMedium");
            this.pannablePictureBoxControlPanelMedium.Name = "pannablePictureBoxControlPanelMedium";
            // 
            // pannablePictureBoxControlPanelSmall
            // 
            resources.ApplyResources(this.pannablePictureBoxControlPanelSmall, "pannablePictureBoxControlPanelSmall");
            this.pannablePictureBoxControlPanelSmall.Name = "pannablePictureBoxControlPanelSmall";
            // 
            // btnReset
            // 
            resources.ApplyResources(this.btnReset, "btnReset");
            this.btnReset.Name = "btnReset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // lblUnsaved
            // 
            resources.ApplyResources(this.lblUnsaved, "lblUnsaved");
            this.lblUnsaved.ForeColor = System.Drawing.Color.Red;
            this.lblUnsaved.Name = "lblUnsaved";
            // 
            // colorPanel
            // 
            resources.ApplyResources(this.colorPanel, "colorPanel");
            this.colorPanel.Name = "colorPanel";
            // 
            // TileIconifierPanel
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.lblUnsaved);
            this.Controls.Add(this.colorPanel);
            this.Controls.Add(this.pnlImages);
            this.Name = "TileIconifierPanel";
            resources.ApplyResources(this, "$this");
            this.Load += new System.EventHandler(this.TileIconifierPanel_Load);
            this.pnlImages.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlImages;
        private System.Windows.Forms.Label lblSmallIcon;
        private System.Windows.Forms.Label lblMediumIcon;
        private PictureBox.PannablePictureBox panPctSmallIcon;
        private PictureBox.PannablePictureBox panPctMediumIcon;
        private System.Windows.Forms.CheckBox chkUseSameImg;
        private PannablePictureBoxControlPanel pannablePictureBoxControlPanelMedium;
        private PannablePictureBoxControlPanel pannablePictureBoxControlPanelSmall;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Label lblUnsaved;
        private ColorPanel colorPanel;
    }
}

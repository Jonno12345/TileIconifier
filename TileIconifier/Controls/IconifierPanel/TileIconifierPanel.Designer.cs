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
            this.tlpPictureBoxSmall = new System.Windows.Forms.TableLayoutPanel();
            this.lblSmallIcon = new System.Windows.Forms.Label();
            this.tlpPictureBoxMedium = new System.Windows.Forms.TableLayoutPanel();
            this.lblMediumIcon = new System.Windows.Forms.Label();
            this.lblUnsaved = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnReset = new TileIconifier.Controls.SkinnableButton();
            this.colorPanel = new TileIconifier.Controls.IconifierPanel.ColorPanel();
            this.panPctSmallIcon = new TileIconifier.Controls.IconifierPanel.PictureBox.PannablePictureBox();
            this.panPctMediumIcon = new TileIconifier.Controls.IconifierPanel.PictureBox.PannablePictureBox();
            this.chkUseSameImg = new TileIconifier.Controls.SkinnableCheckBox();
            this.pannablePictureBoxControlPanelMedium = new TileIconifier.Controls.IconifierPanel.PictureBox.PannablePictureBoxControlPanel();
            this.pannablePictureBoxControlPanelSmall = new TileIconifier.Controls.IconifierPanel.PictureBox.PannablePictureBoxControlPanel();
            this.pnlImages.SuspendLayout();
            this.tlpPictureBoxSmall.SuspendLayout();
            this.tlpPictureBoxMedium.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlImages
            // 
            resources.ApplyResources(this.pnlImages, "pnlImages");
            this.pnlImages.Controls.Add(this.tlpPictureBoxSmall);
            this.pnlImages.Controls.Add(this.tlpPictureBoxMedium);
            this.pnlImages.Controls.Add(this.chkUseSameImg);
            this.pnlImages.Controls.Add(this.pannablePictureBoxControlPanelMedium);
            this.pnlImages.Controls.Add(this.pannablePictureBoxControlPanelSmall);
            this.pnlImages.Name = "pnlImages";
            // 
            // tlpPictureBoxSmall
            // 
            resources.ApplyResources(this.tlpPictureBoxSmall, "tlpPictureBoxSmall");
            this.tlpPictureBoxSmall.Controls.Add(this.lblSmallIcon, 0, 0);
            this.tlpPictureBoxSmall.Controls.Add(this.panPctSmallIcon, 0, 1);
            this.tlpPictureBoxSmall.Name = "tlpPictureBoxSmall";
            // 
            // lblSmallIcon
            // 
            resources.ApplyResources(this.lblSmallIcon, "lblSmallIcon");
            this.lblSmallIcon.Name = "lblSmallIcon";
            // 
            // tlpPictureBoxMedium
            // 
            resources.ApplyResources(this.tlpPictureBoxMedium, "tlpPictureBoxMedium");
            this.tlpPictureBoxMedium.Controls.Add(this.lblMediumIcon, 0, 0);
            this.tlpPictureBoxMedium.Controls.Add(this.panPctMediumIcon, 0, 1);
            this.tlpPictureBoxMedium.Name = "tlpPictureBoxMedium";
            // 
            // lblMediumIcon
            // 
            resources.ApplyResources(this.lblMediumIcon, "lblMediumIcon");
            this.lblMediumIcon.Name = "lblMediumIcon";
            // 
            // lblUnsaved
            // 
            resources.ApplyResources(this.lblUnsaved, "lblUnsaved");
            this.lblUnsaved.ForeColor = System.Drawing.Color.Red;
            this.lblUnsaved.Name = "lblUnsaved";
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.lblUnsaved, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnReset, 1, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // btnReset
            // 
            resources.ApplyResources(this.btnReset, "btnReset");
            this.btnReset.DisabledForeColor = System.Drawing.Color.Empty;
            this.btnReset.Name = "btnReset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // colorPanel
            // 
            resources.ApplyResources(this.colorPanel, "colorPanel");
            this.colorPanel.Name = "colorPanel";
            // 
            // panPctSmallIcon
            // 
            resources.ApplyResources(this.panPctSmallIcon, "panPctSmallIcon");
            this.panPctSmallIcon.AssociatedSize = new System.Drawing.Size(0, 0);
            this.panPctSmallIcon.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panPctSmallIcon.Name = "panPctSmallIcon";
            this.panPctSmallIcon.Click += new System.EventHandler(this.panPctSmallIcon_Click);
            this.panPctSmallIcon.DoubleClick += new System.EventHandler(this.panPctSmallIcon_DoubleClick);
            // 
            // panPctMediumIcon
            // 
            resources.ApplyResources(this.panPctMediumIcon, "panPctMediumIcon");
            this.panPctMediumIcon.AssociatedSize = new System.Drawing.Size(0, 0);
            this.panPctMediumIcon.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panPctMediumIcon.Name = "panPctMediumIcon";
            this.panPctMediumIcon.Click += new System.EventHandler(this.panPctMediumIcon_Click);
            this.panPctMediumIcon.DoubleClick += new System.EventHandler(this.panPctMediumIcon_DoubleClick);
            // 
            // chkUseSameImg
            // 
            resources.ApplyResources(this.chkUseSameImg, "chkUseSameImg");
            this.chkUseSameImg.Checked = true;
            this.chkUseSameImg.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkUseSameImg.DisabledForeColor = System.Drawing.Color.Empty;
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
            // TileIconifierPanel
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.colorPanel);
            this.Controls.Add(this.pnlImages);
            this.Name = "TileIconifierPanel";
            this.Load += new System.EventHandler(this.TileIconifierPanel_Load);
            this.pnlImages.ResumeLayout(false);
            this.pnlImages.PerformLayout();
            this.tlpPictureBoxSmall.ResumeLayout(false);
            this.tlpPictureBoxSmall.PerformLayout();
            this.tlpPictureBoxMedium.ResumeLayout(false);
            this.tlpPictureBoxMedium.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlImages;
        private System.Windows.Forms.Label lblSmallIcon;
        private System.Windows.Forms.Label lblMediumIcon;
        private PictureBox.PannablePictureBox panPctSmallIcon;
        private PictureBox.PannablePictureBox panPctMediumIcon;
        private TileIconifier.Controls.SkinnableCheckBox chkUseSameImg;
        private PannablePictureBoxControlPanel pannablePictureBoxControlPanelMedium;
        private PannablePictureBoxControlPanel pannablePictureBoxControlPanelSmall;
        private TileIconifier.Controls.SkinnableButton btnReset;
        private System.Windows.Forms.Label lblUnsaved;
        private ColorPanel colorPanel;
        private System.Windows.Forms.TableLayoutPanel tlpPictureBoxSmall;
        private System.Windows.Forms.TableLayoutPanel tlpPictureBoxMedium;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}

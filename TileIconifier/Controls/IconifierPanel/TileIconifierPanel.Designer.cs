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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TileIconifierPanel));
            this.pnlImages = new System.Windows.Forms.Panel();
            this.lblUnsaved = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.cmsPicBox = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tmiChangeImage = new System.Windows.Forms.ToolStripMenuItem();
            this.tmiCentreImage = new System.Windows.Forms.ToolStripMenuItem();
            this.btnReset = new TileIconifier.Controls.SkinnableButton();
            this.pannablePictureBoxControlPanelMedium = new TileIconifier.Controls.IconifierPanel.PictureBox.PannablePictureBoxControlPanel();
            this.colorPanel = new TileIconifier.Controls.IconifierPanel.ColorPanel();
            this.pannablePictureBoxControlPanelSmall = new TileIconifier.Controls.IconifierPanel.PictureBox.PannablePictureBoxControlPanel();
            this.chkUseSameImg = new TileIconifier.Controls.SkinnableCheckBox();
            this.tableLayoutPanel2.SuspendLayout();
            this.cmsPicBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlImages
            // 
            resources.ApplyResources(this.pnlImages, "pnlImages");
            this.pnlImages.Name = "pnlImages";
            // 
            // lblUnsaved
            // 
            resources.ApplyResources(this.lblUnsaved, "lblUnsaved");
            this.tableLayoutPanel2.SetColumnSpan(this.lblUnsaved, 2);
            this.lblUnsaved.Name = "lblUnsaved";
            // 
            // tableLayoutPanel2
            // 
            resources.ApplyResources(this.tableLayoutPanel2, "tableLayoutPanel2");
            this.tableLayoutPanel2.Controls.Add(this.btnReset, 2, 2);
            this.tableLayoutPanel2.Controls.Add(this.pannablePictureBoxControlPanelMedium, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.colorPanel, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.lblUnsaved, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.pannablePictureBoxControlPanelSmall, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.chkUseSameImg, 0, 1);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            // 
            // cmsPicBox
            // 
            this.cmsPicBox.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tmiChangeImage,
            this.tmiCentreImage});
            this.cmsPicBox.Name = "cmsPicBox";
            resources.ApplyResources(this.cmsPicBox, "cmsPicBox");
            // 
            // tmiChangeImage
            // 
            this.tmiChangeImage.Name = "tmiChangeImage";
            resources.ApplyResources(this.tmiChangeImage, "tmiChangeImage");
            this.tmiChangeImage.Click += new System.EventHandler(this.tmiChangeImage_Click);
            // 
            // tmiCentreImage
            // 
            this.tmiCentreImage.Name = "tmiCentreImage";
            resources.ApplyResources(this.tmiCentreImage, "tmiCentreImage");
            this.tmiCentreImage.Click += new System.EventHandler(this.tmiCentreImage_Click);
            // 
            // btnReset
            // 
            resources.ApplyResources(this.btnReset, "btnReset");
            this.btnReset.Name = "btnReset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // pannablePictureBoxControlPanelMedium
            // 
            resources.ApplyResources(this.pannablePictureBoxControlPanelMedium, "pannablePictureBoxControlPanelMedium");
            this.pannablePictureBoxControlPanelMedium.Name = "pannablePictureBoxControlPanelMedium";
            this.pannablePictureBoxControlPanelMedium.PannablePictureBoxSize = new System.Drawing.Size(100, 100);
            // 
            // colorPanel
            // 
            resources.ApplyResources(this.colorPanel, "colorPanel");
            this.tableLayoutPanel2.SetColumnSpan(this.colorPanel, 3);
            this.colorPanel.Name = "colorPanel";
            // 
            // pannablePictureBoxControlPanelSmall
            // 
            resources.ApplyResources(this.pannablePictureBoxControlPanelSmall, "pannablePictureBoxControlPanelSmall");
            this.tableLayoutPanel2.SetColumnSpan(this.pannablePictureBoxControlPanelSmall, 2);
            this.pannablePictureBoxControlPanelSmall.Name = "pannablePictureBoxControlPanelSmall";
            this.pannablePictureBoxControlPanelSmall.PannablePictureBoxSize = new System.Drawing.Size(50, 50);
            // 
            // chkUseSameImg
            // 
            resources.ApplyResources(this.chkUseSameImg, "chkUseSameImg");
            this.chkUseSameImg.Checked = true;
            this.chkUseSameImg.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tableLayoutPanel2.SetColumnSpan(this.chkUseSameImg, 3);
            this.chkUseSameImg.Name = "chkUseSameImg";
            this.chkUseSameImg.UseVisualStyleBackColor = true;
            // 
            // TileIconifierPanel
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.pnlImages);
            this.Name = "TileIconifierPanel";
            resources.ApplyResources(this, "$this");
            this.Load += new System.EventHandler(this.TileIconifierPanel_Load);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.cmsPicBox.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlImages;
        private SkinnableCheckBox chkUseSameImg;
        private PannablePictureBoxControlPanel pannablePictureBoxControlPanelMedium;
        private PannablePictureBoxControlPanel pannablePictureBoxControlPanelSmall;
        private TileIconifier.Controls.SkinnableButton btnReset;
        private System.Windows.Forms.Label lblUnsaved;
        private ColorPanel colorPanel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.ContextMenuStrip cmsPicBox;
        private System.Windows.Forms.ToolStripMenuItem tmiChangeImage;
        private System.Windows.Forms.ToolStripMenuItem tmiCentreImage;
    }
}

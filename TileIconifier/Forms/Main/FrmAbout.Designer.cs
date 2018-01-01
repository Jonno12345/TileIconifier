namespace TileIconifier.Forms.Main
{
    partial class FrmAbout
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAbout));
            this.lblVersion = new System.Windows.Forms.Label();
            this.lblTileIconifier = new System.Windows.Forms.Label();
            this.pctLogo = new System.Windows.Forms.PictureBox();
            this.rtxtAbout = new TileIconifier.Controls.SkinnableRichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pctLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // lblVersion
            // 
            resources.ApplyResources(this.lblVersion, "lblVersion");
            this.lblVersion.Name = "lblVersion";
            // 
            // lblTileIconifier
            // 
            resources.ApplyResources(this.lblTileIconifier, "lblTileIconifier");
            this.lblTileIconifier.Name = "lblTileIconifier";
            // 
            // pctLogo
            // 
            this.pctLogo.Image = global::TileIconifier.Properties.Resources.tiles2_shadow;
            resources.ApplyResources(this.pctLogo, "pctLogo");
            this.pctLogo.Name = "pctLogo";
            this.pctLogo.TabStop = false;
            // 
            // rtxtAbout
            // 
            resources.ApplyResources(this.rtxtAbout, "rtxtAbout");
            this.rtxtAbout.Name = "rtxtAbout";
            this.rtxtAbout.ReadOnly = true;
            this.rtxtAbout.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.rtxtAbout_LinkClicked);
            // 
            // FrmAbout
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.lblTileIconifier);
            this.Controls.Add(this.pctLogo);
            this.Controls.Add(this.rtxtAbout);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmAbout";
            this.Load += new System.EventHandler(this.frmAbout_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pctLogo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private TileIconifier.Controls.SkinnableRichTextBox rtxtAbout;
        private System.Windows.Forms.PictureBox pctLogo;
        private System.Windows.Forms.Label lblTileIconifier;
        private System.Windows.Forms.Label lblVersion;
    }
}
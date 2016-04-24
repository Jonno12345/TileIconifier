namespace TileIconifier.Forms
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
            this.rtxtAbout = new System.Windows.Forms.RichTextBox();
            this.pctLogo = new System.Windows.Forms.PictureBox();
            this.lblTileIconifier = new System.Windows.Forms.Label();
            this.lblVersion = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pctLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // rtxtAbout
            // 
            this.rtxtAbout.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.rtxtAbout.Location = new System.Drawing.Point(0, 230);
            this.rtxtAbout.Name = "rtxtAbout";
            this.rtxtAbout.ReadOnly = true;
            this.rtxtAbout.Size = new System.Drawing.Size(608, 222);
            this.rtxtAbout.TabIndex = 1;
            this.rtxtAbout.Text = resources.GetString("rtxtAbout.Text");
            this.rtxtAbout.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.rtxtAbout_LinkClicked);
            // 
            // pctLogo
            // 
            this.pctLogo.Image = global::TileIconifier.Properties.Resources.tiles2_shadow;
            this.pctLogo.Location = new System.Drawing.Point(12, 12);
            this.pctLogo.Name = "pctLogo";
            this.pctLogo.Size = new System.Drawing.Size(199, 202);
            this.pctLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pctLogo.TabIndex = 2;
            this.pctLogo.TabStop = false;
            // 
            // lblTileIconifier
            // 
            this.lblTileIconifier.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTileIconifier.Location = new System.Drawing.Point(212, 67);
            this.lblTileIconifier.Name = "lblTileIconifier";
            this.lblTileIconifier.Size = new System.Drawing.Size(396, 67);
            this.lblTileIconifier.TabIndex = 3;
            this.lblTileIconifier.Text = "TileIconifier";
            this.lblTileIconifier.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblVersion
            // 
            this.lblVersion.Location = new System.Drawing.Point(212, 126);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(396, 16);
            this.lblVersion.TabIndex = 4;
            this.lblVersion.Text = "v[@@CURVER@@]";
            this.lblVersion.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // FrmAbout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(608, 452);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.lblTileIconifier);
            this.Controls.Add(this.pctLogo);
            this.Controls.Add(this.rtxtAbout);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmAbout";
            this.Text = "About TileIconifier";
            this.Load += new System.EventHandler(this.frmAbout_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pctLogo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtxtAbout;
        private System.Windows.Forms.PictureBox pctLogo;
        private System.Windows.Forms.Label lblTileIconifier;
        private System.Windows.Forms.Label lblVersion;
    }
}
namespace TileIconifier.Forms
{
    partial class FrmHelp
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmHelp));
            this.rtxtAbout = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // rtxtAbout
            // 
            this.rtxtAbout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtxtAbout.Location = new System.Drawing.Point(0, 0);
            this.rtxtAbout.Name = "rtxtAbout";
            this.rtxtAbout.ReadOnly = true;
            this.rtxtAbout.Size = new System.Drawing.Size(660, 327);
            this.rtxtAbout.TabIndex = 2;
            this.rtxtAbout.Text = resources.GetString("rtxtAbout.Text");
            this.rtxtAbout.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.rtxtAbout_LinkClicked);
            // 
            // FrmHelp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(660, 327);
            this.Controls.Add(this.rtxtAbout);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmHelp";
            this.Text = "Help";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtxtAbout;
    }
}
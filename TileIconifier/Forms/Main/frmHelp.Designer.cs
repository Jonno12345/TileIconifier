namespace TileIconifier.Forms.Main
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
            this.rtxtAbout = new TileIconifier.Controls.SkinnableRichTextBox();
            this.SuspendLayout();
            // 
            // rtxtAbout
            // 
            resources.ApplyResources(this.rtxtAbout, "rtxtAbout");
            this.rtxtAbout.Name = "rtxtAbout";
            this.rtxtAbout.ReadOnly = true;
            this.rtxtAbout.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.rtxtAbout_LinkClicked);
            // 
            // FrmHelp
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.rtxtAbout);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmHelp";
            this.ResumeLayout(false);

        }

        #endregion

        private TileIconifier.Controls.SkinnableRichTextBox rtxtAbout;
    }
}
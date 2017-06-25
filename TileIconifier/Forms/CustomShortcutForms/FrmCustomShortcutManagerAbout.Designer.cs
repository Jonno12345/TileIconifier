namespace TileIconifier.Forms.CustomShortcutForms
{
    partial class FrmCustomShortcutManagerHelp
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCustomShortcutManagerHelp));
            this.rtxtHelp = new TileIconifier.Controls.SkinnableRichTextBox();
            this.SuspendLayout();
            // 
            // rtxtHelp
            // 
            resources.ApplyResources(this.rtxtHelp, "rtxtHelp");
            this.rtxtHelp.Name = "rtxtHelp";
            this.rtxtHelp.ReadOnly = true;
            this.rtxtHelp.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.rtxtHelp_LinkClicked);
            // 
            // FrmCustomShortcutManagerHelp
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.rtxtHelp);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmCustomShortcutManagerHelp";
            this.Load += new System.EventHandler(this.frmHelp_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private TileIconifier.Controls.SkinnableRichTextBox rtxtHelp;
    }
}
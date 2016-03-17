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
            this.rtxtHelp = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // rtxtHelp
            // 
            this.rtxtHelp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtxtHelp.Location = new System.Drawing.Point(0, 0);
            this.rtxtHelp.Name = "rtxtHelp";
            this.rtxtHelp.ReadOnly = true;
            this.rtxtHelp.Size = new System.Drawing.Size(637, 238);
            this.rtxtHelp.TabIndex = 1;
            this.rtxtHelp.Text = resources.GetString("rtxtHelp.Text");
            this.rtxtHelp.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.rtxtHelp_LinkClicked);
            // 
            // frmCustomShortcutManagerHelp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(637, 238);
            this.Controls.Add(this.rtxtHelp);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmCustomShortcutManagerHelp";
            this.Text = "Custom Shortcut Manager - About";
            this.Load += new System.EventHandler(this.frmHelp_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtxtHelp;
    }
}
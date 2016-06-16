namespace TileIconifier.Forms.Shared
{
    partial class FrmException
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmException));
            this.rtxtUnhandledException = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // rtxtUnhandledException
            // 
            this.rtxtUnhandledException.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtxtUnhandledException.Location = new System.Drawing.Point(0, 0);
            this.rtxtUnhandledException.Name = "rtxtUnhandledException";
            this.rtxtUnhandledException.ReadOnly = true;
            this.rtxtUnhandledException.Size = new System.Drawing.Size(614, 452);
            this.rtxtUnhandledException.TabIndex = 1;
            this.rtxtUnhandledException.Text = resources.GetString("rtxtUnhandledException.Text");
            this.rtxtUnhandledException.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.rtxtException_LinkClicked);
            this.rtxtUnhandledException.MouseUp += new System.Windows.Forms.MouseEventHandler(this.rtxtUnhandledException_MouseUp);
            // 
            // FrmException
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(614, 452);
            this.Controls.Add(this.rtxtUnhandledException);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmException";
            this.Text = "Unhandled Exception!!!";
            this.Load += new System.EventHandler(this.FrmUnhandledExceptionLoad);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtxtUnhandledException;
    }
}
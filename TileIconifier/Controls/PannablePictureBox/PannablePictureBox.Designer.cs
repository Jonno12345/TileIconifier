namespace TileIconifier.Controls.PannablePictureBox
{
    partial class PannablePictureBox
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
            this.pctBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pctBox)).BeginInit();
            this.SuspendLayout();
            // 
            // pctBox
            // 
            this.pctBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pctBox.Location = new System.Drawing.Point(-1, -1);
            this.pctBox.Margin = new System.Windows.Forms.Padding(0);
            this.pctBox.Name = "pctBox";
            this.pctBox.Size = new System.Drawing.Size(152, 152);
            this.pctBox.TabIndex = 0;
            this.pctBox.TabStop = false;
            this.pctBox.Click += new System.EventHandler(this.pctBox_Click);
            this.pctBox.Paint += new System.Windows.Forms.PaintEventHandler(this.pctBox_Paint);
            this.pctBox.DoubleClick += new System.EventHandler(this.pctBox_DoubleClick);
            this.pctBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pctBox_MouseDown);
            this.pctBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pctBox_MouseMove);
            this.pctBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pctBox_MouseUp);
            // 
            // PannablePictureBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pctBox);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "PannablePictureBox";
            this.Load += new System.EventHandler(this.PannablePictureBox_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pctBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pctBox;
    }
}

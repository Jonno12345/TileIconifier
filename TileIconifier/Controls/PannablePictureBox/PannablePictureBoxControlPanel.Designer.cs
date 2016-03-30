namespace TileIconifier.Controls.PannablePictureBox
{
    partial class PannablePictureBoxControlPanel
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
            this.btnShrink = new System.Windows.Forms.Button();
            this.btnEnlarge = new System.Windows.Forms.Button();
            this.btnOpenImage = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.tmrEnlarge = new System.Windows.Forms.Timer(this.components);
            this.tmrShrink = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // btnShrink
            // 
            this.btnShrink.BackgroundImage = global::TileIconifier.Properties.Resources.ZoomOut_12927;
            this.btnShrink.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnShrink.Location = new System.Drawing.Point(3, 36);
            this.btnShrink.Name = "btnShrink";
            this.btnShrink.Size = new System.Drawing.Size(30, 30);
            this.btnShrink.TabIndex = 0;
            this.btnShrink.UseVisualStyleBackColor = true;
            this.btnShrink.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnShrink_MouseDown);
            this.btnShrink.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnShrink_MouseUp);
            // 
            // btnEnlarge
            // 
            this.btnEnlarge.BackgroundImage = global::TileIconifier.Properties.Resources.Zoom_5442;
            this.btnEnlarge.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnEnlarge.Location = new System.Drawing.Point(3, 0);
            this.btnEnlarge.Name = "btnEnlarge";
            this.btnEnlarge.Size = new System.Drawing.Size(30, 30);
            this.btnEnlarge.TabIndex = 1;
            this.btnEnlarge.UseVisualStyleBackColor = true;
            this.btnEnlarge.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnEnlarge_MouseDown);
            this.btnEnlarge.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnEnlarge_MouseUp);
            // 
            // btnOpenImage
            // 
            this.btnOpenImage.BackgroundImage = global::TileIconifier.Properties.Resources.OpenComparisonResult_9697;
            this.btnOpenImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnOpenImage.Location = new System.Drawing.Point(3, 108);
            this.btnOpenImage.Name = "btnOpenImage";
            this.btnOpenImage.Size = new System.Drawing.Size(30, 30);
            this.btnOpenImage.TabIndex = 2;
            this.btnOpenImage.UseVisualStyleBackColor = true;
            this.btnOpenImage.Click += new System.EventHandler(this.btnOpenImage_Click);
            // 
            // btnReset
            // 
            this.btnReset.BackgroundImage = global::TileIconifier.Properties.Resources.ZoomToFit;
            this.btnReset.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnReset.Location = new System.Drawing.Point(3, 72);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(30, 30);
            this.btnReset.TabIndex = 3;
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // tmrEnlarge
            // 
            this.tmrEnlarge.Interval = 50;
            this.tmrEnlarge.Tick += new System.EventHandler(this.tmrEnlarge_Tick);
            // 
            // tmrShrink
            // 
            this.tmrShrink.Interval = 50;
            this.tmrShrink.Tick += new System.EventHandler(this.tmrShrink_Tick);
            // 
            // PannablePictureBoxControlPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btnOpenImage);
            this.Controls.Add(this.btnEnlarge);
            this.Controls.Add(this.btnShrink);
            this.Name = "PannablePictureBoxControlPanel";
            this.Size = new System.Drawing.Size(37, 143);
            this.Load += new System.EventHandler(this.PannablePictureBoxControlPanel_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnShrink;
        private System.Windows.Forms.Button btnEnlarge;
        private System.Windows.Forms.Button btnOpenImage;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Timer tmrEnlarge;
        private System.Windows.Forms.Timer tmrShrink;
    }
}

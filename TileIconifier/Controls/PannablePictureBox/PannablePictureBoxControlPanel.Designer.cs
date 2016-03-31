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
            this.btnCenter = new System.Windows.Forms.Button();
            this.trkZoom = new System.Windows.Forms.TrackBar();
            this.lblPercent = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.trkZoom)).BeginInit();
            this.SuspendLayout();
            // 
            // btnShrink
            // 
            this.btnShrink.BackgroundImage = global::TileIconifier.Properties.Resources.ZoomOut_12927;
            this.btnShrink.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnShrink.Location = new System.Drawing.Point(106, 30);
            this.btnShrink.Name = "btnShrink";
            this.btnShrink.Size = new System.Drawing.Size(27, 28);
            this.btnShrink.TabIndex = 0;
            this.btnShrink.UseVisualStyleBackColor = true;
            this.btnShrink.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnShrink_MouseDown);
            this.btnShrink.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnShrink_MouseUp);
            // 
            // btnEnlarge
            // 
            this.btnEnlarge.BackgroundImage = global::TileIconifier.Properties.Resources.Zoom_5442;
            this.btnEnlarge.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnEnlarge.Location = new System.Drawing.Point(106, -1);
            this.btnEnlarge.Name = "btnEnlarge";
            this.btnEnlarge.Size = new System.Drawing.Size(27, 28);
            this.btnEnlarge.TabIndex = 1;
            this.btnEnlarge.UseVisualStyleBackColor = true;
            this.btnEnlarge.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnEnlarge_MouseDown);
            this.btnEnlarge.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnEnlarge_MouseUp);
            // 
            // btnOpenImage
            // 
            this.btnOpenImage.BackgroundImage = global::TileIconifier.Properties.Resources.OpenComparisonResult_9697;
            this.btnOpenImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnOpenImage.Location = new System.Drawing.Point(106, 123);
            this.btnOpenImage.Name = "btnOpenImage";
            this.btnOpenImage.Size = new System.Drawing.Size(27, 28);
            this.btnOpenImage.TabIndex = 2;
            this.btnOpenImage.UseVisualStyleBackColor = true;
            this.btnOpenImage.Click += new System.EventHandler(this.btnOpenImage_Click);
            // 
            // btnReset
            // 
            this.btnReset.BackgroundImage = global::TileIconifier.Properties.Resources.ZoomToFit;
            this.btnReset.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnReset.Location = new System.Drawing.Point(106, 61);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(27, 28);
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
            // btnCenter
            // 
            this.btnCenter.BackgroundImage = global::TileIconifier.Properties.Resources.Translate;
            this.btnCenter.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnCenter.Location = new System.Drawing.Point(106, 92);
            this.btnCenter.Name = "btnCenter";
            this.btnCenter.Size = new System.Drawing.Size(27, 28);
            this.btnCenter.TabIndex = 4;
            this.btnCenter.UseVisualStyleBackColor = true;
            this.btnCenter.Click += new System.EventHandler(this.btnCenter_Click);
            // 
            // trkZoom
            // 
            this.trkZoom.Location = new System.Drawing.Point(0, 106);
            this.trkZoom.Maximum = 100;
            this.trkZoom.Minimum = 1;
            this.trkZoom.Name = "trkZoom";
            this.trkZoom.Size = new System.Drawing.Size(100, 45);
            this.trkZoom.TabIndex = 5;
            this.trkZoom.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trkZoom.Value = 1;
            this.trkZoom.Scroll += new System.EventHandler(this.trkZoom_Scroll);
            // 
            // lblPercent
            // 
            this.lblPercent.AutoSize = true;
            this.lblPercent.Location = new System.Drawing.Point(36, 131);
            this.lblPercent.Name = "lblPercent";
            this.lblPercent.Size = new System.Drawing.Size(30, 13);
            this.lblPercent.TabIndex = 6;
            this.lblPercent.Text = "xxx%";
            this.lblPercent.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PannablePictureBoxControlPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblPercent);
            this.Controls.Add(this.trkZoom);
            this.Controls.Add(this.btnCenter);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btnOpenImage);
            this.Controls.Add(this.btnEnlarge);
            this.Controls.Add(this.btnShrink);
            this.Name = "PannablePictureBoxControlPanel";
            this.Size = new System.Drawing.Size(133, 161);
            this.Load += new System.EventHandler(this.PannablePictureBoxControlPanel_Load);
            ((System.ComponentModel.ISupportInitialize)(this.trkZoom)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnShrink;
        private System.Windows.Forms.Button btnEnlarge;
        private System.Windows.Forms.Button btnOpenImage;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Timer tmrEnlarge;
        private System.Windows.Forms.Timer tmrShrink;
        private System.Windows.Forms.Button btnCenter;
        private System.Windows.Forms.TrackBar trkZoom;
        private System.Windows.Forms.Label lblPercent;
    }
}

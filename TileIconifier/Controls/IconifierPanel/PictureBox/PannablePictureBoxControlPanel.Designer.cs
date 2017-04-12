namespace TileIconifier.Controls.IconifierPanel.PictureBox
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
            this.btnShrink = new TileIconifier.Controls.SkinnableButton();
            this.btnEnlarge = new TileIconifier.Controls.SkinnableButton();
            this.btnOpenImage = new TileIconifier.Controls.SkinnableButton();
            this.btnReset = new TileIconifier.Controls.SkinnableButton();
            this.tmrEnlarge = new System.Windows.Forms.Timer(this.components);
            this.tmrShrink = new System.Windows.Forms.Timer(this.components);
            this.btnAlign = new TileIconifier.Controls.SkinnableButton();
            this.trkZoom = new TileIconifier.Controls.SkinnableTrackBar();
            this.lblPercent = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.trkZoom)).BeginInit();
            this.SuspendLayout();
            // 
            // btnShrink
            // 
            this.btnShrink.BackgroundImage = global::TileIconifier.Properties.Resources.ZoomOut_12927;
            this.btnShrink.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnShrink.DisabledForeColor = System.Drawing.Color.Empty;
            this.btnShrink.Location = new System.Drawing.Point(106, 30);
            this.btnShrink.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
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
            this.btnEnlarge.DisabledForeColor = System.Drawing.Color.Empty;
            this.btnEnlarge.Location = new System.Drawing.Point(106, -1);
            this.btnEnlarge.Margin = new System.Windows.Forms.Padding(3, 0, 0, 3);
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
            this.btnOpenImage.DisabledForeColor = System.Drawing.Color.Empty;
            this.btnOpenImage.Location = new System.Drawing.Point(106, 123);
            this.btnOpenImage.Margin = new System.Windows.Forms.Padding(3, 3, 0, 0);
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
            this.btnReset.DisabledForeColor = System.Drawing.Color.Empty;
            this.btnReset.Location = new System.Drawing.Point(106, 61);
            this.btnReset.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
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
            // btnAlign
            // 
            this.btnAlign.BackgroundImage = global::TileIconifier.Properties.Resources.Translate;
            this.btnAlign.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnAlign.DisabledForeColor = System.Drawing.Color.Empty;
            this.btnAlign.Location = new System.Drawing.Point(106, 92);
            this.btnAlign.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.btnAlign.Name = "btnAlign";
            this.btnAlign.Size = new System.Drawing.Size(27, 28);
            this.btnAlign.TabIndex = 4;
            this.btnAlign.UseVisualStyleBackColor = true;
            this.btnAlign.Click += new System.EventHandler(this.btnAlign_Click);
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
            this.lblPercent.Location = new System.Drawing.Point(25, 131);
            this.lblPercent.Name = "lblPercent";
            this.lblPercent.Size = new System.Drawing.Size(50, 13);
            this.lblPercent.TabIndex = 6;
            this.lblPercent.Text = "xxx%";
            this.lblPercent.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PannablePictureBoxControlPanel
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.lblPercent);
            this.Controls.Add(this.trkZoom);
            this.Controls.Add(this.btnAlign);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btnOpenImage);
            this.Controls.Add(this.btnEnlarge);
            this.Controls.Add(this.btnShrink);
            this.Name = "PannablePictureBoxControlPanel";
            this.Size = new System.Drawing.Size(133, 154);
            this.Load += new System.EventHandler(this.PannablePictureBoxControlPanel_Load);
            ((System.ComponentModel.ISupportInitialize)(this.trkZoom)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TileIconifier.Controls.SkinnableButton btnShrink;
        private TileIconifier.Controls.SkinnableButton btnEnlarge;
        private TileIconifier.Controls.SkinnableButton btnOpenImage;
        private TileIconifier.Controls.SkinnableButton btnReset;
        private System.Windows.Forms.Timer tmrEnlarge;
        private System.Windows.Forms.Timer tmrShrink;
        private TileIconifier.Controls.SkinnableButton btnAlign;
        private TileIconifier.Controls.SkinnableTrackBar trkZoom;
        private System.Windows.Forms.Label lblPercent;
    }
}

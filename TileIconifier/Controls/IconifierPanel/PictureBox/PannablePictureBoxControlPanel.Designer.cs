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
            this.tmrEnlarge = new System.Windows.Forms.Timer(this.components);
            this.tmrShrink = new System.Windows.Forms.Timer(this.components);
            this.lblPercent = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.flpCommands = new System.Windows.Forms.FlowLayoutPanel();
            this.btnEnlarge = new TileIconifier.Controls.SkinnableButton();
            this.btnShrink = new TileIconifier.Controls.SkinnableButton();
            this.btnReset = new TileIconifier.Controls.SkinnableButton();
            this.btnAlign = new TileIconifier.Controls.SkinnableButton();
            this.btnOpenImage = new TileIconifier.Controls.SkinnableButton();
            this.pnlZoomTrack = new System.Windows.Forms.Panel();
            this.trkZoom = new TileIconifier.Controls.SkinnableTrackBar();
            this.panPct = new TileIconifier.Controls.IconifierPanel.PictureBox.PannablePictureBox();
            this.lblHeader = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.flpCommands.SuspendLayout();
            this.pnlZoomTrack.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trkZoom)).BeginInit();
            this.SuspendLayout();
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
            // lblPercent
            // 
            this.lblPercent.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblPercent.Location = new System.Drawing.Point(37, 26);
            this.lblPercent.Name = "lblPercent";
            this.lblPercent.Size = new System.Drawing.Size(50, 13);
            this.lblPercent.TabIndex = 6;
            this.lblPercent.Text = "xxx%";
            this.lblPercent.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.flpCommands, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.pnlZoomTrack, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.panPct, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblHeader, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(161, 177);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // flpCommands
            // 
            this.flpCommands.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.flpCommands.AutoSize = true;
            this.flpCommands.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flpCommands.Controls.Add(this.btnEnlarge);
            this.flpCommands.Controls.Add(this.btnShrink);
            this.flpCommands.Controls.Add(this.btnReset);
            this.flpCommands.Controls.Add(this.btnAlign);
            this.flpCommands.Controls.Add(this.btnOpenImage);
            this.flpCommands.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpCommands.Location = new System.Drawing.Point(128, 20);
            this.flpCommands.Name = "flpCommands";
            this.tableLayoutPanel1.SetRowSpan(this.flpCommands, 2);
            this.flpCommands.Size = new System.Drawing.Size(30, 150);
            this.flpCommands.TabIndex = 0;
            this.flpCommands.WrapContents = false;
            // 
            // btnEnlarge
            // 
            this.btnEnlarge.BackgroundImage = global::TileIconifier.Properties.Resources.Zoom_5442;
            this.btnEnlarge.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnEnlarge.DisabledForeColor = System.Drawing.Color.Empty;
            this.btnEnlarge.Location = new System.Drawing.Point(1, 1);
            this.btnEnlarge.Margin = new System.Windows.Forms.Padding(1);
            this.btnEnlarge.Name = "btnEnlarge";
            this.btnEnlarge.Size = new System.Drawing.Size(28, 28);
            this.btnEnlarge.TabIndex = 1;
            this.btnEnlarge.UseVisualStyleBackColor = true;
            this.btnEnlarge.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnEnlarge_MouseDown);
            this.btnEnlarge.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnEnlarge_MouseUp);
            // 
            // btnShrink
            // 
            this.btnShrink.BackgroundImage = global::TileIconifier.Properties.Resources.ZoomOut_12927;
            this.btnShrink.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnShrink.DisabledForeColor = System.Drawing.Color.Empty;
            this.btnShrink.Location = new System.Drawing.Point(1, 31);
            this.btnShrink.Margin = new System.Windows.Forms.Padding(1);
            this.btnShrink.Name = "btnShrink";
            this.btnShrink.Size = new System.Drawing.Size(28, 28);
            this.btnShrink.TabIndex = 0;
            this.btnShrink.UseVisualStyleBackColor = true;
            this.btnShrink.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnShrink_MouseDown);
            this.btnShrink.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnShrink_MouseUp);
            // 
            // btnReset
            // 
            this.btnReset.BackgroundImage = global::TileIconifier.Properties.Resources.ZoomToFit;
            this.btnReset.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnReset.DisabledForeColor = System.Drawing.Color.Empty;
            this.btnReset.Location = new System.Drawing.Point(1, 61);
            this.btnReset.Margin = new System.Windows.Forms.Padding(1);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(28, 28);
            this.btnReset.TabIndex = 3;
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnAlign
            // 
            this.btnAlign.BackgroundImage = global::TileIconifier.Properties.Resources.Translate;
            this.btnAlign.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnAlign.DisabledForeColor = System.Drawing.Color.Empty;
            this.btnAlign.Location = new System.Drawing.Point(1, 91);
            this.btnAlign.Margin = new System.Windows.Forms.Padding(1);
            this.btnAlign.Name = "btnAlign";
            this.btnAlign.Size = new System.Drawing.Size(28, 28);
            this.btnAlign.TabIndex = 4;
            this.btnAlign.UseVisualStyleBackColor = true;
            this.btnAlign.Click += new System.EventHandler(this.btnAlign_Click);
            // 
            // btnOpenImage
            // 
            this.btnOpenImage.BackgroundImage = global::TileIconifier.Properties.Resources.OpenComparisonResult_9697;
            this.btnOpenImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnOpenImage.DisabledForeColor = System.Drawing.Color.Empty;
            this.btnOpenImage.Location = new System.Drawing.Point(1, 121);
            this.btnOpenImage.Margin = new System.Windows.Forms.Padding(1);
            this.btnOpenImage.Name = "btnOpenImage";
            this.btnOpenImage.Size = new System.Drawing.Size(28, 28);
            this.btnOpenImage.TabIndex = 2;
            this.btnOpenImage.UseVisualStyleBackColor = true;
            this.btnOpenImage.Click += new System.EventHandler(this.btnOpenImage_Click);
            // 
            // pnlZoomTrack
            // 
            this.pnlZoomTrack.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlZoomTrack.AutoSize = true;
            this.pnlZoomTrack.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pnlZoomTrack.Controls.Add(this.lblPercent);
            this.pnlZoomTrack.Controls.Add(this.trkZoom);
            this.pnlZoomTrack.Location = new System.Drawing.Point(3, 129);
            this.pnlZoomTrack.Name = "pnlZoomTrack";
            this.pnlZoomTrack.Size = new System.Drawing.Size(119, 45);
            this.pnlZoomTrack.TabIndex = 1;
            // 
            // trkZoom
            // 
            this.trkZoom.Dock = System.Windows.Forms.DockStyle.Top;
            this.trkZoom.Location = new System.Drawing.Point(0, 0);
            this.trkZoom.Maximum = 100;
            this.trkZoom.Minimum = 1;
            this.trkZoom.Name = "trkZoom";
            this.trkZoom.Size = new System.Drawing.Size(119, 45);
            this.trkZoom.TabIndex = 5;
            this.trkZoom.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trkZoom.Value = 1;
            this.trkZoom.Scroll += new System.EventHandler(this.trkZoom_Scroll);
            // 
            // panPct
            // 
            this.panPct.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panPct.AssociatedSize = new System.Drawing.Size(0, 0);
            this.panPct.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panPct.Location = new System.Drawing.Point(37, 44);
            this.panPct.Margin = new System.Windows.Forms.Padding(0);
            this.panPct.Name = "panPct";
            this.panPct.Size = new System.Drawing.Size(50, 50);
            this.panPct.TabIndex = 2;
            // 
            // lblHeader
            // 
            this.lblHeader.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblHeader.AutoSize = true;
            this.lblHeader.Location = new System.Drawing.Point(31, 0);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(62, 13);
            this.lblHeader.TabIndex = 3;
            this.lblHeader.Text = "Header text";
            // 
            // PannablePictureBoxControlPanel
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "PannablePictureBoxControlPanel";
            this.Size = new System.Drawing.Size(161, 177);
            this.Load += new System.EventHandler(this.PannablePictureBoxControlPanel_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.flpCommands.ResumeLayout(false);
            this.pnlZoomTrack.ResumeLayout(false);
            this.pnlZoomTrack.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trkZoom)).EndInit();
            this.ResumeLayout(false);

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
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flpCommands;
        private System.Windows.Forms.Panel pnlZoomTrack;
        private PannablePictureBox panPct;
        private System.Windows.Forms.Label lblHeader;
    }
}

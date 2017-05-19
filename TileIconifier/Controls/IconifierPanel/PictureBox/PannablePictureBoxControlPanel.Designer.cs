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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PannablePictureBoxControlPanel));
            this.tmrEnlarge = new System.Windows.Forms.Timer(this.components);
            this.tmrShrink = new System.Windows.Forms.Timer(this.components);
            this.lblPercent = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.flpCommands = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlZoomTrack = new System.Windows.Forms.Panel();
            this.lblHeader = new System.Windows.Forms.Label();
            this.ttpCommands = new System.Windows.Forms.ToolTip(this.components);
            this.btnEnlarge = new TileIconifier.Controls.SkinnableButton();
            this.btnShrink = new TileIconifier.Controls.SkinnableButton();
            this.btnReset = new TileIconifier.Controls.SkinnableButton();
            this.btnAlign = new TileIconifier.Controls.SkinnableButton();
            this.btnOpenImage = new TileIconifier.Controls.SkinnableButton();
            this.trkZoom = new TileIconifier.Controls.SkinnableTrackBar();
            this.panPct = new TileIconifier.Controls.IconifierPanel.PictureBox.PannablePictureBox();
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
            resources.ApplyResources(this.lblPercent, "lblPercent");
            this.lblPercent.Name = "lblPercent";
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.flpCommands, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.pnlZoomTrack, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.panPct, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblHeader, 0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // flpCommands
            // 
            resources.ApplyResources(this.flpCommands, "flpCommands");
            this.flpCommands.Controls.Add(this.btnEnlarge);
            this.flpCommands.Controls.Add(this.btnShrink);
            this.flpCommands.Controls.Add(this.btnReset);
            this.flpCommands.Controls.Add(this.btnAlign);
            this.flpCommands.Controls.Add(this.btnOpenImage);
            this.flpCommands.Name = "flpCommands";
            this.tableLayoutPanel1.SetRowSpan(this.flpCommands, 2);
            // 
            // pnlZoomTrack
            // 
            resources.ApplyResources(this.pnlZoomTrack, "pnlZoomTrack");
            this.pnlZoomTrack.Controls.Add(this.lblPercent);
            this.pnlZoomTrack.Controls.Add(this.trkZoom);
            this.pnlZoomTrack.Name = "pnlZoomTrack";
            // 
            // lblHeader
            // 
            resources.ApplyResources(this.lblHeader, "lblHeader");
            this.lblHeader.Name = "lblHeader";
            // 
            // btnEnlarge
            // 
            this.btnEnlarge.BackgroundImage = global::TileIconifier.Properties.Resources.Zoom_5442;
            resources.ApplyResources(this.btnEnlarge, "btnEnlarge");
            this.btnEnlarge.DisabledForeColor = System.Drawing.Color.Empty;
            this.btnEnlarge.Name = "btnEnlarge";
            this.ttpCommands.SetToolTip(this.btnEnlarge, resources.GetString("btnEnlarge.ToolTip"));
            this.btnEnlarge.UseVisualStyleBackColor = true;
            this.btnEnlarge.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnEnlarge_MouseDown);
            this.btnEnlarge.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnEnlarge_MouseUp);
            // 
            // btnShrink
            // 
            this.btnShrink.BackgroundImage = global::TileIconifier.Properties.Resources.ZoomOut_12927;
            resources.ApplyResources(this.btnShrink, "btnShrink");
            this.btnShrink.DisabledForeColor = System.Drawing.Color.Empty;
            this.btnShrink.Name = "btnShrink";
            this.ttpCommands.SetToolTip(this.btnShrink, resources.GetString("btnShrink.ToolTip"));
            this.btnShrink.UseVisualStyleBackColor = true;
            this.btnShrink.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnShrink_MouseDown);
            this.btnShrink.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnShrink_MouseUp);
            // 
            // btnReset
            // 
            this.btnReset.BackgroundImage = global::TileIconifier.Properties.Resources.ZoomToFit;
            resources.ApplyResources(this.btnReset, "btnReset");
            this.btnReset.DisabledForeColor = System.Drawing.Color.Empty;
            this.btnReset.Name = "btnReset";
            this.ttpCommands.SetToolTip(this.btnReset, resources.GetString("btnReset.ToolTip"));
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnAlign
            // 
            this.btnAlign.BackgroundImage = global::TileIconifier.Properties.Resources.Translate;
            resources.ApplyResources(this.btnAlign, "btnAlign");
            this.btnAlign.DisabledForeColor = System.Drawing.Color.Empty;
            this.btnAlign.Name = "btnAlign";
            this.ttpCommands.SetToolTip(this.btnAlign, resources.GetString("btnAlign.ToolTip"));
            this.btnAlign.UseVisualStyleBackColor = true;
            this.btnAlign.Click += new System.EventHandler(this.btnAlign_Click);
            // 
            // btnOpenImage
            // 
            this.btnOpenImage.BackgroundImage = global::TileIconifier.Properties.Resources.OpenComparisonResult_9697;
            resources.ApplyResources(this.btnOpenImage, "btnOpenImage");
            this.btnOpenImage.DisabledForeColor = System.Drawing.Color.Empty;
            this.btnOpenImage.Name = "btnOpenImage";
            this.ttpCommands.SetToolTip(this.btnOpenImage, resources.GetString("btnOpenImage.ToolTip"));
            this.btnOpenImage.UseVisualStyleBackColor = true;
            this.btnOpenImage.Click += new System.EventHandler(this.btnOpenImage_Click);
            // 
            // trkZoom
            // 
            resources.ApplyResources(this.trkZoom, "trkZoom");
            this.trkZoom.Maximum = 100;
            this.trkZoom.Minimum = 1;
            this.trkZoom.Name = "trkZoom";
            this.trkZoom.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trkZoom.Value = 1;
            this.trkZoom.Scroll += new System.EventHandler(this.trkZoom_Scroll);
            // 
            // panPct
            // 
            resources.ApplyResources(this.panPct, "panPct");
            this.panPct.AssociatedSize = new System.Drawing.Size(0, 0);
            this.panPct.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panPct.Name = "panPct";
            // 
            // PannablePictureBoxControlPanel
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "PannablePictureBoxControlPanel";
            resources.ApplyResources(this, "$this");
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
        private System.Windows.Forms.ToolTip ttpCommands;
    }
}

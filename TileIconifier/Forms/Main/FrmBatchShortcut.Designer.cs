namespace TileIconifier.Forms.Main
{
    partial class FrmBatchShortcut
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmBatchShortcut));
            this.btnBatchAmendBackgroundColor = new TileIconifier.Controls.SkinnableButton();
            this.pctColorPreview = new System.Windows.Forms.PictureBox();
            this.btnSelectNone = new TileIconifier.Controls.SkinnableButton();
            this.btnSelectAll = new TileIconifier.Controls.SkinnableButton();
            this.colorPanel = new TileIconifier.Controls.IconifierPanel.ColorPanel();
            this.lstIconifiedItems = new TileIconifier.Controls.SortableListView();
            this.btnAmendForegroundColor = new TileIconifier.Controls.SkinnableButton();
            this.btnAmendForegroundText = new TileIconifier.Controls.SkinnableButton();
            this.ilsIconifiedItemsSmallIcons = new System.Windows.Forms.ImageList(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pctColorPreview)).BeginInit();
            this.SuspendLayout();
            // 
            // btnBatchAmendBackgroundColor
            // 
            this.btnBatchAmendBackgroundColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnBatchAmendBackgroundColor.Location = new System.Drawing.Point(12, 433);
            this.btnBatchAmendBackgroundColor.Name = "btnBatchAmendBackgroundColor";
            this.btnBatchAmendBackgroundColor.Size = new System.Drawing.Size(86, 49);
            this.btnBatchAmendBackgroundColor.TabIndex = 0;
            this.btnBatchAmendBackgroundColor.Text = "Amend Background Colors";
            this.btnBatchAmendBackgroundColor.UseVisualStyleBackColor = true;
            this.btnBatchAmendBackgroundColor.Click += new System.EventHandler(this.btnBatchAmendBackgroundColor_Click);
            // 
            // pctColorPreview
            // 
            this.pctColorPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pctColorPreview.Location = new System.Drawing.Point(12, 327);
            this.pctColorPreview.Name = "pctColorPreview";
            this.pctColorPreview.Size = new System.Drawing.Size(68, 61);
            this.pctColorPreview.TabIndex = 4;
            this.pctColorPreview.TabStop = false;
            // 
            // btnSelectNone
            // 
            this.btnSelectNone.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSelectNone.Location = new System.Drawing.Point(96, 293);
            this.btnSelectNone.Name = "btnSelectNone";
            this.btnSelectNone.Size = new System.Drawing.Size(78, 28);
            this.btnSelectNone.TabIndex = 2;
            this.btnSelectNone.Text = "Select None";
            this.btnSelectNone.UseVisualStyleBackColor = true;
            this.btnSelectNone.Click += new System.EventHandler(this.btnSelectNone_Click);
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSelectAll.Location = new System.Drawing.Point(12, 293);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(78, 28);
            this.btnSelectAll.TabIndex = 1;
            this.btnSelectAll.Text = "Select All";
            this.btnSelectAll.UseVisualStyleBackColor = true;
            this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click);
            // 
            // colorPanel
            // 
            this.colorPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.colorPanel.AutoSize = true;
            this.colorPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.colorPanel.Location = new System.Drawing.Point(92, 327);
            this.colorPanel.MaximumSize = new System.Drawing.Size(310, 111);
            this.colorPanel.Name = "colorPanel";
            this.colorPanel.Size = new System.Drawing.Size(268, 97);
            this.colorPanel.TabIndex = 3;
            this.colorPanel.ColorUpdate += new System.EventHandler(this.colorPanel_ColorUpdate);
            // 
            // lstIconifiedItems
            // 
            this.lstIconifiedItems.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstIconifiedItems.CheckBoxes = true;
            this.lstIconifiedItems.HideSelection = false;
            this.lstIconifiedItems.Location = new System.Drawing.Point(12, 12);
            this.lstIconifiedItems.Name = "lstIconifiedItems";
            this.lstIconifiedItems.Size = new System.Drawing.Size(348, 275);
            this.lstIconifiedItems.SmallImageList = this.ilsIconifiedItemsSmallIcons;
            this.lstIconifiedItems.TabIndex = 0;
            this.lstIconifiedItems.UseCompatibleStateImageBehavior = false;
            this.lstIconifiedItems.View = System.Windows.Forms.View.Details;
            this.lstIconifiedItems.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lstIconifiedItems_MouseClick);
            // 
            // btnAmendForegroundColor
            // 
            this.btnAmendForegroundColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAmendForegroundColor.Location = new System.Drawing.Point(274, 433);
            this.btnAmendForegroundColor.Name = "btnAmendForegroundColor";
            this.btnAmendForegroundColor.Size = new System.Drawing.Size(86, 49);
            this.btnAmendForegroundColor.TabIndex = 5;
            this.btnAmendForegroundColor.Text = "Amend Foreground Color";
            this.btnAmendForegroundColor.UseVisualStyleBackColor = true;
            this.btnAmendForegroundColor.Click += new System.EventHandler(this.btnAmendForegroundColor_Click);
            // 
            // btnAmendForegroundText
            // 
            this.btnAmendForegroundText.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnAmendForegroundText.Location = new System.Drawing.Point(143, 433);
            this.btnAmendForegroundText.Name = "btnAmendForegroundText";
            this.btnAmendForegroundText.Size = new System.Drawing.Size(86, 49);
            this.btnAmendForegroundText.TabIndex = 6;
            this.btnAmendForegroundText.Text = "Amend Foreground Text";
            this.btnAmendForegroundText.UseVisualStyleBackColor = true;
            this.btnAmendForegroundText.Click += new System.EventHandler(this.btnAmendForegroundText_Click);
            // 
            // ilsIconifiedItemsSmallIcons
            // 
            this.ilsIconifiedItemsSmallIcons.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.ilsIconifiedItemsSmallIcons.ImageSize = new System.Drawing.Size(16, 16);
            this.ilsIconifiedItemsSmallIcons.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // FrmBatchShortcut
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(372, 494);
            this.Controls.Add(this.btnAmendForegroundText);
            this.Controls.Add(this.btnAmendForegroundColor);
            this.Controls.Add(this.btnBatchAmendBackgroundColor);
            this.Controls.Add(this.pctColorPreview);
            this.Controls.Add(this.colorPanel);
            this.Controls.Add(this.btnSelectNone);
            this.Controls.Add(this.btnSelectAll);
            this.Controls.Add(this.lstIconifiedItems);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FrmBatchShortcut";
            this.Text = "Batch Shortcut Operations";
            this.Load += new System.EventHandler(this.FrmBatchShortcut_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pctColorPreview)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.SortableListView lstIconifiedItems;
        private TileIconifier.Controls.SkinnableButton btnSelectAll;
        private TileIconifier.Controls.SkinnableButton btnSelectNone;
        private Controls.IconifierPanel.ColorPanel colorPanel;
        private System.Windows.Forms.PictureBox pctColorPreview;
        private TileIconifier.Controls.SkinnableButton btnBatchAmendBackgroundColor;
        private TileIconifier.Controls.SkinnableButton btnAmendForegroundColor;
        private TileIconifier.Controls.SkinnableButton btnAmendForegroundText;
        private System.Windows.Forms.ImageList ilsIconifiedItemsSmallIcons;
    }
}
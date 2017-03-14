namespace TileIconifier.Forms.Shared
{
    partial class FrmIconSelector
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
            //if(ReturnedBitmap != null)
            //    ReturnedBitmap.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmIconSelector));
            this.opnFile = new System.Windows.Forms.OpenFileDialog();
            this.lblPreview = new System.Windows.Forms.Label();
            this.pctPreview = new System.Windows.Forms.PictureBox();
            this.lblCommonDlls = new System.Windows.Forms.Label();
            this.cmbCommonIconDlls = new TileIconifier.Controls.SkinnableComboBox();
            this.btnBrowseIconPath = new TileIconifier.Controls.SkinnableButton();
            this.txtPathToExtractFrom = new TileIconifier.Controls.SkinnableTextBox();
            this.btnCancel = new TileIconifier.Controls.SkinnableButton();
            this.btnOk = new TileIconifier.Controls.SkinnableButton();
            this.btnBrowseCustomImage = new TileIconifier.Controls.SkinnableButton();
            this.label1 = new System.Windows.Forms.Label();
            this.txtImagePath = new TileIconifier.Controls.SkinnableTextBox();
            this.radUseCustomImage = new TileIconifier.Controls.SkinnableRadioButton();
            this.radIconFromTarget = new TileIconifier.Controls.SkinnableRadioButton();
            this.lvwIcons = new TileIconifier.Controls.SkinnableListView();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.pctPreview)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // opnFile
            // 
            resources.ApplyResources(this.opnFile, "opnFile");
            // 
            // lblPreview
            // 
            resources.ApplyResources(this.lblPreview, "lblPreview");
            this.lblPreview.Name = "lblPreview";
            // 
            // pctPreview
            // 
            resources.ApplyResources(this.pctPreview, "pctPreview");
            this.pctPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pctPreview.Name = "pctPreview";
            this.pctPreview.TabStop = false;
            // 
            // lblCommonDlls
            // 
            resources.ApplyResources(this.lblCommonDlls, "lblCommonDlls");
            this.lblCommonDlls.Name = "lblCommonDlls";
            // 
            // cmbCommonIconDlls
            // 
            resources.ApplyResources(this.cmbCommonIconDlls, "cmbCommonIconDlls");
            this.cmbCommonIconDlls.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCommonIconDlls.FormattingEnabled = true;
            this.cmbCommonIconDlls.Name = "cmbCommonIconDlls";
            this.cmbCommonIconDlls.SelectedIndexChanged += new System.EventHandler(this.cmbCommonIconDlls_SelectedIndexChanged);
            // 
            // btnBrowseIconPath
            // 
            resources.ApplyResources(this.btnBrowseIconPath, "btnBrowseIconPath");
            this.btnBrowseIconPath.Name = "btnBrowseIconPath";
            this.btnBrowseIconPath.UseVisualStyleBackColor = true;
            this.btnBrowseIconPath.Click += new System.EventHandler(this.btnBrowseIconPath_Click);
            // 
            // txtPathToExtractFrom
            // 
            resources.ApplyResources(this.txtPathToExtractFrom, "txtPathToExtractFrom");
            this.txtPathToExtractFrom.Name = "txtPathToExtractFrom";
            this.txtPathToExtractFrom.ReadOnly = true;
            // 
            // btnCancel
            // 
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            resources.ApplyResources(this.btnOk, "btnOk");
            this.btnOk.Name = "btnOk";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnBrowseCustomImage
            // 
            resources.ApplyResources(this.btnBrowseCustomImage, "btnBrowseCustomImage");
            this.btnBrowseCustomImage.Name = "btnBrowseCustomImage";
            this.btnBrowseCustomImage.UseVisualStyleBackColor = true;
            this.btnBrowseCustomImage.Click += new System.EventHandler(this.btnBrowseCustomImage_Click);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // txtImagePath
            // 
            resources.ApplyResources(this.txtImagePath, "txtImagePath");
            this.tableLayoutPanel2.SetColumnSpan(this.txtImagePath, 2);
            this.txtImagePath.Name = "txtImagePath";
            this.txtImagePath.TextChanged += new System.EventHandler(this.txtImagePath_TextChanged);
            // 
            // radUseCustomImage
            // 
            resources.ApplyResources(this.radUseCustomImage, "radUseCustomImage");
            this.tableLayoutPanel1.SetColumnSpan(this.radUseCustomImage, 2);
            this.radUseCustomImage.Name = "radUseCustomImage";
            this.radUseCustomImage.TabStop = true;
            this.radUseCustomImage.UseVisualStyleBackColor = true;
            // 
            // radIconFromTarget
            // 
            resources.ApplyResources(this.radIconFromTarget, "radIconFromTarget");
            this.radIconFromTarget.Checked = true;
            this.radIconFromTarget.Name = "radIconFromTarget";
            this.radIconFromTarget.TabStop = true;
            this.radIconFromTarget.UseVisualStyleBackColor = true;
            // 
            // lvwIcons
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.lvwIcons, 3);
            resources.ApplyResources(this.lvwIcons, "lvwIcons");
            this.lvwIcons.DrawStandardItems = false;
            this.lvwIcons.MultiSelect = false;
            this.lvwIcons.Name = "lvwIcons";
            this.lvwIcons.TileSize = new System.Drawing.Size(50, 50);
            this.lvwIcons.UseCompatibleStateImageBehavior = false;
            this.lvwIcons.View = System.Windows.Forms.View.Tile;
            this.lvwIcons.DrawItem += new System.Windows.Forms.DrawListViewItemEventHandler(this.lvwIcons_DrawItem);
            this.lvwIcons.SelectedIndexChanged += new System.EventHandler(this.lvwIcons_SelectedIndexChanged);
            this.lvwIcons.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lvwIcons_MouseClick);
            this.lvwIcons.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvwIcons_MouseDoubleClick);
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.radIconFromTarget, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnCancel, 2, 5);
            this.tableLayoutPanel1.Controls.Add(this.txtPathToExtractFrom, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnBrowseIconPath, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.cmbCommonIconDlls, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblCommonDlls, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnBrowseCustomImage, 2, 4);
            this.tableLayoutPanel1.Controls.Add(this.lvwIcons, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.radUseCustomImage, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // tableLayoutPanel2
            // 
            resources.ApplyResources(this.tableLayoutPanel2, "tableLayoutPanel2");
            this.tableLayoutPanel1.SetColumnSpan(this.tableLayoutPanel2, 2);
            this.tableLayoutPanel2.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnOk, 2, 1);
            this.tableLayoutPanel2.Controls.Add(this.lblPreview, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.txtImagePath, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.pctPreview, 1, 1);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel1.SetRowSpan(this.tableLayoutPanel2, 2);
            // 
            // FrmIconSelector
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "FrmIconSelector";
            ((System.ComponentModel.ISupportInitialize)(this.pctPreview)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private TileIconifier.Controls.SkinnableListView lvwIcons;
        private TileIconifier.Controls.SkinnableRadioButton radIconFromTarget;
        private TileIconifier.Controls.SkinnableRadioButton radUseCustomImage;
        private TileIconifier.Controls.SkinnableTextBox txtImagePath;
        private System.Windows.Forms.Label label1;
        private TileIconifier.Controls.SkinnableButton btnBrowseCustomImage;
        private TileIconifier.Controls.SkinnableButton btnOk;
        private TileIconifier.Controls.SkinnableButton btnCancel;
        private System.Windows.Forms.OpenFileDialog opnFile;
        private TileIconifier.Controls.SkinnableTextBox txtPathToExtractFrom;
        private TileIconifier.Controls.SkinnableButton btnBrowseIconPath;
        private TileIconifier.Controls.SkinnableComboBox cmbCommonIconDlls;
        private System.Windows.Forms.Label lblCommonDlls;
        private System.Windows.Forms.Label lblPreview;
        private System.Windows.Forms.PictureBox pctPreview;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
    }
}
using TileIconifier.Controls;

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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lvwIcons = new TileIconifier.Controls.IconListView.IconListView();
            ((System.ComponentModel.ISupportInitialize)(this.pctPreview)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
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
            this.tableLayoutPanel1.SetColumnSpan(this.pctPreview, 2);
            this.pctPreview.Name = "pctPreview";
            this.pctPreview.TabStop = false;
            // 
            // lblCommonDlls
            // 
            resources.ApplyResources(this.lblCommonDlls, "lblCommonDlls");
            this.tableLayoutPanel1.SetColumnSpan(this.lblCommonDlls, 2);
            this.lblCommonDlls.Name = "lblCommonDlls";
            // 
            // cmbCommonIconDlls
            // 
            resources.ApplyResources(this.cmbCommonIconDlls, "cmbCommonIconDlls");
            this.tableLayoutPanel1.SetColumnSpan(this.cmbCommonIconDlls, 2);
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
            this.tableLayoutPanel1.SetColumnSpan(this.txtPathToExtractFrom, 2);
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
            this.tableLayoutPanel1.SetColumnSpan(this.txtImagePath, 3);
            this.txtImagePath.Name = "txtImagePath";
            this.txtImagePath.TextChanged += new System.EventHandler(this.txtImagePath_TextChanged);
            // 
            // radUseCustomImage
            // 
            resources.ApplyResources(this.radUseCustomImage, "radUseCustomImage");
            this.tableLayoutPanel1.SetColumnSpan(this.radUseCustomImage, 5);
            this.radUseCustomImage.Name = "radUseCustomImage";
            this.radUseCustomImage.TabStop = true;
            this.radUseCustomImage.UseVisualStyleBackColor = true;
            // 
            // radIconFromTarget
            // 
            resources.ApplyResources(this.radIconFromTarget, "radIconFromTarget");
            this.radIconFromTarget.Checked = true;
            this.tableLayoutPanel1.SetColumnSpan(this.radIconFromTarget, 2);
            this.radIconFromTarget.Name = "radIconFromTarget";
            this.radIconFromTarget.TabStop = true;
            this.radIconFromTarget.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.btnOk, 3, 5);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.pctPreview, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.lblPreview, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.radIconFromTarget, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtImagePath, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.btnCancel, 4, 5);
            this.tableLayoutPanel1.Controls.Add(this.txtPathToExtractFrom, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnBrowseIconPath, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.cmbCommonIconDlls, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblCommonDlls, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnBrowseCustomImage, 4, 4);
            this.tableLayoutPanel1.Controls.Add(this.radUseCustomImage, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.lvwIcons, 0, 2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // lvwIcons
            // 
            resources.ApplyResources(this.lvwIcons, "lvwIcons");
            this.tableLayoutPanel1.SetColumnSpan(this.lvwIcons, 5);
            this.lvwIcons.ItemSize = new System.Drawing.Size(48, 48);
            this.lvwIcons.Name = "lvwIcons";
            this.lvwIcons.SelectedIndexChanged += new System.EventHandler(this.lvwIcons_SelectedIndexChanged);
            this.lvwIcons.ItemActivate += new System.EventHandler(this.lvwIcons_ItemActivate);
            this.lvwIcons.Click += new System.EventHandler(this.lvwIcons_Click);
            // 
            // FrmIconSelector
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "FrmIconSelector";
            ((System.ComponentModel.ISupportInitialize)(this.pctPreview)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private TileIconifier.Controls.SkinnableRadioButton radIconFromTarget;
        private TileIconifier.Controls.SkinnableRadioButton radUseCustomImage;
        private SkinnableTextBox txtImagePath;
        private System.Windows.Forms.Label label1;
        private TileIconifier.Controls.SkinnableButton btnBrowseCustomImage;
        private TileIconifier.Controls.SkinnableButton btnOk;
        private TileIconifier.Controls.SkinnableButton btnCancel;
        private System.Windows.Forms.OpenFileDialog opnFile;
        private SkinnableTextBox txtPathToExtractFrom;
        private TileIconifier.Controls.SkinnableButton btnBrowseIconPath;
        private TileIconifier.Controls.SkinnableComboBox cmbCommonIconDlls;
        private System.Windows.Forms.Label lblCommonDlls;
        private System.Windows.Forms.Label lblPreview;
        private System.Windows.Forms.PictureBox pctPreview;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Controls.IconListView.IconListView lvwIcons;
    }
}
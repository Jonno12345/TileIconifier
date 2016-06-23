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
            this.cmbCommonIconDlls = new System.Windows.Forms.ComboBox();
            this.btnBrowseIconPath = new System.Windows.Forms.Button();
            this.txtPathToExtractFrom = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnBrowseCustomImage = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtImagePath = new System.Windows.Forms.TextBox();
            this.radUseCustomImage = new System.Windows.Forms.RadioButton();
            this.radIconFromTarget = new System.Windows.Forms.RadioButton();
            this.lvwIcons = new System.Windows.Forms.ListView();
            ((System.ComponentModel.ISupportInitialize)(this.pctPreview)).BeginInit();
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
            this.txtImagePath.Name = "txtImagePath";
            this.txtImagePath.TextChanged += new System.EventHandler(this.txtImagePath_TextChanged);
            // 
            // radUseCustomImage
            // 
            resources.ApplyResources(this.radUseCustomImage, "radUseCustomImage");
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
            resources.ApplyResources(this.lvwIcons, "lvwIcons");
            this.lvwIcons.BackColor = System.Drawing.SystemColors.Control;
            this.lvwIcons.MultiSelect = false;
            this.lvwIcons.Name = "lvwIcons";
            this.lvwIcons.OwnerDraw = true;
            this.lvwIcons.TileSize = new System.Drawing.Size(50, 50);
            this.lvwIcons.UseCompatibleStateImageBehavior = false;
            this.lvwIcons.View = System.Windows.Forms.View.Tile;
            this.lvwIcons.DrawItem += new System.Windows.Forms.DrawListViewItemEventHandler(this.lvwIcons_DrawItem);
            this.lvwIcons.SelectedIndexChanged += new System.EventHandler(this.lvwIcons_SelectedIndexChanged);
            this.lvwIcons.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lvwIcons_MouseClick);
            this.lvwIcons.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvwIcons_MouseDoubleClick);
            // 
            // FrmIconSelector
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.lblPreview);
            this.Controls.Add(this.pctPreview);
            this.Controls.Add(this.lblCommonDlls);
            this.Controls.Add(this.cmbCommonIconDlls);
            this.Controls.Add(this.btnBrowseIconPath);
            this.Controls.Add(this.txtPathToExtractFrom);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnBrowseCustomImage);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtImagePath);
            this.Controls.Add(this.radUseCustomImage);
            this.Controls.Add(this.radIconFromTarget);
            this.Controls.Add(this.lvwIcons);
            this.Name = "FrmIconSelector";
            ((System.ComponentModel.ISupportInitialize)(this.pctPreview)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lvwIcons;
        private System.Windows.Forms.RadioButton radIconFromTarget;
        private System.Windows.Forms.RadioButton radUseCustomImage;
        private System.Windows.Forms.TextBox txtImagePath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnBrowseCustomImage;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.OpenFileDialog opnFile;
        private System.Windows.Forms.TextBox txtPathToExtractFrom;
        private System.Windows.Forms.Button btnBrowseIconPath;
        private System.Windows.Forms.ComboBox cmbCommonIconDlls;
        private System.Windows.Forms.Label lblCommonDlls;
        private System.Windows.Forms.Label lblPreview;
        private System.Windows.Forms.PictureBox pctPreview;
    }
}
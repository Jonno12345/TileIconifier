namespace TileIconifier.Forms
{
    partial class frmIconSelector
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
            this.lvwIcons = new System.Windows.Forms.ListView();
            this.radIconFromExe = new System.Windows.Forms.RadioButton();
            this.radUseCustomImage = new System.Windows.Forms.RadioButton();
            this.txtImagePath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.opnImageFile = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // lvwIcons
            // 
            this.lvwIcons.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lvwIcons.Location = new System.Drawing.Point(13, 57);
            this.lvwIcons.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lvwIcons.MultiSelect = false;
            this.lvwIcons.Name = "lvwIcons";
            this.lvwIcons.OwnerDraw = true;
            this.lvwIcons.Size = new System.Drawing.Size(710, 193);
            this.lvwIcons.TabIndex = 3;
            this.lvwIcons.TileSize = new System.Drawing.Size(100, 100);
            this.lvwIcons.UseCompatibleStateImageBehavior = false;
            this.lvwIcons.View = System.Windows.Forms.View.Tile;
            this.lvwIcons.DrawItem += new System.Windows.Forms.DrawListViewItemEventHandler(this.lvwIcons_DrawItem);
            this.lvwIcons.SelectedIndexChanged += new System.EventHandler(this.lvwIcons_SelectedIndexChanged);
            this.lvwIcons.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lvwIcons_MouseClick);
            this.lvwIcons.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvwIcons_MouseDoubleClick);
            // 
            // radIconFromExe
            // 
            this.radIconFromExe.AutoSize = true;
            this.radIconFromExe.Checked = true;
            this.radIconFromExe.Location = new System.Drawing.Point(13, 21);
            this.radIconFromExe.Name = "radIconFromExe";
            this.radIconFromExe.Size = new System.Drawing.Size(171, 24);
            this.radIconFromExe.TabIndex = 4;
            this.radIconFromExe.TabStop = true;
            this.radIconFromExe.Text = "Use Icon from EXE";
            this.radIconFromExe.UseVisualStyleBackColor = true;
            // 
            // radUseCustomImage
            // 
            this.radUseCustomImage.AutoSize = true;
            this.radUseCustomImage.Location = new System.Drawing.Point(13, 279);
            this.radUseCustomImage.Name = "radUseCustomImage";
            this.radUseCustomImage.Size = new System.Drawing.Size(171, 24);
            this.radUseCustomImage.TabIndex = 5;
            this.radUseCustomImage.TabStop = true;
            this.radUseCustomImage.Text = "Use Custom Image";
            this.radUseCustomImage.UseVisualStyleBackColor = true;
            // 
            // txtImagePath
            // 
            this.txtImagePath.Location = new System.Drawing.Point(109, 315);
            this.txtImagePath.Name = "txtImagePath";
            this.txtImagePath.Size = new System.Drawing.Size(430, 26);
            this.txtImagePath.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 318);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 20);
            this.label1.TabIndex = 7;
            this.label1.Text = "Image Path";
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(570, 308);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(128, 41);
            this.btnBrowse.TabIndex = 8;
            this.btnBrowse.Text = "Browse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(241, 355);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(102, 36);
            this.btnOk.TabIndex = 9;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(404, 355);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(102, 36);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // opnImageFile
            // 
            this.opnImageFile.Filter = "Image Files (*.bmp, *.jpg, *.png, *.ico)|(*.bmp;*.jpg;*.png;*.ico";
            // 
            // frmIconSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(144F, 144F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(736, 403);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtImagePath);
            this.Controls.Add(this.radUseCustomImage);
            this.Controls.Add(this.radIconFromExe);
            this.Controls.Add(this.lvwIcons);
            this.Name = "frmIconSelector";
            this.Text = "IconSelector";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.IconSelector_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lvwIcons;
        private System.Windows.Forms.RadioButton radIconFromExe;
        private System.Windows.Forms.RadioButton radUseCustomImage;
        private System.Windows.Forms.TextBox txtImagePath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.OpenFileDialog opnImageFile;
    }
}
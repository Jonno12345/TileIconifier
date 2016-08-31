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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmBatchShortcut));
            this.lstIconifiedItems = new TileIconifier.Controls.SortableListView();
            this.btnSelectAll = new System.Windows.Forms.Button();
            this.btnSelectNone = new System.Windows.Forms.Button();
            this.colorPanel = new TileIconifier.Controls.IconifierPanel.ColorPanel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // lstIconifiedItems
            // 
            this.lstIconifiedItems.CheckBoxes = true;
            this.lstIconifiedItems.HideSelection = false;
            this.lstIconifiedItems.Location = new System.Drawing.Point(12, 12);
            this.lstIconifiedItems.Name = "lstIconifiedItems";
            this.lstIconifiedItems.Size = new System.Drawing.Size(432, 275);
            this.lstIconifiedItems.TabIndex = 0;
            this.lstIconifiedItems.UseCompatibleStateImageBehavior = false;
            this.lstIconifiedItems.View = System.Windows.Forms.View.Details;
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.Location = new System.Drawing.Point(12, 293);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(78, 28);
            this.btnSelectAll.TabIndex = 1;
            this.btnSelectAll.Text = "Select All";
            this.btnSelectAll.UseVisualStyleBackColor = true;
            this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click);
            // 
            // btnSelectNone
            // 
            this.btnSelectNone.Location = new System.Drawing.Point(96, 293);
            this.btnSelectNone.Name = "btnSelectNone";
            this.btnSelectNone.Size = new System.Drawing.Size(78, 28);
            this.btnSelectNone.TabIndex = 2;
            this.btnSelectNone.Text = "Select None";
            this.btnSelectNone.UseVisualStyleBackColor = true;
            this.btnSelectNone.Click += new System.EventHandler(this.btnSelectNone_Click);
            // 
            // colorPanel
            // 
            this.colorPanel.Location = new System.Drawing.Point(450, 79);
            this.colorPanel.MaximumSize = new System.Drawing.Size(310, 111);
            this.colorPanel.Name = "colorPanel";
            this.colorPanel.Size = new System.Drawing.Size(273, 107);
            this.colorPanel.TabIndex = 3;
            this.colorPanel.ColorUpdate += new System.EventHandler(this.colorPanel_ColorUpdate);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(450, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(68, 61);
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // FrmBatchShortcut
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(991, 380);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.colorPanel);
            this.Controls.Add(this.btnSelectNone);
            this.Controls.Add(this.btnSelectAll);
            this.Controls.Add(this.lstIconifiedItems);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmBatchShortcut";
            this.Text = "Batch Shortcut Operations";
            this.Load += new System.EventHandler(this.FrmBatchShortcut_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.SortableListView lstIconifiedItems;
        private System.Windows.Forms.Button btnSelectAll;
        private System.Windows.Forms.Button btnSelectNone;
        private Controls.IconifierPanel.ColorPanel colorPanel;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}
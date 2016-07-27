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
            // FrmBatchShortcut
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(580, 380);
            this.Controls.Add(this.btnSelectNone);
            this.Controls.Add(this.btnSelectAll);
            this.Controls.Add(this.lstIconifiedItems);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmBatchShortcut";
            this.Text = "FrmBatchShortcut";
            this.Load += new System.EventHandler(this.FrmBatchShortcut_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.SortableListView lstIconifiedItems;
        private System.Windows.Forms.Button btnSelectAll;
        private System.Windows.Forms.Button btnSelectNone;
    }
}
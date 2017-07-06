namespace TileIconifier.Skinning.Skins
{
    partial class Form1
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
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("ghjh");
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("yufu");
            this.skinnableComboBox1 = new TileIconifier.Controls.SkinnableComboBox();
            this.skinnableRichTextBox1 = new TileIconifier.Controls.SkinnableRichTextBox();
            this.skinnableCheckBox1 = new TileIconifier.Controls.SkinnableCheckBox();
            this.skinnableListView1 = new TileIconifier.Controls.SkinnableListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.skinnableTextBox1 = new TileIconifier.Controls.SkinnableTextBox();
            this.SuspendLayout();
            // 
            // skinnableComboBox1
            // 
            this.skinnableComboBox1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.skinnableComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.skinnableComboBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.skinnableComboBox1.FormattingEnabled = true;
            this.skinnableComboBox1.Items.AddRange(new object[] {
            "lol",
            "lol2",
            "lol3"});
            this.skinnableComboBox1.Location = new System.Drawing.Point(35, 24);
            this.skinnableComboBox1.Name = "skinnableComboBox1";
            this.skinnableComboBox1.Size = new System.Drawing.Size(121, 21);
            this.skinnableComboBox1.TabIndex = 4;
            // 
            // skinnableRichTextBox1
            // 
            this.skinnableRichTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.skinnableRichTextBox1.Location = new System.Drawing.Point(172, 5);
            this.skinnableRichTextBox1.Name = "skinnableRichTextBox1";
            this.skinnableRichTextBox1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.skinnableRichTextBox1.Size = new System.Drawing.Size(100, 96);
            this.skinnableRichTextBox1.TabIndex = 3;
            this.skinnableRichTextBox1.Text = "vcb\ncvbcvb\nvcbv\ncvb\ncvb";
            // 
            // skinnableCheckBox1
            // 
            this.skinnableCheckBox1.AutoSize = true;
            this.skinnableCheckBox1.Location = new System.Drawing.Point(209, 116);
            this.skinnableCheckBox1.Name = "skinnableCheckBox1";
            this.skinnableCheckBox1.Size = new System.Drawing.Size(126, 17);
            this.skinnableCheckBox1.TabIndex = 2;
            this.skinnableCheckBox1.Text = "skinnableCheckBox1";
            this.skinnableCheckBox1.UseVisualStyleBackColor = true;
            // 
            // skinnableListView1
            // 
            this.skinnableListView1.BorderFocusedColor = System.Drawing.Color.Red;
            this.skinnableListView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.skinnableListView1.FlatHeaderBackColor = System.Drawing.Color.PaleGoldenrod;
            this.skinnableListView1.FlatHeaderForeColor = System.Drawing.Color.CornflowerBlue;
            this.skinnableListView1.HeaderAppearance = TileIconifier.Controls.ListViewHeaderAppearance.Flat;
            this.skinnableListView1.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2});
            this.skinnableListView1.Location = new System.Drawing.Point(93, 160);
            this.skinnableListView1.Name = "skinnableListView1";
            this.skinnableListView1.Size = new System.Drawing.Size(132, 97);
            this.skinnableListView1.TabIndex = 1;
            this.skinnableListView1.UseCompatibleStateImageBehavior = false;
            this.skinnableListView1.UseExplorerStyle = false;
            this.skinnableListView1.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Width = 99;
            // 
            // skinnableTextBox1
            // 
            this.skinnableTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.skinnableTextBox1.BorderFocusedColor = System.Drawing.Color.Red;
            this.skinnableTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.skinnableTextBox1.Location = new System.Drawing.Point(56, 116);
            this.skinnableTextBox1.Name = "skinnableTextBox1";
            this.skinnableTextBox1.Size = new System.Drawing.Size(100, 20);
            this.skinnableTextBox1.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.skinnableComboBox1);
            this.Controls.Add(this.skinnableRichTextBox1);
            this.Controls.Add(this.skinnableCheckBox1);
            this.Controls.Add(this.skinnableListView1);
            this.Controls.Add(this.skinnableTextBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.SkinnableTextBox skinnableTextBox1;
        private Controls.SkinnableListView skinnableListView1;
        private Controls.SkinnableCheckBox skinnableCheckBox1;
        private Controls.SkinnableRichTextBox skinnableRichTextBox1;
        private Controls.SkinnableComboBox skinnableComboBox1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
    }
}
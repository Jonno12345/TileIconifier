namespace TileIconifier.Utilities
{
    partial class IconSelector
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
            this.SuspendLayout();
            // 
            // lvwIcons
            // 
            this.lvwIcons.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvwIcons.Location = new System.Drawing.Point(13, 14);
            this.lvwIcons.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lvwIcons.MultiSelect = false;
            this.lvwIcons.Name = "lvwIcons";
            this.lvwIcons.OwnerDraw = true;
            this.lvwIcons.Size = new System.Drawing.Size(710, 241);
            this.lvwIcons.TabIndex = 3;
            this.lvwIcons.TileSize = new System.Drawing.Size(132, 130);
            this.lvwIcons.UseCompatibleStateImageBehavior = false;
            this.lvwIcons.View = System.Windows.Forms.View.Tile;
            this.lvwIcons.DrawItem += new System.Windows.Forms.DrawListViewItemEventHandler(this.lvwIcons_DrawItem);
            this.lvwIcons.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvwIcons_MouseDoubleClick);
            // 
            // IconSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(736, 269);
            this.Controls.Add(this.lvwIcons);
            this.Name = "IconSelector";
            this.Text = "IconSelector";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.IconSelector_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvwIcons;
    }
}
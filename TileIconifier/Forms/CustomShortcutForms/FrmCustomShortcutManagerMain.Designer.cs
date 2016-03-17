namespace TileIconifier.Forms.CustomShortcutForms
{
    partial class FrmCustomShortcutManagerMain
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
            this.btnCreateNewShortcut = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnDeleteCustomShortcut = new System.Windows.Forms.Button();
            this.lstCustomShortcuts = new TileIconifier.Controls.SortableListView();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCreateNewShortcut
            // 
            this.btnCreateNewShortcut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCreateNewShortcut.Location = new System.Drawing.Point(482, 27);
            this.btnCreateNewShortcut.Name = "btnCreateNewShortcut";
            this.btnCreateNewShortcut.Size = new System.Drawing.Size(131, 38);
            this.btnCreateNewShortcut.TabIndex = 1;
            this.btnCreateNewShortcut.Text = "Create New Shortcut";
            this.btnCreateNewShortcut.UseVisualStyleBackColor = true;
            this.btnCreateNewShortcut.Click += new System.EventHandler(this.btnCreateNewShortcut_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(623, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // btnDeleteCustomShortcut
            // 
            this.btnDeleteCustomShortcut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeleteCustomShortcut.Location = new System.Drawing.Point(482, 71);
            this.btnDeleteCustomShortcut.Name = "btnDeleteCustomShortcut";
            this.btnDeleteCustomShortcut.Size = new System.Drawing.Size(131, 38);
            this.btnDeleteCustomShortcut.TabIndex = 3;
            this.btnDeleteCustomShortcut.Text = "Delete Custom Shortcut";
            this.btnDeleteCustomShortcut.UseVisualStyleBackColor = true;
            this.btnDeleteCustomShortcut.Click += new System.EventHandler(this.btnDeleteCustomShortcut_Click);
            // 
            // lstCustomShortcuts
            // 
            this.lstCustomShortcuts.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstCustomShortcuts.FullRowSelect = true;
            this.lstCustomShortcuts.Location = new System.Drawing.Point(12, 27);
            this.lstCustomShortcuts.Name = "lstCustomShortcuts";
            this.lstCustomShortcuts.Size = new System.Drawing.Size(464, 246);
            this.lstCustomShortcuts.TabIndex = 5;
            this.lstCustomShortcuts.UseCompatibleStateImageBehavior = false;
            this.lstCustomShortcuts.View = System.Windows.Forms.View.Details;
            // 
            // frmCustomShortcutManagerMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(623, 285);
            this.Controls.Add(this.lstCustomShortcuts);
            this.Controls.Add(this.btnDeleteCustomShortcut);
            this.Controls.Add(this.btnCreateNewShortcut);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(501, 159);
            this.Name = "FrmCustomShortcutManagerMain";
            this.Text = "Custom Shortcut Manager";
            this.Load += new System.EventHandler(this.frmCustomShortcutManagerMain_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnCreateNewShortcut;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.Button btnDeleteCustomShortcut;
        private Controls.SortableListView lstCustomShortcuts;
    }
}
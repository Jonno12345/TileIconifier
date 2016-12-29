using TileIconifier.Controls;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCustomShortcutManagerMain));
            this.btnCreateNewShortcut = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnDeleteCustomShortcut = new System.Windows.Forms.Button();
            this.lstCustomShortcuts = new TileIconifier.Controls.SortableListView();
            this.btnGotoShortcut = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCreateNewShortcut
            // 
            resources.ApplyResources(this.btnCreateNewShortcut, "btnCreateNewShortcut");
            this.btnCreateNewShortcut.Name = "btnCreateNewShortcut";
            this.btnCreateNewShortcut.UseVisualStyleBackColor = true;
            this.btnCreateNewShortcut.Click += new System.EventHandler(this.btnCreateNewShortcut_Click);
            // 
            // menuStrip1
            // 
            resources.ApplyResources(this.menuStrip1, "menuStrip1");
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.menuStrip1.Name = "menuStrip1";
            // 
            // aboutToolStripMenuItem
            // 
            resources.ApplyResources(this.aboutToolStripMenuItem, "aboutToolStripMenuItem");
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // btnDeleteCustomShortcut
            // 
            resources.ApplyResources(this.btnDeleteCustomShortcut, "btnDeleteCustomShortcut");
            this.btnDeleteCustomShortcut.Name = "btnDeleteCustomShortcut";
            this.btnDeleteCustomShortcut.UseVisualStyleBackColor = true;
            this.btnDeleteCustomShortcut.Click += new System.EventHandler(this.btnDeleteCustomShortcut_Click);
            // 
            // lstCustomShortcuts
            // 
            resources.ApplyResources(this.lstCustomShortcuts, "lstCustomShortcuts");
            this.lstCustomShortcuts.FullRowSelect = true;
            this.lstCustomShortcuts.HideSelection = false;
            this.lstCustomShortcuts.Name = "lstCustomShortcuts";
            this.lstCustomShortcuts.UseCompatibleStateImageBehavior = false;
            this.lstCustomShortcuts.View = System.Windows.Forms.View.Details;
            // 
            // btnGotoShortcut
            // 
            resources.ApplyResources(this.btnGotoShortcut, "btnGotoShortcut");
            this.btnGotoShortcut.Name = "btnGotoShortcut";
            this.btnGotoShortcut.UseVisualStyleBackColor = true;
            this.btnGotoShortcut.Click += new System.EventHandler(this.btnGotoShortcut_Click);
            // 
            // FrmCustomShortcutManagerMain
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.btnGotoShortcut);
            this.Controls.Add(this.lstCustomShortcuts);
            this.Controls.Add(this.btnDeleteCustomShortcut);
            this.Controls.Add(this.btnCreateNewShortcut);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FrmCustomShortcutManagerMain";
            this.Resize += new System.EventHandler(this.FrmCustomShortcutManagerMain_Resize);
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
        private SortableListView lstCustomShortcuts;
        private System.Windows.Forms.Button btnGotoShortcut;
    }
}
namespace SolutionBuilder.WinForms
{
    partial class FrmMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reloadFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openSourceFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.launchRefactoringToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.readMetxtToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TreeConfiguration = new System.Windows.Forms.TreeView();
            this.lightTheme = new System.Windows.Forms.ImageList(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.pnlHost = new System.Windows.Forms.Panel();
            this.ctlEmpty1 = new SolutionBuilder.WinForms.Panels.ctlEmpty();
            this.mnuInstallation = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.enableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.disableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.excludeEverythingExceptThisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.includeEverthingExceptThisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.mnuInstallation.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1219, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.toolStripMenuItem1,
            this.refreshToolStripMenuItem,
            this.reloadFileToolStripMenuItem,
            this.closeToolStripMenuItem,
            this.toolStripMenuItem2,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Image = global::SolutionBuilder.WinForms.Resource1.NewNamedSet;
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.newToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.newToolStripMenuItem.Text = "New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.NewToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Image = global::SolutionBuilder.WinForms.Resource1.OpenFolder;
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.openToolStripMenuItem.Text = "&Open...";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Image = global::SolutionBuilder.WinForms.Resource1.Save;
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.saveToolStripMenuItem.Text = "&Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Image = global::SolutionBuilder.WinForms.Resource1.SaveAs;
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.saveAsToolStripMenuItem.Text = "Save as...";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(175, 6);
            // 
            // refreshToolStripMenuItem
            // 
            this.refreshToolStripMenuItem.Image = global::SolutionBuilder.WinForms.Resource1.Refresh;
            this.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
            this.refreshToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.refreshToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.refreshToolStripMenuItem.Text = "Refresh";
            this.refreshToolStripMenuItem.Click += new System.EventHandler(this.RefreshToolStripMenuItem_Click);
            // 
            // reloadFileToolStripMenuItem
            // 
            this.reloadFileToolStripMenuItem.Image = global::SolutionBuilder.WinForms.Resource1.ReloadXML;
            this.reloadFileToolStripMenuItem.Name = "reloadFileToolStripMenuItem";
            this.reloadFileToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.reloadFileToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.reloadFileToolStripMenuItem.Text = "Reload file";
            this.reloadFileToolStripMenuItem.Click += new System.EventHandler(this.ReloadFileToolStripMenuItem_Click);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Image = global::SolutionBuilder.WinForms.Resource1.CloseDocument;
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.closeToolStripMenuItem.Text = "Close file";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.CloseToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(175, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Image = global::SolutionBuilder.WinForms.Resource1.Exit;
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openSourceFileToolStripMenuItem,
            this.launchRefactoringToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // openSourceFileToolStripMenuItem
            // 
            this.openSourceFileToolStripMenuItem.Name = "openSourceFileToolStripMenuItem";
            this.openSourceFileToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.openSourceFileToolStripMenuItem.Size = new System.Drawing.Size(222, 22);
            this.openSourceFileToolStripMenuItem.Text = "Open source file";
            this.openSourceFileToolStripMenuItem.Click += new System.EventHandler(this.OpenSourceFileToolStripMenuItem_Click);
            // 
            // launchRefactoringToolStripMenuItem
            // 
            this.launchRefactoringToolStripMenuItem.Name = "launchRefactoringToolStripMenuItem";
            this.launchRefactoringToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.L)));
            this.launchRefactoringToolStripMenuItem.Size = new System.Drawing.Size(222, 22);
            this.launchRefactoringToolStripMenuItem.Text = "Launch refactoring";
            this.launchRefactoringToolStripMenuItem.Click += new System.EventHandler(this.LaunchRefactoringToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.readMetxtToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(24, 20);
            this.helpToolStripMenuItem.Text = "?";
            this.helpToolStripMenuItem.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // readMetxtToolStripMenuItem
            // 
            this.readMetxtToolStripMenuItem.Name = "readMetxtToolStripMenuItem";
            this.readMetxtToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.readMetxtToolStripMenuItem.Text = "ReadMe.txt";
            this.readMetxtToolStripMenuItem.Click += new System.EventHandler(this.ReadMetxtToolStripMenuItem_Click);
            // 
            // TreeConfiguration
            // 
            this.TreeConfiguration.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TreeConfiguration.Dock = System.Windows.Forms.DockStyle.Left;
            this.TreeConfiguration.FullRowSelect = true;
            this.TreeConfiguration.HideSelection = false;
            this.TreeConfiguration.ImageIndex = 0;
            this.TreeConfiguration.ImageList = this.lightTheme;
            this.TreeConfiguration.Location = new System.Drawing.Point(0, 24);
            this.TreeConfiguration.Name = "TreeConfiguration";
            this.TreeConfiguration.SelectedImageIndex = 0;
            this.TreeConfiguration.Size = new System.Drawing.Size(432, 509);
            this.TreeConfiguration.TabIndex = 1;
            this.TreeConfiguration.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TreeConfiguration_AfterSelect);
            this.TreeConfiguration.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TreeConfiguration_MouseDown);
            // 
            // lightTheme
            // 
            this.lightTheme.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.lightTheme.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("lightTheme.ImageStream")));
            this.lightTheme.TransparentColor = System.Drawing.Color.Transparent;
            this.lightTheme.Images.SetKeyName(0, "Root");
            this.lightTheme.Images.SetKeyName(1, "ConfigurationEditor");
            this.lightTheme.Images.SetKeyName(2, "SolutionDisabled");
            this.lightTheme.Images.SetKeyName(3, "Solution");
            this.lightTheme.Images.SetKeyName(4, "ReplaceAll");
            this.lightTheme.Images.SetKeyName(5, "InsertClause");
            this.lightTheme.Images.SetKeyName(6, "BuildSolution");
            this.lightTheme.Images.SetKeyName(7, "CopyItem");
            this.lightTheme.Images.SetKeyName(8, "Action");
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.statusStrip1.Location = new System.Drawing.Point(0, 533);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1219, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // splitter1
            // 
            this.splitter1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.splitter1.Location = new System.Drawing.Point(432, 24);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(7, 509);
            this.splitter1.TabIndex = 3;
            this.splitter1.TabStop = false;
            // 
            // pnlHost
            // 
            this.pnlHost.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlHost.Location = new System.Drawing.Point(439, 24);
            this.pnlHost.Name = "pnlHost";
            this.pnlHost.Size = new System.Drawing.Size(780, 509);
            this.pnlHost.TabIndex = 4;
            // 
            // ctlEmpty1
            // 
            this.ctlEmpty1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctlEmpty1.Location = new System.Drawing.Point(439, 24);
            this.ctlEmpty1.Name = "ctlEmpty1";
            this.ctlEmpty1.Size = new System.Drawing.Size(780, 509);
            this.ctlEmpty1.TabIndex = 5;
            this.ctlEmpty1.OpenFileRequest += new System.EventHandler(this.CtlEmpty1_OpenFileRequest);
            // 
            // mnuInstallation
            // 
            this.mnuInstallation.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.enableToolStripMenuItem,
            this.disableToolStripMenuItem,
            this.toolStripSeparator1,
            this.excludeEverythingExceptThisToolStripMenuItem,
            this.includeEverthingExceptThisToolStripMenuItem});
            this.mnuInstallation.Name = "mnuInstallation";
            this.mnuInstallation.Size = new System.Drawing.Size(235, 120);
            // 
            // enableToolStripMenuItem
            // 
            this.enableToolStripMenuItem.Name = "enableToolStripMenuItem";
            this.enableToolStripMenuItem.Size = new System.Drawing.Size(234, 22);
            this.enableToolStripMenuItem.Text = "Enable";
            this.enableToolStripMenuItem.Click += new System.EventHandler(this.EnableToolStripMenuItem_Click);
            // 
            // disableToolStripMenuItem
            // 
            this.disableToolStripMenuItem.Name = "disableToolStripMenuItem";
            this.disableToolStripMenuItem.Size = new System.Drawing.Size(234, 22);
            this.disableToolStripMenuItem.Text = "Disable";
            this.disableToolStripMenuItem.Click += new System.EventHandler(this.DisableToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(231, 6);
            // 
            // excludeEverythingExceptThisToolStripMenuItem
            // 
            this.excludeEverythingExceptThisToolStripMenuItem.Name = "excludeEverythingExceptThisToolStripMenuItem";
            this.excludeEverythingExceptThisToolStripMenuItem.Size = new System.Drawing.Size(234, 22);
            this.excludeEverythingExceptThisToolStripMenuItem.Text = "Exclude everything except this";
            this.excludeEverythingExceptThisToolStripMenuItem.Click += new System.EventHandler(this.ExcludeEverythingExceptThisToolStripMenuItem_Click);
            // 
            // includeEverthingExceptThisToolStripMenuItem
            // 
            this.includeEverthingExceptThisToolStripMenuItem.Name = "includeEverthingExceptThisToolStripMenuItem";
            this.includeEverthingExceptThisToolStripMenuItem.Size = new System.Drawing.Size(234, 22);
            this.includeEverthingExceptThisToolStripMenuItem.Text = "Include everthing except this";
            this.includeEverthingExceptThisToolStripMenuItem.Click += new System.EventHandler(this.IncludeEverthingExceptThisToolStripMenuItem_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1219, 555);
            this.Controls.Add(this.ctlEmpty1);
            this.Controls.Add(this.pnlHost);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.TreeConfiguration);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.statusStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Advanced Solution Refactor";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.mnuInstallation.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem openToolStripMenuItem;
        private ToolStripMenuItem saveToolStripMenuItem;
        private ToolStripMenuItem helpToolStripMenuItem;
        private TreeView TreeConfiguration;
        private StatusStrip statusStrip1;
        private Splitter splitter1;
        private Panel pnlHost;
        private ImageList lightTheme;
        private ToolStripMenuItem saveAsToolStripMenuItem;
        private ToolStripSeparator toolStripMenuItem1;
        private ToolStripMenuItem closeToolStripMenuItem;
        private ToolStripSeparator toolStripMenuItem2;
        private ToolStripMenuItem exitToolStripMenuItem;
        private Panels.ctlEmpty ctlEmpty1;
        private ToolStripMenuItem refreshToolStripMenuItem;
        private ToolStripMenuItem reloadFileToolStripMenuItem;
        private ToolStripMenuItem newToolStripMenuItem;
        private ToolStripMenuItem toolsToolStripMenuItem;
        private ToolStripMenuItem openSourceFileToolStripMenuItem;
        private ToolStripMenuItem readMetxtToolStripMenuItem;
        private ToolStripMenuItem launchRefactoringToolStripMenuItem;
        private ContextMenuStrip mnuInstallation;
        private ToolStripMenuItem enableToolStripMenuItem;
        private ToolStripMenuItem disableToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem excludeEverythingExceptThisToolStripMenuItem;
        private ToolStripMenuItem includeEverthingExceptThisToolStripMenuItem;
    }
}

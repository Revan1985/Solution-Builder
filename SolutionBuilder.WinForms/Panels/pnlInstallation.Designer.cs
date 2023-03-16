namespace SolutionBuilder.WinForms.Panels
{
    partial class pnlInstallation
    {
        /// <summary> 
        /// Variabile di progettazione necessaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Pulire le risorse in uso.
        /// </summary>
        /// <param name="disposing">ha valore true se le risorse gestite devono essere eliminate, false in caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Codice generato da Progettazione componenti

        /// <summary> 
        /// Metodo necessario per il supporto della finestra di progettazione. Non modificare 
        /// il contenuto del metodo con l'editor di codice.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtSolutionFilename = new System.Windows.Forms.TextBox();
            this.txtSourcePath = new System.Windows.Forms.TextBox();
            this.cmdSolutionFilename = new System.Windows.Forms.Button();
            this.cmdSourcePath = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtOutputPath = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.chkEnabled = new System.Windows.Forms.CheckBox();
            this.chkClearDestination = new System.Windows.Forms.CheckBox();
            this.cmdOutputPath = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.lstCommands = new System.Windows.Forms.ListView();
            this.colName = new System.Windows.Forms.ColumnHeader();
            this.colOrder = new System.Windows.Forms.ColumnHeader();
            this.colType = new System.Windows.Forms.ColumnHeader();
            this.mnuCommands = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addCommandToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editCommandToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.bringUpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sendDownToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1.SuspendLayout();
            this.mnuCommands.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.txtName, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtSolutionFilename, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.txtSourcePath, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.cmdSolutionFilename, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.cmdSourcePath, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.txtOutputPath, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.label7, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.label8, 0, 7);
            this.tableLayoutPanel1.Controls.Add(this.chkEnabled, 1, 6);
            this.tableLayoutPanel1.Controls.Add(this.chkClearDestination, 1, 7);
            this.tableLayoutPanel1.Controls.Add(this.cmdOutputPath, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 8);
            this.tableLayoutPanel1.Controls.Add(this.lstCommands, 1, 8);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(10);
            this.tableLayoutPanel1.RowCount = 9;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(871, 378);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 15);
            this.label2.TabIndex = 0;
            this.label2.Text = "Solution filename";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 78);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 15);
            this.label3.TabIndex = 0;
            this.label3.Text = "Source path";
            // 
            // txtName
            // 
            this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.SetColumnSpan(this.txtName, 2);
            this.txtName.Location = new System.Drawing.Point(119, 13);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(739, 23);
            this.txtName.TabIndex = 1;
            this.txtName.TextChanged += new System.EventHandler(this.txtName_TextChanged);
            this.txtName.Validated += new System.EventHandler(this.txt_Validated);
            // 
            // txtSolutionFilename
            // 
            this.txtSolutionFilename.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSolutionFilename.Location = new System.Drawing.Point(119, 42);
            this.txtSolutionFilename.Name = "txtSolutionFilename";
            this.txtSolutionFilename.Size = new System.Drawing.Size(707, 23);
            this.txtSolutionFilename.TabIndex = 1;
            this.txtSolutionFilename.TextChanged += new System.EventHandler(this.txtName_TextChanged);
            this.txtSolutionFilename.Validated += new System.EventHandler(this.txt_Validated);
            // 
            // txtSourcePath
            // 
            this.txtSourcePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSourcePath.Location = new System.Drawing.Point(119, 73);
            this.txtSourcePath.Name = "txtSourcePath";
            this.txtSourcePath.Size = new System.Drawing.Size(707, 23);
            this.txtSourcePath.TabIndex = 1;
            this.txtSourcePath.TextChanged += new System.EventHandler(this.txtName_TextChanged);
            this.txtSourcePath.Validated += new System.EventHandler(this.txt_Validated);
            // 
            // cmdSolutionFilename
            // 
            this.cmdSolutionFilename.AutoSize = true;
            this.cmdSolutionFilename.Location = new System.Drawing.Point(832, 42);
            this.cmdSolutionFilename.Name = "cmdSolutionFilename";
            this.cmdSolutionFilename.Size = new System.Drawing.Size(26, 25);
            this.cmdSolutionFilename.TabIndex = 2;
            this.cmdSolutionFilename.Text = "...";
            this.cmdSolutionFilename.UseVisualStyleBackColor = true;
            this.cmdSolutionFilename.Click += new System.EventHandler(this.cmdSolutionFilename_Click);
            // 
            // cmdSourcePath
            // 
            this.cmdSourcePath.AutoSize = true;
            this.cmdSourcePath.Location = new System.Drawing.Point(832, 73);
            this.cmdSourcePath.Name = "cmdSourcePath";
            this.cmdSourcePath.Size = new System.Drawing.Size(26, 25);
            this.cmdSourcePath.TabIndex = 2;
            this.cmdSourcePath.Text = "...";
            this.cmdSourcePath.UseVisualStyleBackColor = true;
            this.cmdSourcePath.Click += new System.EventHandler(this.cmdSourcePath_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 108);
            this.label4.Margin = new System.Windows.Forms.Padding(3, 7, 3, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 15);
            this.label4.TabIndex = 0;
            this.label4.Text = "Output path";
            // 
            // txtOutputPath
            // 
            this.txtOutputPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOutputPath.Location = new System.Drawing.Point(119, 104);
            this.txtOutputPath.Name = "txtOutputPath";
            this.txtOutputPath.Size = new System.Drawing.Size(707, 23);
            this.txtOutputPath.TabIndex = 1;
            this.txtOutputPath.TextChanged += new System.EventHandler(this.txtName_TextChanged);
            this.txtOutputPath.Validated += new System.EventHandler(this.txt_Validated);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(13, 139);
            this.label7.Margin = new System.Windows.Forms.Padding(3, 7, 3, 6);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(49, 15);
            this.label7.TabIndex = 0;
            this.label7.Text = "Enabled";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(13, 167);
            this.label8.Margin = new System.Windows.Forms.Padding(3, 7, 3, 6);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(96, 15);
            this.label8.TabIndex = 0;
            this.label8.Text = "Clear destination";
            // 
            // chkEnabled
            // 
            this.chkEnabled.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.chkEnabled.AutoSize = true;
            this.chkEnabled.Location = new System.Drawing.Point(119, 139);
            this.chkEnabled.Name = "chkEnabled";
            this.chkEnabled.Size = new System.Drawing.Size(15, 14);
            this.chkEnabled.TabIndex = 3;
            this.chkEnabled.UseVisualStyleBackColor = true;
            this.chkEnabled.CheckedChanged += new System.EventHandler(this.chk_CheckedChanged);
            this.chkEnabled.Validated += new System.EventHandler(this.txt_Validated);
            // 
            // chkClearDestination
            // 
            this.chkClearDestination.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.chkClearDestination.AutoSize = true;
            this.chkClearDestination.Location = new System.Drawing.Point(119, 167);
            this.chkClearDestination.Name = "chkClearDestination";
            this.chkClearDestination.Size = new System.Drawing.Size(15, 14);
            this.chkClearDestination.TabIndex = 3;
            this.chkClearDestination.UseVisualStyleBackColor = true;
            this.chkClearDestination.CheckedChanged += new System.EventHandler(this.chk_CheckedChanged);
            this.chkClearDestination.Validated += new System.EventHandler(this.txt_Validated);
            // 
            // cmdOutputPath
            // 
            this.cmdOutputPath.AutoSize = true;
            this.cmdOutputPath.Location = new System.Drawing.Point(832, 104);
            this.cmdOutputPath.Name = "cmdOutputPath";
            this.cmdOutputPath.Size = new System.Drawing.Size(26, 25);
            this.cmdOutputPath.TabIndex = 2;
            this.cmdOutputPath.Text = "...";
            this.cmdOutputPath.UseVisualStyleBackColor = true;
            this.cmdOutputPath.Click += new System.EventHandler(this.cmdOutputPath_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 195);
            this.label5.Margin = new System.Windows.Forms.Padding(3, 7, 3, 6);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(69, 15);
            this.label5.TabIndex = 0;
            this.label5.Text = "Commands";
            // 
            // lstCommands
            // 
            this.lstCommands.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstCommands.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colName,
            this.colOrder,
            this.colType});
            this.tableLayoutPanel1.SetColumnSpan(this.lstCommands, 2);
            this.lstCommands.ContextMenuStrip = this.mnuCommands;
            this.lstCommands.FullRowSelect = true;
            this.lstCommands.Location = new System.Drawing.Point(119, 191);
            this.lstCommands.MultiSelect = false;
            this.lstCommands.Name = "lstCommands";
            this.lstCommands.Size = new System.Drawing.Size(739, 174);
            this.lstCommands.TabIndex = 4;
            this.lstCommands.UseCompatibleStateImageBehavior = false;
            this.lstCommands.View = System.Windows.Forms.View.Details;
            this.lstCommands.SelectedIndexChanged += new System.EventHandler(this.lstCommands_SelectedIndexChanged);
            this.lstCommands.DoubleClick += new System.EventHandler(this.lstCommands_DoubleClick);
            this.lstCommands.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lstCommands_KeyDown);
            // 
            // colName
            // 
            this.colName.Text = "Name";
            this.colName.Width = 200;
            // 
            // colOrder
            // 
            this.colOrder.Text = "#";
            this.colOrder.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // colType
            // 
            this.colType.Text = "Type";
            this.colType.Width = 200;
            // 
            // mnuCommands
            // 
            this.mnuCommands.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addCommandToolStripMenuItem,
            this.editCommandToolStripMenuItem,
            this.toolStripMenuItem1,
            this.removeToolStripMenuItem,
            this.toolStripMenuItem2,
            this.bringUpToolStripMenuItem,
            this.sendDownToolStripMenuItem});
            this.mnuCommands.Name = "mnuCommands";
            this.mnuCommands.Size = new System.Drawing.Size(208, 126);
            // 
            // addCommandToolStripMenuItem
            // 
            this.addCommandToolStripMenuItem.Image = global::SolutionBuilder.WinForms.Resource1.AddClause;
            this.addCommandToolStripMenuItem.Name = "addCommandToolStripMenuItem";
            this.addCommandToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
            this.addCommandToolStripMenuItem.Text = "Add...";
            this.addCommandToolStripMenuItem.Click += new System.EventHandler(this.addCommandToolStripMenuItem_Click);
            // 
            // editCommandToolStripMenuItem
            // 
            this.editCommandToolStripMenuItem.Enabled = false;
            this.editCommandToolStripMenuItem.Image = global::SolutionBuilder.WinForms.Resource1.EditInput;
            this.editCommandToolStripMenuItem.Name = "editCommandToolStripMenuItem";
            this.editCommandToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
            this.editCommandToolStripMenuItem.Text = "Edit";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(204, 6);
            // 
            // removeToolStripMenuItem
            // 
            this.removeToolStripMenuItem.Enabled = false;
            this.removeToolStripMenuItem.Image = global::SolutionBuilder.WinForms.Resource1.Remove;
            this.removeToolStripMenuItem.Name = "removeToolStripMenuItem";
            this.removeToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
            this.removeToolStripMenuItem.Text = "Remove";
            this.removeToolStripMenuItem.Click += new System.EventHandler(this.removeToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(204, 6);
            // 
            // bringUpToolStripMenuItem
            // 
            this.bringUpToolStripMenuItem.Enabled = false;
            this.bringUpToolStripMenuItem.Image = global::SolutionBuilder.WinForms.Resource1.Arrowhead_Top;
            this.bringUpToolStripMenuItem.Name = "bringUpToolStripMenuItem";
            this.bringUpToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Up)));
            this.bringUpToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
            this.bringUpToolStripMenuItem.Text = "Bring up";
            this.bringUpToolStripMenuItem.Click += new System.EventHandler(this.bringUpToolStripMenuItem_Click);
            // 
            // sendDownToolStripMenuItem
            // 
            this.sendDownToolStripMenuItem.Enabled = false;
            this.sendDownToolStripMenuItem.Image = global::SolutionBuilder.WinForms.Resource1.Arrowhead_Down;
            this.sendDownToolStripMenuItem.Name = "sendDownToolStripMenuItem";
            this.sendDownToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Down)));
            this.sendDownToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
            this.sendDownToolStripMenuItem.Text = "Send Down";
            this.sendDownToolStripMenuItem.Click += new System.EventHandler(this.sendDownToolStripMenuItem_Click);
            // 
            // pnlInstallation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "pnlInstallation";
            this.Size = new System.Drawing.Size(871, 378);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.mnuCommands.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private Label label1;
        private Label label2;
        private Label label3;
        private TextBox txtName;
        private TextBox txtSolutionFilename;
        private TextBox txtSourcePath;
        private Button cmdSolutionFilename;
        private Button cmdSourcePath;
        private Label label4;
        private TextBox txtOutputPath;
        private Label label7;
        private Label label8;
        private CheckBox chkEnabled;
        private CheckBox chkClearDestination;
        private Button cmdOutputPath;
        private Label label5;
        private ListView lstCommands;
        private ColumnHeader colName;
        private ContextMenuStrip mnuCommands;
        private ToolStripMenuItem addCommandToolStripMenuItem;
        private ToolStripMenuItem editCommandToolStripMenuItem;
        private ToolStripSeparator toolStripMenuItem1;
        private ToolStripMenuItem removeToolStripMenuItem;
        private ToolStripSeparator toolStripMenuItem2;
        private ToolStripMenuItem bringUpToolStripMenuItem;
        private ToolStripMenuItem sendDownToolStripMenuItem;
        private ColumnHeader colOrder;
        private ColumnHeader colType;
    }
}

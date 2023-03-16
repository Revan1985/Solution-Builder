namespace SolutionBuilder.WinForms.Panels
{
    partial class pnlRoot
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
            this.txtOutputPath = new System.Windows.Forms.TextBox();
            this.txtSourcePath = new System.Windows.Forms.TextBox();
            this.txtTemporaryPath = new System.Windows.Forms.TextBox();
            this.cmdOutput = new System.Windows.Forms.Button();
            this.cmdSource = new System.Windows.Forms.Button();
            this.cmdTemp = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.lstInstallations = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.mnuConfigurations = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addCommandToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editCommandToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1.SuspendLayout();
            this.mnuConfigurations.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.txtOutputPath, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtSourcePath, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.txtTemporaryPath, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.cmdOutput, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.cmdSource, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.cmdTemp, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.lstInstallations, 1, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(10);
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(861, 351);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Output path";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 15);
            this.label2.TabIndex = 0;
            this.label2.Text = "Source path";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 80);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 15);
            this.label3.TabIndex = 0;
            this.label3.Text = "Temporary path";
            // 
            // txtOutputPath
            // 
            this.txtOutputPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOutputPath.Location = new System.Drawing.Point(109, 13);
            this.txtOutputPath.Name = "txtOutputPath";
            this.txtOutputPath.Size = new System.Drawing.Size(707, 23);
            this.txtOutputPath.TabIndex = 1;
            this.txtOutputPath.TextChanged += new System.EventHandler(this.txtOutputPath_TextChanged);
            this.txtOutputPath.Validated += new System.EventHandler(this.txt_Validated);
            // 
            // txtSourcePath
            // 
            this.txtSourcePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSourcePath.Location = new System.Drawing.Point(109, 44);
            this.txtSourcePath.Name = "txtSourcePath";
            this.txtSourcePath.Size = new System.Drawing.Size(707, 23);
            this.txtSourcePath.TabIndex = 1;
            this.txtSourcePath.TextChanged += new System.EventHandler(this.txtOutputPath_TextChanged);
            this.txtSourcePath.Validated += new System.EventHandler(this.txt_Validated);
            // 
            // txtTemporaryPath
            // 
            this.txtTemporaryPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTemporaryPath.Location = new System.Drawing.Point(109, 75);
            this.txtTemporaryPath.Name = "txtTemporaryPath";
            this.txtTemporaryPath.Size = new System.Drawing.Size(707, 23);
            this.txtTemporaryPath.TabIndex = 1;
            this.txtTemporaryPath.TextChanged += new System.EventHandler(this.txtOutputPath_TextChanged);
            this.txtTemporaryPath.Validated += new System.EventHandler(this.txt_Validated);
            // 
            // cmdOutput
            // 
            this.cmdOutput.AutoSize = true;
            this.cmdOutput.Location = new System.Drawing.Point(822, 13);
            this.cmdOutput.Name = "cmdOutput";
            this.cmdOutput.Size = new System.Drawing.Size(26, 25);
            this.cmdOutput.TabIndex = 2;
            this.cmdOutput.Text = "...";
            this.cmdOutput.UseVisualStyleBackColor = true;
            this.cmdOutput.Click += new System.EventHandler(this.cmdOutput_Click);
            // 
            // cmdSource
            // 
            this.cmdSource.AutoSize = true;
            this.cmdSource.Location = new System.Drawing.Point(822, 44);
            this.cmdSource.Name = "cmdSource";
            this.cmdSource.Size = new System.Drawing.Size(26, 25);
            this.cmdSource.TabIndex = 2;
            this.cmdSource.Text = "...";
            this.cmdSource.UseVisualStyleBackColor = true;
            this.cmdSource.Click += new System.EventHandler(this.cmdOutput_Click);
            // 
            // cmdTemp
            // 
            this.cmdTemp.AutoSize = true;
            this.cmdTemp.Location = new System.Drawing.Point(822, 75);
            this.cmdTemp.Name = "cmdTemp";
            this.cmdTemp.Size = new System.Drawing.Size(26, 25);
            this.cmdTemp.TabIndex = 2;
            this.cmdTemp.Text = "...";
            this.cmdTemp.UseVisualStyleBackColor = true;
            this.cmdTemp.Click += new System.EventHandler(this.cmdOutput_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 110);
            this.label4.Margin = new System.Windows.Forms.Padding(3, 7, 3, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(86, 15);
            this.label4.TabIndex = 0;
            this.label4.Text = "Configurations";
            // 
            // lstInstallations
            // 
            this.lstInstallations.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.tableLayoutPanel1.SetColumnSpan(this.lstInstallations, 2);
            this.lstInstallations.ContextMenuStrip = this.mnuConfigurations;
            this.lstInstallations.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstInstallations.FullRowSelect = true;
            this.lstInstallations.Location = new System.Drawing.Point(109, 106);
            this.lstInstallations.Name = "lstInstallations";
            this.lstInstallations.Size = new System.Drawing.Size(739, 232);
            this.lstInstallations.TabIndex = 3;
            this.lstInstallations.UseCompatibleStateImageBehavior = false;
            this.lstInstallations.View = System.Windows.Forms.View.Details;
            this.lstInstallations.SelectedIndexChanged += new System.EventHandler(this.lstInstallations_SelectedIndexChanged);
            this.lstInstallations.DoubleClick += new System.EventHandler(this.lstInstallations_DoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Name";
            this.columnHeader1.Width = 200;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Solution";
            this.columnHeader2.Width = 200;
            // 
            // mnuConfigurations
            // 
            this.mnuConfigurations.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addCommandToolStripMenuItem,
            this.editCommandToolStripMenuItem,
            this.toolStripMenuItem1,
            this.removeToolStripMenuItem});
            this.mnuConfigurations.Name = "mnuCommands";
            this.mnuConfigurations.Size = new System.Drawing.Size(118, 76);
            // 
            // addCommandToolStripMenuItem
            // 
            this.addCommandToolStripMenuItem.Image = global::SolutionBuilder.WinForms.Resource1.AddClause;
            this.addCommandToolStripMenuItem.Name = "addCommandToolStripMenuItem";
            this.addCommandToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.addCommandToolStripMenuItem.Text = "Add...";
            this.addCommandToolStripMenuItem.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // editCommandToolStripMenuItem
            // 
            this.editCommandToolStripMenuItem.Enabled = false;
            this.editCommandToolStripMenuItem.Image = global::SolutionBuilder.WinForms.Resource1.EditInput;
            this.editCommandToolStripMenuItem.Name = "editCommandToolStripMenuItem";
            this.editCommandToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.editCommandToolStripMenuItem.Text = "Edit";
            this.editCommandToolStripMenuItem.Click += new System.EventHandler(this.editCommandToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(114, 6);
            // 
            // removeToolStripMenuItem
            // 
            this.removeToolStripMenuItem.Enabled = false;
            this.removeToolStripMenuItem.Image = global::SolutionBuilder.WinForms.Resource1.Remove;
            this.removeToolStripMenuItem.Name = "removeToolStripMenuItem";
            this.removeToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.removeToolStripMenuItem.Text = "Remove";
            this.removeToolStripMenuItem.Click += new System.EventHandler(this.removeToolStripMenuItem_Click);
            // 
            // pnlRoot
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "pnlRoot";
            this.Size = new System.Drawing.Size(861, 351);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.mnuConfigurations.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private Label label1;
        private Label label2;
        private Label label3;
        private TextBox txtOutputPath;
        private TextBox txtSourcePath;
        private TextBox txtTemporaryPath;
        private Button cmdOutput;
        private Button cmdSource;
        private Button cmdTemp;
        private Label label4;
        private ListView lstInstallations;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private ContextMenuStrip mnuConfigurations;
        private ToolStripMenuItem addCommandToolStripMenuItem;
        private ToolStripMenuItem editCommandToolStripMenuItem;
        private ToolStripSeparator toolStripMenuItem1;
        private ToolStripMenuItem removeToolStripMenuItem;
    }
}

namespace SolutionBuilder.WinForms.Panels
{
    partial class pnlCommand
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
            this.txtName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lstActions = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.mnuActions = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuActionAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuActionEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuActionRemove = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuActionUp = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuActionDown = new System.Windows.Forms.ToolStripMenuItem();
            this.cmbType = new System.Windows.Forms.ComboBox();
            this.lstProjects = new System.Windows.Forms.ListView();
            this.colName = new System.Windows.Forms.ColumnHeader();
            this.mnuProjects = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addCommandToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1.SuspendLayout();
            this.mnuActions.SuspendLayout();
            this.mnuProjects.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.txtName, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.lstActions, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.cmbType, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.lstProjects, 1, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(10);
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 87F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(743, 423);
            this.tableLayoutPanel1.TabIndex = 2;
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
            this.label2.Location = new System.Drawing.Point(13, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 15);
            this.label2.TabIndex = 0;
            this.label2.Text = "Type";
            // 
            // txtName
            // 
            this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtName.Location = new System.Drawing.Point(101, 13);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(629, 23);
            this.txtName.TabIndex = 1;
            this.txtName.TextChanged += new System.EventHandler(this.txtName_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 75);
            this.label5.Margin = new System.Windows.Forms.Padding(3, 7, 3, 6);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(82, 15);
            this.label5.TabIndex = 0;
            this.label5.Text = "Project names";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 317);
            this.label3.Margin = new System.Windows.Forms.Padding(3, 7, 3, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 15);
            this.label3.TabIndex = 0;
            this.label3.Text = "Actions";
            // 
            // lstActions
            // 
            this.lstActions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstActions.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.lstActions.ContextMenuStrip = this.mnuActions;
            this.lstActions.FullRowSelect = true;
            this.lstActions.Location = new System.Drawing.Point(101, 313);
            this.lstActions.Name = "lstActions";
            this.lstActions.Size = new System.Drawing.Size(629, 97);
            this.lstActions.TabIndex = 5;
            this.lstActions.UseCompatibleStateImageBehavior = false;
            this.lstActions.View = System.Windows.Forms.View.Details;
            this.lstActions.SelectedIndexChanged += new System.EventHandler(this.lstActions_SelectedIndexChanged);
            this.lstActions.DoubleClick += new System.EventHandler(this.lstActions_DoubleClick);
            this.lstActions.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lstActions_KeyDown);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Name";
            this.columnHeader1.Width = 200;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Description";
            this.columnHeader2.Width = 400;
            // 
            // mnuActions
            // 
            this.mnuActions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuActionAdd,
            this.mnuActionEdit,
            this.toolStripSeparator1,
            this.mnuActionRemove,
            this.toolStripSeparator2,
            this.mnuActionUp,
            this.mnuActionDown});
            this.mnuActions.Name = "mnuCommands";
            this.mnuActions.Size = new System.Drawing.Size(208, 148);
            // 
            // mnuActionAdd
            // 
            this.mnuActionAdd.Image = global::SolutionBuilder.WinForms.Resource1.AddClause;
            this.mnuActionAdd.Name = "mnuActionAdd";
            this.mnuActionAdd.Size = new System.Drawing.Size(207, 22);
            this.mnuActionAdd.Text = "Add...";
            this.mnuActionAdd.Click += new System.EventHandler(this.mnuActionAdd_Click);
            // 
            // mnuActionEdit
            // 
            this.mnuActionEdit.Enabled = false;
            this.mnuActionEdit.Image = global::SolutionBuilder.WinForms.Resource1.EditInput;
            this.mnuActionEdit.Name = "mnuActionEdit";
            this.mnuActionEdit.Size = new System.Drawing.Size(207, 22);
            this.mnuActionEdit.Text = "Edit";
            this.mnuActionEdit.Click += new System.EventHandler(this.mnuActionEdit_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(204, 6);
            // 
            // mnuActionRemove
            // 
            this.mnuActionRemove.Enabled = false;
            this.mnuActionRemove.Image = global::SolutionBuilder.WinForms.Resource1.Remove;
            this.mnuActionRemove.Name = "mnuActionRemove";
            this.mnuActionRemove.Size = new System.Drawing.Size(207, 22);
            this.mnuActionRemove.Text = "Remove";
            this.mnuActionRemove.Click += new System.EventHandler(this.mnuActionRemove_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(204, 6);
            // 
            // mnuActionUp
            // 
            this.mnuActionUp.Enabled = false;
            this.mnuActionUp.Image = global::SolutionBuilder.WinForms.Resource1.Arrowhead_Top;
            this.mnuActionUp.Name = "mnuActionUp";
            this.mnuActionUp.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Up)));
            this.mnuActionUp.Size = new System.Drawing.Size(207, 22);
            this.mnuActionUp.Text = "Bring up";
            this.mnuActionUp.Click += new System.EventHandler(this.mnuActionUp_Click);
            // 
            // mnuActionDown
            // 
            this.mnuActionDown.Enabled = false;
            this.mnuActionDown.Image = global::SolutionBuilder.WinForms.Resource1.Arrowhead_Down;
            this.mnuActionDown.Name = "mnuActionDown";
            this.mnuActionDown.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Down)));
            this.mnuActionDown.Size = new System.Drawing.Size(207, 22);
            this.mnuActionDown.Text = "Send Down";
            this.mnuActionDown.Click += new System.EventHandler(this.mnuActionDown_Click);
            // 
            // cmbType
            // 
            this.cmbType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbType.Enabled = false;
            this.cmbType.FormattingEnabled = true;
            this.cmbType.Items.AddRange(new object[] {
            "Replace",
            "Replace-CsProj",
            "Replace-Properties",
            "Replace-References",
            "Insert-References",
            "Copy",
            "Build"});
            this.cmbType.Location = new System.Drawing.Point(101, 42);
            this.cmbType.Name = "cmbType";
            this.cmbType.Size = new System.Drawing.Size(629, 23);
            this.cmbType.TabIndex = 6;
            // 
            // lstProjects
            // 
            this.lstProjects.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstProjects.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colName});
            this.lstProjects.ContextMenuStrip = this.mnuProjects;
            this.lstProjects.FullRowSelect = true;
            this.lstProjects.Location = new System.Drawing.Point(101, 71);
            this.lstProjects.Name = "lstProjects";
            this.lstProjects.Size = new System.Drawing.Size(629, 236);
            this.lstProjects.TabIndex = 4;
            this.lstProjects.UseCompatibleStateImageBehavior = false;
            this.lstProjects.View = System.Windows.Forms.View.Details;
            this.lstProjects.SelectedIndexChanged += new System.EventHandler(this.lstProjects_SelectedIndexChanged);
            this.lstProjects.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lstProjects_KeyDown);
            this.lstProjects.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.lstProjects_PreviewKeyDown);
            // 
            // colName
            // 
            this.colName.Text = "Name";
            this.colName.Width = 500;
            // 
            // mnuProjects
            // 
            this.mnuProjects.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addCommandToolStripMenuItem,
            this.toolStripMenuItem1,
            this.removeToolStripMenuItem});
            this.mnuProjects.Name = "mnuCommands";
            this.mnuProjects.Size = new System.Drawing.Size(118, 54);
            // 
            // addCommandToolStripMenuItem
            // 
            this.addCommandToolStripMenuItem.Image = global::SolutionBuilder.WinForms.Resource1.AddClause;
            this.addCommandToolStripMenuItem.Name = "addCommandToolStripMenuItem";
            this.addCommandToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.addCommandToolStripMenuItem.Text = "Add...";
            this.addCommandToolStripMenuItem.Click += new System.EventHandler(this.addCommandToolStripMenuItem_Click);
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
            // pnlCommand
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "pnlCommand";
            this.Size = new System.Drawing.Size(743, 423);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.mnuActions.ResumeLayout(false);
            this.mnuProjects.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private Label label1;
        private Label label2;
        private TextBox txtName;
        private Label label5;
        private Label label3;
        private ListView lstCommands;
        private ColumnHeader colName;
        private ListView listView1;
        private ColumnHeader columnHeader1;
        private ComboBox cmbType;
        private ListView lstActions;
        private ListView lstProjects;
        private ContextMenuStrip mnuProjects;
        private ToolStripMenuItem addCommandToolStripMenuItem;
        private ToolStripSeparator toolStripMenuItem1;
        private ToolStripMenuItem removeToolStripMenuItem;
        private ContextMenuStrip mnuActions;
        private ToolStripMenuItem mnuActionAdd;
        private ToolStripMenuItem mnuActionEdit;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem mnuActionRemove;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripMenuItem mnuActionUp;
        private ToolStripMenuItem mnuActionDown;
        private ColumnHeader columnHeader2;
    }
}

namespace SolutionBuilder.WinForms.Panels
{
    partial class frmSolutionExplorer
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSolutionExplorer));
            this.treeSolution = new System.Windows.Forms.TreeView();
            this.lightTheme = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // treeSolution
            // 
            this.treeSolution.BackColor = System.Drawing.SystemColors.Control;
            this.treeSolution.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeSolution.ImageIndex = 0;
            this.treeSolution.ImageList = this.lightTheme;
            this.treeSolution.Location = new System.Drawing.Point(0, 0);
            this.treeSolution.Name = "treeSolution";
            this.treeSolution.SelectedImageIndex = 0;
            this.treeSolution.Size = new System.Drawing.Size(446, 501);
            this.treeSolution.TabIndex = 0;
            this.treeSolution.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseDoubleClick);
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
            this.lightTheme.Images.SetKeyName(9, "FolderClosed");
            this.lightTheme.Images.SetKeyName(10, "CSProjectNode");
            // 
            // frmSolutionExplorer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(446, 501);
            this.Controls.Add(this.treeSolution);
            this.KeyPreview = true;
            this.Name = "frmSolutionExplorer";
            this.Text = "Solution explorer";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmSolutionExplorer_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private TreeView treeSolution;
        private ImageList lightTheme;
    }
}
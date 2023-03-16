using Microsoft.Build.Construction;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SolutionBuilder.WinForms.Panels
{
    public delegate void ProjectSelectedEventHandler(object sender, ProjectInSolution project);

    public partial class frmSolutionExplorer : Form
    {
        public event ProjectSelectedEventHandler? ProjectSelected;
        private List<string> _selectedProjects = new List<string>();

        public frmSolutionExplorer()
        {
            InitializeComponent();
        }

        public void SetSelectedProjects(List<string> projects)
        {
            _selectedProjects = projects;
            MarkSelectedItems();
        }

        private void MarkSelectedItems()
        {
            foreach (var node in _prjNodes)
            {
                if (node.Tag is ProjectInSolution prj)
                {
                    if (_selectedProjects.Contains(prj.ProjectName))
                    {
                        node.NodeFont = new Font(treeSolution.Font, FontStyle.Strikeout);
                        node.ForeColor = Color.Gray;
                    }
                }
            }
        }

        private List<TreeNode> _prjNodes = new List<TreeNode>();

        public void SetSolutionFilePath(string solutionFilePath)
        {
            _prjNodes = new List<TreeNode>();

            if (!File.Exists(solutionFilePath))
                return;

            SolutionFile sln = SolutionFile.Parse(solutionFilePath);
            var projectInSolution = sln.ProjectsInOrder.OrderBy(o => o.ProjectName);

            List<ProjectInSolution> solutionFolders = new();
            List<ProjectInSolution> projects = new();

            foreach (var item in projectInSolution)
            {
                System.Diagnostics.Debug.WriteLine(item.ProjectName);

                if (item.ProjectType == SolutionProjectType.SolutionFolder)
                    solutionFolders.Add(item);

                if (item.ProjectType == SolutionProjectType.KnownToBeMSBuildFormat)
                    projects.Add(item);
            }



            TreeNode nRoot = new TreeNode
            {
                Text = $"Solution '{Path.GetFileNameWithoutExtension(solutionFilePath)}'",
                ImageKey = "Solution",
                SelectedImageKey = "Solution"
            };

            TreeNode nFolder;
            treeSolution.CheckBoxes = false;
            treeSolution.Nodes.Clear();
            treeSolution.Nodes.Add(nRoot);

            Dictionary<string, TreeNode> nodeFolders = new Dictionary<string, TreeNode>();

            foreach (var solutionFolder in solutionFolders)
            {
                nFolder = new TreeNode
                {
                    Text = solutionFolder.ProjectName,
                    ImageKey = "FolderClosed",
                    SelectedImageKey = "FolderClosed",
                };
                nRoot.Nodes.Add(nFolder);

                nodeFolders.Add(solutionFolder.RelativePath, nFolder);
            }

            TreeNode nProj;

            string? path = null;

            foreach (var proj in projects)
            {
                nProj = new TreeNode
                {
                    Text = proj.ProjectName,
                    ImageKey = "CSProjectNode",
                    SelectedImageKey = "CSProjectNode",
                    Tag = proj
                };

                if (proj != null && proj.RelativePath != null)
                {
                    path = Path.GetDirectoryName(proj.RelativePath);

                    if (path != null)
                    {
                        string? parent = Directory.GetParent(path)?.Name;
                        if (parent != null)
                        {
                            if (nodeFolders.ContainsKey(parent))
                                nodeFolders[parent].Nodes.Add(nProj);
                            else
                                nRoot.Nodes.Add(nProj);
                        }

                        _prjNodes.Add(nProj);

                    }
                }
            }

            MarkSelectedItems();

            nRoot.ExpandAll();

        }

        private void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node != null && e.Node.Tag is ProjectInSolution proj)
            {
                if (_selectedProjects.Contains(proj.ProjectName))
                    return;

                _selectedProjects.Add(proj.ProjectName);
                ProjectSelected?.Invoke(this, proj);
                MarkSelectedItems();
            }
        }

        private void frmSolutionExplorer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
            else if (e.KeyCode == Keys.Enter)
            {
                if (treeSolution.SelectedNode is not null && treeSolution.SelectedNode.Tag is ProjectInSolution)
                {
                    treeView1_NodeMouseDoubleClick(treeSolution, new(treeSolution.SelectedNode, MouseButtons.Left, 1, 0, 0));
                }
            }
        }
    }
}

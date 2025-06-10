
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Linq;
using System.Collections.Generic;

using SolutionBuilder.Model;
using SolutionBuilder.WinForms.Panels;
using System.Diagnostics;
using System.Text;
using System.ComponentModel;

namespace SolutionBuilder.WinForms
{
    public enum NodeType
    {
        Root,
        Installation,
        Command,
        Action
    }

    

    public partial class FrmMain : Form
    {
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public static string AppName { get; set; } = "Rebrandizer";

        private const string nodeActionPrefix = nameof(NodeAction);

        private bool _fileIsChanged;
       
        private string? _currentFilename = null;


        private static SolutionConfiguration? _configuration = null;
        
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public static SolutionConfiguration? Configuration {
            get => _configuration;
            set => _configuration = value;
        }


        public FrmMain()
        {
            InitializeComponent();

            ctlEmpty1.Dock = DockStyle.Fill;
            ctlEmpty1.BringToFront();

            FormClosed += FrmMain_FormClosed;

            var lastFilename = Properties.Settings.Default.LastFileOpened;

            if (lastFilename!=null && lastFilename !="")
            {
                OpenFile(lastFilename);
            }

            WriteTitle();

            TreeConfiguration.HideSelection = false;
            TreeConfiguration.FullRowSelect = true;
        }

        private void FrmMain_FormClosed(object? sender, FormClosedEventArgs e)
        {
            Properties.Settings.Default.LastFileOpened = _currentFilename??"";
            Properties.Settings.Default.Save();
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool FileIsChanged
        {
            get => _fileIsChanged;
            set
            {
                _fileIsChanged = value;
                WriteTitle();
            }
        }

        private void WriteTitle()
        {
            if (_currentFilename == null)
            {
                Text = AppName;
            }
            else
            {
                StringBuilder sb = new(AppName);
                sb.Append(" - ");
                sb.Append(_currentFilename);
                //Text = $"{AppName} - {_currentFilename}";
                if (_fileIsChanged) 
                {
                    sb.Append('*');
                    //Text += "*"; 
                }
                Text = sb.ToString();
            }
        }

        private void LaunchOpenFile()
        {
            OpenFileDialog openFileDialog = new()
            {
                Filter = "Configuration (json)|*.json"
            };
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filename = openFileDialog.FileName;
                OpenFile(filename);
            }
        }

        private void ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sender is ToolStripMenuItem menu)
            {
                if (menu.Name == helpToolStripMenuItem.Name) { }
                if (menu.Name == openToolStripMenuItem.Name)
                {
                    LaunchOpenFile();
                }
                if (menu.Name == saveToolStripMenuItem.Name)
                {
                    if (_currentFilename != null)
                        Save(_currentFilename);
                    else
                        SaveAs();
                }
                if (menu.Name == saveAsToolStripMenuItem.Name)
                {
                    SaveAs();
                }
            }
        }

        private void OpenFile(string filename)
        {
            ctlEmpty1.Hide();
            _currentFilename = filename;
            Text = filename;
            using StreamReader reader = new(filename);
            string json = reader.ReadToEnd();
            if (!Model.SolutionConfiguration.TryParse(json, out _configuration))
            {
                MessageBox.Show("Cannot load configuration, please check provided json file is for Solution Builder", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                DrawTree();
            }
        }

        private void SaveAs()
        {
            SaveFileDialog sfd = new()
            {
                Filter = "Configuration (json)|*.json"
            };
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                Save(sfd.FileName);
            }
        }

        private void Save(string path)
        {
            _currentFilename = path;

            //SaveFileDialog sfd = new();
            //sfd.Filter = "Configuration (json)|*.json";
            //if (sfd.ShowDialog() == DialogResult.OK)
            //{
                if (pnlHost.Controls.Count > 0)
                {
                    //richiedo la validazione
                    if (pnlHost.Controls[0] is IManualValidator pnl)
                    {
                        pnl.ManualValidate();
                    }
                }

                //using FileStream createStream = File.Create(sfd.FileName);
                string json = Newtonsoft.Json.JsonConvert.SerializeObject(_configuration, Newtonsoft.Json.Formatting.Indented);

                //createStream.Position = 0;
                File.WriteAllText(path, json);

                FileIsChanged = false;

                //MessageBox.Show("File saved", "Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
        }

        private void DrawTree()
        {
            TreeConfiguration.Nodes.Clear();
            if (_configuration != null)
            {
                //root
                NodeRoot root = new()
                {
                    ImageKey = "Root",
                    SelectedImageKey = "Root",
                    Configuration = _configuration
                };
                TreeConfiguration.Nodes.Add(root);

                //Installations (configuration)
                //NodeInstallation nInstallation;
                
                foreach (var inst in _configuration.Installations)
                {
                    AddInstallationNode(root, inst);
                }

                root.Expand();
                TreeConfiguration.SelectedNode = root;
            }


        }

        private static void AddCommandNodes(NodeInstallation nInstallation)
        {
            if (nInstallation.Installation != null && nInstallation.Installation.Commands != null)
            {
                foreach (var cmd in nInstallation.Installation.Commands.OrderBy (o=>o.Order))
                {
                    AddCommandNode(nInstallation, cmd);
                }
            }
        }

        public static void AddCommandNode(NodeInstallation nInstallation, Command cmd)
        {
            NodeCommand nCommand;
            //NodeAction nAction;

            nCommand = new NodeCommand(cmd);
            nInstallation.Nodes.Add(nCommand);
            nCommand.SetData(cmd);

            if (cmd.Actions != null)
            {
                foreach (var action in cmd.Actions.OrderBy(o=>o.Value.Order))
                {
                    //nAction = new NodeAction();
                    //nCommand.Nodes.Add(nAction);
                    //nAction.Name = $"{nodeActionPrefix}@{action.Key}";
                    //nAction.Key = action.Key;
                    //nAction.Data = action.Value;
                    AddActionNode(nCommand, action.Key, action.Value);
                }
            }
        }

        private static void AddActionNode(NodeCommand nCommand, string key, CommandAction action)
        {
            NodeAction nAction = new();
            nCommand.Nodes.Add(nAction);
            nAction.Name = $"{nodeActionPrefix}@{key}";
            nAction.Key = key;
            nAction.Data = action;
        }

        private void TreeConfiguration_AfterSelect(object sender, TreeViewEventArgs e)
        {
            ShowPanel();
        }

        private void ShowPanel()
        {
            ctlEmpty1.Hide();

            pnlHost.Controls.Clear();

            if (TreeConfiguration.SelectedNode is Node node)
            {
                switch (node.NodeType)
                {
                    case NodeType.Root when node is NodeRoot root:
                        ShowPanelRoot(root.Configuration);
                        break;
                    case NodeType.Installation when node is NodeInstallation installation:
                        ShowPanelInstallation(installation.Installation);
                        break;
                    case NodeType.Command when (node is NodeCommand command && node.Parent is NodeInstallation nInstallation):
                        ShowPanelCommand(command.Data, nInstallation.Installation);
                        break;
                    case  NodeType.Action when node is NodeAction action:
                        ShowPanelAction(action.Key, action.Data);
                        break;
                }
            }
        }

        private void ShowPanelAction(string? key, CommandAction? action)
        {
            var pnl = new pnlAction();
            pnl.ActionChanged += Pnl_ActionChanged;
            pnlHost.Controls.Add(pnl);
            pnl.Dock = DockStyle.Fill;
            pnl.SetData(key, action);
        }

        private void Pnl_ActionChanged(object sender,string key, CommandAction action)
        {
            if (TreeConfiguration.SelectedNode is NodeAction nAction)
            {
                if (nAction.Parent is NodeCommand nCommand)
                {
                    Command cmd = nCommand.Data;
                    cmd.Actions[key] = action;
                }

                nAction.Data = action;
                FileIsChanged = true;

            }
            
        }

        private void ShowPanelCommand (Command? command, Installation? installation)
        {
            var pnl = new pnlCommand();
            pnl.CommandChanged += PnlCommand_CommandChanged;
            pnl.ActionRemoved += Pnl_ActionRemoved;
            pnl.ActionEditRequest += Pnl_ActionEditRequest;
            pnl.ActionAdded += Pnl_ActionAdded;
            pnlHost.Controls.Add(pnl);
            pnl.Dock = DockStyle.Fill;
            pnl.SetData(installation, command);
        }

        private void Pnl_ActionAdded(object sender, string key, CommandAction action)
        {
            if (TreeConfiguration.SelectedNode is NodeCommand nCommand)
            {
                AddActionNode(nCommand, key, action);
            }
        }

        private void Pnl_ActionEditRequest(object sender, string key, CommandAction action)
        {
            if (TreeConfiguration.SelectedNode is NodeCommand nCommand)
            {
                if (nCommand.Nodes.ContainsKey(key))
                {
                    TreeConfiguration.SelectedNode = nCommand.Nodes[key];
                }
            }
        }

        private void Pnl_ActionRemoved(object sender, string key, CommandAction action)
        {
            if (TreeConfiguration.SelectedNode is NodeCommand nCommand)
            {
                if (nCommand.Nodes.ContainsKey(key))
                {
                    nCommand.Nodes.Remove(nCommand.Nodes[key]!);
                }
            }
            FileIsChanged = true;
        }

        private void PnlCommand_CommandChanged(object sender, Command cmd)
        {
            //dal pannello command posso avere cambiato nome o tipo

            if (TreeConfiguration.SelectedNode is NodeCommand nCommand)
            {
                nCommand.SetData(cmd);
            }
            FileIsChanged = true;
        }

        private void ShowPanelInstallation(Installation? data)
        {
            var pnlInstallation = new pnlInstallation();
            pnlInstallation.ValueChanged += PnlInstallation_ValueChanged;
            pnlInstallation.CommandAdded += PnlInstallation_CommandAdded;
            pnlInstallation.CommandRemoved += PnlInstallation_CommandRemoved;
            pnlInstallation.CommandEditRequest += PnlInstallation_CommandEditRequest;
            pnlHost.Controls.Add(pnlInstallation);
            pnlInstallation.Dock = DockStyle.Fill;
            pnlInstallation.SetData(data);
        }

        private void PnlInstallation_CommandRemoved(object sender, Command cmd)
        {
            FileIsChanged = true;

            if (TreeConfiguration.SelectedNode != null && TreeConfiguration.SelectedNode is NodeInstallation n)
            {
                if (n.Nodes != null)
                {
                    var children = n.Nodes.OfType<NodeCommand>();
                    var nCommand = children.FirstOrDefault(o => o.Data != null && o.Data == cmd);

                    if (nCommand != null && n.Installation !=null )
                        n.Nodes.Remove((TreeNode)nCommand);
                }
            }
        }

        private void PnlInstallation_CommandEditRequest(object sender, Command cmd)
        {
            //devo modificare le proprietà di un Command quindi lo cerco tra i nodi figlio
            TreeNode currentNode = TreeConfiguration.SelectedNode;

            if (currentNode!=null && currentNode is NodeInstallation node)
            {
                var nCommand = node.Nodes.OfType<NodeCommand>().FirstOrDefault(o => o.Data == cmd);
                if (nCommand !=null)
                    TreeConfiguration.SelectedNode = nCommand;
            }

        }

        private void PnlInstallation_CommandAdded(object sender, Command cmd)
        {
            if (TreeConfiguration.SelectedNode is NodeInstallation nInstallation)
            {
                FileIsChanged = true;
                AddCommandNode(nInstallation, cmd);
            }
        }

        private void PnlInstallation_ValueChanged(object sender, EventArgs e)
        {
            if (sender is pnlInstallation pnl && _configuration !=null)
            {
                var data = pnl.GetData();
                if (data!=null)
                {
                    FileIsChanged = true;
                    //aggiornare treeItem
                    RefreshTree();
                }
            }
        }

        private void RefreshTree()
        {
            foreach (NodeInstallation nConfig in TreeConfiguration.Nodes[0].Nodes)
            {
                if (nConfig != null && nConfig.Installation != null)
                {
                    nConfig.Text = nConfig.Installation.Name;
                    if (!(nConfig.Installation?.Enabled ?? false))
                    {
                        nConfig.ImageKey = "SolutionDisabled";
                        nConfig.SelectedImageKey = "SolutionDisabled";
                    }
                    else
                    {
                        nConfig.ImageKey = "Solution";
                        nConfig.SelectedImageKey = "Solution";
                    }
                }
            }
        }

        private void ShowPanelRoot(SolutionConfiguration? data)
        {
            var pnl = new pnlRoot();
            pnl.ValueChanged += PnlRoot_ValueChanged;
            pnl.InstallationAdded += Pnl_InstallationAdded;
            pnl.InstallationRemoved += Pnl_InstallationRemoved;
            pnl.InstallationEditRequest += Pnl_InstallationEditRequest;
            pnlHost.Controls.Add(pnl);
            pnl.Dock = DockStyle.Fill;
            pnl.SetData(data);
        }

        private void Pnl_InstallationEditRequest(pnlRoot sender, Installation installation)
        {
            var node = TreeConfiguration.Nodes[0].Nodes.OfType<NodeInstallation>().FirstOrDefault(o => o.Installation == installation);

            if (node is NodeInstallation n)
            {
                TreeConfiguration.SelectedNode = n;
            }

        }

        private void Pnl_InstallationRemoved(pnlRoot sender, Installation installation)
        {
            //Ho rimosso un elemento Installation
            //Devo rimuovere il nodo corrispondente

            if (TreeConfiguration.Nodes !=null && TreeConfiguration.Nodes.Count > 0 && TreeConfiguration.Nodes[0] is NodeRoot root)
            {
                NodeInstallation? n = root.Nodes.Cast<NodeInstallation>().ToList().FirstOrDefault(o => o.Installation == installation);
                if (n != null)
                    root.Nodes.Remove(n);

                FileIsChanged = true;

            }

        }

        private void Pnl_InstallationAdded(pnlRoot sender, Installation installation)
        {
            if (TreeConfiguration.SelectedNode is NodeRoot nRoot)
            {
                FileIsChanged = true;
                AddInstallationNode(nRoot, installation);
            }
        }

        private static void AddInstallationNode(NodeRoot nRoot, Installation installation)
        {
            NodeInstallation nInstallation = new(installation);
            nRoot.Nodes.Add(nInstallation);
            nInstallation.Installation = installation;

            AddCommandNodes(nInstallation);

            if (installation.Enabled)
                nInstallation.ExpandAll();
        }

        private void PnlRoot_ValueChanged(object sender, EventArgs e)
        {
            if (sender is pnlRoot pnl)
            {
                var data = pnl.GetConfiguration();

                if (_configuration != null && data!=null)
                {
                    FileIsChanged = true;
                }

            }
        }

        private void CloseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _currentFilename = null;
            _configuration = null;
            TreeConfiguration.Nodes.Clear();
            pnlHost.Controls.Clear();
            ctlEmpty1.Show();
            WriteTitle();
        }

        private void CtlEmpty1_OpenFileRequest(object sender, EventArgs e)
        {
            LaunchOpenFile();
        }

        private void RefreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DrawTree();
        }

        private void ReloadFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_currentFilename!=null)
                OpenFile(_currentFilename);
        }

        private void NewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _currentFilename = null;
            _configuration = new SolutionConfiguration();
            WriteTitle();
            DrawTree();
        }

        private void OpenSourceFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenSourceFile();
        }

        private void OpenSourceFile()
        {
            if (_currentFilename!=null && File.Exists(_currentFilename))
            {
                var pi = new ProcessStartInfo
                {
                    UseShellExecute = true,
                    FileName = "code",
                    Arguments = _currentFilename,
                    WindowStyle = ProcessWindowStyle.Hidden,
                };

                try
                {
                    var res = Process.Start(pi);
                }
                catch (Exception)
                {
                    //probabilmente VSCode non è installato
                    //Uso l'editor predefinito
                    try
                    {
                        Process.Start("notepad.exe",_currentFilename);
                    }
                    catch (Exception ex1)
                    {
                        MessageBox.Show(ex1.Message); ;
                    }

                    
                }

                

                //System.Diagnostics.Process.Start("Code.exe",_currentFilename);
            }
        }

        private void ReadMetxtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("notepad.exe",Path.Combine(Application.StartupPath,"Help","Readme.md" ));
        }

        private void LaunchRefactoringToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LaunchRefactor();
        }

        private void LaunchRefactor()
        {
            var pi = new ProcessStartInfo
            {
                //UseShellExecute = true,
                //WindowStyle = ProcessWindowStyle.Hidden,
                FileName = Path.Combine(Application.StartupPath,"Console", "SolutionBuilder.Console.exe"),
                Arguments = $"-F\"{_currentFilename}\"",
            };

            try
            {
                Process.Start(pi);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void TreeConfiguration_MouseDown(object sender, MouseEventArgs e)
        {
            var node = TreeConfiguration.GetNodeAt(e.X, e.Y);
            if (node == null)
                return;
            else
                TreeConfiguration.SelectedNode = node;

            if (node is NodeInstallation && e.Button == MouseButtons.Right)
            {
                mnuInstallation.Show(TreeConfiguration, e.Location );
                
            }
        }

        private void EnableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (TreeConfiguration.SelectedNode is NodeInstallation n )
            {
                if (n.Installation != null)
                {
                    n.Installation.Enabled = true;
                    n.RefreshIcon();

                    if (pnlHost.Controls.Count != 0 && pnlHost.Controls[0] is pnlInstallation pnl)
                        pnl.IsEnabled = true;

                }
            }
        }

        private void DisableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (TreeConfiguration.SelectedNode is NodeInstallation n)
            {
                if (n.Installation != null)
                {
                    n.Installation.Enabled = false;
                    n.RefreshIcon();

                    if (pnlHost.Controls.Count != 0 && pnlHost.Controls[0] is pnlInstallation pnl)
                        pnl.IsEnabled = false;
                }
            }
        }

        private void ExcludeEverythingExceptThisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_configuration != null && TreeConfiguration.SelectedNode is NodeInstallation n)
            {
                foreach (var installation in _configuration.Installations)
                {
                    installation.Enabled = false;
                }
                if (n.Installation != null)
                {
                    n.Installation.Enabled = true;
                    n.RefreshIcon();

                    if (pnlHost.Controls.Count != 0 && pnlHost.Controls[0] is pnlInstallation pnl)
                        pnl.IsEnabled = true;
                }
            }
            RefreshTree();
        }

        private void IncludeEverthingExceptThisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_configuration != null && TreeConfiguration.SelectedNode is NodeInstallation n)
            {
                foreach (var installation in _configuration.Installations)
                {
                    installation.Enabled = true;
                }
                if (n.Installation != null)
                {
                    n.Installation.Enabled = false;
                    n.RefreshIcon();

                    if (pnlHost.Controls.Count != 0 && pnlHost.Controls[0] is pnlInstallation pnl)
                        pnl.IsEnabled = false;
                }
            }
            RefreshTree();
        }
    }

    public class Node : TreeNode
    {
        public virtual NodeType NodeType { get; }
    }

    public class NodeInstallation : Node
    {
        public NodeInstallation(Installation? data)
        {
            Installation = data;
            ImageKey = "Solution";
            SelectedImageKey = "Solution";


        }

        private Installation? _installation;
        public Installation? Installation
        {
            get => _installation;
            set
            {
                _installation = value;
                SetData(value);
            }
        }

        public override NodeType NodeType
        {
            get => NodeType.Installation;
        }

        public void SetData(Installation? data)
        {
            _installation = data;

            if (data == null)
                return;

            Text = data?.Name ?? "";
            RefreshIcon();
        }

        public void RefreshIcon()
        {
            if (!(_installation?.Enabled ?? false))
            {
                ImageKey = "SolutionDisabled";
                SelectedImageKey = "SolutionDisabled";
            }
            else
            {
                ImageKey = "Solution";
                SelectedImageKey = "Solution";
            }
        }

    }

    public class NodeRoot : Node
    {
        public NodeRoot()
        {
            ImageKey = "ConfigurationEditor";
            SelectedImageKey = "ConfigurationEditor";
        }

        public override NodeType NodeType
        {
            get => NodeType.Root;
        }

        private SolutionConfiguration? _configuration;
        public SolutionConfiguration? Configuration
        {
            get => _configuration;
            set
            {
                _configuration = value;
                Text = "Configuration";



            }
        }
    }

    public class NodeAction :Node
    {
        private string? _key;

        public NodeAction ()
        {
            
        }

        public override NodeType NodeType
        {
            get => NodeType.Action;
        }

        public string? Key
        {
            get => _key;
            set
            {
                _key = value;
                Name = value;
                ElaborateText();
            }
        }

        private void ElaborateText ()
        {
            if (_data != null)
            {
                Command cmd = ((NodeCommand)Parent).Data;
                Text = _data.ToString(cmd, Key??"");
            }

            //if (_data != null)
            //{
                

            //    var parent = this.Parent;
            //    Command cmd = ((NodeCommand)Parent).Data;
            //    switch (cmd)
            //    {
            //        case CommandReplace:
            //            Text = $"{Key} [{_data.Source}] -> [{_data.Value}]";
            //            break;
            //        case CommandReplaceCsProject:
            //        case CommandReplaceProperties:
            //            Text = $"{Key} [{_data.Source}] = '{_data.Value}'";
            //            break;
            //        case CommandReplaceReferences:
            //            Text = $"{Key} [{_data.Source}] -> [{_data.File}]";
            //            break;
            //        case CommandCopy:
            //            break;
            //        case CommandBuild:
            //            break;
            //        default:
            //            Text = $"{Key} [{_data.Source}] -> [{_data.Value}]";
            //            break;
            //    }

            //}
            //else
            //    this.Text = "";
        }

        private CommandAction? _data;
        public CommandAction? Data { get => _data; 
            set
            {
                _data = value;
                ElaborateText();
            }
        }

      
    }

    public class NodeCommand(Command data) : Node
    {
        private Command _command = data;

        public override NodeType NodeType
        {
            get => NodeType.Command;
        }

        public Command Data
        {
            get => _command;
            set => SetData(value);
        }

        public void SetData(Command data)
        {
            _command = data;
            Text = data.Name;


            ImageKey = data switch
            {
                CommandReplace or CommandReplaceCsProject or CommandReplaceProperties or CommandReplaceReferences =>
                "ReplaceAll",
                CommandInsertReference => "InsertClause",
                CommandCopy => "CopyItem",
                CommandBuild => "BuildSolution",
                _ => "Action",
            };

            SelectedImageKey = ImageKey;
        }
    }
}

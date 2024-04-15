using SolutionBuilder.Model;
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
    public delegate void CommandEventHandler (object sender, Command cmd);
    

    public partial class pnlInstallation : UserControl, IManualValidator
    {
        public event CommandEventHandler? CommandAdded;
        public event CommandEventHandler? CommandRemoved;
        public event CommandEventHandler? CommandEditRequest;

        public event ValueChangedEventHandler? ValueChanged;
        


        private Installation? _data;

        public pnlInstallation()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Indica se l'Installation è Enabled o no
        /// </summary>
        public bool IsEnabled 
        { 
            get => chkEnabled.Checked;  
            set
            {
                chkEnabled.Checked = value;
            }
        }


        public void SetData(Installation? data)
        {
            _data = data;

            if (data == null)
                return;

            txtName.Text = data.Name ?? "";
            txtSolutionFilename.Text = data.SolutionFilename ?? "";
            txtSourcePath.Text = data.SourcePath ?? "";
            txtOutputPath.Text = data.OutputPath ?? "";
            chkEnabled.Checked = data.Enabled;
            chkClearDestination.Checked = data.ClearDestination;


            
            lstCommands.Items.Clear();
            foreach (var item in data.Commands.OrderBy (o=>o.Order))
            {
                AddCommandToList(item);
            }

        }

        private void AddCommandToList(Command item)
        {
            ListViewItem li;
            li = new ListViewItem();
            li.Text = item.Name;
            li.Name = item.Name;
            lstCommands.Items.Add(li);
            li.SubItems.Add(item.Order.ToString());
            li.SubItems.Add(item.Type);
            li.Tag = item;
        }

        public Installation? GetData()
        {
            return _data;
        }

        private void txt_Validated(object sender, EventArgs e)
        {
            //ManualValidate();
        }

        private void cmdSolutionFilename_Click(object sender, EventArgs e)
        {
            string mainPath = FrmMain.Configuration?.SourcePath ?? "";
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Solution file (*.sln)|*.sln";
            ofd.InitialDirectory = mainPath;

            DialogResult result = ofd.ShowDialog();

            if (result== DialogResult.OK && ofd.FileName !=null)
            {
                txtSolutionFilename.Text = Path.GetFileName(ofd.FileName);
                string? folder = Path.GetDirectoryName(ofd.FileName);

                if (folder != null && folder != string.Empty)
                {
                    txtSourcePath.Text = SimplifyFolder (mainPath, folder);
                    ManualValidate();
                }
            }
        }

        private string? SimplifyFolder(string mainPath, string folder)
        {
            //string mainPath = frmMain.Configuration?.SourcePath ?? "";

            if (folder != null && folder.StartsWith(mainPath))
            {
                string s = folder.Substring(mainPath.Length);
                if (s.StartsWith("\\"))
                {
                    s = s.Substring(1);
                    folder = s;
                }
            }
            return folder;
        }

        private void OpenFolder (string mainPath, string relative, TextBox outputObject)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (Directory.Exists(relative))
                fbd.InitialDirectory = relative;
            else
            {
                string combine = System.IO.Path.Combine(mainPath, relative);
                if (Directory.Exists(combine))
                    fbd.InitialDirectory = combine;
                else
                    fbd.InitialDirectory = mainPath;
            }

            var result = fbd.ShowDialog();

            if (result == DialogResult.OK)
            {
                outputObject.Text = SimplifyFolder(mainPath, fbd.SelectedPath);
                txt_Validated(outputObject, EventArgs.Empty);
            }
        }

        private void cmdOutputPath_Click(object sender, EventArgs e)
        {
            string mainPath = FrmMain.Configuration?.OutputPath ?? "";
            OpenFolder(mainPath, txtOutputPath.Text, txtOutputPath);

        }

        private void cmdSourcePath_Click(object sender, EventArgs e)
        {
            string mainPath = FrmMain.Configuration?.SourcePath ?? "";
            OpenFolder(mainPath, txtSourcePath.Text, txtSourcePath);
        }

        private void chk_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Focused)
                ManualValidate();
        }

        public void ManualValidate()
        {
            if (_data != null)
            {
                _data.Name = txtName.Text;
                _data.SolutionFilename = txtSolutionFilename.Text;
                _data.SourcePath = txtSourcePath.Text;
                _data.OutputPath = txtOutputPath.Text;
                _data.Enabled = chkEnabled.Checked;
                _data.ClearDestination = chkClearDestination.Checked;
                ValueChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        private void addCommandToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddCommand fAdd = new frmAddCommand();
            var result = fAdd.ShowDialog();

            if (result== DialogResult.OK)
            {
                string cmdType = fAdd.CommandType;

                Command? command = cmdType switch 
                {
                    "Replace" => new CommandReplace(),
                    "Replace-CsProj" => new CommandReplaceCsProject(),
                    "Replace-Properties" => new CommandReplaceProperties(),
                    "Replace-References" => new CommandReplaceReferences(),
                    "Insert-References" => new CommandInsertReference(),
                    "Copy" => new CommandCopy(),
                    "Build"=> new CommandBuild(),
                    _ => null
                };

                if (command!=null && _data!=null)
                {
                    _data.Commands.Add(command);
                    command.Name = fAdd.CommandName;
                    command.Type = cmdType;
                    command.Order = _data.Commands.Max(o => o.Order) + 1;

                    AddCommandToList (command);

                    CommandAdded?.Invoke(this, command);

                }

            }

        }

        private void lstCommands_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstCommands.SelectedItems.Count==0)
            {
                editCommandToolStripMenuItem.Enabled = false;
                removeToolStripMenuItem.Enabled = false;
                bringUpToolStripMenuItem.Enabled = false;   
                sendDownToolStripMenuItem.Enabled = false;  
            }
            else
            {
                editCommandToolStripMenuItem.Enabled = true;
                removeToolStripMenuItem.Enabled = true;
                bringUpToolStripMenuItem.Enabled = true;
                sendDownToolStripMenuItem.Enabled = true;
            }
        }

        private void bringUpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BringUpCommand();
        }

        private void sendDownToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SendDownCommand();
        }

        private void BringUpCommand()
        {
            ListViewItem currentItem = lstCommands.SelectedItems[0];
            int currentIndex = currentItem.Index;
            if (currentIndex > 0)
            {
                lstCommands.Items.Remove(currentItem);
                lstCommands.Items.Insert(currentIndex - 1, currentItem);
                currentItem.Selected = true;
                currentItem.Focused = true;
                RearrangeCommandOrder();
                ManualValidate();
            }
        }

        private void RearrangeCommandOrder()
        {
            foreach (ListViewItem li in lstCommands.Items)
            {
                if (li.Tag is Command cmd)
                {
                    cmd.Order = li.Index + 1;
                    li.SubItems[1].Text = cmd.Order.ToString ();
                }
            }
        }

        private void SendDownCommand()
        {
            ListViewItem currentItem = lstCommands.SelectedItems[0];
            if (currentItem.Tag is Command cmd)
            {
                int currentIndex = currentItem.Index;
                if (currentIndex < lstCommands.Items.Count-1)
                {
                    lstCommands.Items.Remove(currentItem);
                    lstCommands.Items.Insert(currentIndex + 1, currentItem);
                    currentItem.Selected = true;
                    currentItem.Focused = true;
                    RearrangeCommandOrder();
                    ManualValidate();
                }
            }
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RemoveItem();
        }

        private void RemoveItem()
        {
            if (lstCommands.SelectedItems.Count != 0)
            {
                if (lstCommands.SelectedItems[0].Tag is Command cmd)
                {
                    if (_data!=null)
                        _data.Commands.Remove(cmd);

                    CommandRemoved?.Invoke(this, cmd);
                }
                lstCommands.Items.Remove(lstCommands.SelectedItems[0]);
            }
        }

        private void lstCommands_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
                RemoveItem();
        }

        private void lstCommands_DoubleClick(object sender, EventArgs e)
        {
            EditItem();
        }

        private void EditItem()
        {
            if (lstCommands.SelectedItems.Count != 0)
            {
                if (lstCommands.SelectedItems[0].Tag is Command cmd)
                {
                    CommandEditRequest?.Invoke(this, cmd);
                }
            }
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            if (((Control)sender).Focused)
            {
                ManualValidate();
            }
        }
    }
}

using Microsoft.Build.Construction;
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
    public delegate void CommandActionEventHandler(object sender, string key, CommandAction action);

    public partial class pnlCommand : UserControl
    {
        public event CommandEventHandler? CommandChanged;
        public event CommandActionEventHandler ActionRemoved;
        public event CommandActionEventHandler ActionEditRequest;
        public event CommandActionEventHandler ActionAdded;

        private Command? _data;
        private Installation? _installation;

        public pnlCommand()
        {
            InitializeComponent();
        }

        public void SetData(Installation? installation, Command? data)
        {

            _data = data;
            _installation = installation;

            if (data != null)
            {
                txtName.Text = data.Name;
                cmbType.Text = data.Type;

                lstProjects.Items.Clear();
                ListViewItem li;
                foreach (var item in data.ProjectNames)
                {
                    li = new ListViewItem();
                    li.Text = item;
                    lstProjects.Items.Add(li);
                }

                ShowActionList();

            }



        }

        /// <summary>
        /// chiedo alla soluzione l'elenco dei progetti
        /// </summary>
        public void ShowProjectList()
        {
            if (_installation != null)
            {
                string mainPath = FrmMain.Configuration?.SourcePath ?? "";
                string sourcePath = _installation.SourcePath;
                string solution = _installation.SolutionFilename;
                string solutionPath = Path.Combine(mainPath, sourcePath, solution);

                frmSolutionExplorer f = new frmSolutionExplorer();
                f.ProjectSelected += F_ProjectSelected;

                if (_data != null)
                    f.SetSelectedProjects(_data.ProjectNames);

                f.SetSolutionFilePath(solutionPath);
                f.ShowDialog();

            }
        }

        public void ShowActionList()
        {
            lstActions.Items.Clear();
            ListViewItem li;

            if (_data != null && _data.Actions != null)
            {
                var actions = _data.Actions;

                foreach (var action in actions.OrderBy(o => o.Value.Order))
                {
                    li = new ListViewItem();
                    li.Text = action.Key;
                    //li.Text = action.Value.ToString(_data, action.Key);
                    li.Tag = action;
                    lstActions.Items.Add(li);
                    li.SubItems.Add(action.Value.ToStringOnlyAction(_data, action.Key));
                }
            }

            //lstActions.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);

        }

        private void F_ProjectSelected(object sender, ProjectInSolution project)
        {
            lstProjects.Items.Add(project.ProjectName);
            lstProjects.Sort();
            ManualValidate();
        }

        private void lstProjects_SelectedIndexChanged(object sender, EventArgs e)
        {
            removeToolStripMenuItem.Enabled = lstProjects.SelectedItems.Count > 0;
        }

        private void addCommandToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowProjectList();
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //in teoria arrivo qui con un elemento selezionato
            RemoveProject();

        }

        private void RemoveProject()
        {
            if (lstProjects.SelectedItems.Count > 0 && _data != null)
            {
                string projName = lstProjects.SelectedItems[0].Text;
                if (_data.ProjectNames.Contains(projName))
                    _data.ProjectNames.Remove(projName);

                lstProjects.Items.Remove(lstProjects.SelectedItems[0]);
                ManualValidate();
            }
        }

        private void lstProjects_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {

        }

        private void lstProjects_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                RemoveProject();
            }
        }

        private void txt_Validated(object sender, EventArgs e)
        {
            if (((Control)sender).Focused)
                ManualValidate();
        }

        private void ManualValidate()
        {
            if (_data != null)
            {
                _data.Name = txtName.Text;
                //_data.Type = cmbType.Text;

                CommandChanged?.Invoke(this, _data);
            }
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            if (((Control)sender).Focused)
                ManualValidate();
        }

        private void mnuActionAdd_Click(object sender, EventArgs e)
        {
            if (_data != null)
            {

                frmAddAction f = new();
                var result = f.ShowDialog();

                if (result == DialogResult.OK)
                {
                    var value = f.GetData();
                    int order = (_data.Actions.Count == 0 ? 0 : _data.Actions.Max(o => o.Value.Order)) + 1;
                    CommandAction action = value.data with
                    {
                        Order = order,
                    };
                    _data.Actions.Add(value.key, action);
                    ShowActionList();
                    ActionAdded?.Invoke(this, value.key, action);
                }
            }

        }

        private void lstActions_SelectedIndexChanged(object sender, EventArgs e)
        {
            mnuActionEdit.Enabled = lstActions.SelectedItems.Count > 0;
            mnuActionRemove.Enabled = lstActions.SelectedItems.Count > 0;

            mnuActionUp.Enabled = lstActions.SelectedItems.Count > 0;
            mnuActionDown.Enabled = lstActions.SelectedItems.Count > 0;
        }

        private void mnuActionRemove_Click(object sender, EventArgs e)
        {
            DeleteAction();
        }

        private void DeleteAction()
        {
            if (lstActions.SelectedItems.Count != 0)
            {
                ListViewItem li = lstActions.SelectedItems[0];
                if (_data != null)
                {
                    if (li.Tag is KeyValuePair<string, CommandAction> action)
                    {
                        _data.Actions.Remove(action.Key);
                        ActionRemoved(this, action.Key, action.Value);
                    }
                }
                lstActions.Items.Remove(li);
            }
        }

        private void lstActions_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                DeleteAction();
            }
        }

        private void lstActions_DoubleClick(object sender, EventArgs e)
        {
            EditAction();
        }

        private void EditAction()
        {
            if (lstActions.SelectedItems.Count != 0)
            {
                ListViewItem li = lstActions.SelectedItems[0];
                if (_data != null)
                {
                    if (li.Tag is KeyValuePair<string, CommandAction> action)
                    {
                        ActionEditRequest?.Invoke(this, action.Key, action.Value);
                    }
                }
            }
        }


        private void mnuActionUp_Click(object sender, EventArgs e)
        {
            BringUpAction();
        }

        private void BringUpAction()
        {
            ListViewItem currentItem = lstActions.SelectedItems[0];
            int currentIndex = currentItem.Index;
            if (currentIndex > 0)
            {
                lstActions.Items.Remove(currentItem);
                lstActions.Items.Insert(currentIndex - 1, currentItem);
                currentItem.Selected = true;
                currentItem.Focused = true;
                RearrangeCommandOrder();
                ManualValidate();

                if (_data != null)
                    CommandChanged?.Invoke(this, _data);
            }
        }

        private void RearrangeCommandOrder()
        {
            foreach (ListViewItem li in lstActions.Items)
            {
                if (li.Tag is KeyValuePair<string, CommandAction> element)
                {
                    CommandAction action = element.Value with
                    {
                        Order = li.Index + 1,
                    };
                    _data!.Actions[element.Key] = action;
                    //element.Value = action;
                }
            }
        }

        private void mnuActionDown_Click(object sender, EventArgs e)
        {
            SendDownAction();
        }

        private void SendDownAction()
        {
            ListViewItem currentItem = lstActions.SelectedItems[0];
            int currentIndex = currentItem.Index;
            if (currentIndex < lstActions.Items.Count - 1)
            {
                lstActions.Items.Remove(currentItem);
                lstActions.Items.Insert(currentIndex + 1, currentItem);
                currentItem.Selected = true;
                currentItem.Focused = true;
                RearrangeCommandOrder();
                ManualValidate();

                if (_data != null)
                    CommandChanged?.Invoke(this, _data);
            }
        }

        private void mnuActionEdit_Click(object sender, EventArgs e)
        {
            EditAction();
        }
    }
}

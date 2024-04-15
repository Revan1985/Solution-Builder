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
    public delegate void InstallationEventHandler(pnlRoot sender, Installation installation);
    
    public delegate void ValueChangedEventHandler (object sender, EventArgs e);

    public partial class pnlRoot : UserControl, IManualValidator
    {
        public event ValueChangedEventHandler? ValueChanged;
        public event InstallationEventHandler? InstallationAdded;
        public event InstallationEventHandler? InstallationRemoved;
        public event InstallationEventHandler? InstallationEditRequest;

        public pnlRoot()
        {
            InitializeComponent();
            
        }

        private SolutionConfiguration? _data;

        public void SetData(SolutionConfiguration? data)
        {

            _data = data;

            if (data!=null)
            {
                txtOutputPath.Text = data.OutputPath;
                txtSourcePath.Text = data.SourcePath;
                txtTemporaryPath.Text = data.TemporaryPath;

                lstInstallations.Items.Clear();
                if (data.Installations != null)
                {
                    foreach (var item in data.Installations)
                    {
                        AddInstallationToList(item);
                    }
                }
            }
        }

        private void AddInstallationToList(Installation item)
        {
            ListViewItem li;
            li = new ListViewItem
            {
                Text = item.Name,
                Name = item.Name,
                Tag = item
            };
            lstInstallations.Items.Add(li);
            if (item.SolutionFilename != null)
                li.SubItems.Add(item.SolutionFilename);
        }

        public SolutionConfiguration? GetConfiguration()
        {
            return _data;
        }

        private void txt_Validated(object sender, EventArgs e)
        {
            
        }

        private void cmdOutput_Click(object sender, EventArgs e)
        {
            TextBox? txtRef = null;
            if (sender is Button button)
            {
                txtRef =
                    button.Name == cmdOutput.Name ? txtOutputPath :
                    button.Name == cmdSource.Name ? txtSourcePath :
                    button.Name == cmdTemp.Name ? txtTemporaryPath :
                    null;
            }

            if (txtRef == null)
                return;


            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (Directory.Exists(txtRef.Text))
                fbd.InitialDirectory = txtRef.Text;

            var result = fbd.ShowDialog();

            if (result== DialogResult.OK)
            {
                txtRef.Text = fbd.SelectedPath;
                ManualValidate();
            }
        }

        public void ManualValidate()
        {
            if (_data != null)
            {
                _data.OutputPath = txtOutputPath.Text;
                _data.SourcePath = txtSourcePath.Text;
                _data.TemporaryPath = txtTemporaryPath.Text;
                ValueChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        private void txtOutputPath_TextChanged(object sender, EventArgs e)
        {
            if (((Control)sender).Focused)
            {
                ManualValidate();
            }
        }

        private void ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ( sender is ToolStripMenuItem item && item.Name == addCommandToolStripMenuItem.Name)
            {
                AddInstallation();
            }
        }

        private void AddInstallation()
        {
            if (_data == null) return;

            frmAddInstallation fAdd = new();
            var result = fAdd.ShowDialog();

            if (result == DialogResult.OK)
            {
                string name = fAdd.GetValue();

                Installation cfg = new Installation { Name = name };
                _data.Installations.Add(cfg);

                AddInstallationToList (cfg);
                InstallationAdded?.Invoke(this, cfg);
            }
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {


            DialogResult result = MessageBox.Show("Remove selected item?", FrmMain.AppName,  MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result== DialogResult.Yes)
            {
                if (lstInstallations.SelectedItems.Count > 0)
                {
                    for (int i = lstInstallations.SelectedItems.Count - 1; i >= 0; i--)
                    {
                        if (lstInstallations.SelectedItems[i].Tag is Installation inst)
                        {
                            InstallationRemoved?.Invoke(this, inst);
                            _data?.Installations.Remove(inst);
                        }
                        lstInstallations.Items.Remove(lstInstallations.SelectedItems[i]);
                    }
                }
            }
        }

        private void lstInstallations_SelectedIndexChanged(object sender, EventArgs e)
        {
            removeToolStripMenuItem.Enabled = lstInstallations.SelectedItems.Count > 0;
            editCommandToolStripMenuItem.Enabled = lstInstallations.SelectedItems.Count > 0;
        }

        private void editCommandToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditItem();
        }

        private void lstInstallations_DoubleClick(object sender, EventArgs e)
        {
            EditItem();
        }

        private void EditItem()
        {
            if (lstInstallations.SelectedItems.Count !=0)
            {
                if (lstInstallations.SelectedItems[0].Tag is Installation inst)
                {
                    InstallationEditRequest?.Invoke(this, inst);
                }
            }
        }
    }
}

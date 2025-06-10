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
    public delegate void ActionEventHandler(object sender, string key, CommandAction action);

    public partial class pnlAction : UserControl
    {
        public event ActionEventHandler? ActionChanged;
        private CommandAction? _data;
        private string? _key;

        public pnlAction()
        {
            InitializeComponent();
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool NameEnabled
        {
            get => txtName.Enabled;
            set
            {
                txtName.ReadOnly = !value;
            }
        }

        public void SetData(string? key, CommandAction? data)
        {
            _key = key;
            _data = data;

            txtFile.Clear();
            txtSource.Clear();
            txtSourceFallback.Clear();
            txtValue.Clear();


            txtName.Text = key ?? "";


            if (data != null)
            {
                txtSource.Text = data.Source;
                txtSourceFallback.Text = data.SourceFallback;
                txtValue.Text = data.Value;
                txtFile.Text = data.File;
            }
        }

        public (string key, CommandAction action) GetData()
        {
            if (_data != null && _key!=null)
                return new (_key, _data);
            else
                return new (txtName.Text , new CommandAction(0, txtSource.Text, txtSourceFallback.Text, txtValue.Text, txtFile.Text));
        }

        private void txt_Validated(object sender, EventArgs e)
        {
           
        }

        private void ManualValidate()
        {
            if (_data != null)
            {
                CommandAction action = _data with
                {
                    Source = txtSource.Text,
                    SourceFallback = txtSourceFallback.Text,
                    Value = txtValue.Text,
                    File = txtFile.Text
                };

                ActionChanged?.Invoke(this, _key??"" , action);
                _data = action;
            }
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            if (((Control)sender).Focused)
                ManualValidate();
        }
    }
}

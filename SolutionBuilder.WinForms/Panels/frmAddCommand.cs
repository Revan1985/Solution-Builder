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
    public partial class frmAddCommand : Form
    {
        public frmAddCommand()
        {
            InitializeComponent();
            cmbType.SelectedIndex = 0;

            txtName.TextChanged += TxtName_TextChanged;

        }

        private void TxtName_TextChanged(object? sender, EventArgs e)
        {
            cmdOk.Enabled = txtName.Text != "";
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string CommandName
        {
            get { return txtName.Text ; }
            set { txtName.Text = value ; }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string CommandType
        {
            get { return cmbType.Text ; }
            set { cmbType.Text = value ; }
        }

    }
}

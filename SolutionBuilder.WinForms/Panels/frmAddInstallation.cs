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
    public partial class frmAddInstallation : Form
    {
        public frmAddInstallation()
        {
            InitializeComponent();
        }

        public string GetValue()
        {
            return txtName.Text;
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            cmdOk.Enabled = txtName.Text != "";
        }
    }
}

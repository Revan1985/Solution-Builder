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
    public partial class ctlEmpty : UserControl
    {
        public event EventHandler? OpenFileRequest;

        public ctlEmpty()
        {
            InitializeComponent();
        }

        private void lnkOpen_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenFileRequest?.Invoke (this, EventArgs.Empty);
        }
    }
}

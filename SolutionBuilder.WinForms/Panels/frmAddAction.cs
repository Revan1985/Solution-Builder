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
    public partial class frmAddAction : Form
    {
        public frmAddAction()
        {
            InitializeComponent();
            pnlAction1.NameEnabled = true;
        }

        public (string key, CommandAction data) GetData()
        {

            var data = pnlAction1.GetData();
            return data;
        }

    }
}

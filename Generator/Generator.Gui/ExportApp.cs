using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Generator.Gui
{
    public partial class ExportApp : Form
    {
        public String Sql
        {
            get { return sqlTextBox.Text; }
            set { sqlTextBox.Text = value; }
        }

        public ExportApp()
        {
            InitializeComponent();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Generator.Controls
{
    public partial class Join : UserControl
    {
        public ComboBox LeftSide
        {
            get { return leftSide; }
        }

        public ComboBox RightSide
        {
            get { return rightSide; }
        }

        public Join()
        {
            InitializeComponent();
        }
    }
}

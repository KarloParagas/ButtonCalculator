using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SuperCalc
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            displayBox.Text = "";
        }

        private void buttonClick(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            displayBox.Text = displayBox.Text + btn.Text;
        }
    }
}

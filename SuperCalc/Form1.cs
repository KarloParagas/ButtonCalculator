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

        /// <summary>
        /// Clears the display box when user clicks the "Clear" button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clearButton_Click(object sender, EventArgs e)
        {
            displayBox.Text = "";
        }

        /// <summary>
        /// Displays the corresponding numbers clicked to the display box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void numbersButton(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            displayBox.Text = displayBox.Text + btn.Text;
        }

        /// <summary>
        /// Clears the first set of operands when an operator is clicked.
        /// Applies the correct operator to perform the calculation.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void operatorButton(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            displayBox.Text = "";

            //TODO: Make the operator functional
        }
    }
}

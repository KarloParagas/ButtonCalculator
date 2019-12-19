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

        //Properties
        public double num1 { get; set; }
        public string op { get; set; }
        public double num2 { get; set; }

        /// <summary>
        /// Clears the display box when user clicks the "Clear" button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClearButton_Click(object sender, EventArgs e)
        {
            displayBox.Text = "";
        }

        /// <summary>
        /// Displays the corresponding numbers clicked to the display box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NumbersButton(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            displayBox.Text += btn.Text;
        }

        /// <summary>
        /// Grabs and clears the first set of operands when an operator is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OperatorButton(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            //Grab the operator that the user inputted and set it to the property above
            op = (sender as Button).Text;

            //Grab the first set of numbers that the user inputted and set it to the property above
            num1 = Convert.ToDouble(displayBox.Text);

            displayBox.Text = "";
        }

        private void EqualsButton_Click(object sender, EventArgs e)
        {
            //Grab the second set of numbers that the user inputted and set it to the property above
            num2 = Convert.ToDouble(displayBox.Text);

            double result = PerformCalculation();

            //Display the result in the display box
            displayBox.Text = result.ToString();
        }

        private double PerformCalculation()
        {
            double result = 0;

            if (op == "/") 
            {
                result = (double)num1 / num2;
            }
            if (op == "*")
            {
                result = (double)num1 * num2;
            }
            if (op == "-")
            {
                result = (double)num1 - num2;
            }
            if (op == "+")
            {
                result = (double)num1 + num2;
            }

            return result;
        }
    }
}

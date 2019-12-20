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
        public double num { get; set; }
        public string op { get; set; }

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
        private void NumbersButton_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            displayBox.Text += btn.Text;            
        }

        /// <summary>
        /// Grabs then clears the first set of operand when an operator is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OperatorButton_Click(object sender, EventArgs e)
        {
            //Grab the operator that the user inputted and set it to the property above
            op = (sender as Button).Text;

            //Grab the first set of numbers that the user inputted and set it to the property above
            num = Convert.ToDouble(displayBox.Text);

            if (op == "sqrt")
            {
                double result = Math.Sqrt(num);
                displayBox.Text = result.ToString();
            }
            else if (op == "1/X") 
            {
                double result = (double) 1 / num;
                displayBox.Text = result.ToString();
            }
            else
            {
                displayBox.Text = "";
            }
        }

        /// <summary>
        /// Performs the necessary calculation between the two number inputs
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EqualsButton_Click(object sender, EventArgs e)
        {
            //Grab the second set of numbers that the user inputted and set it to the property above
            double num2 = Convert.ToDouble(displayBox.Text);

            double result = PerformCalculation(num2);

            //Display the result in the display box
            displayBox.Text = result.ToString();
        }

        private double PerformCalculation(double num2)
        {
            double result = 0;

            if (op == "/") 
            {
                result = (double)num / num2;
            }
            if (op == "*")
            {
                result = (double)num * num2;
            }
            if (op == "-")
            {
                result = (double)num - num2;
            }
            if (op == "+")
            {
                result = (double)num + num2;
            }

            return result;
        }

        //TODO: +/- button functionality

        //TODO: Allow the user to perform multiple operations without having to press the equals button first
        //      (Operations are only currently between 2 number sets at a time)

        //TODO: Allow the user to perform the next calculation/operation without having to press the clear button beforehand               
    }
}

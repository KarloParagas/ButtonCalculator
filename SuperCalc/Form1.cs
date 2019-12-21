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

        private void Form1_Load(object sender, EventArgs e)
        {
            decimalButton.Enabled = false;
        }

        /// <summary>
        /// Clears the display box when user clicks the "Clear" button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClearButton_Click(object sender, EventArgs e)
        {
            displayBox.Text = "";
            decimalButton.Enabled = false;
        }

        /// <summary>
        /// Displays the corresponding numbers clicked to the display box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NumbersButton_Click(object sender, EventArgs e)
        {
            decimalButton.Enabled = true;

            Button btn = (Button)sender;

            displayBox.Text += btn.Text;            
        }

        private void posNegButton_Click(object sender, EventArgs e)
        {
            //If user already has a negative, remove it
            if (displayBox.Text.Contains("-"))
            {
                displayBox.Text = displayBox.Text.Remove(0, 1);
            }
            else //If not, add one
            {
                displayBox.Text = "-" + displayBox.Text;
            }
        }

        /// <summary>
        /// Grabs then clears the first set of operand when an operator is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OperatorButton_Click(object sender, EventArgs e)
        {
            if (IsPresent() == true) 
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
                    double result = (double)1 / num;
                    displayBox.Text = result.ToString();
                }
                else
                {
                    displayBox.Text = "";
                }           
            }
        }

        /// <summary>
        /// Performs the necessary calculation between the two number inputs
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EqualsButton_Click(object sender, EventArgs e)
        {
            if (IsPresent() == true) 
            {
                //Grab the second set of numbers that the user inputted and set it to the property above
                double num2 = Convert.ToDouble(displayBox.Text);

                double result = PerformCalculation(num2);

                //Display the result in the display box
                displayBox.Text = result.ToString();           
            }
        }

        private bool IsPresent()
        {
            if (displayBox.Text == "" || displayBox.Text == "-") 
            {
                return false;
            }
            return true;
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
        //TODO: Allow the user to perform multiple operations without having to press the equals button first
        //      (Operations are only currently between 2 number sets at a time)

        //TODO: Allow the user to perform the next calculation/operation without having to press the clear button beforehand               
    }
}

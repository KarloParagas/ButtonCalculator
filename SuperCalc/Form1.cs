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

        public double num2 { get; set; }

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
            label1.Text = "";
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

            //If the display box has "Cannot divide by zero", clear it
            if (displayBox.Text == "Cannot divide by zero") 
            {
                displayBox.Text = "";
                label1.Text = "";
            }

            //Displays what the user clicked to the display box
            displayBox.Text += btn.Text;

            //Displays what the user clicked above the display box
            label1.Text += btn.Text;
        }

        private void posNegButton_Click(object sender, EventArgs e)
        {
            //If user already has a negative, remove it
            if (displayBox.Text.Contains("-"))
            {
                displayBox.Text = displayBox.Text.Remove(0, 1);
                label1.Text = label1.Text.Remove(0, 1);
            }
            else //If not, add one
            {
                displayBox.Text = "-" + displayBox.Text;
                label1.Text = "-" + label1.Text;
            }
        }
        private void decimalButton_Click(object sender, EventArgs e)
        {
            //If the display box doesn't have a decimal
            if (!displayBox.Text.Contains(".")) 
            {
                displayBox.Text += ".";
                label1.Text += ".";
            }
        }

        /// <summary>
        /// Grabs then clears the first set of operand when an operator is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OperatorButton_Click(object sender, EventArgs e)
        {
            if (IsDataValid() == true) 
            {
                //Grab the operator that the user inputted and set it to the property above
                op = (sender as Button).Text;

                //Grab the first set of numbers that the user inputted and set it to the property above
                num = Convert.ToDouble(displayBox.Text);

                if (op == "sqrt")
                {
                    double total = PerformCalculation();
                    displayBox.Text = total.ToString();
                }
                else if (op == "1/X")
                {
                    if (num == 0)
                    {
                        displayBox.Text = "Cannot divide by zero";
                    }
                    else 
                    {
                        double total = PerformCalculation();
                        displayBox.Text = total.ToString();
                    }
                }
                else
                {
                    //Display it on the label
                    label1.Text += " " + op + " ";

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
            if (IsDataValid() == true && op != "sqrt" && op != "1/X") 
            {
                //Grab the second set of numbers that the user inputted and set it to the property above
                num2 = Convert.ToDouble(displayBox.Text);

                //If user tries to divide by zero
                if (op == "/" && num2 == 0)
                {
                    displayBox.Text = "Cannot divide by zero";
                }
                else 
                {
                    double total = PerformCalculation();

                    //Display the result in the display box
                    displayBox.Text = total.ToString();

                    decimalButton.Enabled = false;
                }
            }
        }

        private bool IsDataValid()
        {
            if (displayBox.Text == "" || displayBox.Text == "-" || displayBox.Text == "Cannot divide by zero" 
             || displayBox.Text == "." || displayBox.Text == "NaN") 
            {
                return false;
            }
            return true;
        }

        private double PerformCalculation()
        {
            double total = 0;

            if (op == "/") 
            {
                total = (double)num / num2;
            }
            if (op == "*")
            {
                total = (double)num * num2;
            }
            if (op == "-")
            {
                total = (double)num - num2;
            }
            if (op == "+")
            {
                total = (double)num + num2;
            }
            if (op == "sqrt") 
            {
                total = Math.Sqrt(num);
            }
            if (op == "1/X") 
            {
                total = (double)1 / num;
            }

            //Set the total to the num property
            num = total;

            return total; 
        }

        //TODO: Allow the user to perform multiple operations without having to press the equals button first
        //      or the clear button to do another calculation (Operations are only currently between 2 number sets at a time)            
    }
}

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

        //TODO: If, for example, 10 / 0 is done without using an equals button and user tries to do another operation, users shouldn't be able to do so.
        //TODO: If, for example, user does 1 + 2 + 3 + 4 and tries to click the "sqrt" or "1/X" next, it doesn't perform the calculation

        public string op { get; set; }

        public double num = 0;

        bool IsOperatorBtnClicked = false;

        bool IsEqualsBtnClicked = false;

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
            num = 0;
            displayBox.Text = "0";
            label1.Text = "";
            decimalButton.Enabled = false;
            IsOperatorBtnClicked = false;
            IsEqualsBtnClicked = false;
        }

        /// <summary>
        /// Displays the corresponding numbers clicked to the display box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NumbersButton_Click(object sender, EventArgs e)
        {
            decimalButton.Enabled = true;

            if (displayBox.Text == "Cannot divide by zero" || displayBox.Text == "NaN" || displayBox.Text == "0" || IsOperatorBtnClicked) 
            {
                if (IsEqualsBtnClicked == true) 
                {
                    label1.Text = "";
                }
                displayBox.Text = "";
            }

            IsOperatorBtnClicked = false;
            IsEqualsBtnClicked = false;

            //Displays what the user clicked to the display box
            displayBox.Text += (sender as Button).Text;
        }

        private void posNegButton_Click(object sender, EventArgs e)
        {
            if (displayBox.Text == "0") 
            {
                displayBox.Text = "";
            }

            //If the display box already has a negative button, remove it
            if (displayBox.Text.Contains("-"))
            {
                displayBox.Text = displayBox.Text.Remove(0, 1);
            }
            else if (displayBox.Text != "Cannot divide by zero")
            {
                displayBox.Text = "-" + displayBox.Text;
            }
        }

        private void decimalButton_Click(object sender, EventArgs e)
        {
            //If the display box doesn't have a decimal, add it
            if (!displayBox.Text.Contains(".")) 
            {
                if (displayBox.Text != "Cannot divide by zero") 
                {
                    displayBox.Text += ".";
                    label1.Text += ".";              
                }
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
                if (num != 0 && IsEqualsBtnClicked == false) //NOTE (Needs fixing): If an answer is zero even though youre doing multiple operations consecutively, it won't trigger on the next one
                {
                    label1.Text += " " + displayBox.Text + " " + (sender as Button).Text;

                    if (op == "sqrt")
                    {
                        label1.Text = "Square Root of " + displayBox.Text;
                    }
                    else if (op == "1/X")
                    {
                        if (num == 0)
                        {
                            displayBox.Text = "Cannot divide by zero";
                        }
                        else
                        {
                            label1.Text = "1/(" + displayBox.Text + ")";
                        }
                    }

                    double total = PerformCalculation();
                    displayBox.Text = total.ToString();

                    //Set num variable to the total
                    num = Convert.ToDouble(displayBox.Text);

                    //Grab the operator that the user inputted and set it to the property above
                    op = (sender as Button).Text;

                    decimalButton.Enabled = false;
                    IsOperatorBtnClicked = true;
                }
                else 
                {
                    //Grab the operator that the user inputted and set it to the property above
                    op = (sender as Button).Text;

                    //Grab the first set of numbers that the user inputted and set it to the property above
                    num = Convert.ToDouble(displayBox.Text);

                    if (op == "sqrt")
                    {
                        label1.Text = "Square Root of " + displayBox.Text;

                        double total = PerformCalculation();
                        displayBox.Text = total.ToString();

                        //Set num variable to the total
                        num = Convert.ToDouble(displayBox.Text);
                    }
                    else if (op == "1/X")
                    {
                        if (num == 0)
                        {
                            displayBox.Text = "Cannot divide by zero";
                        }
                        else
                        {
                            label1.Text = "1/(" + displayBox.Text + ")";

                            double total = PerformCalculation();
                            displayBox.Text = total.ToString();

                            //Set num variable to the total
                            num = Convert.ToDouble(displayBox.Text);
                        }
                    }
                    else
                    {
                        //Display it on the label
                        label1.Text += num + " " + (sender as Button).Text;

                        //Clear the display box for the second set of numbers could be entered
                        displayBox.Text = "";
                    }

                    IsOperatorBtnClicked = true;
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
                if (IsEqualsBtnClicked == false) 
                {
                    label1.Text += " " + displayBox.Text;

                    //If user tries to divide by zero
                    if (op == "/" && Convert.ToDouble(displayBox.Text) == 0)
                    {
                        displayBox.Text = "Cannot divide by zero";
                    }
                    else 
                    {
                        double total = PerformCalculation();

                        //Display the result in the display box
                        displayBox.Text = total.ToString();

                        //Set num variable to the total
                        num = Convert.ToDouble(displayBox.Text);

                        decimalButton.Enabled = false;
                    }               
                }
            }

            IsEqualsBtnClicked = true;
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
                total = (double)num / Convert.ToDouble(displayBox.Text);
            }
            if (op == "*")
            {
                total = (double)num * Convert.ToDouble(displayBox.Text);
            }
            if (op == "-")
            {
                total = (double)num - Convert.ToDouble(displayBox.Text);
            }
            if (op == "+")
            {
                total = (double)num + Convert.ToDouble(displayBox.Text);
            }
            if (op == "sqrt") 
            {
                total = Math.Sqrt(num);
            }
            if (op == "1/X") 
            {
                total = (double)1 / num;
            }

            return total; 
        }  
    }
}

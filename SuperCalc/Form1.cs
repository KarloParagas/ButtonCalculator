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

        public string op { get; set; }

        public double num = 0;

        bool IsOperatorBtnClicked = false;

        bool IsEqualsBtnClicked = false;

        bool IsNumNegative = false;

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

            if (IsEqualsBtnClicked == true && IsNumNegative == true && !displayBox.Text.Contains("-")) 
            {
                IsEqualsBtnClicked = false;

                displayBox.Text = "";

                label1.Text = num + " " + op;
            }

            if (displayBox.Text == "Cannot divide by zero" || displayBox.Text == "NaN" || displayBox.Text == "0" || IsOperatorBtnClicked) 
            {
                if (displayBox.Text != "-") 
                {
                    displayBox.Text = "";              
                }
            }

            IsOperatorBtnClicked = false;
            IsEqualsBtnClicked = false;

            //Checks if the whole number is positive or negative
            if (displayBox.Text.Contains("-"))
            {
                IsNumNegative = true;
            }
            else 
            {
                IsNumNegative = false;           
            }

            //Displays what the user clicked to the display box
            displayBox.Text += (sender as Button).Text;
        }

        private void posNegButton_Click(object sender, EventArgs e)
        {
            if (displayBox.Text == "0" || IsEqualsBtnClicked == true)
            {
                displayBox.Text = "";
            }
            else if (displayBox.Text != "" && IsEqualsBtnClicked == true && IsNumNegative == false) 
            {
                displayBox.Text = "";
            }

            //Turns the whole number negative or positive
            if (!displayBox.Text.Contains("-"))
            {
                if (displayBox.Text != "Cannot divide by zero")
                {
                    if (num != 0 && IsOperatorBtnClicked == true)
                    {
                        displayBox.Text = "";
                    }
                    displayBox.Text = "-" + displayBox.Text;
                    IsNumNegative = true;
                }
            }
            else 
            {
                displayBox.Text = displayBox.Text.Remove(0, 1);
                IsNumNegative = false;
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

                        decimalButton.Enabled = false;
                    }               
                }
            }

            IsEqualsBtnClicked = true;
        }

        /// <summary>
        /// Applies the desired operation to the set of numbers inputted
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OperatorButton_Click(object sender, EventArgs e)
        {
            if (IsDataValid() == true) 
            {
                if (num == 0 && IsEqualsBtnClicked == true)
                {
                    clearButton.PerformClick();
                }

                if (num != 0 && IsEqualsBtnClicked == true) 
                {
                    label1.Text += " " + (sender as Button).Text;
                    displayBox.Text = "";
                }

                if (num != 0 && IsEqualsBtnClicked == false)
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

                    if (total.ToString() == "∞")
                    {
                        MessageBox.Show("Cannot do anything with infinity");
                        clearButton.PerformClick();
                    }
                    else
                    {
                        displayBox.Text = total.ToString();

                        //Grab the operator that the user inputted and set it to the property above
                        op = (sender as Button).Text;

                        decimalButton.Enabled = false;
                        IsOperatorBtnClicked = true;
                    }
                }
                else if (IsEqualsBtnClicked == false) 
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
                else // If IsEqualsBtnClicked == true
                {
                    op = (sender as Button).Text;
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

            //Set the total to num, so the user can perform another operation with it. If needed.
            num = total;

            return total; 
        }  
    }
}

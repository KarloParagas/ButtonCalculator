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

        //TODO: +/- functionality needs adjusting. Must be able to do things such as: 1 + 1 + -2 
        //TODO: If, for example, 1 + 1 is done using an equals button click, the user should be able to spam the equals button and it should only keep adding 1 to the total

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

            //If the display box has "Cannot divide by zero", clear it
            if (displayBox.Text == "Cannot divide by zero" || displayBox.Text == "NaN" || displayBox.Text == "0" 
             || IsOperatorBtnClicked || IsEqualsBtnClicked == true) 
            {
                displayBox.Text = "";
            }

            IsOperatorBtnClicked = false;
            IsEqualsBtnClicked = false;

            //Displays what the user clicked to the display box
            displayBox.Text += (sender as Button).Text;

            //Displays what the user clicked above the display box
            label1.Text += (sender as Button).Text;
        }

        private void posNegButton_Click(object sender, EventArgs e)
        {
            //If user already has a negative, remove it
            if (displayBox.Text.Contains("-"))
            {
                displayBox.Text = displayBox.Text.Remove(0, 1);
                label1.Text = label1.Text.Remove(0, 1);
            }
            else if(displayBox.Text != "Cannot divide by zero")
            {
                    displayBox.Text = "-";

                    label1.Text += "-";
            }
        }

        private void decimalButton_Click(object sender, EventArgs e)
        {
            //If the display box doesn't have a decimal
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
                if (num != 0 && IsEqualsBtnClicked == false)
                {
                    equalsButton.PerformClick();

                    op = (sender as Button).Text;

                    if (op == "sqrt")
                    {
                        double total = PerformCalculation();
                        displayBox.Text = total.ToString();

                        //Set num variable to the total
                        num = Convert.ToDouble(displayBox.Text);

                        label1.Text += " " + op;
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

                            //Set num variable to the total
                            num = Convert.ToDouble(displayBox.Text);

                            label1.Text += " " + op;
                        }
                    }
                    else 
                    {
                        label1.Text += " " + op + " ";                   
                    }

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
                        double total = PerformCalculation();
                        displayBox.Text = total.ToString();

                        //Set num variable to the total
                        num = Convert.ToDouble(displayBox.Text);

                        label1.Text += " " + op;
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

                            //Set num variable to the total
                            num = Convert.ToDouble(displayBox.Text);

                            label1.Text += " " + op;
                        }
                    }
                    else
                    {
                        //Display it on the label
                        label1.Text += " " + op + " ";

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
                //If user tries to divide by zero
                if (op == "/" && Convert.ToDouble(displayBox.Text) == 0)
                {
                    displayBox.Text = "Cannot divide by zero";
                }
                else 
                {
                    double total = PerformCalculation();

                    //Display the result in the display box, and the label display above
                    displayBox.Text = total.ToString();

                    //Set num variable to the total
                    num = Convert.ToDouble(displayBox.Text);

                    decimalButton.Enabled = false;
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

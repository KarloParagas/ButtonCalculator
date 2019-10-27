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
            zeroButton.Click += new System.EventHandler(ClickedButton);
            oneButton.Click += new System.EventHandler(ClickedButton);
            twoButton.Click += new System.EventHandler(ClickedButton);
            threeButton.Click += new System.EventHandler(ClickedButton);
            fourButton.Click += new System.EventHandler(ClickedButton);
            fiveButton.Click += new System.EventHandler(ClickedButton);
            sixButton.Click += new System.EventHandler(ClickedButton);
            sevenButton.Click += new System.EventHandler(ClickedButton);
            eightButton.Click += new System.EventHandler(ClickedButton);
            nineButton.Click += new System.EventHandler(ClickedButton);
            divideButton.Click += new System.EventHandler(ClickedButton);
            multiplyButton.Click += new System.EventHandler(ClickedButton);
            minusButton.Click += new System.EventHandler(ClickedButton);
            plusButton.Click += new System.EventHandler(ClickedButton);
            sqrtButton.Click += new System.EventHandler(ClickedButton);
            fractionButton.Click += new System.EventHandler(ClickedButton);
            decimalButton.Click += new System.EventHandler(ClickedButton);
            posNegButton.Click += new System.EventHandler(ClickedButton);
        }

        public void ClickedButton(object sender, EventArgs e) 
        {
            displayBox.Text += (sender as Button).Text;
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            displayBox.Text = "";
        }

    }
}

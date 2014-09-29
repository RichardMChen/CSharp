using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Math_Quiz
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Random rand = new Random();  // Random object to generate the random numbers for the quiz.
        int add1 = 0, add2 = 0;  // Integers for addition problem.
        int subtract1 = 0, subtract2 = 0;  // Integers for subtraction problem.
        int multiply1 = 0, multiply2 = 0;  // Integers for multiplication problem.
        int divide1 = 0, divide2 = 0;  // Integers for division problem.
        int timeLeft = 0;  // Integer to hold the timer.

        public void QuizStart()
        {
            // Fill in the addition problem.
            add1 = rand.Next(101);
            add2 = rand.Next(101);
            label_PlusLeft.Text = add1.ToString();
            label_PlusRight.Text = add2.ToString();
            numericUpDown_Sum.Value = 0;

            // Fill in the subtraction problem.
            subtract1 = rand.Next(1, 101);
            subtract2 = rand.Next(1, subtract1);
            label_MinusLeft.Text = subtract1.ToString();
            label_MinusRight.Text = subtract2.ToString();
            numericUpDown_Difference.Value = 0;

            // Fill in the multiplication problem.
            multiply1 = rand.Next(51);
            multiply2 = rand.Next(51);
            label_TimesLeft.Text = multiply1.ToString();
            label_TimesRight.Text = multiply2.ToString();
            numericUpDown_Product.Value = 0;

            // Fill in the division problem.
            divide2 = rand.Next(2, 11);

            // Temperary integer to multiply with the divisor in order for division problem answer to be a whole number.
            int tempQuotient = rand.Next(2, 11);  

            divide1 = divide2 * tempQuotient;
            label_DivideLeft.Text = divide1.ToString();
            label_DivideRight.Text = divide2.ToString();
            numericUpDown_Quotient.Value = 0;

            // Start the timer.
            timeLeft = 30;
            label_Time.Text = "30 seconds";
            timer1.Start();
        }

        private bool CheckAnswer()
        {
            if ( (add1 + add2 == numericUpDown_Sum.Value) && (subtract1 - subtract2 == numericUpDown_Difference.Value)
                && (multiply1 * multiply2 == numericUpDown_Product.Value) && (divide1 / divide2 == numericUpDown_Quotient.Value) )
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void button_Start_Click(object sender, EventArgs e)
        {
            QuizStart();   // Start by generating the quiz.
            button_Start.Enabled = false;
            label_Time.BackColor = System.Drawing.Color.White;  // Changes the label's color to white when the start button is pressed.
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // When the time left reaches 11 seconds then the label's color is changed to red for the last 10 seconds.
            if(timeLeft <= 11)
            {
                label_Time.BackColor = System.Drawing.Color.Red;
            }

            // If answers are correct, stop the timer, and display messagebox.
            if (CheckAnswer() == true)
            {
                timer1.Stop();
                MessageBox.Show("CONSGRATULATIONS!!!\nYou got all the answers correct.");
                button_Start.Enabled = true;
            }
            else if (timeLeft > 0)
            {
                // Display new time left by updating the Time Left label.
                timeLeft--;
                label_Time.Text = timeLeft + " seconds";
            }
            else
            {
                // If the user runs out of time, stope the timer, show messagebox, and fill in answers.
                timer1.Stop();
                label_Time.Text = "Time's up!";
                MessageBox.Show("You did not finish in time.");
                numericUpDown_Sum.Value = add1 + add2;
                numericUpDown_Difference.Value = subtract1 - subtract2;
                numericUpDown_Product.Value = multiply1 * multiply2;
                numericUpDown_Quotient.Value = divide1 / divide2;
                button_Start.Enabled = true;
            }
        }

        private void answer_Enter(object sender, EventArgs e)
        {
            NumericUpDown answerBox = sender as NumericUpDown;  // Select answer in NumericUpDown control.
            if(answerBox != null)
            {
                int answerLength = answerBox.Value.ToString().Length;
                answerBox.Select(0, answerLength);
            }
        }
        
    }
}

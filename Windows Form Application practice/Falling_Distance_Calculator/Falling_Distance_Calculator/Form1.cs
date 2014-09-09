using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Falling_Distance_Calculator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button_Calculate_Click(object sender, EventArgs e)
        {
            //double distance = 0.0;
            //distance = FallingDistance(double.Parse(textBox_Time.Text));
            //label_DisplayDistance.Text = distance.ToString();
            label_DisplayDistance.Text = FallingDistance(double.Parse(textBox_Time.Text)).ToString();
        }

        private double FallingDistance(double time)
        {
            const double GRAVITY = 9.8;
            return 0.5 * GRAVITY * time * time;
        }
    }
}

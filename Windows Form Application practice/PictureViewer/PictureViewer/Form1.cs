using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PictureViewer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // Show the Open File dialog. If the user clicks OK, load the 
        // picture that the user chose. 
        private void button_ShowPicture_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Load(openFileDialog1.FileName);
            }
        }

        private void button_ClearPicture_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = null;
        }

        private void button_SetBackgroundColor_Click(object sender, EventArgs e)
        {
            // Show the color dialog box. If the user clicks OK, change the 
            // PictureBox control's background to the color the user chose.
            if(colorDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.BackColor = colorDialog1.Color;
            }
        }

        private void button_Close_Click(object sender, EventArgs e)
        {
            // Close the form when the Close button is pressed.
            this.Close();
        }

        private void checkBox_Strech_CheckedChanged(object sender, EventArgs e)
        {
            // If the user selects the Stretch check box,  
            // change the PictureBox's 
            // SizeMode property to "Stretch". If the user clears 
            // the check box, change it to "Normal".
            if(checkBox_Strech.Checked)
            {
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            else
            {
                pictureBox1.SizeMode = PictureBoxSizeMode.Normal;
            }
        }
    }
}

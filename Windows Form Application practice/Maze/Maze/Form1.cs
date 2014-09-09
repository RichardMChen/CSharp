using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Maze
{
    public partial class Form1 : Form
    {
        // This sound player will play a sound when the player hits a wall.
        System.Media.SoundPlayer startSoundPlayer = new System.Media.SoundPlayer(@"C:\Users\Richard\Documents\GitHub\C#\Windows Form Application practice\Maze\Maze\194439__high-festiva__gem-ping.wav");

        System.Media.SoundPlayer finishSoundPlayer = new System.Media.SoundPlayer(@"C:\Users\Richard\Documents\GitHub\C#\Windows Form Application practice\Maze\Maze\42106__marcuslee__laser-wrath-4.wav");

        public Form1()
        { 
            InitializeComponent();
            MoveToStart();
        }

        private void label_Finish_MouseEnter(object sender, EventArgs e)
        {
            // Play a sound, show a congratulations message box and close the form when done.
            finishSoundPlayer.Play();
            MessageBox.Show("Congratulations!!!");
            Close();
        }

        /// <summary>
        /// Play a sound, move the pointer to a point 10 pixels down and to the right
        /// of the starting point in the upper-left corner of the maze.
        /// </summary>
        private void MoveToStart()
        {
            startSoundPlayer.Play();
            Point startingPoint = panel1.Location;
            startingPoint.Offset(10, 10);
            Cursor.Position = PointToScreen(startingPoint);
        }

        private void wall_MouseEnter(object sender, EventArgs e)
        {
            // When the mouse pointer hits a wall, call the MoveToStart()
            MoveToStart();
        }
    }
}

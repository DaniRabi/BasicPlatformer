using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormApp
{
    public partial class Form1 : Form
    {
        bool right, left, jump;
        int G = 20, Force, index = 0;
        List<PictureBox> blocks;


        public Form1()
        {
            InitializeComponent();

            player.Top = floor.Top - player.Height;
            blocks = new List<PictureBox>() { floor, platform1, platform2 };
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right)
            {
                right = true;
            }
            if (e.KeyCode == Keys.Left)
            {
                left = true;
            }
            if (e.KeyCode == Keys.Escape)
                Close();
            if (!jump && e.KeyCode == Keys.Up)
            {
                jump = true;
                Force = G;
                player.Image = Image.FromFile("jump.png");
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            index++;

            if (right && index % 15 == 0)
            {
                player.Image = Image.FromFile("walk_r.gif");
            }
            if (left && index % 15 == 0)
            {
                player.Image = Image.FromFile("walk_l.gif");
            }     

            SideCollisions();

            if (right)
            {
                player.Left += 3;
            }
            if (left)
            {
                player.Left -= 3;
            }
            if (jump)
            {
                player.Top -= Force;
                Force -= 1;
            }
            
            if (player.Top >= floor.Top + player.Height)
            {
                player.Top = floor.Top + player.Height;
                if (jump)
                    Stand();
                jump = false;
            }
            else player.Top += 5;


            TopBottomCollisions();
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right)
            {
                right = false;
                Stand();
            }
            if (e.KeyCode == Keys.Left)
            {
                left = false;
                Stand();
            }

        }

        private void SideCollisions()
        {
            foreach (PictureBox block in blocks)
            {
                if (player.Right > block.Left && player.Left < block.Right - player.Width && player.Bottom < block.Bottom && player.Bottom > block.Top)
                {
                    right = false;
                }
                if (player.Left < block.Right && player.Right > block.Left + player.Width && player.Bottom < block.Bottom && player.Bottom > block.Top)
                {
                    left = false;
                }
            }
        }

        private void TopBottomCollisions()
        {
            foreach (PictureBox block in blocks)
            {
                if (player.Top + player.Height >= block.Top && player.Top < block.Top)
                {
                    if (player.Left + player.Width > block.Left && player.Left + player.Width < block.Left + block.Width + player.Width)
                    {
                        jump = false;
                        Force = 0;
                        player.Top = block.Location.Y - player.Height;
                    }
                    //else jump = true;
                }

                if (player.Left + player.Width > block.Left && player.Left + player.Width < block.Left + block.Width + player.Width 
                    && player.Top - block.Bottom <= 10 && player.Top - block.Top > -10)
                {
                    Force = -1;
                }
            }
        }

        private void Stand()
        {
            player.Image = Image.FromFile("stand.png");
        }
    }
}

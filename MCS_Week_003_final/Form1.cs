using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MazeFinal
{
    public partial class Form1 : Form
    {
        Color currentColor = Color.White;
        private int XTILES = 25; //Number of X tiles
        private int YTILES = 25; //Number of Y tiles
        private int TILESIZE = 10; //Size of the tiles (pixles)
        private PictureBox[,] mazeTiles;

        public Form1()
        {
            InitializeComponent();
            createNewMaze();
        }

        private void createNewMaze()
        {
            mazeTiles = new PictureBox[XTILES, YTILES];

            //Loop for getting all tiles
            for (int i = 0; i < XTILES; i++)
            {
                for (int j = 0; j < YTILES; j++)
                {
                    //initialize a new PictureBox array at cordinate XTILES, YTILES
                    mazeTiles[i, j] = new PictureBox();


                    //calculate size and location
                    int xPosition = (i * TILESIZE) + 13; //13 is padding from left
                    int yPosition = (j * TILESIZE) + 45; //45 is padding from top
                    mazeTiles[i, j].SetBounds(xPosition, yPosition, TILESIZE, TILESIZE);

                    //make top left and right bottom corner light blue. Used for start and finish
                    if ((i == 0 && j == 0) || (i == XTILES - 1 && j == YTILES - 1))
                        mazeTiles[i, j].BackColor = Color.LightBlue;
                    else
                    {
                        //make all other tiles white
                        mazeTiles[i, j].BackColor = Color.White;
                        //make it clickable


                    }

                    //Add to controls to form (display picture box)
                    this.Controls.Add(mazeTiles[i, j]);
                }
            }
        }



        private void button1_Click(object sender, EventArgs e)
        {
            //Create a previously searched array
            bool[,] alreadySearched = new bool[XTILES, YTILES];

            //Starts the recursive solver at tile (0,0). If false maze can not be solved.
            if (!solveMaze(0, 0, alreadySearched))
                MessageBox.Show("Maze can not be solved.");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Change all greay tiles to white
            for (int i = 0; i < XTILES; i++)
            {
                for (int j = 0; j < YTILES; j++)
                {
                    if (mazeTiles[i, j].BackColor == Color.Gray)
                        mazeTiles[i, j].BackColor = Color.White;
                }
            }

            //Reset start and finish to light blue
            mazeTiles[0, 0].BackColor = Color.LightBlue;
            mazeTiles[XTILES - 1, YTILES - 1].BackColor = Color.LightBlue;
        }

        private bool solveMaze(int xPos, int yPos, bool[,] alreadySearched)
        {
            bool correctPath = false;
            //should the computer check this tile
            bool shouldCheck = true;

            //Check for out of boundaries
            if (xPos >= XTILES || xPos < 0 || yPos >= YTILES || yPos < 0)
                shouldCheck = false;
            else
            {
                //Check if at finish, not (0,0 and colored light blue)
                if (mazeTiles[xPos, yPos].BackColor == Color.LightBlue && (xPos != 0 && yPos != 0))
                {
                    correctPath = true;
                    shouldCheck = false;
                }

                //Check for a wall
                if (mazeTiles[xPos, yPos].BackColor == Color.Black)
                    shouldCheck = false;

                //Check if previously searched
                if (alreadySearched[xPos, yPos])
                    shouldCheck = false;
            }

            //Search the Tile
            if (shouldCheck)
            {
                //mark tile as searched
                alreadySearched[xPos, yPos] = true;

                //Check right tile
                correctPath = correctPath || solveMaze(xPos + 1, yPos, alreadySearched);
                //Check down tile
                correctPath = correctPath || solveMaze(xPos, yPos + 1, alreadySearched);
                //check left tile
                correctPath = correctPath || solveMaze(xPos - 1, yPos, alreadySearched);
                //check up tile
                correctPath = correctPath || solveMaze(xPos, yPos - 1, alreadySearched);
            }

            //make correct path gray
            if (correctPath)
                mazeTiles[xPos, yPos].BackColor = Color.Gray;

            return correctPath;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Random rdm = new Random();
            //blocks off paths
            // mazeTiles[0,1].BackColor = Color.Black;

            //these block horizontal paths   (top to bottom)       
            mazeTiles[16, 0].BackColor = Color.Black;
            // mazeTiles[13, 2].BackColor = Color.Black;
            mazeTiles[7, 4].BackColor = Color.Black;
            //mazeTiles[17, 6].BackColor = Color.Black;
            mazeTiles[15, 8].BackColor = Color.Black;

            mazeTiles[0, 20].BackColor = Color.Black;

            //these block near the exit
            /// mazeTiles[11, 22].BackColor = Color.Black;
            mazeTiles[15, 24].BackColor = Color.Black;
            mazeTiles[24, 13].BackColor = Color.Black;
            mazeTiles[22, 14].BackColor = Color.Black;
            mazeTiles[22, 15].BackColor = Color.Black;
            //  mazeTiles[13,2].BackColor = Color.Black;
            for (int i = 4; i <= 18; i++)
            {   //vertical lines
                mazeTiles[i, 3].BackColor = Color.Black;
                mazeTiles[i, 5].BackColor = Color.Black;
                mazeTiles[i, 7].BackColor = Color.Black;
                mazeTiles[i, 23].BackColor = Color.Black;
                mazeTiles[i, 24].BackColor = Color.Black;

                //horizontal lines
                mazeTiles[1, i].BackColor = Color.Black;
                mazeTiles[2, i].BackColor = Color.Black;

            }

            for (int i = 2; i <= 16; i++)
            {
                //horizontal longer

                mazeTiles[i, 1].BackColor = Color.Black;

                mazeTiles[i, 9].BackColor = Color.Black;


            }
            for (int i = 14; i < 22; i++)
            {

                //8 tiles horizaltal
                mazeTiles[i, 5].BackColor = Color.Black;
                mazeTiles[i, 7].BackColor = Color.Black;
                mazeTiles[i, 9].BackColor = Color.Black;
                mazeTiles[i, 14].BackColor = Color.Black;
                mazeTiles[i, 15].BackColor = Color.Black;




                //8 tiles vertical 
                mazeTiles[4, i].BackColor = Color.Black;
                mazeTiles[5, i].BackColor = Color.Black;
                mazeTiles[7, i].BackColor = Color.Black;
                mazeTiles[9, i].BackColor = Color.Black;
                mazeTiles[11, i].BackColor = Color.Black;
                mazeTiles[13, i].BackColor = Color.Black;

            }
            for (int i = 9; i <= 12; i++)
            {
                mazeTiles[6, i].BackColor = Color.Black;
                mazeTiles[7, i].BackColor = Color.Black;
                //blocks these off
                // mazeTiles[7,13].BackColor = Color.Black;

                mazeTiles[11, i].BackColor = Color.Black;
            }

            for (int i = 12; i <= 15; i++)
            {
                //3 tiles horizantal
                mazeTiles[i, 9].BackColor = Color.Black;


                //3 tiles vertically
                mazeTiles[9, i].BackColor = Color.Black;
                mazeTiles[23, i].BackColor = Color.Black;

            }
            for (int i = 15; i <= 23; i++)
            {
                //horizantal tiles
                mazeTiles[i, 1].BackColor = Color.Black;
                mazeTiles[i, 7].BackColor = Color.Black;
                mazeTiles[i, 11].BackColor = Color.Black;
                mazeTiles[i, 12].BackColor = Color.Black;

                mazeTiles[i, 20].BackColor = Color.Black;
                mazeTiles[i, 21].BackColor = Color.Black;


                //vertical 8 tiles
                mazeTiles[7, i].BackColor = Color.Black;
                mazeTiles[1, i].BackColor = Color.Black;

            }
            for (int i = 23; i <= 7; i++)
            {
                mazeTiles[i, 19].BackColor = Color.Black;
                mazeTiles[i, 4].BackColor = Color.Black;
                mazeTiles[i, 6].BackColor = Color.Black;

                mazeTiles[1, i].BackColor = Color.Black;
                mazeTiles[6, i].BackColor = Color.Black;


            }
            for (int i = 15; i <= 24; i++)
            {

                //horizontal lines 

                // these 3 create rectangle on bottom right
                mazeTiles[i, 17].BackColor = Color.Black;
                mazeTiles[i, 18].BackColor = Color.Black;

            }
            for (int i = 21; i <= 23; i++)
            {
                mazeTiles[22, i].BackColor = Color.Black;
                mazeTiles[23, i].BackColor = Color.Black;
            }
            for (int i = 23; i <= 24; i++)
            {
                mazeTiles[19, i].BackColor = Color.Black;
                mazeTiles[20, i].BackColor = Color.Black;
            }

            for (int i = 1; i <= 5; i++)
            {


                mazeTiles[23, i].BackColor = Color.Black;
            }


        }
    }
}

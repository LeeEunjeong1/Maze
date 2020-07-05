using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MazeSolver
{
    class GUI
    {
        public Button start;
        public Button search;
        public Button reset;
        public ComboBox algorithm;
        public Form form;
        public PictureBox box;
        public Bitmap canvas;

        public int[,][] maze;
       // public Dictionary<int, Color> colors;

        public GUI()
        {

            form = new Form
            {
                Text = "Eunjeong"
            };
            form.SetDesktopBounds(800, 600, 900, 639);

            box = new PictureBox
            {
                Bounds = new Rectangle(100, 100, 400, 400),
                BackColor = Color.Gray
            };
            form.Controls.Add(box);

            canvas = new Bitmap(400, 400);

            start = new Button()
            {
                Bounds = new Rectangle(500, 20, 120, 50),
                BackColor = Color.LightYellow,
                Text = "Start"
            };
            start.Click += new EventHandler(Click_Start);
            form.Controls.Add(start);

            search = new Button()
            {
                Bounds = new Rectangle(740, 20, 120, 50),
                BackColor = Color.LightYellow,
                Text = "Search"
            };
            search.Click += new EventHandler(Click_Search);
            form.Controls.Add(search);

            reset = new Button()
            {
                Bounds = new Rectangle(620, 20, 120, 50),
                BackColor = Color.LightYellow,
                Text = "Reset"
            };
            reset.Click += new EventHandler(Click_Reset);
            form.Controls.Add(reset);

            algorithm = new ComboBox
            {
                Bounds = new Rectangle(740, 90, 120, 50),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            algorithm.Items.Add(new DepthFirstSearch());
            algorithm.Items.Add(new BreadthFirstSearch());
            algorithm.SelectedIndex = 0;
            form.Controls.Add(algorithm);

        }

        private void Click_Start(object sender, EventArgs e)
        {
            maze = new int[Program.X,Program.Y][];
            box.Image = canvas;

            Task.Factory.StartNew(() => Program.BuildMaze(maze));
        }

        private void Click_Search(object sender, EventArgs e)
        {
            SearchAlgorithm alg = (SearchAlgorithm)algorithm.SelectedItem;
            Task.Factory.StartNew(() => Program.Search(maze, alg));
        }

        private void Click_Reset(object sender, EventArgs e)
        {
            Graphics g = Graphics.FromImage(canvas);
            g.FillRectangle(new SolidBrush(Color.Gray), 0, 0, 400, 400);
            box.Refresh();
        }



        public void Draw(int[,][] maze, int r, int c, Color color)
        {
            Graphics g = Graphics.FromImage(canvas);
            SolidBrush brush = new SolidBrush(color);

            int[] cell_data = maze[r, c];
            if (r == 0 && c == 0)
                g.FillRectangle(new SolidBrush(Color.LightYellow), 5, 5, 30, 30);
            else if (r == maze.GetLength(0)-1 && c == maze.GetLength(1)-1)
                g.FillRectangle(new SolidBrush(Color.LightYellow), r * 40 + 5, c * 40 + 5, 30, 30);
            else
                g.FillRectangle(brush, r * 40 + 5, c * 40 + 5, 30, 30);
            if (cell_data[0] == 1)
            {
                g.FillRectangle(brush, r * 40 + 5, c * 40, 30, 5);
            }
            if (cell_data[1] == 1)
            {
                g.FillRectangle(brush, r * 40, c * 40 + 5, 5, 30);
            }
            if (cell_data[2] == 1)
            {
                g.FillRectangle(brush, r * 40 + 5, (c + 1) * 40 - 5, 30, 5);
            }
            if (cell_data[3] == 1)
            {
                g.FillRectangle(brush, (r + 1) * 40 - 5, c * 40 + 5, 5, 30);
            }

            box.Refresh();
        }

        public void Draw(int[,][] maze, int oldR, int oldC, int r, int c)
        {
            Graphics g = Graphics.FromImage(canvas);
            SolidBrush brush = new SolidBrush(Color.White);

            int[] cell_data = maze[oldR, oldC];
            if (oldR == 0 && oldC == 0)
                g.FillRectangle(new SolidBrush(Color.LightYellow), 5, 5, 30, 30);
            else if (oldR == maze.GetLength(0) - 1 && oldC == maze.GetLength(1) - 1)
                g.FillRectangle(new SolidBrush(Color.LightYellow), oldR * 40 + 5, oldC * 40 + 5, 30, 30);
            else
                g.FillRectangle(brush, oldR * 40 + 5, oldC * 40 + 5, 30, 30);
            if (cell_data[0] == 1)
            {
                g.FillRectangle(brush, oldR * 40 + 5, oldC * 40 - 5, 30, 5);
            }
            if (cell_data[1] == 1)
            {
                g.FillRectangle(brush, oldR * 40 - 5, oldC * 40 + 5, 5, 30);
            }
            if (cell_data[2] == 1)
            {
                g.FillRectangle(brush, oldR * 40 + 5, (oldC + 1) * 40 - 5, 30, 5);
            }
            if (cell_data[3] == 1)
            {
                g.FillRectangle(brush, (oldR + 1) * 40 - 5, oldC * 40 + 5, 5, 30);
            }

            cell_data = maze[r, c];
            g.FillRectangle(brush, r * 40 + 5, c * 40 + 5, 30, 30);
            if (cell_data[0] == 1)
            {
                g.FillRectangle(brush, r * 40 + 5, c * 40, 30, 5);
            }
            if (cell_data[1] == 1)
            {
                g.FillRectangle(brush, r * 40, c * 40 + 5, 5, 30);
            }
            if (cell_data[2] == 1)
            {
                g.FillRectangle(brush, r * 40 + 5, (c + 1) * 40, 30, 5);
            }
            if (cell_data[3] == 1)
            {
                g.FillRectangle(brush, (r + 1) * 40, c * 40 + 5, 5, 30);
            }   

            box.Refresh();
        }


    }
}

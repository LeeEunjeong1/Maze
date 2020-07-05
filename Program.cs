using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace MazeSolver
{
    static class Program
    {

        public static GUI gui;
        public static Random rng = new Random();
        public static int X = 10;
        public static int Y = 10; 

        public static int Main(string[] args)
        {
            gui = new GUI();
            gui.form.ShowDialog();

            return 0;
        }


        public static void Search(int[,][] maze, SearchAlgorithm alg)
        {
            alg.Search(maze);
        }

        public static void BuildMaze(int[,][] maze)
        {
            int r = 0;
            int c = 0;
            Stack<Tuple<int,int>> history = new Stack<Tuple<int, int>>();

            for (int i = 0; i < X; i++)
            {
                for (int j = 0; j < Y; j++)
                {
                    maze[i, j] = new int[]{0, 0, 0, 0, 0};
                }
            }

            while (history.Count > 0 || (r == 0 && c == 0))
            {
                maze[r, c][4] = 1;
                List<string> check = new List<string>();

                if (c > 0 && maze[r, c - 1][4] == 0)
                {
                    check.Add("L");
                }
                if (r > 0 && maze[r - 1, c][4] == 0)
                {
                    check.Add("U");
                }
                if (c < X - 1 && maze[r, c + 1][4] == 0)
                {
                    check.Add("R");
                }
                if (r < Y - 1 && maze[r + 1, c][4] == 0)
                {
                    check.Add("D");
                }

                int oldR = r;
                int oldC = c;

                if (check.Count > 0)
                {
                    string dir = check[rng.Next(check.Count)];
                    switch(dir)
                    {
                        case "L":
                            maze[r, c][0] = 1;
                            c--;
                            maze[r, c][2] = 1;
                            break;
                        case "U":
                            maze[r, c][1] = 1;
                            r--;
                            maze[r, c][3] = 1;
                            break;
                        case "R":
                            maze[r, c][2] = 1;
                            c++;
                            maze[r, c][0] = 1;
                            break;
                        case "D":
                            maze[r, c][3] = 1;
                            r++;
                            maze[r, c][1] = 1;
                            break;
                    }
                    history.Push(Tuple.Create(r, c));

                }
                else
                {
                    Tuple<int, int> cell = history.Pop();
                    r = cell.Item1;
                    c = cell.Item2;
                }

                gui.form.Invoke((MethodInvoker)(() => gui.Draw(maze, oldR, oldC, r, c)));
                Thread.Sleep(100);

            }

        }
    }
}

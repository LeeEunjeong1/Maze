using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MazeSolver
{
    class DepthFirstSearch : SearchAlgorithm
    {
        public override void Search(int[,][] maze)
        {
            int r = 0;
            int c = 0;

            int[,] visited = new int[Program.X,Program.Y];
            Stack<Tuple<int,int>> toDo = new Stack<Tuple<int,int>>();
            toDo.Push(Tuple.Create(r,c));

            while ((c < Program.X - 1 || r < Program.Y - 1) && toDo.Count > 0)
            {
                Tuple<int, int> cell = toDo.Pop();
                r = cell.Item1;
                c = cell.Item2;

                visited[r, c] = 1;

                Program.gui.form.Invoke((MethodInvoker)(() => Program.gui.Draw(maze, r, c, Color.LightBlue)));
                Thread.Sleep(100);

                List<string> check = new List<string>();
                if (maze[r, c][0] == 1 && c > 0 && visited[r, c - 1] != 1)
                {
                    check.Add("L");
                }
                if (maze[r, c][1] == 1 && r > 0 && visited[r - 1, c] != 1)
                {
                    check.Add("U");
                }
                if (maze[r, c][2] == 1 && c < Program.X - 1 && visited[r, c + 1] != 1)
                {
                    check.Add("R");
                }
                if (maze[r, c][3] == 1 && r < Program.Y && visited[r + 1, c] != 1)
                {
                    check.Add("D");
                }

                if (check.Count > 0)
                {
                    foreach (string s in check)
                    {
                        int newC = c;
                        int newR = r;
                        switch (s)
                        {
                            case "L":
                                newC--;
                                break;
                            case "U":
                                newR--;
                                break;
                            case "R":
                                newC++;
                                break;
                            case "D":
                                newR++;
                                break;
                        }

                        toDo.Push(Tuple.Create(newR, newC));

                    }
                }
            }

        }

        public override string ToString()
        {
            return "Depth-First Search";
        }

    }
}

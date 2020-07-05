using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeSolver
{
    abstract class SearchAlgorithm
    {

        abstract public void Search(int[,][] maze);

    }
}

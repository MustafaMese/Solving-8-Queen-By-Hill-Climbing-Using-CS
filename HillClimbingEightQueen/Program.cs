using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HillClimbingEightQueen
{
    class Program
    {
        static void Main(string[] args)
        {
            ProblemSolver problemSolver = new ProblemSolver(new Board());
            problemSolver.Solve();

            Console.ReadLine();
        }
    }
}

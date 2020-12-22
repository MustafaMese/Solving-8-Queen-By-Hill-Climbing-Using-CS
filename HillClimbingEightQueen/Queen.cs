using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HillClimbingEightQueen
{
    class Queen
    {
        private int[] point = new int[2];
        public int[] Point { get => point; set => point = value; }

        public Queen(int x, int y)
        {
            point[0] = x;
            point[1] = y;
        }

        public bool IsConflict(int[] targetPoint)
        {
            // Check rows and columns
            if (targetPoint[0] == point[0] || targetPoint[1] == point[1])
                return true;
            // Check diagonal
            else if (Math.Abs(targetPoint[0] - point[0]) == Math.Abs(targetPoint[1] - point[1]))
                return true;
            else
                return false;
        }

        public void Move(int[] nextPoint)
        {
            point[0] = nextPoint[0];
            point[1] = nextPoint[1];
        }

    }
}

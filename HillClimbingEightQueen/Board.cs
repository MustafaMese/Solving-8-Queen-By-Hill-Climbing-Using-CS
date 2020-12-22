using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HillClimbingEightQueen
{
    class Board
    {
        public int[][] squares;
        public List<Queen> queens;

        // Can be used as Problem Generator.
        public Board() 
        {
            int[][] s = {   new int[8] { -1, -1, -1, -1, -1, -1, -1, -1 },
                            new int[8] { -1, -1, -1, -1, -1, -1, -1, -1 },
                            new int[8] { -1, -1, -1, -1, -1, -1, -1, -1 }, 
                            new int[8] { -1, -1, -1, -1, -1, -1, -1, -1 }, 
                            new int[8] { -1, -1, -1, -1, -1, -1, -1, -1 },
                            new int[8] { -1, -1, -1, -1, -1, -1, -1, -1 },
                            new int[8] { -1, -1, -1, -1, -1, -1, -1, -1 }, 
                            new int[8] { -1, -1, -1, -1, -1, -1, -1, -1 } };

            squares = s;
            // Setting squares.
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    squares[i][j] = 0;
                }
            }

            queens = new List<Queen>();

            Random random = new Random();
            // Adding queens.
            for (int x = 0; x < 8; x++)
            {
                int y = random.Next(8);
                Queen q = new Queen(x, y);
                squares[x][y] = 1;
                queens.Add(q);
            }
        }

        public void MoveQueen(Queen queen, int[] oldPoint, int[] nextPoint)
        {
            squares[oldPoint[0]][oldPoint[1]] = 0;
            squares[nextPoint[0]][nextPoint[1]] = 1;
            queen.Move(nextPoint);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HillClimbingEightQueen
{
    class ProblemSolver
    {
        private Board board;

        public ProblemSolver(Board board)
        {
            this.board = board;
        }

        public void Solve()
        {
            PrintBoard();
            Console.WriteLine("-------------------------");
            int heuristic;
            bool randomChoosing = false;
            Random random = new Random();
            int step = 0;

            int[][] tempBoard = {   new int[8] { -1, -1, -1, -1, -1, -1, -1, -1 },
                                        new int[8] { -1, -1, -1, -1, -1, -1, -1, -1 },
                                        new int[8] { -1, -1, -1, -1, -1, -1, -1, -1 },
                                        new int[8] { -1, -1, -1, -1, -1, -1, -1, -1 },
                                        new int[8] { -1, -1, -1, -1, -1, -1, -1, -1 },
                                        new int[8] { -1, -1, -1, -1, -1, -1, -1, -1 },
                                        new int[8] { -1, -1, -1, -1, -1, -1, -1, -1 },
                                        new int[8] { -1, -1, -1, -1, -1, -1, -1, -1 } };

            while (true)
            {
                heuristic = CalculateHeuristic(board.queens);
                if (heuristic == 0)
                    break;
                step++;
                CopyArray(board.squares, tempBoard);

                for (int i = 0; i < board.queens.Count; i++)
                {
                    int[] oldPoint = new int[2];
                    CopyArray(board.queens[i].Point, oldPoint);
                    int colIndex = oldPoint[0];
                    int[] currentState = GetCurrentState(colIndex);

                    bool isEqual = false;
                    int bestIndex;
                    if (!randomChoosing)
                         bestIndex = GetBestNeighbour(currentState, oldPoint, colIndex, out isEqual);
                    else
                        bestIndex = random.Next(8);

                    if (isEqual)
                        continue;
                    else
                    {
                        int[] nextPoint = { colIndex, bestIndex };
                        board.MoveQueen(board.queens[i], oldPoint, nextPoint);
                    }
                }

                if (randomChoosing)
                    randomChoosing = false;

                if (!CompareArrays(board.squares, tempBoard))
                    randomChoosing = true;
                else
                {
                    heuristic = CalculateHeuristic(board.queens);
                    if (heuristic == 0)
                        break;
                }
            }
            PrintBoard();
            Console.WriteLine("Step Count:" + step);
        }

        private int[] GetCurrentState(int col)
        {
            int[] state = new int[8];

            for (int i = 0; i < board.squares.GetLength(0); i++)
                state[i] = board.squares[col][i];

            return state;
        }

        private int GetBestNeighbour(int[] currentState, int[] oldPoint, int colIndex, out bool isEqual)
        {
            int bestHeuristic = 0;
            int[] bestState = new int[8];
            List<Queen> tempQueenList = CopyList(board.queens);
            CopyArray(currentState, bestState);

            bestHeuristic = CalculateHeuristic(tempQueenList);
            Queen q = GetQueen(colIndex, tempQueenList);
            int bestIndex = oldPoint[1];

            for (int i = 0; i < currentState.Length; i++)
            {
                if (currentState[i] == 0)
                {
                    q.Point[1] = i;
                    int h = CalculateHeuristic(tempQueenList);
                    if (h < bestHeuristic)
                    {
                        bestHeuristic = h;
                        ResetArray(bestState);
                        bestState[i] = 1;
                        bestState[bestIndex] = 0;
                        bestIndex = i;
                    }
                }
                else
                    continue;
            }

            isEqual = CompareArrays(bestState, currentState);

            return bestIndex;
        }

        private Queen GetQueen(int colIndex, List<Queen> tempQueenList)
        {
            Queen q = null;
            for (int i = 0; i < tempQueenList.Count; i++)
            {
                if (tempQueenList[i].Point[0] == colIndex)
                {
                    q = tempQueenList[i];
                    break;
                }
            }

            return q;
        }

        private int CalculateHeuristic(List<Queen> queens)
        {
            int attacking = 0;
            for (int i = 0; i < queens.Count; i++)
            {
                for (int y = 0; y < queens.Count; y++)
                {
                    if (queens[i].Point == queens[y].Point)
                        continue;

                    if (queens[i].IsConflict(queens[y].Point))
                        attacking++;
                }
            }

            return (int)(attacking / 2);
        }

        private bool CompareArrays(int[][] firstArray, int[][] secondArray)
        {
            for (int x = 0; x < firstArray.GetLength(0); x++)
            {
                for (int y = 0; y < firstArray.GetLength(0); y++)
                {
                    if (firstArray[x][y] != secondArray[x][y])
                        return false;
                }
            }
            return true;
        }

        private bool CompareArrays(int[] firstState, int[] secondState)
        {
            for (int i = 0; i < firstState.Length; i++)
            {
                if (firstState[i] != secondState[i])
                    return false;
            }
            return true;
        }

        private void ResetArray(int[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = 0;
            }
        }

        private void CopyArray(int[][] initialState, int[][] tempState)
        {
            for (var i = 0; i < initialState.GetLength(0); i++)
                for (var z = 0; z < initialState.GetLength(0); z++)
                    tempState[i][z] = initialState[i][z];
        }

        private void CopyArray(int[] initialState, int[] tempState)
        {
            for (int i = 0; i < initialState.Length; i++)
                tempState[i] = initialState[i];
        }

        private List<Queen> CopyList(List<Queen> q1)
        {
            List<Queen> q2 = new List<Queen>();

            for (int i = 0; i < q1.Count; i++)
            {
                Queen q = new Queen(q1[i].Point[0], q1[i].Point[1]);
                q2.Add(q);
            }

            return q2;
        }

        public void PrintBoard()
        {
            for (int i = 0; i < board.squares.GetLength(0); i++)
            {
                Console.Write(" ");
                for (int y = 0; y < board.squares.GetLength(0); y++)
                    Console.Write(board.squares[i][y]);

                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}

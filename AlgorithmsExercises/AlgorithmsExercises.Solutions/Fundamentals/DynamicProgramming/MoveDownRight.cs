using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmsExercises.Solutions.Fundamentals.DynamicProgramming
{
    public class MoveDownRight
    {
        public static void MoveDownRightProblem()
        {
            int rows = int.Parse(Console.ReadLine());
            int cols = int.Parse(Console.ReadLine());
            int[,] inputMatrix = new int[rows, cols];
            int[,] sumMatrix = new int[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                int[] row = Console.ReadLine()
                    .Split(" ")
                    .Select(int.Parse)
                    .ToArray();
                for (int j = 0; j < cols; j++)
                {
                    inputMatrix[i, j] = row[j];
                }
            }

            int curMax = 0;
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    if (row == 0 && col == 0)
                    {
                        sumMatrix[row, col] = inputMatrix[row, col];
                        continue;
                    }
                    else if (col == 0)
                    {
                        curMax = sumMatrix[row - 1, col];
                    }
                    else if (row == 0)
                    {
                        curMax = sumMatrix[row, col - 1];
                    }
                    else
                    {
                        curMax = Math.Max(sumMatrix[row - 1, col], sumMatrix[row, col - 1]);
                    }

                    sumMatrix[row, col] = curMax + inputMatrix[row, col];
                }
            }

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Console.Write($" {sumMatrix[i, j]}");
                }
                Console.WriteLine();
            }

            Console.WriteLine();
            Console.WriteLine();

            int curRow = rows - 1;
            int curCol = cols - 1;
            var path = new Stack<string>();
            path.Push($"[{curRow}, {curCol}]");
            while (curRow > 0 || curCol > 0)
            {
                if (curCol <= 0 && curRow > 0)
                {
                    curRow -= 1;
                }
                else if (curRow <= 0 && curCol > 0)
                {
                    curCol -= 1;
                }
                else if (sumMatrix[curRow - 1, curCol] > sumMatrix[curRow, curCol - 1])
                {
                    curRow -= 1;
                }
                else if (sumMatrix[curRow - 1, curCol] < sumMatrix[curRow, curCol - 1])
                {
                    curCol -= 1;
                }

                path.Push($"[{curRow}, {curCol}]");
            }

            Console.WriteLine(string.Join(", ", path));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmsExercises.Solutions.Fundamentals.RecursionAndBacktracking
{
    public class FindAllPathsInLabyrinth
    {
        public static void FindAllPaths()
        {
            var rows = int.Parse(Console.ReadLine());
            var cols = int.Parse(Console.ReadLine());

            var labyrinth = new char[rows,cols];
            for (int i = 0; i < rows; i++)
            {
                string rowElements = Console.ReadLine();
                for (int j = 0; j < cols; j++)
                {
                    labyrinth[i,j] = rowElements[j];
                }
            }

            FindAllPathsInternal(labyrinth, 0, 0, string.Empty, new Stack<string>());
        }

        private static void FindAllPathsInternal(char[,] labyrinth, int row, int col, string directionMoved, Stack<string> directionsMoved)
        {
            if (IsOutBoundary(labyrinth, row, col))
            {
                return;
            }

            if (labyrinth[row,col] == '*' || labyrinth[row, col] == 'x')
            {
                return;
            }

            directionsMoved.Push(directionMoved);
            if (labyrinth[row,col] == 'e')
            {
                Console.WriteLine(string.Join(string.Empty, directionsMoved.Reverse()));
                directionsMoved.Pop();
                return;
            }

            labyrinth[row, col] = 'x';
            FindAllPathsInternal(labyrinth, row, col + 1, "R", directionsMoved);
            FindAllPathsInternal(labyrinth, row, col - 1, "L", directionsMoved);
            FindAllPathsInternal(labyrinth, row + 1, col, "D", directionsMoved);
            FindAllPathsInternal(labyrinth, row - 1, col, "U", directionsMoved);

            labyrinth[row, col] = '-';
            directionsMoved.Pop();
        }

        private static bool IsOutBoundary(char[,] labyrinth, int row, int col)
        {
            return row < 0 || col < 0 || row >= labyrinth.GetLength(0) || col >= labyrinth.GetLength(1);
        }
    }
}

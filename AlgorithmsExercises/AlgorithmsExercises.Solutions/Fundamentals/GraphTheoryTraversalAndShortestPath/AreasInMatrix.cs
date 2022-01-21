using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmsExercises.Solutions.Fundamentals.GraphTheoryTraversalAndShortestPath
{
    public class AreasInMatrix
    {
        private static string[,] _matrix;
        private static Dictionary<string,int> _areas = new Dictionary<string, int>();
        
        public static void FindAreas()
        {
            var rows = int.Parse(Console.ReadLine());
            var cols = int.Parse(Console.ReadLine());
            _matrix = new string[rows, cols];
            for (int i = 0; i < rows; i++)
            {
                var row = Console.ReadLine().ToArray();
                for (int j = 0; j < cols; j++)
                {
                    _matrix[i, j] = row[j].ToString();
                }
            }

            FindAreas(0);
            Print();
        }


        private static void Print()
        {
            Console.WriteLine($"Areas: {_areas.Count}");
            foreach (var kvp in _areas)
            {
                Console.WriteLine($"Letter '{kvp.Key}'-> {kvp.Value}");
            }
        }

        private static void FindAreas(int row)
        {
            if (row >= _matrix.GetLength(0))
            {
                return;
            }

            for (int col = 0; col < _matrix.GetLength(1); col++)
            {
                var originalCellValue = _matrix[row, col];
                if (originalCellValue.ToString() == "x")
                {
                    continue;
                }

                if (!_areas.ContainsKey(originalCellValue))
                {
                    _areas.Add(originalCellValue, 1);
                }
                else
                {
                    _areas[originalCellValue] += 1;
                }
                

                Step(row, col, originalCellValue);
            }

            FindAreas(row + 1);
        }

        private static void Step(int row, int col, string originalcellValue)
        {
            if (IsOutBoundary(row, col))
            {
                return;
            }

            if (_matrix[row, col] != originalcellValue)
            {
                return;
            }

            _matrix[row, col] = "x";
            Step(row, col + 1, originalcellValue);
            Step(row, col - 1, originalcellValue);
            Step(row + 1, col, originalcellValue);
            Step(row - 1, col, originalcellValue);
        }

        private static bool IsOutBoundary(int row, int col)
        {
            return row < 0 || col < 0 || row >= _matrix.GetLength(0) || col >= _matrix.GetLength(1);
        }
    }
}

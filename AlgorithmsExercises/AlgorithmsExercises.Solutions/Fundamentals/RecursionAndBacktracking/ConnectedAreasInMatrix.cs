using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmsExercises.Solutions.Fundamentals.RecursionAndBacktracking
{
    public class Area
    {
        public int Size { get; set; }
        public int Row { get; set; }
        public int Col { get; set; }

        public override string ToString()
        {
            return $"({Row}, {Col}), size: {Size}";
        }
    }

    public class ConnectedAreasInMatrix
    {
        private static char[,] _matrix;
        private static List<Area> _areas = new List<Area>();

        public static void ConnectedAreas()
        {
            var rows = int.Parse(Console.ReadLine());
            var cols = int.Parse(Console.ReadLine());
            _matrix = new char[rows, cols];
            for (int i = 0; i < rows; i++)
            {
                var row  = Console.ReadLine().ToArray();
                for (int j = 0; j < cols; j++)
                {
                    _matrix[i, j] = row[j];
                }
            }

            FindAreas(0);
        }

        private static void Print()
        {
           var orderedAreas = _areas
                .OrderByDescending(a => a.Size)
                .ThenBy(a => a.Row)
                .ThenBy(a => a.Col)
                .ToArray();
            Console.WriteLine($"Total areas found: {orderedAreas.Count()}");
            for(int i = 0; i < orderedAreas.Length; i++)
            {
                Console.WriteLine($"Area #{i + 1} at {orderedAreas[i]}");
            }
        }

        private static void FindAreas(int row)
        {
            if (row >= _matrix.GetLength(0))
            {
                Print();
                return;
            }

            for (int col = 0; col < _matrix.GetLength(1); col++)
            {
                var originalCellValue = _matrix[row, col];
                var area = new Area
                {
                    Row = row,
                    Col = col,
                    Size = 0
                };

                Step(row, col, area);
                if (originalCellValue == '-')
                {
                    _areas.Add(area);
                    area = new Area
                    {
                        Row = row,
                        Col = col,
                        Size = 0
                    };
                }
            }

            FindAreas(row + 1);
        }

        private static void Step(int row, int col, Area curArea)
        {
            if (IsOutBoundary(row, col))
            {
                return;
            }

            if (_matrix[row, col] == '*' || _matrix[row, col] == 'x')
            {
                return;
            }

            _matrix[row, col] = 'x';
            curArea.Size++;
            Step(row, col + 1, curArea);
            Step(row, col - 1, curArea);
            Step(row + 1, col, curArea);
            Step(row - 1, col, curArea);
        }

        private static bool IsOutBoundary(int row, int col)
        {
            return row < 0 || col < 0 || row >= _matrix.GetLength(0) || col >= _matrix.GetLength(1);
        }
    }
}

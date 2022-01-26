using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmsExercises.Solutions.Fundamentals.DynamicProgramming
{
    public class PascalTriangle
    {
        private static Dictionary<string, long> _calculatedNumbers = new Dictionary<string, long>();

        public static void GetNumber()
        {
            int row = int.Parse(Console.ReadLine());
            int col = int.Parse(Console.ReadLine());

            var number = CalculateNumberFromPascalTriangle(row, col);
            Console.WriteLine(number);
        }

        private static long CalculateNumberFromPascalTriangle(int row, int col)
        {
            if (_calculatedNumbers.ContainsKey($"{row}-{col}"))
            {
                return _calculatedNumbers[$"{row}-{col}"];
            }
            if (col == 0 ||
                row == 0 ||
                col == row)
            {
                return 1;
            }

            var number1 = CalculateNumberFromPascalTriangle(row - 1, col - 1);
            var number2 = CalculateNumberFromPascalTriangle(row - 1, col);
            var number = number1 + number2;
            if (!_calculatedNumbers.ContainsKey($"{row}-{col}"))
            {
                _calculatedNumbers.Add($"{row}-{col}", number);
            }
            if (!_calculatedNumbers.ContainsKey($"{row}-{row - col}"))
            {
                _calculatedNumbers.Add($"{row}-{row - col}", number);
            }

            return number;
        }
    }
}

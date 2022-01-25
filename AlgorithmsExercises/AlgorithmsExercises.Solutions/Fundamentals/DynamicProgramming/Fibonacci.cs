using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmsExercises.Solutions.Fundamentals.DynamicProgramming
{
    public class Fibonacci
    {
        private static Dictionary<int, long> _calculatedFib = new Dictionary<int, long>();

        public static void CalcFibonacci()
        {
            var n = int.Parse(Console.ReadLine());

            var fibonacci = FibonaccInternal(n);

            Console.WriteLine(fibonacci);
        }

        private static long FibonaccInternal(int n)
        {
            if (_calculatedFib.ContainsKey(n))
            {
                return _calculatedFib[n];
            }

            if (n <= 0)
            {
                return 0;
            }

            if (n <= 2)
            {
                return 1;
            }

            var fib = FibonaccInternal(n - 1) + FibonaccInternal(n - 2);

            _calculatedFib.Add(n, fib);

            return fib;
        }
    }
}

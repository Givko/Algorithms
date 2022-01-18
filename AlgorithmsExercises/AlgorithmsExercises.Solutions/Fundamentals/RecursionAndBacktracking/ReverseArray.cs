using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmsExercises.Solutions.Fundamentals.RecursionAndBacktracking
{
    public class ReverseArray
    {
        private static int[] _array;

        public static void Reverse(int[] array)
        {
            _array = array;
            Reverse(0);
            Console.WriteLine(String.Join(" ", _array));
        }

        private static void Reverse(int idx)
        {
            if (idx == _array.Length / 2)
            {
                return;
            }

            Swap(idx, _array.Length - 1 - idx);
            Reverse(idx + 1);
        }

        private static void Swap(int idx1, int idx2)
        {
            var temp = _array[idx1];
            _array[idx1] = _array[idx2];
            _array[idx2] = temp;
        }
    }
}

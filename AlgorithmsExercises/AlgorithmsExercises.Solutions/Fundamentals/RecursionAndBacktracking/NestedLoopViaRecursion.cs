using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmsExercises.Solutions.Fundamentals.RecursionAndBacktracking
{
    public class NestedLoopViaRecursion
    {
        static List<int> helperArray = new List<int>();
        public static void SimulateNestedLoops(int n)
        {
            Loop(1, n);
        }

        private static void Loop(int loopNumber, int numberOfLoops)
        {
            if (loopNumber > numberOfLoops)
            {
                Console.WriteLine(string.Join(" ", helperArray));
                return;
            }

            for (int j = 1; j <= numberOfLoops; j++)
            {
                helperArray.Add(j);
                Loop(loopNumber + 1, numberOfLoops);
                helperArray.RemoveAt(helperArray.Count - 1);
            }
        }
    }
}

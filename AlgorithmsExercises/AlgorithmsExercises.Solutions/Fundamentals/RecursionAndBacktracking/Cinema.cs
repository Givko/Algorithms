using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgorithmsExercises.Solutions.Fundamentals.RecursionAndBacktracking
{
    public class Cinema
    {
        //Peter, Amy, George, Rick
        //Amy - 1
        //Rick - 4
        //generate

        private static Dictionary<int, string> reservedSeats = new Dictionary<int, string>();
        private static List<string> seatCombination;

        public static void GetCombinations()
        {
            seatCombination = Console.ReadLine().Split(", ").ToList();

            while (true)
            {
                var input = Console.ReadLine();
                if (input == "generate")
                {
                    break;
                }

                var guestAndSeat = input.Split(" - ");
                var seat = int.Parse(guestAndSeat[1]) - 1;
                var guest = guestAndSeat[0];
                reservedSeats.Add(seat, guest);

                var currentSeat = seatCombination.IndexOf(guest);

                Swap(currentSeat, seat);
            }

            GenerateCombinations(0);
        }

        private static void GenerateCombinations(int indexToSwap)
        {
            if (indexToSwap >= seatCombination.Count)
            {
                Print();
                return;
            }
            GenerateCombinations(indexToSwap + 1);

            if (reservedSeats.ContainsKey(indexToSwap))
            {
                return;
            }

            for (int i = indexToSwap + 1; i < seatCombination.Count; i++)
            {
                if (reservedSeats.ContainsKey(i))
                    continue;

                Swap(indexToSwap, i);
                GenerateCombinations(indexToSwap + 1);
                Swap(indexToSwap, i);

            }
        }

        private static void Print()
        {
            Console.WriteLine(string.Join(" ", seatCombination));
        }

        private static void Swap(int index1, int index2)
        {
            var temp = seatCombination[index1];
            seatCombination[index1] = seatCombination[index2];
            seatCombination[index2] = temp;
        }
    }
}

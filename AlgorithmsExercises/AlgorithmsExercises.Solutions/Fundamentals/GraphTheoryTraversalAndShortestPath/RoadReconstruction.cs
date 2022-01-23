using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmsExercises.Solutions.Fundamentals.GraphTheoryTraversalAndShortestPath
{
    public class Street
    {
        public int First { get; set; }
        public int Second { get; set; }
    }

    public class RoadReconstruction
    {
        private static Dictionary<int, List<int>> _graph = new Dictionary<int, List<int>>();
        private static List<Street> _streets = new List<Street>();

        public static void FindImportantStreets()
        {
            var buildings = int.Parse(Console.ReadLine());
            for (int i = 0; i < buildings; i++)
            {
                _graph.Add(i, new List<int>());
            }

            var streets = int.Parse(Console.ReadLine());
            for (int i = 0; i < streets; i++)
            {
                var street = Console.ReadLine().Split(" - ");
                var firstBuilding = int.Parse(street[0]);
                var secondBuilding = int.Parse(street[1]);

                _graph[firstBuilding].Add(secondBuilding);
                _graph[secondBuilding].Add(firstBuilding);
                _streets.Add(
                    new Street
                    {
                        First = firstBuilding,
                        Second = secondBuilding
                    });
                _streets.Add(
                    new Street
                    {
                        First = secondBuilding,
                        Second = firstBuilding
                    });

            }

            _streets = _streets
                .OrderBy(s => s.First)
                .ThenBy(s => s.Second)
                .ToList();
            var importantEdges = new HashSet<string>();
            foreach (var street in _streets)
            {
                _graph[street.First].Remove(street.Second);
                _graph[street.Second].Remove(street.First);
                if (importantEdges.Contains($"{street.First} {street.Second}") ||
                    importantEdges.Contains($"{street.Second} {street.First}"))
                {
                    continue;
                }

                var cycleFound = FindShortestPath(street.First, street.Second);
                if (!cycleFound)
                {
                    importantEdges.Add($"{street.First} {street.Second}");
                    _graph[street.First].Add(street.Second);
                    _graph[street.Second].Add(street.First);
                }
                else
                {
                    _graph[street.First].Add(street.Second);
                    _graph[street.Second].Add(street.First);
                }

            }

            Console.WriteLine($"Important streets:");
            foreach (var edge in importantEdges)
            {
                Console.WriteLine(edge);
            }
        }

        private static bool FindShortestPath(int start, int destination)
        {
            var visitedNodes = new HashSet<int>();
            var queue = new Queue<int>();
            queue.Enqueue(start);
            visitedNodes.Add(start);
            while (queue.Count > 0)
            {
                var curNode = queue.Dequeue();
                if (curNode == destination)
                {
                    return true;
                }

                foreach (var childNode in _graph[curNode])
                {
                    if (visitedNodes.Contains(childNode))
                    {
                        continue;
                    }

                    queue.Enqueue(childNode);
                    visitedNodes.Add(childNode);
                }
            }

            return false;
        }

    }
}

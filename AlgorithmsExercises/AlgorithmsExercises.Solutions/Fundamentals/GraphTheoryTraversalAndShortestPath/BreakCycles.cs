using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmsExercises.Solutions.Fundamentals.GraphTheoryTraversalAndShortestPath
{
    public class Edge
    {
        public string First { get; set; }
        public string Second { get; set; }
    }

    public class BreakCycles
    {
        private static Dictionary<string, List<string>> _graph = new Dictionary<string, List<string>>();
        private static List<Edge> _edges = new List<Edge>();

        public static void IsGraphAcyclic()
        {
            SolveProblem();
        }

        private static void SolveProblem()
        {
            var nodes = int.Parse(Console.ReadLine());
            for (int i = 0; i < nodes; i++)
            {
                var pair = Console.ReadLine();

                var pairArray = pair.Split(" -> ", StringSplitOptions.RemoveEmptyEntries);
                var startPoint = pairArray[0];

                var destinations = pairArray[1].Split(" ").ToList();
                _graph[startPoint] = destinations;
                foreach (var destination in destinations)
                {
                    _edges.Add(
                         new Edge
                         {
                             First = startPoint,
                             Second = destination
                         });
                }

            }
            _edges = _edges
                .OrderBy(e => e.First)
                .ThenBy(e => e.Second)
                .ToList();

            var removedEdges = new List<string>();
            foreach (var edge in _edges)
            {
                var isRemove = _graph[edge.First].Remove(edge.Second) && _graph[edge.Second].Remove(edge.First);
                if (!isRemove)
                {
                    continue;
                }

                var cycleFound = FindShortestPath(edge.First, edge.Second);
                if (cycleFound)
                {
                    removedEdges.Add($"{edge.First} - {edge.Second}");
                }
                else
                {
                    _graph[edge.First].Add(edge.Second);
                    _graph[edge.Second].Add(edge.First);
                }
            }

            Console.WriteLine($"Edges to remove: {removedEdges.Count}");
            foreach (var edge in removedEdges)
            {
                Console.WriteLine(edge);
            }
        }

        private static bool FindShortestPath(string start, string destination)
        {
            var visitedNodes = new HashSet<string>();
            var queue = new Queue<string>();
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

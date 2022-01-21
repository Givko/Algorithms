using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmsExercises.Solutions.Fundamentals.GraphTheoryTraversalAndShortestPath
{
    public class DistanceBetweenVertices
    {
        private static  Dictionary<int, List<int>> _graph = new Dictionary<int, List<int>>();
        private static Dictionary<int,List<int>> _pathsToFind = new Dictionary<int, List<int>>();
        
        public static void ShortestPath()
        {
            ReadGraph();
            foreach (var node in _graph)
            {
                ;
            }
            foreach (var path in _pathsToFind)
            {
                foreach (var destination in path.Value)
                {
                    FindShortestPath(path.Key, destination);
                }
            }
        }

        private static void FindShortestPath(int start, int destination)
        {
            var visitedNodes = new HashSet<int>();
            var queue = new Queue<int>();
            var routes = new Dictionary<int, int>();
            foreach (var point in _graph)
            {
                routes.Add(point.Key, -1);
            }

            queue.Enqueue(start);
            visitedNodes.Add(start);
            while (queue.Count > 0)
            {
                var curNode = queue.Dequeue();
                if (curNode == destination)
                {
                    break;
                }

                foreach (var childNode in _graph[curNode])
                {
                    if (visitedNodes.Contains(childNode))
                    {
                        continue;
                    }

                    routes[childNode] = curNode;
                    queue.Enqueue(childNode);
                    visitedNodes.Add(childNode);
                }
            }

            int distance = 0;
            int node = destination;
            while(node != -1)
            {
                node = routes[node];
                distance++;
                if (node == start)
                {
                    break;
                }
                if (node==-1)
                {
                    distance = -1;
                    break;
                }

            }

            Console.WriteLine($"{{{start}, {destination}}} -> {distance}");
            
        }

        private static void ReadGraph()
        {
            var n = int.Parse(Console.ReadLine());
            var p = int.Parse(Console.ReadLine());

            for (int i = 0; i < n; i++)
            {
                string[] pair = Console.ReadLine().Split(':', StringSplitOptions.RemoveEmptyEntries);
                int startPoint = int.Parse(pair[0]);
                _graph.Add(startPoint, new List<int>());
                if (pair.Length == 1)
                {
                    continue;
                }

                int[] destinations = pair[1]
                    .Split(" ")
                    .Select(int.Parse)
                    .ToArray();

                foreach (var destination in destinations)
                {
                    _graph[startPoint].Add(destination);
                }

            }

            for (int i = 0; i < p; i++)
            {
                int[] pair = Console.ReadLine()
                    .Split("-")
                    .Select(int.Parse)
                    .ToArray();
                if (!_pathsToFind.ContainsKey(pair[0]))
                {
                    _pathsToFind.Add(pair[0], new List<int> { pair[1] });
                    continue;
                }

                _pathsToFind[pair[0]].Add(pair[1]);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wintellect.PowerCollections;

namespace AlgorithmsExercises.Solutions.Advanced
{
    public class Dijsktra
    {
        private class Edge
        {
            public int First { get; set; }
            public int Second { get; set; }
            public int Weight { get; set; }
        }

        private class Distance
        {
            public int PrevNode { get; set; }
            public double DistanceValue { get; set; }
        }

        private static Dictionary<int, List<Edge>> _edges = new Dictionary<int, List<Edge>>();
        private static Dictionary<int, Distance> _distances = new Dictionary<int, Distance>(); 
        
        public static void DijsktraShortestPath()
        {
            ReadGraph();

            var startNode = int.Parse(Console.ReadLine());
            var endNode = int.Parse(Console.ReadLine());

            _distances[startNode].DistanceValue = 0;
            var queue = new OrderedBag<int>(Comparer<int>.Create((f, s) => (int)(_distances[f].DistanceValue - _distances[s].DistanceValue)));
            queue.Add(startNode);

            while (queue.Count > 0)
            {
                var curNode = queue.RemoveFirst();

                if (curNode == endNode)
                {
                    break;
                }

                if (_edges[curNode].Count == 0)
                {
                    break;
                }

                foreach (var childEdge in _edges[curNode])
                {
                    var otherNode = childEdge.First == curNode 
                        ? childEdge.Second 
                        : childEdge.First;

                    if (double.IsPositiveInfinity(_distances[otherNode].DistanceValue))
                    {
                        queue.Add(otherNode);
                    }

                    var newDistance =  _distances[curNode].DistanceValue + childEdge.Weight;
                    if (_distances[otherNode].DistanceValue > newDistance)
                    {
                        _distances[otherNode].DistanceValue = newDistance;
                        _distances[otherNode].PrevNode = curNode;
                    }

                    queue = new OrderedBag<int>(queue, (f, s) => (int)(_distances[f].DistanceValue - _distances[s].DistanceValue));
                }
            }

            var path = new Stack<int>();
            var node = endNode;
            path.Push(node);
            while (node != startNode)
            {
                node = _distances[node].PrevNode;
                path.Push(node);
            }

            Console.WriteLine(string.Join(" ", path));
        }

        public static void ReadGraph()
        {
            var edges = int.Parse(Console.ReadLine());

            for (int i = 0; i < edges; i++)
            {
                var edgeString = Console.ReadLine()
                    .Split(", ")
                    .Select(int.Parse)
                    .ToArray();

                var firstNode = edgeString[0];
                var secondNode = edgeString[1];
                var edge = new Edge
                {
                    First = firstNode,
                    Second = secondNode,
                    Weight = edgeString[2]
                };

                if (!_edges.ContainsKey(firstNode))
                {
                    _edges.Add(firstNode, new List<Edge>());
                }

                if (!_edges.ContainsKey(secondNode))
                {
                    _edges.Add(secondNode, new List<Edge>());
                }

                _edges[firstNode].Add(edge);
                _edges[secondNode].Add(edge);
            }

            foreach (var key in _edges.Keys)
            {
                _distances.Add(
                        key,
                        new Distance
                        {
                            PrevNode = key,
                            DistanceValue = double.PositiveInfinity
                        });
            }
        }
    }
}

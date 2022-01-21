using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmsExercises.Solutions.Fundamentals.GraphTheoryTraversalAndShortestPath
{
    internal class CyclesInAGraph
    {
        private static Dictionary<string,List<string>> _graph = new Dictionary<string, List<string>>();

        public static void IsGraphAcyclic()
        {
            ReadGraph();
            IsGraphAcyclicInernal();
        }

        private static void ReadGraph()
        {
            while(true)
            {
                var pair = Console.ReadLine();
                if (pair.ToLower() == "End".ToLower())
                    break;

                var pairArray = pair.Split('-', StringSplitOptions.RemoveEmptyEntries);
                var startPoint = pairArray[0];
                if (!_graph.ContainsKey(startPoint))
                {
                    _graph.Add(startPoint, new List<string>());
                }
                
                _graph[startPoint].Add(pairArray[1]);
            }
        }

        private static void IsGraphAcyclicInernal()
        {
            try
            {
                foreach (var node in _graph.Keys)
                {
                    DFS(node, node);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }

            Console.WriteLine("Acyclic: Yes");
        }

        private static void DFS(string curStartingPoint, string originalStartingPoint)
        {
            if (!_graph.ContainsKey(curStartingPoint))
            {
                return;
            }

            foreach (var child in _graph[curStartingPoint])
            {
                if (child == originalStartingPoint)
                {
                    throw new InvalidOperationException("Acyclic: No");
                }

                DFS(child, originalStartingPoint);
            }
        }
    }
}

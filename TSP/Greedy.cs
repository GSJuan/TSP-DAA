using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace TSP
{
    internal class Greedy : IAlgorithm
    {
        private int limitTime = 0;
        public Stopwatch limit = new Stopwatch();
        public Greedy(int limitTime)
        {
            this.limitTime = limitTime;
        }
        public List<string> Solve(Graph problem, string origin)
        {
            string startingNode = origin;
            List<string> visited = new List<string>();
            visited.Add(startingNode);
            
            while (visited.Count < problem.nodeQuantity)
            {
                int min = int.MaxValue;
                string minNode = " ";
                List<Transition> possibleWays = problem.GetTransitions(startingNode);
                foreach (Transition path in possibleWays)
                {
                    string destination = path.Node2;
                    
                    if (path.Node1 != startingNode)
                    {
                        destination = path.Node1;
                    }

                    if (!visited.Contains(destination))
                    {
                        int cost = path.Cost;
                        if (cost < min)
                        {
                            min = cost;
                            minNode = destination;
                        }
                    }
                }
                visited.Add(minNode);
                startingNode = minNode;
            }
            visited.Add(origin);
            return visited;
        }
    }
}

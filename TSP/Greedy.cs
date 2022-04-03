using System;
using System.Collections.Generic;
using System.Text;

namespace TSP
{
    internal class Greedy : IAlgorithm
    {
        
        public List<char> Solve(Graph problem, char origin)
        {
            char startingNode = origin;
            List<char> visited = new List<char>();
            visited.Add(startingNode);
            
            while (visited.Count < problem.nodeQuantity)
            {
                int min = int.MaxValue;
                char minNode = ' ';
                List<Transition> possibleWays = problem.GetTransitions(startingNode);
                foreach (Transition path in possibleWays)
                {
                    char destination = path.Node2;
                    
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

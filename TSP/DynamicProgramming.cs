using System;
using System.Collections.Generic;
using System.Text;

namespace TSP
{
    internal class DynamicProgramming : IAlgorithm
    {
        public List<string> visited = new List<string>();
        public int cost = 0;
        public List<string> Solve(Graph problem, string startingNode)
        {
            visited.Add(startingNode);

            string nextCity = NextCity(problem, startingNode);

            if (nextCity == "end")
            {
                visited.Add(visited[0]);
                return visited;
            }
            return visited;
        }

        public string NextCity(Graph problem, string currentCity)
        {
            string nextCity = "end";
            int minCost = int.MaxValue;
            foreach (string city in problem.nodes)
            {
                if (city != currentCity && !visited.Contains(city))
                {
                    int cost = problem.GetCost(currentCity, city);
                    if (cost < minCost)
                    {
                        minCost = cost;
                        nextCity = city;
                    }
                }
            }
            return nextCity;
        }
    }
}

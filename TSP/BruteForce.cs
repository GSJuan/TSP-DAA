using System;
using System.Collections.Generic;
using System.Text;

namespace TSP
{
    internal class BruteForce : IAlgorithm
    {
        private List<string> minPath = new List<string>();
        private string startingNode;
        private Graph graph;
        private int minCost = int.MaxValue;
        public List<string> Solve(Graph problem, string startingNode)
        {
            this.graph = problem;
            this.startingNode = startingNode;
            List<string> nodes = new List<string>(graph.nodes);
            nodes.Remove(startingNode);
            
            permute(nodes, 0, nodes.Count - 1);

            return minPath;
        }

        private void permute(List<string> nodes, int left, int right)
        {
            if (left == right)
            {
                nodes.Insert(0, this.startingNode);
                nodes.Add(this.startingNode);
                int cost = this.graph.GetPathCost(nodes);

                if (cost < this.minCost)
                {
                    this.minCost = this.graph.GetPathCost(nodes);
                    this.minPath = new List<string>(nodes);
                }

                nodes.RemoveAt(0);
                nodes.RemoveAt(nodes.Count - 1);
            }
            else
            {
                for (int i = left; i <= right; i++)
                {
                    swap(ref nodes, left, i);
                    permute(nodes, left + 1, right);
                    swap(ref nodes, left, i);
                }
            }
        }

        private void swap(ref List<string> nodes, int i, int j)
        {
            (nodes[j], nodes[i]) = (nodes[i], nodes[j]);
        }
    }
}

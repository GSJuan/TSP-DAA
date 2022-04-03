using System;
using System.Collections.Generic;
using System.Text;

namespace TSP
{
    internal class BruteForce : IAlgorithm
    {
        private List<char> minPath = new List<char>();
        private char startingNode;
        private Graph graph;
        private int minCost = int.MaxValue;
        public List<char> Solve(Graph problem, char startingNode)
        {
            this.graph = problem;
            this.startingNode = startingNode;
            List<char> nodes = new List<char>(graph.nodes);
            nodes.Remove(startingNode);
            
            permute(nodes, 0, nodes.Count - 1);

            return minPath;
        }

        private void permute(List<char> nodes, int left, int right)
        {
            if (left == right)
            {
                nodes.Insert(0, this.startingNode);
                nodes.Add(this.startingNode);
                int cost = this.graph.GetPathCost(nodes);

                if (cost < this.minCost)
                {
                    this.minCost = this.graph.GetPathCost(nodes);
                    this.minPath = new List<char>(nodes);
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

        private void swap(ref List<char> nodes, int i, int j)
        {
            (nodes[j], nodes[i]) = (nodes[i], nodes[j]);
        }
    }
}

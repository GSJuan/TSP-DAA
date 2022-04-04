using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace TSP
{
    internal class BruteForce : IAlgorithm
    {
        public Stopwatch limit = new Stopwatch();
        private int timeLimit = 300000;
        
        private List<string> minPath = new List<string>();
        private string startingNode;
        private Graph graph;
        private int minCost = int.MaxValue;

        public bool excesive = false;

        public BruteForce(int msLimit)
        {
            this.timeLimit = msLimit;
        }
        public List<string> Solve(Graph problem, string startingNode)
        {
            this.graph = problem;
            this.startingNode = startingNode;
            List<string> nodes = new List<string>(graph.nodes);
            nodes.Remove(startingNode);
            limit.Restart();
            permute(nodes, 0, nodes.Count - 1);
            limit.Stop();
            return minPath;
        }

        private void permute(List<string> nodes, int left, int right)
        {
            if (limit.ElapsedMilliseconds > timeLimit)
            {
                this.excesive = true;
                return;
            }
            
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

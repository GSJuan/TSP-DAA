using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TSP
{
    public class Graph
    {

        public List<Transition> transitions = new List<Transition>();
        public List<string> nodes = new List<string>();
        public int nodeQuantity = 0;

        public Graph(string filePath)
        {

            string[] lines = File.ReadAllLines(filePath);
            
            nodeQuantity = int.Parse(lines[0]);
            //Console.Write(nodeQuantity);
            for (int i = 1; i < lines.Length; i++)
            {
                string[] line = lines[i].Split(' ');
                string nodeA = line[0];
                string nodeB = line[1];
                int cost = int.Parse(line[2]);

                if (!nodes.Contains(nodeA))
                {
                    nodes.Add(nodeA);
                }
                
                if (!nodes.Contains(nodeB))
                {
                    nodes.Add(nodeB);
                }
                
                //Console.Write(nodeA + " " + nodeB + " " + cost + "\n");
                transitions.Add(new Transition(nodeA, nodeB, cost));
            }

        }

        public int GetCost(string nodeA, string nodeB)
        {
            foreach (Transition transition in transitions)
            {
                if ((transition.Node1 == nodeA && transition.Node2 == nodeB) || (transition.Node2 == nodeA && transition.Node1 == nodeB))
                {
                    return transition.Cost;
                }
            }
            return -1;
        }

        public int GetPathCost(List<string> path)
        {
            int cost = 0;
            for (int i = 0; i < path.Count - 1; i++)
            {
                cost += GetCost(path[i], path[i + 1]);
            }
            return cost;
        }

        public List<Transition> GetTransitions(string node)
        {
            List<Transition> transitions = new List<Transition>();
            foreach (Transition transition in this.transitions)
            {
                if (transition.Node1 == node || transition.Node2 == node)
                {
                    transitions.Add(transition);
                }
            }
            return transitions;
        }

        public int GetDistance(string nodeA, string nodeB)
        {
            foreach (Transition transition in transitions)
            {
                if ((transition.Node1 == nodeA && transition.Node2 == nodeB) || (transition.Node2 == nodeA && transition.Node1 == nodeB))
                {
                    return transition.Cost;
                }
            }
            return -1;
        }
    }
}

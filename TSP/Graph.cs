using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TSP
{
    public class Graph
    {

        public List<Transition> transitions = new List<Transition>();
        public int nodeQuantity = 0;

        public Graph(string filePath)
        {

            string[] lines = File.ReadAllLines(filePath);
            Console.WriteLine("Reading file...");
            
            nodeQuantity = int.Parse(lines[0]);
            Console.Write(nodeQuantity);
            for (int i = 1; i < lines.Length; i++)
            {
                string[] line = lines[i].Split(' ');
                char nodeA = char.Parse(line[0]);
                char nodeB = char.Parse(line[1]);
                int cost = int.Parse(line[2]);
                Console.Write(nodeA + " " + nodeB + " " + cost + "\n");
                transitions.Add(new Transition(nodeA, nodeB, cost));
            }

        }
    }
}

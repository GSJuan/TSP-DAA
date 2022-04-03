using System;
using System.Collections.Generic;
using System.Text;

namespace TSP
{
    public class InstanceGenerator
    {
        public List<List<string>> graph;

        public List<char> letters = new List<char>() { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };

        public InstanceGenerator ()
        {
                     
        }
        
        public void Generate (int nodes)
        {
            Console.WriteLine("Generating instance of size {0}...", nodes);
            Random rnd = new Random();
            int numberOfTransitions = (nodes * (nodes - 1)) / 2;
            string fileName = nodes.ToString() + "_nodes.txt";
            string filePath = @"C:\Users\Juan\source\repos\TSP-DAA\TSP\problems\";
            System.IO.StreamWriter file = new System.IO.StreamWriter(filePath + fileName);
            List<string> nodeNames = new List<string>();
            int offset = (nodes / letters.Count) + 1;

            for (int i = 1; i <= offset; i++)
            {
                int limit = nodes;
                if (i == offset)
                {
                    limit = nodes - (letters.Count * (i - 1));
                }

                for (int j = 0; j < limit; j++)
                {
                    nodeNames.Add(new String(letters[j], i));
                }
            }
            file.WriteLine(nodes.ToString());
            for (int i = 0; i < nodeNames.Count; i++)
            {
                string node1 = nodeNames[i];
                for (int j = i + 1; j < nodeNames.Count; j++)
                {
                    string node2 = nodeNames[j];
                    int cost = rnd.Next(1, 100);
                    file.WriteLine(node1 + " " + node2 + " " + cost.ToString());
                }
            }
            file.Close();
            
            Console.WriteLine("Instance written to file {0}", fileName);
        }
    }
}

using System;

namespace TSP
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            string filePath = @"C:\Users\Juan\source\repos\TSP-DAA\TSP\4_nodos.txt";
            Graph problem = new Graph(filePath);
            TableDrawing table = new TableDrawing(200);
            table.PrintLine();
            table.PrintRow("Instance", "BruteForce Value", "BruteForce Time", "Greedy Value", "Greedy Time", "Dynamic Programming Value", "Dynamic Programming Time");
            table.PrintLine();
        }
    }
}

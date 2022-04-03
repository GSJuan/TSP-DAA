using System;
using System.Collections.Generic;
using System.Diagnostics;

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
            Stopwatch chrono = new Stopwatch();
            
            table.PrintLine();
            table.PrintRow("Instance", "BruteForce Value", "BruteForce Time", "Greedy Value", "Greedy Time", "Dynamic Programming Value", "Dynamic Programming Time");
            table.PrintLine();

            Greedy greed = new Greedy();
            chrono.Restart();
            List<char> greedMinPath = greed.Solve(problem, 'A');
            chrono.Stop();
            var elapsedMsGreedy = chrono.Elapsed;
            int greedCost = problem.GetPathCost(greedMinPath);

            BruteForce brute = new BruteForce();
            chrono.Restart();
            List<char> bruteMinPath = brute.Solve(problem, 'A');
            chrono.Stop();
            var elapsedMsBrute = chrono.Elapsed;
            int bruteCost = problem.GetPathCost(bruteMinPath);
            
            table.PrintRow("4 nodos", bruteCost.ToString(), elapsedMsBrute.ToString(), greedCost.ToString(), elapsedMsGreedy.ToString(), "0", "0");

        }
    }
}

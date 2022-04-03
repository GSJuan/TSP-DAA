using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace TSP
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hi!, please enter the complete path of the folder containing the input problems");
            string folderPath = @""; // C:\Users\Juan\source\repos\TSP-DAA\TSP\problems
            folderPath += Console.ReadLine();

            List<string> filesPaths = new List<string>();
            foreach (string filePath in Directory.GetFiles(folderPath, "*.txt"))
            {
                    filesPaths.Add(filePath);
            }          

            TableDrawing table = new TableDrawing(200);
            Stopwatch chrono = new Stopwatch();
            
            table.PrintLine();
            table.PrintRow("Instance", "BruteForce Value", "BruteForce Time", "Greedy Value", "Greedy Time", "Dynamic Programming Value", "Dynamic Programming Time");
            table.PrintLine();

            foreach(string filePath in filesPaths)
            {
                string filename = filePath.LastIndexOf('\\') != -1 ? filePath.Substring(filePath.LastIndexOf('\\') + 1) : filePath;
                Graph problem = new Graph(filePath);

                Greedy greed = new Greedy();
                chrono.Restart();
                List<string> greedMinPath = greed.Solve(problem, "A");
                chrono.Stop();
                var elapsedMsGreedy = chrono.Elapsed;
                int greedCost = problem.GetPathCost(greedMinPath);

                BruteForce brute = new BruteForce();
                chrono.Restart();
                List<string> bruteMinPath = brute.Solve(problem, "A");
                chrono.Stop();
                var elapsedMsBrute = chrono.Elapsed;
                int bruteCost = problem.GetPathCost(bruteMinPath);

                table.PrintRow(filename, bruteCost.ToString(), elapsedMsBrute.ToString(), greedCost.ToString(), elapsedMsGreedy.ToString(), "0", "0");
                table.PrintLine();
            }
        }
    }
}

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

            Console.Write("Hi, choose 0 if you wish to solve all existing tsp problems in problems folder, or 1 if you wish to generate a new instance:");
            int choice = Convert.ToInt32(Console.ReadLine());
            if (choice == 0)
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

                foreach (string filePath in filesPaths)
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

                    DynamicProgramming dynamic = new DynamicProgramming();
                    chrono.Restart();
                    List<string> dynamicMinPath = dynamic.Solve(problem, "A");
                    chrono.Stop();
                    var elapsedMsDynamic = chrono.Elapsed;
                    int dynamicCost = problem.GetPathCost(bruteMinPath);

                    table.PrintRow(filename, bruteCost.ToString(), elapsedMsBrute.ToString(), greedCost.ToString(), elapsedMsGreedy.ToString(), dynamicCost.ToString(), elapsedMsDynamic.ToString());
                    table.PrintLine();
                }
            }
            else if (choice == 1)
            {
                Console.Write("Write the number of nodes in the graph you wish to generate:");
                int nodes = Convert.ToInt32(Console.ReadLine());
                if(nodes > 0)
                {
                    InstanceGenerator generator = new InstanceGenerator();
                    generator.Generate(nodes);
                }
                else Console.WriteLine("Wrong input, that was not a number > 0");


            }

            else Console.WriteLine("Wrong input");

        }
    }
}

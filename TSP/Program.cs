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
            const int TIME_LIMIT = 300000;

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

                    Greedy greed = new Greedy(TIME_LIMIT);
                    chrono.Restart();
                    List<string> greedMinPath = greed.Solve(problem, "A");
                    chrono.Stop();
                    var elapsedMsGreedy = chrono.Elapsed;
                    int greedCost = problem.GetPathCost(greedMinPath);

                    BruteForce brute = new BruteForce(TIME_LIMIT);
                    chrono.Restart();
                    List<string> bruteMinPath = brute.Solve(problem, "A");
                    chrono.Stop();
                    var elapsedMsBrute = chrono.Elapsed;
                    int bruteCost = problem.GetPathCost(bruteMinPath);

                    DynamicProgramming dynamic = new DynamicProgramming(TIME_LIMIT);
                    chrono.Restart();
                    List<string> dynamicMinPath = dynamic.Solve(problem, "A");
                    chrono.Stop();
                    var elapsedMsDynamic = chrono.Elapsed;
                    //int dynamicCost = problem.GetPathCost(dynamicMinPath);

                    string greedyTime = elapsedMsGreedy.ToString();

                    string bruteTime = elapsedMsBrute.ToString();
                    if(brute.excesive)
                    {
                        bruteTime = "Excesive";
                    }

                    string dynamicTime = elapsedMsDynamic.ToString();
                    if (dynamic.excesive)
                    {
                        dynamicTime = "Excesive";
                    }

                    table.PrintRow(filename, bruteCost.ToString(), bruteTime, greedCost.ToString(), greedyTime, dynamicMinPath[0], dynamicTime);
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

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace TSP
{
    internal class DynamicProgramming : IAlgorithm
    {
        private int timeLimit = 0;
        public bool excesive = false;
        public Stopwatch limit = new Stopwatch();
        
        int nodes;              // the number of nodes
        int setSize;        // the nodes^2
        float[,] adjacency;     // the adjacency matrix
        float[][,] memo;       // the dynamic programming memoised results array
        int[] S;            // index of set position within sets array
        int[][] sets;       // jagged array of sets by cardinality(number of 1's)
                            // (subproblems are ordered by increasing cardinality)
        int[] cardCount;    // a count of the cardinality of each set
        
        public DynamicProgramming(int limitTime)
        {
            this.timeLimit = limitTime;
        }

        
        public List<string> Solve(Graph problem, string startingNode)
        {
           
            this.nodes = problem.nodeQuantity;
            this.adjacency = problem.adjacencyMatrix;
            this.setSize = (int)Math.Pow(2, nodes - 1);
            S = new int[setSize];
            sets = new int[nodes - 1][];
            cardCount = new int[nodes - 1];
            memo = new float[2][,];

            int c;
            for (int i = 0; i < nodes - 1; i++)
            {
                c = Combinations(nodes - 1, i + 1);
                sets[i] = new int[c];
            }

            // populate S, sets and cardCount arrays
            // cardCount maintains a running count during this routine
            byte t;
            for (int i = 1; i < setSize; i++)
            {
                t = Cardinality(i);
                S[i] = cardCount[t - 1];
                sets[t - 1][cardCount[t - 1]++] = i;
            }
            List<string> result = new List<string>();
            result.Add(Dynamic().ToString());
            return result;
        }

        public float Dynamic()
        {

            // create A subarrays, one for each subproblem size / cardinality
            // A is memoised so only two sets of subproblems, past and present, are maintained
            // the subarrays are 2D, storing the number of possible combinations of subproblems
            // by the number of possible destinations
            int c;
            for (int i = 0; i < 2; i++)
            {
                c = Combinations(nodes - 1, i + 1);
                memo[i] = new float[c, i + 1];
            }

            int n, x, countj, countk;
            float z = 0;

            // initialise A with distances from source city to each destination city
            for (int j = 0; j < cardCount[0]; j++)
                memo[0][j, 0] = adjacency[0, j + 1];

            // main loop iterates through increasing subproblem sizes
            for (int m = 1; m < nodes - 1; m++)
            {
                // second loop iterates through each subproblem set
                for (int s = 0; s < cardCount[m]; s++)
                {
                    x = sets[m][s];
                    countj = 0;

                    // third loop iterates through each destination
                    for (int j = 0; j < nodes - 1; j++)
                        if (((x >> j) & 1) == 1) // only continue if j is a member of x
                        {
                            countk = 0;
                            z = float.PositiveInfinity;
                            for (int k = 0; k < nodes - 1; k++)
                            {
                                if (limit.ElapsedMilliseconds > this.timeLimit)
                                {
                                    this.excesive = true;
                                    return -1;
                                }
                                if ((((x >> k) & 1) == 1) && j != k) // only continue if k is
                                {                                    // a member of x and is not j
                                        // n is the set x excluding j
                                        n = S[x - (1 << j)];
                                        // z is the previous subproblem size's shortest path length to j via k
                                        z = Math.Min(z, memo[(m - 1) % 2][n, countk++] + adjacency[k + 1, j + 1]);
                                }
                            }                                                            
                                
                            memo[m % 2][s, countj++] = z; // set the shortest path length for the current
                        }                              // subproblem size and destination
                }

                // after an iteration of subproblem sizes has completed
                // replace the previous 2D array of subproblem solutions
                // with a new array of appropriate size
                c = Combinations(nodes - 1, m + 2);
                memo[(m + 1) % 2] = null;
                memo[(m + 1) % 2] = new float[c, m + 2];
            }

            // having computed shortest paths to each destination, visiting every other city precisely once
            // determine the shortest round-trip tour by adding a trip back to the source city
            z = float.PositiveInfinity;
            for (int j = 0; j < nodes - 1; j++)
            {
                z = Math.Min(z, memo[(nodes - 2) % 2][0, j] + adjacency[j + 1, 0]);
            }

            return z;

        }

        // determine the number of 1s in the binary representation of the input
        byte Cardinality(int i)
        {
            byte count = 0;
            while (i > 0)
            {
                count += (byte)(i & 1);
                i >>= 1;
            }
            return count;
        }

        // compute the number of possible combinations, n choose k
        // minimizing the potential for arithmetical overflow
        public int Combinations(int n, int k)
        {
            long r = 1;
            for (int i = 0; i < k; i++)
            {
                r *= (n - i);
                r /= (i + 1);
            }
            return (int)r;
        }        
    }
}

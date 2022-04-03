using System;
using System.Collections.Generic;
using System.Text;

namespace TSP
{
    public interface IAlgorithm
    {
        List<string> Solve(Graph problem, string startingNode);
    }
}
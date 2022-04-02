using System;
using System.Collections.Generic;
using System.Text;

namespace TSP
{
    public interface IAlgorithm
    {
        List<int> Solve(Graph problem);
    }
}
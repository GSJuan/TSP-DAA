using System;
using System.Collections.Generic;
using System.Text;

namespace TSP
{
    public class Transition
    {
        public string Node1 { get; set; }
        public int Cost { get; set; }
        public string Node2 { get; set; }
        public Transition(string nodeA, string nodeB, int cost)
        {
            Node1 = nodeA;
            Cost = cost;
            Node2 = nodeB;
        }
    }
    
}

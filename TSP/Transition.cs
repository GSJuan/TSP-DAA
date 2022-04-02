using System;
using System.Collections.Generic;
using System.Text;

namespace TSP
{
    public class Transition
    {
        public char Node1 { get; set; }
        public int Cost { get; set; }
        public char Node2 { get; set; }
        public Transition(char nodeA, char nodeB, int cost)
        {
            Node1 = nodeA;
            Cost = cost;
            Node2 = nodeB;
        }
    }
    
}

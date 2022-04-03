using System;
using System.Collections.Generic;
using System.Text;

namespace TSP
{
    public class InstanceGenerator
    {
        public List<List<string>> graph;

        public InstanceGenerator (int nodes)
        {
            Random rnd = new Random();
            


        }
        public void WriteFile(string filename)
        {
            System.IO.StreamWriter file = new System.IO.StreamWriter(filename);
            file.WriteLine("NAME : " + filename);
           
            file.Close();
        } 
    }
}

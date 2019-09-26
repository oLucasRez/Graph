using System;

namespace Graph
{
    class Program
    {
        static void Main(string[] args)
        {
            DirectedGraph<char> graph = new DirectedGraph<char>();
            graph.AddNode('a');
            graph.AddNode('b');
            graph.AddNode('c');
            graph.AddNode('a');
            graph.AddNode('e');
            graph.AddNode('g');
            graph.AddNode('f');
            graph.AddNode('d');
            graph.AddArc('a', 'c', 1);
            graph.AddArc('f', 'd', 1);
            graph.AddArc('g', 'e', 1);
            graph.AddArc('a', 'b', 1);
            graph.AddArc('c', 'a', 1);
            graph.AddArc('b', 'g', 1);
            graph.AddArc('d', 'f', 1);
            graph.AddArc('a', 'f', 1);
            var dfs = graph.DFS('d');
            //foreach(var i in dfs)
            //{
            //    foreach (var j in i) Console.Write(j + ", ");
            //    Console.WriteLine("-");
            //}
        }
    }
}

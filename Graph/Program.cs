using System;
using System.Collections.Generic;

namespace Graph
{
    class Program
    {
        static void Main(string[] args)
        {
            DirectedGraph<int> graph = new DirectedGraph<int>();
            graph.AddNode(1);
            graph.AddNode(2);
            graph.AddNode(3);
            graph.AddNode(4);
            graph.AddNode(5);
            graph.AddNode(6);
            graph.AddArc(1, 2, 7.0);
            graph.AddArc(1, 3, 8.0);
            graph.AddArc(2, 1, 3.0);
            graph.AddArc(2, 5, 4.0);
            graph.AddArc(2, 6, 8.0);
            graph.AddArc(3, 5, 10.0);
            graph.AddArc(4, 3, 1.0);
            graph.AddArc(5, 4, 9.0);
            graph.AddArc(6, 4, 5.0);
            var dij = graph.Dijkstra(3);
            foreach (var key in dij.Keys)
            {
                Console.WriteLine(key + " > " + dij[key].Item1 + " " + dij[key].Item2);
            }
            //var bfs = graph.BFS(5);
            //foreach(var key in bfs.Keys)
            //{
            //    Console.WriteLine(key + " > " + bfs[key].Item1 + " " + bfs[key].Item2);
            //}

            //graph.AddNode("cueca");
            //graph.AddNode("calça");
            //graph.AddNode("tênis");
            //graph.AddNode("cinto");
            //graph.AddNode("meia");
            //graph.AddNode("desodorante");
            //graph.AddNode("camisa");
            //graph.AddNode("óculos");
            //graph.AddNode("cabelo");
            //graph.AddArc("cueca", "calça", 1);
            //graph.AddArc("calça", "tênis", 1);
            //graph.AddArc("calça", "cinto", 1);
            //graph.AddArc("meia", "tênis", 1);
            //graph.AddArc("desodorante", "camisa", 1);
            //graph.AddArc("camisa", "óculos", 1);
            //graph.AddArc("camisa", "cabelo", 1);
            //Stack<string> stack = graph.Topographic();
            //while (stack.Count != 0) Console.WriteLine(stack.Pop());
        }
    }
}

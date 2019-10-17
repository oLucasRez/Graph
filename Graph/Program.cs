using System;
using System.Collections.Generic;
using System.Linq;

namespace Graph
{
    class Program
    {
        static void Main(string[] args)
        {
            int mudRoads = 0;
            int nCases = int.Parse(Console.ReadLine());
            for (int i = 1; i <= nCases; i++)
            {
                UndirectedGraph<int> graph = new UndirectedGraph<int>();
                int nV = int.Parse(Console.ReadLine());
                int nE = int.Parse(Console.ReadLine());
                for (int j = 1; j <= nV; j++) graph.AddNode(j);
                for (int j = 0; j < nE; j++)
                {
                    string[] edge = Console.ReadLine().Split();
                    graph.AddEdge(int.Parse(edge[0]), int.Parse(edge[1]));
                }
                var dfs = graph.DFS();
                int nIslands = 0;
                List<int> visit = new List<int>();
                foreach (var list in dfs)
                {
                    Func<bool> contain = () =>
                    {
                        foreach (var visited in visit)
                        {
                            if (list.Contains(visited)) return true;
                        }
                        return false;
                    };
                    if (list.Count < nV && !contain())
                    {
                        nIslands++;
                        visit.Add(list.First());
                    }
                }
                mudRoads = nIslands - 1;
                if (mudRoads == -1) Console.WriteLine("Caso #" + i + ": a promessa foi cumprida");
                else Console.WriteLine("Caso #" + i + ": ainda falta(m) " + mudRoads + " estrada(s)");
            }
        }
    }
}

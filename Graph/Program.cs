using System;
using System.Collections.Generic;
using System.Linq;

namespace Graph
{
    public class Graph<T>
    {
        protected Dictionary<T, List<Node>> LdA = new Dictionary<T, List<Node>>();
        protected int nVertex = 0;

        protected class Node
        {
            public T content;
            public double weight;
            public Node(T value, double weight)
            {
                content = value;
                this.weight = weight;
            }
        }

        public void AddNode(T value)
        {
            foreach (T key in LdA.Keys) if (key.Equals(value)) return;
            LdA.Add(value, new List<Node>());
            nVertex++;
        }

        Dictionary<T, bool> open;
        public List<T> DFS(T start)
        {
            open = new Dictionary<T, bool>();
            List<List<T>> lists = new List<List<T>>();
            foreach (T k in LdA.Keys) open.Add(k, true);
            return DFSVisit(start, null);
        }

        List<T> DFSVisit(T current, List<T> list)
        {
            open[current] = false;
            if (list == null) list = new List<T>();
            list.Add(current);
            foreach (Node next in LdA[current])
                if (open[next.content]) DFSVisit(next.content, list);
            open[current] = false;
            return list;
        }

        public List<List<T>> DFS()
        {
            List<List<T>> lists = new List<List<T>>();
            foreach (T key in LdA.Keys) lists.Add(DFS(key));
            return lists;
        }

        public void AddEdge(T value1, T value2)
        {
            if (LdA.ContainsKey(value1) && LdA.ContainsKey(value2))
            {
                foreach (Node node in LdA[value1]) if (node.content.Equals(value2)) return;
                LdA[value1].Add(new Node(value2, 1));
                LdA[value2].Add(new Node(value1, 1));
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            int mudRoads = 0;
            int nCases = int.Parse(Console.ReadLine());
            for (int i = 1; i <= nCases; i++)
            {
                Graph<int> graph = new Graph<int>();
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Graph
{
    //public class Graph<T>
    //{
    //    protected Dictionary<T, List<Node>> LdA = new Dictionary<T, List<Node>>();
    //    protected int nVertex = 0;

    //    protected class Node
    //    {
    //        public T content;
    //        public double weight;
    //        public Node(T value, double weight)
    //        {
    //            content = value;
    //            this.weight = weight;
    //        }
    //    }

    //    public void AddNode(T value)
    //    {
    //        foreach (T key in LdA.Keys) if (key.Equals(value)) return;
    //        LdA.Add(value, new List<Node>());
    //        nVertex++;
    //    }

    //    Dictionary<T, bool> open;
    //    public List<T> DFS(T start)
    //    {
    //        open = new Dictionary<T, bool>();
    //        List<List<T>> lists = new List<List<T>>();
    //        foreach (T k in LdA.Keys) open.Add(k, true);
    //        return DFSVisit(start, null);
    //    }

    //    List<T> DFSVisit(T current, List<T> list)
    //    {
    //        open[current] = false;
    //        if (list == null) list = new List<T>();
    //        list.Add(current);
    //        foreach (Node next in LdA[current])
    //            if (open[next.content]) DFSVisit(next.content, list);
    //        open[current] = false;
    //        return list;
    //    }

    //    public List<List<T>> DFS()
    //    {
    //        List<List<T>> lists = new List<List<T>>();
    //        foreach (T key in LdA.Keys) lists.Add(DFS(key));
    //        return lists;
    //    }

    //    public void AddEdge(T value1, T value2)
    //    {
    //        if (LdA.ContainsKey(value1) && LdA.ContainsKey(value2))
    //        {
    //            foreach (Node node in LdA[value1]) if (node.content.Equals(value2)) return;
    //            LdA[value1].Add(new Node(value2, 1));
    //            LdA[value2].Add(new Node(value1, 1));
    //        }
    //    }
    //}

    class Program
    {
        static void Main(string[] args)
        {
            int col = int.Parse(Console.ReadLine());
            int row = int.Parse(Console.ReadLine());
            char[][] matrix = new char[row][];
            for (int i = 0; i < row; i++)
                matrix[i] = Console.ReadLine().ToCharArray();
            Dictionary<char, Vector2> direction = new Dictionary<char, Vector2>(){
                {'^', new Vector2(-1, 0) },
                {'v', new Vector2(1, 0) },
                {'>', new Vector2(0, 1) },
                {'<', new Vector2(0, -1) }
            };
            Vector2 me = new Vector2(0, 0);
            char c = matrix[(int)me.X][(int)me.Y];
            List<Vector2> visited = new List<Vector2>();
            char lastDirect = '.';
            char resp = ' ';
            while (c != '*')
            {
                if (c != '.')
                {
                    lastDirect = c;
                    var a = new Vector2(me.X, me.Y);
                    if (visited.Where(x => x.X == a.X && x.Y == a.Y).Count() == 0)
                    {
                        visited.Add(a);
                    }
                    else
                    {
                        resp = '!';
                        break;
                    }
                    me.X += direction[c].X;
                    me.Y += direction[c].Y;
                }
                else
                {
                    me.X += direction[lastDirect].X;
                    me.Y += direction[lastDirect].Y;
                }
                if(me.X < 0 || me.X >= row || me.Y < 0 || me.Y >= col)
                {
                    resp = '!';
                    break;
                }
                c = matrix[(int)me.X][(int)me.Y];
            }
            if (resp == ' ') Console.WriteLine("*");
            else Console.WriteLine(resp);
        }
    }
}

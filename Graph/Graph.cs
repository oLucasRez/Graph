using System;
using System.Collections.Generic;
using System.Text;

namespace Graph
{
    public abstract class Graph<T>
    {
        protected Dictionary<T, List<Node>> LdA = new Dictionary<T, List<Node>>();
        //protected Dictionary<T, color> visit = new Dictionary<T, color>();
        protected enum color { white, grey, black }

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
            //visit.Add(value, color.white);
        }

        public abstract void AddEdge(T value1, T value2, double weight);
        public abstract void AddArc(T value1, T value2, double weight);

        public List<List<T>> DFS(T origin)
        {
            Dictionary<T, color> visit;
            List<List<T>> lists = new List<List<T>>();
            
                visit = new Dictionary<T, color>();
                foreach (T k in LdA.Keys) visit.Add(k, color.white);
                //Console.WriteLine(origin);
                //if (visit[origin] == color.white)
                    lists.Add(DFSVisit(origin, null));
            
            return lists;

            List<T> DFSVisit(T current, List<T> list)
            {
                visit[current] = color.grey;
                if (list == null) list = new List<T>();
                list.Add(current);
                Console.WriteLine("in " + current);
                foreach (Node next in LdA[current])
                {
                    if (visit[next.content] == color.white) DFSVisit(next.content, list);
                }
                visit[current] = color.black;
                Console.WriteLine("out " + current);
                return list;
            }
        }


        public T DFS(Predicate<T> predicate)
        {
            Dictionary<T, color> visit = new Dictionary<T, color>();
            foreach (T key in LdA.Keys) visit.Add(key, color.white);
            T found = default;
            foreach (T key in LdA.Keys)
            {
                if (visit[key] == color.white)
                    found = DFSVisit(key, predicate);
                if (found != default) return found;
            }
            return default;

            T DFSVisit(T current, Predicate<T> pred)
            {
                visit[current] = color.grey;
                foreach (Node next in LdA[current])
                {
                    if (visit[next.content] == color.white) DFSVisit(next.content, pred);
                }
                visit[current] = color.black;
                if (pred(current)) return current;
                else return default;
            }
        }


        public void BFS(Predicate<T> predicate)
        {

        }
    }
    public class UndirectedGraph<T> : Graph<T>
    {
        public override void AddEdge(T value1, T value2, double weight)
        {
            if (LdA.ContainsKey(value1) && LdA.ContainsKey(value2))
            {
                foreach (Node node in LdA[value1]) if (node.content.Equals(value2)) return;
                LdA[value1].Add(new Node(value1, weight));
                LdA[value2].Add(new Node(value2, weight));
            }
        }

        public override void AddArc(T value1, T value2, double weight)
        {
            throw new NotImplementedException();
        }
    }

    public class DirectedGraph<T> : Graph<T>
    {
        public override void AddArc(T from, T to, double weight)
        {
            if (LdA.ContainsKey(from) && LdA.ContainsKey(to))
            {
                foreach (Node node in LdA[from]) if (node.content.Equals(to)) return;
                LdA[from].Add(new Node(to, weight));
            }
        }

        public override void AddEdge(T value1, T value2, double weight)
        {
            throw new NotImplementedException();
        }
    }
}

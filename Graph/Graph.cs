using System;
using System.Collections.Generic;
using System.Text;

namespace Graph
{
    public abstract class Graph<T>
    {
        protected Dictionary<T, List<Node>> LdA = new Dictionary<T, List<Node>>();
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
        }

        public abstract void AddEdge(T value1, T value2, double weight);
        public abstract void AddArc(T value1, T value2, double weight);

        public List<T> DFS(T origin)
        {
            Dictionary<T, color> visit = new Dictionary<T, color>();
            List<List<T>> lists = new List<List<T>>();
            foreach (T k in LdA.Keys) visit.Add(k, color.white);
            return DFSVisit(origin, null);
            List<T> DFSVisit(T current, List<T> list)
            {
                visit[current] = color.grey;
                if (list == null) list = new List<T>();
                list.Add(current);
                foreach (Node next in LdA[current])
                    if (visit[next.content] == color.white) DFSVisit(next.content, list);
                visit[current] = color.black;
                return list;
            }
        }

        public List<List<T>> DFS()
        {
            List<List<T>> lists = new List<List<T>>();
            foreach (T key in LdA.Keys) lists.Add(DFS(key));
            return lists;
        }

        public void BestWay(T start, Dictionary<T, T> anterior, Dictionary<T, double> distance)
        {
            int cont = LdA.Count, nVertex = LdA.Count;
            bool[] visit = new bool[nVertex];
            anterior = new Dictionary<T, T>();
            distance = new Dictionary<T, double>();
            int u, v;
            //for (int i = 0; i < nVertex; i++)
            //{
            //    anterior[i] = default;
            //    distance[i] = -1;
            //    visit[i] = false;
            //}
            distance[start] = 0;
            while (cont != 0)
            {
                u = BestWaySearch(distance);
                cont--;
                foreach
            }

            int BestWaySearch(double[] dist){
                bool first = true;
                int shortest = -1;
                for(int i = 0; i < nVertex; i++)
                {
                    if(dist[i] >= 0 && !visit[i])
                    {
                        if (first)
                        {
                            shortest = i;
                            first = false;
                        }
                        else
                        {
                            if (dist[shortest] > dist[i])
                                shortest = i;
                        }
                    }
                }
                return shortest;
            }
        }



        //public T DFS(Predicate<T> predicate)
        //{
        //    T result = default;
        //    foreach(T key in LdA.Keys)
        //    {
        //        Dictionary<T, color> visit = new Dictionary<T, color>();
        //        List<List<T>> lists = new List<List<T>>();
        //        foreach (T k in LdA.Keys) visit.Add(k, color.white);
        //        result = DFSVisit(key, null);
        //        T DFSVisit(T current, List<T> list)
        //        {
        //            visit[current] = color.grey;
        //            if (list == null) list = new List<T>();
        //            list.Add(current);
        //            foreach (Node next in LdA[current])
        //                if (visit[next.content] == color.white) DFSVisit(next.content, list);
        //            visit[current] = color.black;
        //            if (predicate(current)) return current;
        //            else return default;
        //        }
        //    }
        //    return result;


        //    //Dictionary<T, color> visit = new Dictionary<T, color>();
        //    //foreach (T key in LdA.Keys) visit.Add(key, color.white);
        //    //T found = default;
        //    //foreach (T key in LdA.Keys)
        //    //{
        //    //    if (visit[key] == color.white)
        //    //        found = DFSVisit(key, predicate);
        //    //    if (found != default) return found;
        //    //}
        //    //return default;

        //    //T DFSVisit(T current, Predicate<T> pred)
        //    //{
        //    //    visit[current] = color.grey;
        //    //    foreach (Node next in LdA[current])
        //    //    {
        //    //        if (visit[next.content] == color.white) DFSVisit(next.content, pred);
        //    //    }
        //    //    visit[current] = color.black;
        //    //    if (pred(current)) return current;
        //    //    else return default;
        //    //}
        //}

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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Graph
{
    public abstract class Graph<T>
    {
        protected Dictionary<T, List<Node>> LdA = new Dictionary<T, List<Node>>();
        protected enum color { white, grey, black }
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

        public void RemoveNode(T value)
        {
            LdA.Remove(value);
            foreach(List<Node> list in LdA.Values)
            {
                foreach(Node node in list)
                {
                    if (node.content.Equals(value)) list.Remove(node);
                }
            }
            nVertex--;
        }

        public abstract void AddEdge(T value1, T value2, double weight);
        public abstract void AddArc(T value1, T value2, double weight);

        //public Dictionary<T, Tuple<T, int>> BFS(T origin)
        //{
        //    Dictionary<T, Tuple<T, int>> final = new Dictionary<T, Tuple<T, int>>();
        //    Dictionary<T, color> visit = new Dictionary<T, color>();
        //    foreach (T k in LdA.Keys) visit.Add(k, color.white);

        //    Queue<T> queue = new Queue<T>();
        //    queue.Enqueue(origin);
        //    visit[origin] = color.grey;

        //    while(queue.Count != 0)
        //    {
        //        T father = queue.Dequeue();
        //        foreach (var a in LdA[father])
        //        {
        //            queue.Enqueue(a.content);
        //        }
        //        tuples.Add(new Tuple<T, int>(default, 0));
        //    }
        //    return final;
        //}

        public List<T> DFS(T start)
        {
            Dictionary<T, bool> open = new Dictionary<T, bool>();
            List<List<T>> lists = new List<List<T>>();
            foreach (T k in LdA.Keys) open.Add(k, true);
            return DFSVisit(start, null);
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
        }

        public List<List<T>> DFS()
        {
            List<List<T>> lists = new List<List<T>>();
            foreach (T key in LdA.Keys) lists.Add(DFS(key));
            return lists;
        }

        public Stack<T> Topographic()
        {
            Stack<T> temp = new Stack<T>(), res = new Stack<T>();
            Dictionary<T, int> incidence = new Dictionary<T, int>();
            foreach (var lda in LdA) incidence.Add(lda.Key, 0);
            foreach (var lda in LdA)
                foreach (var adj in lda.Value)
                    incidence[adj.content]++;
            while (res.Count < nVertex)
            {
                Dictionary<T, int> aux = new Dictionary<T, int>(incidence);
                foreach (T key in aux.Keys)
                {
                    if (incidence[key] == 0)
                    {
                        temp.Push(key);
                        incidence[key] = -1;
                    }
                }
                res.Push(temp.Pop());
                foreach (var i in LdA[res.Peek()])
                {
                    incidence[i.content]--;
                }
            }
            return res;
        }

        public Dictionary<T, Tuple<T, double>> Dijkstra(T start)
        {
            Dictionary<T, bool> open = new Dictionary<T, bool>();
            Dictionary<T, T> previous = new Dictionary<T, T>();
            Dictionary<T, double> distance = new Dictionary<T, double>();
            foreach (T key in LdA.Keys)
            {
                open.Add(key, true);
                previous.Add(key, default);
                distance.Add(key, double.MaxValue);
            }
            distance[start] = 0;
            while (open.Values.Where(x => x).Count() > 0)
            {
                double min = double.MaxValue;
                T current = default;
                foreach (var pair in distance)
                {
                    if (open[pair.Key] && pair.Value <= min)
                    {
                        current = pair.Key;
                        min = pair.Value;
                    }
                }
                open[current] = false;
                foreach (var adj in LdA[current])
                {
                    if (open[adj.content] && distance[current] + adj.weight < distance[adj.content])
                    {
                        distance[adj.content] = distance[current] + adj.weight;
                        previous[adj.content] = current;
                    }
                }
            }
            Dictionary<T, Tuple<T, double>> res = new Dictionary<T, Tuple<T, double>>();
            foreach (T key in LdA.Keys)
                res.Add(key, new Tuple<T, double>(previous[key], distance[key]));
            return res;
        }

        public Dictionary<T, Tuple<T, double>> BFS(T start)
        {
            Dictionary<T, bool> open = new Dictionary<T, bool>();
            Dictionary<T, T> previous = new Dictionary<T, T>();
            Dictionary<T, double> distance = new Dictionary<T, double>();
            foreach (T key in LdA.Keys)
            {
                open.Add(key, true);
                previous.Add(key, default);
                distance.Add(key, double.MaxValue);
            }
            open[start] = false;
            distance[start] = 0;
            Queue<T> queue = new Queue<T>();
            queue.Enqueue(start);
            while (queue.Count != 0)
            {
                T current = queue.Dequeue();
                foreach (var adj in LdA[current])
                {
                    if (open[adj.content])
                    {
                        open[adj.content] = false;
                        distance[adj.content] = distance[current] + adj.weight;
                        previous[adj.content] = current;
                        queue.Enqueue(adj.content);
                    }
                }
            }
            Dictionary<T, Tuple<T, double>> res = new Dictionary<T, Tuple<T, double>>();
            foreach (T key in LdA.Keys)
                res.Add(key, new Tuple<T, double>(previous[key], distance[key]));
            return res;
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

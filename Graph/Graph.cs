using System;
using System.Collections.Generic;
using System.Text;

namespace Graph
{
    public abstract class Graph<T>
    {
        protected Dictionary<Node, List<Node>> LdA = new Dictionary<Node, List<Node>>();
        protected enum color { white, grey, black }

        protected class Node
        {
            public T content;
            public double weight;
            public color visit;
            public Node(T value)
            {
                content = value;
                visit = color.white;
            }
            public Node(T value, double weight)
            {
                content = value;
                visit = color.white;
                this.weight = weight;
            }
        }

        public void AddNode(T value)
        {
            foreach (Node node in LdA.Keys) if (node.content.Equals(value)) return;
            LdA.Add(new Node(value), new List<Node>());
        }

        public abstract void AddEdge(T value1, T value2, double weight);
        public abstract void AddArc(T value1, T value2, double weight);

        public T DFS(Predicate<T> predicate)
        {
            foreach (Node node in LdA.Keys) node.visit = color.white;
            List<List<Node>> lists = new List<List<Node>>();
            T found = default;
            foreach (Node node in LdA.Keys)
            {
                if (node.visit == color.white)
                    lists.Add(DFSVisit(node, null, predicate, out found));
                if (found != default) return found;
            }
            return default;
        }

        private List<Node> DFSVisit(Node current, List<Node> list, Predicate<T> predicate, out T found)
        {
            current.visit = color.grey;
            if (list == null) list = new List<Node>();
            list.Add(current);
            foreach (Node next in LdA[current])
            {
                if (next.visit == color.white) DFSVisit(next, list, predicate, out found);
            }
            current.visit = color.black;
            if (predicate(current.content)) found = current.content;
            else found = default;
            return list;
        }

        public void BFS(Predicate<T> predicate)
        {

        }
    }
    public class UndirectedGraph<T> : Graph<T>
    {
        public override void AddEdge(T value1, T value2, double weight)
        {
            Node node1 = null, node2 = null;
            foreach (Node node in LdA.Keys)
            {
                if (node.content.Equals(value1)) node1 = node;
                if (node.content.Equals(value2)) node2 = node;
            }
            if (node1 != null && node2 != null)
            {
                foreach (Node node in LdA[node1]) if (node.content.Equals(value2)) return;
                LdA[node1].Add(new Node(value2, weight));
                LdA[node2].Add(new Node(value1, weight));
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
            Node nodeFrom = null, nodeTo = null;
            foreach (Node node in LdA.Keys)
            {
                if (node.content.Equals(from)) nodeFrom = node;
                if (node.content.Equals(to)) nodeTo = node;
            }
            if (nodeFrom != null && nodeTo != null)
            {
                foreach (Node node in LdA[nodeFrom]) if (node.content.Equals(to)) return;
                LdA[nodeFrom].Add(new Node(to, weight));
            }
        }

        public override void AddEdge(T value1, T value2, double weight)
        {
            throw new NotImplementedException();
        }
    }
}

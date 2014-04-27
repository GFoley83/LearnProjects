using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ctarti.DataStructures
{
    public class GraphNode
    {
        public string Key = "";
        public List<GraphEdge> AdjacencyList = new List<GraphEdge>();
        public bool Visited = false;

        public GraphNode(string key)
        { this.Key = key; }

        public bool IsAdjacent(GraphNode node)
        {
            if (AdjacencyList == null)
                return false;

            if (AdjacencyList.Any<GraphEdge>(e => e.Neighbor.Key == node.Key))
                return true;
            else
                return false;
        }
        public string GetAdjacent()
        {
            StringBuilder sb = new StringBuilder();
            foreach (GraphEdge edge in AdjacencyList)
            {
                sb.AppendFormat("{0}:{1}, ", edge.Neighbor.Key, edge.Cost);
            }

            return string.Format("{0}==> {1}", ToString(), sb.ToString());
        }
        public void PrintNode()
        {
            Console.WriteLine(GetAdjacent());
        }

        public override string ToString()
        { return Key; }
    }

    public class GraphEdge
    {
        public int Cost;
        public GraphNode Neighbor;

        public GraphEdge(GraphNode neighbor)
        { this.Neighbor = neighbor; Cost = 0; }

        public GraphEdge(GraphNode neighbor, int cost)
        { this.Neighbor = neighbor; this.Cost = cost; }
    }

    public class GraphCollection : IDebugging
    {
        public int Count { get; private set; }
        public List<GraphNode> Nodes = new List<GraphNode>();

        public void AddNode(GraphNode node)
        {
            if (ContainsNode(node))
                //Duplicate
                return;
            else
                Nodes.Add(node);
        }
        public void RemoveNode(GraphNode node)
        {
            if (Nodes.Any<GraphNode>(n => n.Key == node.Key))
                Nodes.Remove(node);
        }
        public void Clear() { Nodes.Clear(); }
        public bool ContainsNode(GraphNode node) 
        {
            if (Nodes == null)
                return false;

            if (Nodes.Any<GraphNode>(n => n.Key == node.Key))
                return true;
            else
                return false;            
        }
        public GraphNode SearchNode(GraphNode node)
        {
            if (ContainsNode(node))
                return Nodes.First<GraphNode>(n => n.Key == node.Key);
            else
                return null;
        }

        public void AddDirectedEdge(GraphNode source, GraphNode target)
        { AddDirectedEdge(source, target, 0); }
        public void AddDirectedEdge(GraphNode source, GraphNode target, int cost)
        {
            if (!(source.IsAdjacent(target)))
                source.AdjacencyList.Add(new GraphEdge(target, cost));
        }
        public void AddUndirectedEdge(GraphNode source, GraphNode target)
        { AddUndirectedEdge(source, target, 0); }
        public void AddUndirectedEdge(GraphNode source, GraphNode target, int cost)
        {
            if (!(source.IsAdjacent(target)))
                source.AdjacencyList.Add(new GraphEdge(target, cost));

            if (!(target.IsAdjacent(target)))
                source.AdjacencyList.Add(new GraphEdge(source, cost));
        }

        public void PrintCollection()
        {
            foreach (GraphNode node in Nodes)
            {
                Console.WriteLine(node.GetAdjacent());
            }
        }
        public object GenerateRandomCollection(int size, int minValue, int maxValue)
        {
            GraphCollection graph = new GraphCollection();
            //Create Nodes
            for (int i = 0; i < size; i++)
            {
                graph.AddNode(new GraphNode(i.ToString()));
            }

            //Create Edges
            Random r = new Random();
            int edgeCount = 0;
            int edgeTargetCount = r.Next(minValue, maxValue);
            while (edgeCount < edgeTargetCount)
            {
                GraphNode source = new GraphNode(r.Next(0, size).ToString());
                GraphNode target = new GraphNode(r.Next(0, size).ToString());

                //Add Edges only if both source and target exist
                if (graph.ContainsNode(source) && (graph.ContainsNode(target)) && (source.Key != target.Key))
                {
                    source = graph.SearchNode(source);
                    target = graph.SearchNode(target);

                    graph.AddDirectedEdge(source, target);
                    edgeCount++;
                }
            }
                    
            return graph;
        }

        public void DeapthFirstTraversal(GraphNode node)
        {
            //Recursion Break Condition
            if (node == null) return;
            
            node.PrintNode();
            node.Visited = true;

            foreach (GraphEdge e in node.AdjacencyList)
            {
                if (e.Neighbor.Visited == false)
                    DeapthFirstTraversal(e.Neighbor);
            }
        }

        public void BreadthFirstTraversal(GraphNode node)
        {
            Queue<GraphNode> q = new Queue<GraphNode>();
            node.PrintNode();
            node.Visited = true;

            q.Enqueue(node); //Add to end of queue

            while (q.Count<GraphNode>() > 0)
            {
                GraphNode r = q.Dequeue(); //Remove from front of queue
                foreach (GraphEdge e in r.AdjacencyList)
                {
                    GraphNode n = e.Neighbor;
                    if (n.Visited == false)
                    {
                        n.PrintNode();
                        n.Visited = true;
                        q.Enqueue(n);
                    }
                }
            }
        }

        public void ResetVisitedNodes()
        {
            foreach (GraphNode node in Nodes)
            {
                node.Visited = false;
            }
        }
    }
}

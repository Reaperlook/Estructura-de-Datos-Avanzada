using System;
using System.Collections.Generic;
using System.Linq;

public class Edge : IComparable<Edge>
{
    public string Source { get; set; }
    public string Destination { get; set; }
    public double Weight { get; set; }

    public int CompareTo(Edge other)
    {
        return Weight.CompareTo(other.Weight);
    }
}

public class DSU
{
    private Dictionary<string, string> parent = new Dictionary<string, string>();

    public DSU(IEnumerable<string> vertices)
    {
        foreach (var v in vertices) parent[v] = v;
    }

    public string Find(string i)
    {
        if (parent[i] != i)
            parent[i] = Find(parent[i]);
        return parent[i];
    }

    public void Union(string i, string j)
    {
        string rootI = Find(i);
        string rootJ = Find(j);
        if (rootI != rootJ) parent[rootI] = rootJ;
    }
}

public class Graph
{
    private List<Edge> edges = new List<Edge>();
    private HashSet<string> vertices = new HashSet<string>();
    private Dictionary<string, List<(string to, double w)>> adj = new Dictionary<string, List<(string, double)>>();

    public void AddEdge(string u, string v, double w)
    {
        edges.Add(new Edge { Source = u, Destination = v, Weight = w });
        vertices.Add(u);
        vertices.Add(v);

        if (!adj.ContainsKey(u)) adj[u] = new List<(string, double)>();
        if (!adj.ContainsKey(v)) adj[v] = new List<(string, double)>();
        
        adj[u].Add((v, w));
        adj[v].Add((u, w));
    }

    public void KruskalMST()
    {
        edges.Sort();
        DSU dsu = new DSU(vertices);
        List<Edge> mst = new List<Edge>();
        double totalWeight = 0;

        foreach (var edge in edges)
        {
            if (dsu.Find(edge.Source) != dsu.Find(edge.Destination))
            {
                dsu.Union(edge.Source, edge.Destination);
                mst.Add(edge);
                totalWeight += edge.Weight;
            }
        }

        Console.WriteLine($"\n[Kruskal] Costo Total: {totalWeight:F1}");
        foreach (var e in mst)
            Console.WriteLine($"{e.Source} - {e.Destination}: {e.Weight}");
    }

    public void PrimMST(string startNode)
    {
        var mstEdges = new List<Edge>();
        var visited = new HashSet<string>();
        var pq = new List<Edge>();
        double totalWeight = 0;

        visited.Add(startNode);
        foreach(var neighbor in adj[startNode])
        {
            pq.Add(new Edge { Source = startNode, Destination = neighbor.to, Weight = neighbor.w });
        }

        while (visited.Count < vertices.Count && pq.Count > 0)
        {
            pq.Sort(); 
            var minEdge = pq[0];
            pq.RemoveAt(0);

            if (visited.Contains(minEdge.Destination)) continue;

            visited.Add(minEdge.Destination);
            mstEdges.Add(minEdge);
            totalWeight += minEdge.Weight;

            foreach (var neighbor in adj[minEdge.Destination])
            {
                if (!visited.Contains(neighbor.to))
                {
                    pq.Add(new Edge { Source = minEdge.Destination, Destination = neighbor.to, Weight = neighbor.w });
                }
            }
        }

        Console.WriteLine($"\n[Prim] Costo Total: {totalWeight:F1}");
        foreach (var e in mstEdges)
            Console.WriteLine($"{e.Source} - {e.Destination}: {e.Weight}");
    }
}

class Program
{
    static void Main()
    {
        var grafo = new Graph();

        grafo.AddEdge("Casa", "Starbucks", 2.1);
        grafo.AddEdge("Casa", "Universidad", 3.8);
        grafo.AddEdge("Starbucks", "Universidad", 1.4);
        grafo.AddEdge("Starbucks", "Plaza", 2.0);
        grafo.AddEdge("Universidad", "Gimnasio", 1.2);
        grafo.AddEdge("Gimnasio", "Plaza", 2.5);
        grafo.AddEdge("Plaza", "Central", 1.1);
        grafo.AddEdge("Central", "Casa", 4.0);
        grafo.AddEdge("Casa", "Gimnasio", 3.3);
        grafo.AddEdge("Universidad", "Central", 2.7);
        grafo.AddEdge("Gimnasio", "Starbucks", 2.2);
        grafo.AddEdge("Plaza", "Casa", 3.6);

        Console.WriteLine("=== SEMANA 7: ÁRBOLES GENERADORES MÍNIMOS (MST) ===");
        grafo.KruskalMST();
        grafo.PrimMST("Casa");
    }
}
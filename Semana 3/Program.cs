using System;
using System.Collections.Generic;
using System.IO;

public class Graph<T> {
    public readonly Dictionary<T, List<(T to, double w)>> adj = new();

    public void AddVertex(T v){ if(!adj.ContainsKey(v)) adj[v] = new List<(T,double)>(); }

    public void AddEdge(T u, T v, double w = 1, bool directed = true){
        AddVertex(u); AddVertex(v);
        adj[u].Add((v,w));
        if(!directed) adj[v].Add((u,w));
    }

    public IEnumerable<T> Vertices() => adj.Keys;
    public IEnumerable<(T to, double w)> Neighbors(T u) => adj.TryGetValue(u, out var list) ? list : new List<(T,double)>();
}

class Program {
    static void Main() {
        var mapa = new Graph<string>();

        mapa.AddEdge("Casa", "Starbucks", 2.1);
        mapa.AddEdge("Casa", "Universidad", 3.8);
        mapa.AddEdge("Starbucks", "Universidad", 1.4);
        mapa.AddEdge("Starbucks", "Plaza", 2.0);
        mapa.AddEdge("Universidad", "Gimnasio", 1.2);
        mapa.AddEdge("Gimnasio", "Plaza", 2.5);
        mapa.AddEdge("Plaza", "Central", 1.1);
        mapa.AddEdge("Central", "Casa", 4.0);
        mapa.AddEdge("Casa", "Gimnasio", 3.3);
        mapa.AddEdge("Universidad", "Central", 2.7);
        mapa.AddEdge("Gimnasio", "Starbucks", 2.2);
        mapa.AddEdge("Plaza", "Casa", 3.6);

        using (StreamWriter sw = new StreamWriter("edges.txt")) {
            foreach(var u in mapa.Vertices()) {
                foreach(var edge in mapa.Neighbors(u)) {
                    sw.WriteLine($"{u} {edge.to} {edge.w}");
                }
            }
        }
        Console.WriteLine("Archivo edges.txt generado.");
    }
}
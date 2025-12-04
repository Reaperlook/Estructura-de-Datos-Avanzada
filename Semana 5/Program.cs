using System;
using System.Collections.Generic;
using System.Linq;

public class Graph<T>
{
    private Dictionary<T, List<T>> adj = new Dictionary<T, List<T>>();

    public void AddEdge(T u, T v)
    {
        if (!adj.ContainsKey(u)) adj[u] = new List<T>();
        if (!adj.ContainsKey(v)) adj[v] = new List<T>();
        adj[u].Add(v); 
    }

    public List<T> BFS_ShortestPath(T start, T end)
    {
        if (!adj.ContainsKey(start) || !adj.ContainsKey(end)) return null;

        var previous = new Dictionary<T, T>();
        var queue = new Queue<T>();
        var visited = new HashSet<T>();

        queue.Enqueue(start);
        visited.Add(start);

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            if (current.Equals(end)) break;

            if (adj.ContainsKey(current)) {
                foreach (var neighbor in adj[current])
                {
                    if (!visited.Contains(neighbor))
                    {
                        visited.Add(neighbor);
                        previous[neighbor] = current;
                        queue.Enqueue(neighbor);
                    }
                }
            }
        }

        if (!previous.ContainsKey(end)) return null;

        var path = new List<T>();
        var curr = end;
        while (!curr.Equals(start))
        {
            path.Add(curr);
            curr = previous[curr];
        }
        path.Add(start);
        path.Reverse();
        return path;
    }

    public List<T> DFS_Traverse(T start)
    {
        var visited = new HashSet<T>();
        var result = new List<T>();
        DFS_Recursive(start, visited, result);
        return result;
    }

    private void DFS_Recursive(T node, HashSet<T> visited, List<T> result)
    {
        visited.Add(node);
        result.Add(node);

        if (adj.ContainsKey(node))
        {
            foreach (var neighbor in adj[node])
            {
                if (!visited.Contains(neighbor))
                {
                    DFS_Recursive(neighbor, visited, result);
                }
            }
        }
    }
}

class Program
{
    static void Main()
    {
        var mapa = new Graph<string>();

        mapa.AddEdge("Casa", "Starbucks");
        mapa.AddEdge("Casa", "Universidad");
        mapa.AddEdge("Starbucks", "Universidad");
        mapa.AddEdge("Starbucks", "Plaza");
        mapa.AddEdge("Universidad", "Gimnasio");
        mapa.AddEdge("Gimnasio", "Plaza");
        mapa.AddEdge("Plaza", "Central");
        mapa.AddEdge("Central", "Casa");
        mapa.AddEdge("Casa", "Gimnasio");
        mapa.AddEdge("Universidad", "Central");
        mapa.AddEdge("Gimnasio", "Starbucks");
        mapa.AddEdge("Plaza", "Casa");

        Console.WriteLine("--- BFS (Ruta Corta) Casa -> Plaza ---");
        var ruta = mapa.BFS_ShortestPath("Casa", "Plaza");
        if (ruta != null) Console.WriteLine(string.Join(" -> ", ruta));
        else Console.WriteLine("No hay ruta.");

        Console.WriteLine("\n--- DFS (Exploracion) desde Casa ---");
        var exploracion = mapa.DFS_Traverse("Casa");
        Console.WriteLine(string.Join(" -> ", exploracion));
    }
}
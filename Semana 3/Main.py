from collections import defaultdict
import os

def load_graph(path: str, directed: bool = True):
    adj = defaultdict(list)
    if not os.path.exists(path):
        return {}
        
    with open(path, 'r', encoding='utf-8') as f:
        for line in f:
            line = line.strip()
            if not line: continue
            parts = line.split()
            u, v = parts[0], parts[1]
            w = float(parts[2]) if len(parts) > 2 else 1.0
            adj[u].append((v,w))
            if not directed:
                adj[v].append((u,w))
    return adj

if __name__ == "__main__":
    grafo = load_graph("edges.txt", directed=True)
    
    if grafo:
        print(f"{'VÉRTICE':<15} | {'GRADO':<6} | {'CONEXIONES'}")
        print("-" * 60)
        
        for nodo in sorted(grafo.keys()):
            vecinos = grafo.get(nodo, [])
            grado = len(vecinos)
            detalles = ", ".join([f"{v}({w})" for v, w in vecinos])
            print(f"{nodo:<15} | {grado:<6} | {detalles}")
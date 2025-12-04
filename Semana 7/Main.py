import heapq

class DSU:
    def __init__(self, vertices):
        self.parent = {v: v for v in vertices}

    def find(self, i):
        if self.parent[i] != i:
            self.parent[i] = self.find(self.parent[i])
        return self.parent[i]

    def union(self, i, j):
        root_i = self.find(i)
        root_j = self.find(j)
        if root_i != root_j:
            self.parent[root_i] = root_j
            return True
        return False

class GraphMST:
    def __init__(self):
        self.edges = []
        self.adj = {}
        self.vertices = set()

    def add_edge(self, u, v, w):
        self.edges.append((u, v, w))
        self.vertices.add(u)
        self.vertices.add(v)
        
        if u not in self.adj: self.adj[u] = []
        if v not in self.adj: self.adj[v] = []
        
        self.adj[u].append((v, w))
        self.adj[v].append((u, w))

    def kruskal_mst(self):
        self.edges.sort(key=lambda x: x[2])
        dsu = DSU(self.vertices)
        mst = []
        cost = 0

        for u, v, w in self.edges:
            if dsu.union(u, v):
                mst.append((u, v, w))
                cost += w
        
        print(f"\n[Kruskal] Costo Total: {cost:.1f}")
        for u, v, w in mst:
            print(f"{u} - {v}: {w}")

    def prim_mst(self, start_node):
        visited = {start_node}
        mst = []
        cost = 0
        pq = []

        for v, w in self.adj[start_node]:
            heapq.heappush(pq, (w, start_node, v))

        while pq and len(visited) < len(self.vertices):
            w, u, v = heapq.heappop(pq)
            
            if v in visited:
                continue
            
            visited.add(v)
            mst.append((u, v, w))
            cost += w

            for next_v, next_w in self.adj[v]:
                if next_v not in visited:
                    heapq.heappush(pq, (next_w, v, next_v))

        print(f"\n[Prim] Costo Total: {cost:.1f}")
        for u, v, w in mst:
            print(f"{u} - {v}: {w}")

if __name__ == "__main__":
    g = GraphMST()
    
    data = [
        ("Casa", "Starbucks", 2.1), ("Casa", "Universidad", 3.8),
        ("Starbucks", "Universidad", 1.4), ("Starbucks", "Plaza", 2.0),
        ("Universidad", "Gimnasio", 1.2), ("Gimnasio", "Plaza", 2.5),
        ("Plaza", "Central", 1.1), ("Central", "Casa", 4.0),
        ("Casa", "Gimnasio", 3.3), ("Universidad", "Central", 2.7),
        ("Gimnasio", "Starbucks", 2.2), ("Plaza", "Casa", 3.6)
    ]

    for u, v, w in data:
        g.add_edge(u, v, w)

    print("=== SEMANA 7: ÁRBOLES GENERADORES MÍNIMOS (MST) ===")
    g.kruskal_mst()
    g.prim_mst("Casa")
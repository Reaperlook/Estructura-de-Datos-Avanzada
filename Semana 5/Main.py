from collections import deque, defaultdict

class Graph:
    def __init__(self):
        self.adj = defaultdict(list)

    def add_edge(self, u, v):
        self.adj[u].append(v)

    def bfs_shortest_path(self, start, end):
        if start not in self.adj: return None
        
        queue = deque([start])
        visited = {start}
        parent = {start: None}

        while queue:
            current = queue.popleft()
            if current == end: break
            
            for neighbor in self.adj[current]:
                if neighbor not in visited:
                    visited.add(neighbor)
                    parent[neighbor] = current
                    queue.append(neighbor)
        
        if end not in parent: return None
        
        path = []
        curr = end
        while curr is not None:
            path.append(curr)
            curr = parent[curr]
        return path[::-1]

    def dfs_traverse(self, start):
        visited = set()
        result = []
        
        def _dfs(node):
            visited.add(node)
            result.append(node)
            for neighbor in self.adj[node]:
                if neighbor not in visited:
                    _dfs(neighbor)
        
        if start in self.adj:
            _dfs(start)
        return result

if __name__ == "__main__":
    mapa = Graph()
    
    conexiones = [
        ("Casa", "Starbucks"), ("Casa", "Universidad"),
        ("Starbucks", "Universidad"), ("Starbucks", "Plaza"),
        ("Universidad", "Gimnasio"), ("Gimnasio", "Plaza"),
        ("Plaza", "Central"), ("Central", "Casa"),
        ("Casa", "Gimnasio"), ("Universidad", "Central"),
        ("Gimnasio", "Starbucks"), ("Plaza", "Casa")
    ]
    
    for u, v in conexiones:
        mapa.add_edge(u, v)

    print("BFS Casa -> Plaza:", mapa.bfs_shortest_path("Casa", "Plaza"))
    print("DFS desde Casa:", mapa.dfs_traverse("Casa"))
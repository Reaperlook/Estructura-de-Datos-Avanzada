import heapq
from collections import Counter

class Node:
    def __init__(self, char, freq):
        self.char = char
        self.freq = freq
        self.left = None
        self.right = None

    def __lt__(self, other):
        return self.freq < other.freq

def build_huffman_tree(text):
    frequency = Counter(text)
    heap = [Node(char, freq) for char, freq in frequency.items()]
    heapq.heapify(heap)

    while len(heap) > 1:
        left = heapq.heappop(heap)
        right = heapq.heappop(heap)
        merged = Node(None, left.freq + right.freq)
        merged.left = left
        merged.right = right
        heapq.heappush(heap, merged)

    return heap[0]

def build_codes(node, prefix="", codebook={}):
    if node:
        if node.char is not None:
            codebook[node.char] = prefix
        build_codes(node.left, prefix + "0", codebook)
        build_codes(node.right, prefix + "1", codebook)
    return codebook

def compress(text, codebook):
    return ''.join(codebook[char] for char in text)

if __name__ == "__main__":
    map_data = """Casa Starbucks 2.1
Casa Universidad 3.8
Starbucks Universidad 1.4
Starbucks Plaza 2.0
Universidad Gimnasio 1.2
Gimnasio Plaza 2.5
Plaza Central 1.1
Central Casa 4.0
Casa Gimnasio 3.3
Universidad Central 2.7
Gimnasio Starbucks 2.2
Plaza Casa 3.6"""

    print("=== SEMANA 8: COMPRESIÓN DE HUFFMAN ===")
    
    root = build_huffman_tree(map_data)
    codes = build_codes(root)
    
    encoded_data = compress(map_data, codes)
    
    original_size = len(map_data) * 8
    compressed_size = len(encoded_data)
    
    print(f"\nDatos originales:\n{map_data}")
    print("\n--- Estadísticas ---")
    print(f"Tamaño Original (ASCII 8-bit): {original_size} bits")
    print(f"Tamaño Comprimido (Huffman):   {compressed_size} bits")
    print(f"Ahorro de espacio:             {100 - (compressed_size/original_size)*100:.2f}%")
    
    print("\n--- Códigos Generados (Muestra) ---")
    for char, code in list(codes.items())[:5]: 
        visible_char = char if char != '\n' else '\\n'
        print(f"'{visible_char}': {code}")
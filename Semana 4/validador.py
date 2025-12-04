def is_graphical_sequence(degrees):
    seq = sorted(degrees, reverse=True)
    
    if sum(seq) % 2 != 0:
        return False

    while seq:
        d1 = seq.pop(0)
        
        if d1 == 0: return True
        if d1 > len(seq): return False
        
        for i in range(d1):
            seq[i] -= 1
            if seq[i] < 0: return False
            
        seq.sort(reverse=True)
        
    return True

if __name__ == "__main__":
    test_cases = [
        ([4, 3, 3, 2, 2, 2, 1, 1], True),
        ([3, 3, 3, 1], False)
    ]
    
    for seq, expected in test_cases:
        print(f"Secuencia: {seq} -> {is_graphical_sequence(seq)}")
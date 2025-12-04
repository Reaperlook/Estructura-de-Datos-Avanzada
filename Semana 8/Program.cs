using System;

public class Node
{
    public string Key;
    public int Height;
    public Node Left, Right;

    public Node(string key)
    {
        Key = key;
        Height = 1;
    }
}

public class AVLTree
{
    public Node Root;

    public int Height(Node N)
    {
        if (N == null) return 0;
        return N.Height;
    }

    public int Max(int a, int b)
    {
        return (a > b) ? a : b;
    }

    public Node RightRotate(Node y)
    {
        Node x = y.Left;
        Node T2 = x.Right;

        x.Right = y;
        y.Left = T2;

        y.Height = Max(Height(y.Left), Height(y.Right)) + 1;
        x.Height = Max(Height(x.Left), Height(x.Right)) + 1;

        return x;
    }

    public Node LeftRotate(Node x)
    {
        Node y = x.Right;
        Node T2 = y.Left;

        y.Left = x;
        x.Right = T2;

        x.Height = Max(Height(x.Left), Height(x.Right)) + 1;
        y.Height = Max(Height(y.Left), Height(y.Right)) + 1;

        return y;
    }

    public int GetBalance(Node N)
    {
        if (N == null) return 0;
        return Height(N.Left) - Height(N.Right);
    }

    public Node Insert(Node node, string key)
    {
        if (node == null) return new Node(key);

        int compareResult = string.Compare(key, node.Key);

        if (compareResult < 0)
            node.Left = Insert(node.Left, key);
        else if (compareResult > 0)
            node.Right = Insert(node.Right, key);
        else
            return node;

        node.Height = 1 + Max(Height(node.Left), Height(node.Right));

        int balance = GetBalance(node);

        if (balance > 1 && string.Compare(key, node.Left.Key) < 0)
            return RightRotate(node);

        if (balance < -1 && string.Compare(key, node.Right.Key) > 0)
            return LeftRotate(node);

        if (balance > 1 && string.Compare(key, node.Left.Key) > 0)
        {
            node.Left = LeftRotate(node.Left);
            return RightRotate(node);
        }

        if (balance < -1 && string.Compare(key, node.Right.Key) < 0)
        {
            node.Right = RightRotate(node.Right);
            return LeftRotate(node);
        }

        return node;
    }

    public bool Search(Node node, string key)
    {
        if (node == null) return false;
        
        int compareResult = string.Compare(key, node.Key);
        
        if (compareResult == 0) return true;
        if (compareResult < 0) return Search(node.Left, key);
        return Search(node.Right, key);
    }

    public void InOrder(Node node)
    {
        if (node != null)
        {
            InOrder(node.Left);
            Console.WriteLine($"- {node.Key} (H:{node.Height})");
            InOrder(node.Right);
        }
    }
}

class Program
{
    static void Main()
    {
        AVLTree tree = new AVLTree();
        string[] lugares = { 
            "Casa", "Starbucks", "Universidad", 
            "Gimnasio", "Plaza", "Central" 
        };

        foreach (var lugar in lugares)
        {
            tree.Root = tree.Insert(tree.Root, lugar);
        }

        Console.WriteLine("=== SEMANA 8: ÍNDICE AVL (Lugares Ordenados) ===");
        tree.InOrder(tree.Root);

        Console.WriteLine("\n--- Prueba de Búsqueda ---");
        string buscar1 = "Plaza";
        string buscar2 = "Cine"; 

        Console.WriteLine($"¿Existe '{buscar1}'? {tree.Search(tree.Root, buscar1)}");
        Console.WriteLine($"¿Existe '{buscar2}'? {tree.Search(tree.Root, buscar2)}");
    }
}
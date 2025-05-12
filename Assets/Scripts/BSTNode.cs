using UnityEngine;

// Represents a node in a Binary Search Tree (BST)
public class BSTNode <T> 
{
    public T Value;
    public BSTNode<T> Left;
    public BSTNode<T> Right;

    public BSTNode(T value)
    {
        this.Value = value;
        Left = Right = null;
    }
}

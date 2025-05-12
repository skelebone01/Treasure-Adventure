using System;
using System.Text;
using UnityEngine;

// A generic Binary Search Tree (BST) implementation that stores comparable values.
public class BST<T> where T : IComparable<T>
{
    private BSTNode<T> root;
    public int Size;

    public BST()//constructer
    {
        root = null;
        Size = 0;
    }

    public void Insert(T value)// Inserts a value into the BST, maintaining sorted order
    {
        if (root == null)
        {
            root = new BSTNode<T>(value);
            Size++;
            return;
        }

        var temp = root;
        while (temp != null)
        {
            if (value.CompareTo(temp.Value) < 0)//if the value is smaller go to the left
            {
                if (temp.Left == null)
                {
                    temp.Left = new BSTNode<T>(value);
                    Size++;
                    return;
                }
                temp = temp.Left;
            }
            else if (value.CompareTo(temp.Value) > 0)//if the value is bigger go to the right
            {
                if (temp.Right == null)
                {
                    temp.Right = new BSTNode<T>(value);
                    Size++;
                    return;
                }
                temp = temp.Right;
            }
            else
            {
                return;
            }
        }
    }

    private void PreOrderTraversal(BSTNode<T> node, StringBuilder sb)
    {
        if (node != null)
        {
            sb.AppendLine($"{node.Value}");//adding the value
            PreOrderTraversal(node.Left, sb);//travels to the left of the subtree
            PreOrderTraversal(node.Right, sb);//travels to the right of the subtree
        }
    }

    public string PreOrderTraversal()
    {
        var sb = new StringBuilder();
        PreOrderTraversal(root, sb);
        return sb.ToString().Trim();
    }

    public T FindMin()//this function finds the min value by constintly going to the left
    {
        if (root == null)
            throw new InvalidOperationException("Tree is empty.");

        var temp = root;
        while (temp.Left != null)
            temp = temp.Left;

        return temp.Value;
    }

    public T FindMax()//this function finds the Max value by constintly going to the right
    {
        if (root == null)
            throw new InvalidOperationException("Tree is empty.");

        var temp = root;
        while (temp.Right != null)
            temp = temp.Right;

        return temp.Value;
    }
}

using UnityEngine;
//node that is used for the linked list and double linked list
public class Node
{
    public CellData info;
    public Node next;
    public Node prev;

    public Node(Transform nodePosition, GameObject visualObject, int index)
    {
        info = new CellData(nodePosition, visualObject, index);
        next = null;
    }
}

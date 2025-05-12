using UnityEngine;

public class DoubleLinkedList : IList
{
    private Node _head;
    private Node _tail;

    public Node head
    {
        get { return _head; }
        private set { _head = value; }
    }

    public Node tail
    {
        get { return _tail; }
        private set { _tail = value; }
    }

    public void BuildList(Transform[] transforms, GameObject[] visuals)//this function connects all the nodes in order and sets the values
    {
        head = null;
        tail = null;
        Node current = null;

        for (int i = 0; i < transforms.Length; i++)
        {
            Node newNode = new Node(transforms[i], visuals[i], i);

            if (head == null)
            {
                head = newNode;
                current = head;
            }
            else
            {
                current.next = newNode;
                newNode.prev = current;
                current = newNode;
            }
        }

        tail = current; // last node becomes tail
    }

    public Node FindByIndex(int index)
    {
        // Decide whether to start from head or tail
        if (head == null || index < 0)
            return null;

        int middle = (head.info.index + tail.info.index) / 2;

        if (index <= middle)
        {
            Node temp = head;
            while (temp != null)
            {
                if (temp.info.index == index)
                    return temp;
                temp = temp.next;
            }
        }
        else
        {
            Node temp = tail;
            while (temp != null)
            {
                if (temp.info.index == index)
                    return temp;
                temp = temp.prev;
            }
        }

        return null;
    }
}
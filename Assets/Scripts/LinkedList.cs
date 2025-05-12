using UnityEngine;
public class LinkedList : IList
{
    private Node _head;

    public Node head
    {
        get { return _head; }
        private set { _head = value; }
    }

    public void BuildList(Transform[] transforms, GameObject[] visuals)
    {
        head = null;
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
                current = newNode;
            }
        }
    }
    public Node FindByIndex(int index)
    {
        Node temp = head;
        while (temp != null)
        {
            if (temp.info.index == index)
                return temp;
            temp = temp.next;
        }
        return null;
    }

}

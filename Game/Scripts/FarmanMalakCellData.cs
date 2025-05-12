using UnityEngine;
//this class holds the data for each cell(Node)
public class CellData
{
    public Transform nodePosition; //coordinates
    public GameObject visualObject;
    public int index;
    public float type;//type to determine what it dose
    public Node forward;
    public Node backward;

    public CellData(Transform nodePosition, GameObject visualObject, int index)//constructer
    {
        this.nodePosition = nodePosition;
        this.visualObject = visualObject;
        this.index = index;
        this.type = 0; // default type
    }
}

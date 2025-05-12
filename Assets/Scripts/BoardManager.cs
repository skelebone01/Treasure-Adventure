using UnityEngine;
//this class runs at the start of each level and basiclly generates a map
public class BoardManager : MonoBehaviour
{
    [SerializeField] private int level = 1; // iam using Level to be able to tell what level to play so i can know what to use linked list or double list
    [SerializeField] private Transform[] nodeTransforms;// this holds the transform postion of game objects to control players movement
    [SerializeField] private GameObject[] nodes;//this holds the gameobjects of nodes or the steps
    [SerializeField] private int treasureCount;
    [SerializeField] private int trapCount;
    [SerializeField] private int forwardCount;
    [SerializeField] private int backwardCount;
    [SerializeField] private Material arrowMaterial; // this holds a material i use it to change the colur

    IList currentList;
    public Node head;

    private void Start()
    {
        InitBoard();
        LocateTreasures();
        LocateTraps();

        if (level == 2)
        {
            LocateForward();
            LocateBackward();
        }

        DrawArrows();
    }

    private void InitBoard()//This function connects between the nodes and the cells basiclly generating the map
    {
        if (level == 1) // using interface to determin using linked list or double linked list
            currentList = new LinkedList();
        if (level == 2)
            currentList = new DoubleLinkedList();

        currentList.BuildList(nodeTransforms, nodes);
        head = currentList.head;
        FindAnyObjectByType<PlayerMover>().SetCurrentNode(head);//this sends the head of the node to the player mover class
    }

    private void LocateTreasures()  //this function locates treasure by generating a random index and checking if the cell is empty
    {
        for (int i = 0; i < treasureCount; i++)
        {
            int randomIndex = Random.Range(2, nodeTransforms.Length);//generating random index making sure its not too close to the bginning
            Node temp = currentList.FindByIndex(randomIndex);

            if (temp != null && temp.info.type == 0)//checking if its empty
            {
                temp.info.type = 1;
                temp.info.visualObject.GetComponent<Renderer>().material.color = Color.green;//this changes the colur of the node(gameobject)
            }
            else { i--; }//restarting if we fail to find an empty node
        }
    }

    private void LocateTraps()  //this function locates Traps by generating a random index and checking if the cell is empty
    {
        for (int i = 0; i < trapCount; i++)
        {
            int randomIndex = Random.Range(2, nodeTransforms.Length);//setting a random index making sure its not too close to tje beginning
            Node temp = currentList.FindByIndex(randomIndex);

            if (temp != null && temp.info.type == 0)//cheking if its empty
            {
                temp.info.type = 2;
                temp.info.visualObject.GetComponent<Renderer>().material.color = Color.red;//this changes the colur of the node(gameobject)
            }
            else { i--; }//restarting if the cell is too empty
        }
    }

    private void LocateForward() //this function only active in level 2
    {
        for (int i = 0; i < forwardCount; i++)
        {
            int randomIndex = Random.Range(2, nodeTransforms.Length - 6);//setting a rabdom index making sure its not too close to the ending 
            Node temp = currentList.FindByIndex(randomIndex);

            if (temp != null && temp.info.type == 0)//checing if the node is empty
            {
                temp.info.type = 3;
                temp.info.visualObject.GetComponent<Renderer>().material.color = Color.magenta;//this changes the colur of the node(gameobject)

                Node jumpTarget = temp;
                int jumpSteps = Random.Range(2, 5);//setting a random num for the jump
                for (int j = 0; j < jumpSteps && jumpTarget.next != null; j++)
                    jumpTarget = jumpTarget.next;

                temp.info.forward = jumpTarget;
            }
            else { i--; }//reseting if the cell is not empty
        }
    }

    private void LocateBackward()
    {
        for (int i = 0; i < backwardCount; i++)
        {
            int randomIndex = Random.Range(6, nodeTransforms.Length);//setting a random index making sure it out of range of the jump 
            Node temp = currentList.FindByIndex(randomIndex);

            if (temp != null && temp.info.type == 0)
            {
                temp.info.type = 4;
                temp.info.visualObject.GetComponent<Renderer>().material.color = Color.black;//this changes the colur of the node(gameobject)

                Node jumpTarget = temp;
                int jumpSteps = Random.Range(2, 5);//setting the rage of the jump
                for (int j = 0; j < jumpSteps && jumpTarget.prev != null; j++)
                    jumpTarget = jumpTarget.prev;

                temp.info.backward = jumpTarget;
            }
            else { i--; }//rest if the node is not empty
        }
    }

    private void DrawArrows() //this method draws the arrows between the ladders in bakward and forward cells
    {
        Node temp = head;
        while (temp != null)
        {
            if (temp.info.type == 3 && temp.info.forward != null)
                CreateArrow(temp.info.nodePosition.position, temp.info.forward.info.nodePosition.position, Color.magenta);
            else if (temp.info.type == 4 && temp.info.backward != null)
                CreateArrow(temp.info.nodePosition.position, temp.info.backward.info.nodePosition.position, Color.black);

            temp = temp.next;
        }
    }

    private void CreateArrow(Vector3 start, Vector3 end, Color color)
    {
        GameObject arrow = new GameObject("Arrow");//this creates an arrow named game objects
        LineRenderer lr = arrow.AddComponent<LineRenderer>();// and here we add a componenet to the game object
        lr.positionCount = 2;//setting th arrows postion between the two jumps
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
        lr.startWidth = 0.5f;
        lr.endWidth = 0.5f;

        Material matInstance = new Material(arrowMaterial);//using materail to set the colur
        matInstance.color = color;
        lr.material = matInstance;

        lr.startColor = color;
        lr.endColor = color;
    }
}
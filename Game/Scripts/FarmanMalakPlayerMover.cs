using System;
using System.Collections;
using UnityEngine;
//this class moves the player and checks the cell
public class PlayerMover : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] float moveSpeed = 5f;//the speed of the character moving in game

    public Boolean isMoving;
    Node currentNode;//holds where the player is


    public void MoveSteps(int steps)
    {
        StartCoroutine(Moving(steps));
    }


    IEnumerator Moving(int steps)
    {
        isMoving = true;
        Node nextNode = currentNode;

        for (int i = 0; i < steps; i++)
        {
            if (nextNode.next != null)
            {
                nextNode = nextNode.next;
                yield return StartCoroutine(MoveToPosition(nextNode.info.nodePosition.position)); // waits for MoveToPosition to finish before continuing the loop
            }
            else
            {
                Debug.Log("Reached end. Loading next scene...");
                PlayerData.playerScore = ScoreManager.instance.score;
                yield return new WaitForSeconds(1f);//this makes the code stop for a second
                FindFirstObjectByType<LoadScene>()?.LoadCurrentScene();//this loads the next scene
                FindFirstObjectByType<ScoreManager>()?.SaveScore();//this saves the score
                yield break;//this dosnt cause any delay just returns and continues the code immditly
            }
        }

        currentNode = nextNode;

        // Handle landing effects (treasure, trap, forward, backward)
        isMoving = false;
        yield return StartCoroutine(HandleLandingEffects());//after finishing the moving it checks where it landed
    }

    //

    private IEnumerator HandleLandingEffects()
    {
        bool movedAgain = true;

        while (movedAgain)// if the player moves using forward or backward we do another check what it steps on
        {
            movedAgain = false;

            float type = currentNode.info.type;

            if (type == 1) // Treasure
            {
                ScoreManager.instance.AddScore(10);//adds 10 to score
            }
            else if (type == 2) // Trap
            {
                ScoreManager.instance.SubtractScore(5);//takes 5 from score
            }
            else if (type == 3 && currentNode.info.forward != null) // Forward Jump
            {
                yield return StartCoroutine(MoveToPosition(currentNode.info.forward.info.nodePosition.position));
                currentNode = currentNode.info.forward;
                movedAgain = true;
            }
            else if (type == 4 && currentNode.info.backward != null) // Backward Jump
            {
                yield return StartCoroutine(MoveToPosition(currentNode.info.backward.info.nodePosition.position));
                currentNode = currentNode.info.backward;
                movedAgain = true;
            }
        }
    }

    //

    private IEnumerator MoveToPosition(Vector3 target)//this fumction bascilly takes the target cords and moves the player to it
    {
        // Rotate toward the target
        Vector3 direction = (target - transform.position).normalized;
        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));//this caculates how much it needs to rotate to look at the direction of the target
            while (Quaternion.Angle(transform.rotation, lookRotation) > 1f)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * moveSpeed * 2f);//this makes it turn slowly
                yield return null;//this delays the code for frame 
            }
        }

        // Move toward the target
        while (Vector3.Distance(transform.position, target) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * moveSpeed);//using MoveTowards method to move the player
            yield return null;
        }
    }

    //

    public void SetCurrentNode(Node N)
    {
        currentNode = N;

    }
}

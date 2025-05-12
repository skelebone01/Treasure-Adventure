using UnityEngine;
using UnityEngine.UI;
//this class runs a roll in managing the player by checking the dice genrating random numb
public class GameManager : MonoBehaviour
{
    [SerializeField] private Sprite[] diceFaces; //  6 dice face sprites here
    [SerializeField] private Image diceImage;    //   dice UI Image here

    private bool canRoll = true;

    public void RollDiceButton()
    {
        if (!canRoll)
            return;

        canRoll = false;

        int diceRoll = Random.Range(1, 7);

        // Update dice photo
        if (diceFaces.Length >= 6 && diceImage != null)
        {
            diceImage.sprite = diceFaces[diceRoll - 1];
        }

        // Find PlayerMover dynamically and move player
        PlayerMover playerMover = FindFirstObjectByType<PlayerMover>();
        if (playerMover != null)
        {
            playerMover.MoveSteps(diceRoll); 
            StartCoroutine(WaitForMovement(playerMover));
        }
        else
        {
            Debug.LogWarning("No PlayerMover found in scene!");
            canRoll = true;
        }

        Debug.Log("Rolled: " + diceRoll);
    }

    private System.Collections.IEnumerator WaitForMovement(PlayerMover playerMover)
    {
        while (playerMover.isMoving)
        {
            yield return null;//while the player is moving it maakes a delay per frame
        }
        canRoll = true;
    }
}

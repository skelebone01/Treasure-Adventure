using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LoadScene : MonoBehaviour
{
    [SerializeField] string scene;
    [SerializeField] TextMeshProUGUI scoreText;
    public static string lastScene;

    void Start()//in the scene after level 1 is finished this writes the score in the text
    {
        if (scoreText != null)
        {
            scoreText.SetText("Your Score is " + PlayerData.playerScore);
        }
    }

    public void LoadCurrentScene()//this loades the scene that is given by the user
    {
        SceneManager.LoadScene(scene);
    }

    public void BackToLastScene()//this loads the last scene that the player was in
    {
        if (!string.IsNullOrEmpty(lastScene))
        {
            SceneManager.LoadScene(lastScene);
        }
        else
        {
            Debug.LogWarning("No previous scene saved!");
        }
    }
    public void Load(string nextScene)//this loads the scene that comes from outside and its mostly used in alot of the buttons
    {
        lastScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(nextScene);
    }
}

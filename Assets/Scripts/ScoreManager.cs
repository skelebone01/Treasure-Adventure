using TMPro;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
//this is  the score for in game
public class ScoreManager : MonoBehaviour
{

    public static ScoreManager instance; // singleton for easy access

    public int score = 0;
    [SerializeField] TextMeshProUGUI scoreText;//in game score text
    [SerializeField] TextMeshProUGUI playerText;//in game player text

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        UpdateScoreUI();
    }

    public void AddScore(int amount)//adds tpo the score
    {
        score += amount;
        UpdateScoreUI();
    }

    public void SubtractScore(int amount)//subtracts the score
    {
        score -= amount;
        UpdateScoreUI();
    }

    void UpdateScoreUI()//updates it in the ui of the game
    {
        scoreText.text = "Score: " + score;
        playerText.text = PlayerData.playerName;
    }


    public void SaveScore()//saves the score player name and the level at the end of the level
    {
        string username = PlayerData.playerName;
        int score = PlayerData.playerScore;
        string filePath = PlayerData.filePath;
        string data = $"{username}|{score}|{SceneManager.GetActiveScene().name}\n";

        File.AppendAllText(filePath, data);

        Debug.Log("Saved score to: " + filePath);
    }
}
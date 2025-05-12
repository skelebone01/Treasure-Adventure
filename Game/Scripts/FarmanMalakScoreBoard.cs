using UnityEngine;
using TMPro;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using Unity.VisualScripting;
using System.Text;
//this basiclly runs the scoreboard in the scoreboard scene
public class ScoreBoard : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreboardText;
    [SerializeField] TextMeshProUGUI minScoreText;
    [SerializeField] TextMeshProUGUI maxScoreText;
    [SerializeField] TMP_InputField usernameInput;

    string filePath;

    void Start()//this bascilly puts the score on the score board as soon as the player comes in the page
    {
        filePath = PlayerData.filePath;
        LoadScoreboard();
    }

    public void LoadScoreboard()//this function takes it from the file and puts it in the scoreboard text
    {
        if (!File.Exists(filePath) || File.ReadAllLines(filePath) is { Length: 0 })//makes sure the file leads somehwere and it actully exists
        {
            scoreboardText.text = "No scores yet!";
            Debug.LogWarning("Score file not found or is empty.");
            return;
        }

        string[] lines = File.ReadAllLines(filePath);//reads it and puts it in a strin array organazing per line
        var textLines= new StringBuilder();

        foreach (string line in lines)//takes each line and puts it in a string builder to split it
        {
            var temp = line.Split("|");

            if (temp.Length != 3 || string.IsNullOrWhiteSpace(temp[0]) || string.IsNullOrWhiteSpace(temp[2]))
            {
                scoreboardText.text = "Invalid Syntax";
                Debug.LogWarning("Invalid Syntax");
                continue;
            }
            textLines.Append($"{temp[0]} >> {temp[1]} >> {temp[2]}\n");
        }
        scoreboardText.text = textLines.ToString();
    }
    public void OnSearchButtonClicked()//when the search button is cliked
    {
        string username = usernameInput.text.Trim();
        if (string.IsNullOrEmpty(username)) return;

        BST<UserScore> bst = new BST<UserScore>();//creats a BST
        string[] lines = File.ReadAllLines(filePath);//takes all the data storing it in a stringbuilder line by line

        foreach (string line in lines)
        {
            var temp = line.Split("|");
            UserScore user = null;

            if (temp.Length != 3 || string.IsNullOrWhiteSpace(temp[0]) || string.IsNullOrWhiteSpace(temp[2]) || !int.TryParse(temp[1], out int score))//makes sure all the data is there
            {
                Debug.LogWarning("Invalid Syntax");
                continue;
            }

            user = new UserScore(temp[0], score, temp[2]);//creats a user class and stores the data
            if(user.UserName.ToLower() == username.ToLower()){
                bst.Insert(user);//puts the use class in BST
            }
        }

        DisplayResults(bst);
    }

    private void DisplayResults(BST<UserScore> bst)
    {
        if (bst == null || bst.Size == 0)
        {
            scoreboardText.text = "No scores found.";
            minScoreText.text = "-";
            maxScoreText.text = "-";
            return;
        }

        scoreboardText.text = bst.PreOrderTraversal();
        minScoreText.text = "Min: " + bst.FindMin().Score;
        maxScoreText.text = "Max: " + bst.FindMax().Score;
    }
}
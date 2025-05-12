using UnityEngine;
//this classs holds the players data as static sp it would carry it to another scenes
public class PlayerData
{
    public static string filePath = Application.persistentDataPath + "/scores.txt";//file pathe where the score.txt
    public static string playerName;
    public static int playerScore;
}

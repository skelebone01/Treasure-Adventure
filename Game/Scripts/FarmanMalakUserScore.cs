using UnityEngine;
//the values of the BSTnodes in the BST
public class UserScore : System.IComparable<UserScore>
{
    public string UserName { get; set; }
    public int Score { get; set; }
    public string Level { get; set; }

    public UserScore(string username, int score, string level){
        this.UserName = username;
        this.Score = score;
        this.Level = level;
    }

    public int CompareTo(UserScore other)
    {
        if (other == null) return 1;
        return Score.CompareTo(other.Score);
    }

    public override string ToString()
    {
        return $"{UserName} >> {Score} >> {Level}";
    }
}


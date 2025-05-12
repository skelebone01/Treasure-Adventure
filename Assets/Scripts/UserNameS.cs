using TMPro;
using UnityEngine;
//this runs for the username scene
public class UserNameS : MonoBehaviour
{
    [SerializeField] TMP_InputField usernameInput;
    [SerializeField] TextMeshProUGUI errorText;
    public void userinfo()
    {
        if (string.IsNullOrWhiteSpace(usernameInput.text))
        {
            errorText.SetText("Error please enter a name");
        }
        else
        {
            PlayerData.playerName = usernameInput.text;
            FindFirstObjectByType<LoadScene>().LoadCurrentScene();
        }
    }
}

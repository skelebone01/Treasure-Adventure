using UnityEngine;
//this class is for exiting the game 
public class ExitGame : MonoBehaviour
{
    public void QuitGame()
    {

#if UNITY_EDITOR//it checks if youer in the unity editor if not it exits
        UnityEditor.EditorApplication.isPlaying = false;
#else

        Application.Quit();
#endif
    }
}
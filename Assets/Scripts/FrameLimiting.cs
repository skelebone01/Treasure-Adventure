using UnityEngine;
//this class limits the frame rat in the game to 60 fps
public class FrameLimiting : MonoBehaviour
{
    
    void Start()
    {
        QualitySettings.vSyncCount = 0; 
        Application.targetFrameRate = 60;
    }
}

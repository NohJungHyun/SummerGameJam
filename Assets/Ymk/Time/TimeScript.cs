using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeScript : MonoBehaviour
{
    public static float platformSpeed()
    {
        if (Application.platform == RuntimePlatform.WindowsEditor)
            return 1;
        else
            return Time.deltaTime / 0.002f;
    }

    public GameObject runBtn;
    public GameObject stopBtn;

    //일시정지 명령어
    public void RunGame(bool state)
    {
        Time.timeScale = state ? 1 : 0;
        runBtn.SetActive(!state);
        stopBtn.SetActive(state);
    }


    bool bPaused = false;
    
    private void OnApplicationPause(bool pause)
    {  
        if (pause)
        {
            bPaused = true;
            RunGame(false);
        }
        else
        {
            if (bPaused)
            {
                bPaused = false;
            }
        }
    }
}

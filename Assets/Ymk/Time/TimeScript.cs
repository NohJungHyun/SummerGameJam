using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeScript : MonoBehaviour
{
    public static float platformSpeed()
    {
        if (Application.platform == RuntimePlatform.Android)
            return 3;
        else
            return 1;
    }


    public GameObject runBtn;
    public GameObject stopBtn;

    //�Ͻ����� ��ɾ�
    public void RunGame(bool state)
    {
        Time.timeScale = state ? 1 : 0;
        runBtn.SetActive(!state);
        stopBtn.SetActive(state);
    }
}

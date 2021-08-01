using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    //�ΰ��� ������ �̵�
    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.Scene.InGame);
    }

    //���� ����
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }

    public void RecallIntro()
    {
        SceneManager.LoadScene(SceneManager.Scene.Intro);
    }
}

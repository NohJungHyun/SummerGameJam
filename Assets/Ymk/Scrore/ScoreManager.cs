using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    private Text text;

    public int kill;

    private void Awake()
    {
        kill = 0;
        if (instance == null)
        {
            text = transform.Find("nowscore").GetChild(0).GetComponent<Text>();
            transform.Find("bestscore").GetChild(0).GetComponent<Text>().text = GetBestScore().ToString();
            instance = this;
        }
        else
            Destroy(gameObject);
    }

    public static void AddScore(int n)
    {
        int now = int.Parse(instance.text.text);
        now += n;
        now = Mathf.Max(0, now);
        instance.text.text = now.ToString();
    }

    public static int GetNowScore()
    {
        return int.Parse(instance.text.text);
    }

    public static int GetBestScore()
    {
        DataManager.instance.LoadPlayData();
        if (DataManager.instance.playData != null)
            return DataManager.instance.playData.highScore;
        else
            return 0;
    }
}

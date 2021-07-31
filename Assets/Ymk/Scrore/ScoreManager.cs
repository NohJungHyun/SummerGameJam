using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    [System.NonSerialized]
    public Text text;
    [System.NonSerialized]
    public Text bestText;
    private Animation animaton;

    private void Awake()
    {
        if (instance == null)
        {
            text = transform.Find("nowscore").GetChild(0).GetComponent<Text>();
            animaton = transform.Find("nowscore").GetComponent<Animation>();
            bestText = transform.Find("bestscore").GetChild(0).GetComponent<Text>();
            instance = this;
        }
        else
            Destroy(gameObject);
    }

    public static void AddScore(int n)
    {
        int now = int.Parse(instance.text.text);
        now += n;
        instance.animaton.Play();
        instance.text.text = now.ToString();
        {
            int hs = DataManager.instance.playData.highScore;
            DataManager.instance.playData.highScore = Mathf.Max(hs, now);
            instance.bestText.text = DataManager.instance.playData.highScore.ToString();
        }

    }

    private void Start()
    {
        DataManager.instance.LoadPlayData();
        if (DataManager.instance.playData != null)
        {
            bestText.text = DataManager.instance.playData.highScore.ToString();
            DataManager.instance.playData.tryNum++;
        }
    }

    private void OnApplicationQuit()
    {
        DataManager.instance.SaveData();
    }





}

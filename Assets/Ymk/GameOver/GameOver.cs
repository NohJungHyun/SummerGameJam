using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public static bool gameEnd = false;

    Animation animation;

    public Text nowScore;
    public Text bestScore;
    public Text killnum;
    public Text trynum;

    public void OnEnable()
    {
        gameEnd = true;
        animation = GetComponent<Animation>();
        animation.Play();
        int nowS = ScoreManager.GetNowScore();
        nowScore.text = nowS.ToString();
        int bestS = ScoreManager.GetBestScore();
        if(bestS < nowS)
        {
            DataManager.instance.playData.highScore = nowS;
            bestS = nowS;
        }
        bestScore.text = bestS.ToString();
        killnum.text = ScoreManager.instance.kill.ToString();
        DataManager.instance.playData.tryNum++;
        trynum.text = DataManager.instance.playData.tryNum.ToString();
        DataManager.instance.SaveData();
    }

    public void GoTitle()
    {
        SceneManager.LoadScene(SceneManager.Scene.Title);

    }

    public void GoRetry()
    {

        SceneManager.LoadScene(SceneManager.Scene.InGame);

    }
}

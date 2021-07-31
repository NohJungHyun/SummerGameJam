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
        trynum.text = DataManager.instance.playData.tryNum.ToString();
    }

    public void GoTitle()
    {
        gameEnd = false;
        SceneManager.LoadScene(SceneManager.Scene.Title);

    }

    public void GoRetry()
    {
        gameEnd = false;
        SceneManager.LoadScene(SceneManager.Scene.InGame);

    }
}

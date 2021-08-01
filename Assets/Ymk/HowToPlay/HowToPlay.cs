using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HowToPlay : MonoBehaviour
{
    public GameObject inGame;
    public GameObject howToPlay;
    public List<GameObject> page;
    public GameObject back;
    private void Start()
    {
        DataManager.instance.LoadPlayData();

        if(DataManager.instance.playData != null)
        {
            if (DataManager.instance.playData.tryNum == 0 && DataManager.instance.playData.highScore == 0)
                howToPlay.SetActive(true);
            else
                inGame.SetActive(true);
        }
        else
            inGame.SetActive(true);

    }

    public void NextPage(int n)
    {
        Debug.Log(n);
        if(n == 2)
        {
            back.gameObject.SetActive(false);
            inGame.SetActive(true);
        }
        page[n].GetComponent<Animation>().Play();
    }



}

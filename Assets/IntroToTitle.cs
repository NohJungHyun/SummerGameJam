using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;

public class IntroToTitle : MonoBehaviour
{
    public float moveNextNum;
    public PlayableDirector playableDirector;
    // Start is called before the first frame update
    void Start()
    {
        playableDirector.Play();
    }

    private void OnEnable()
    {
        playableDirector.stopped += CheckState;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CheckState(PlayableDirector director)
    {
        SceneManager.LoadScene(SceneManager.Scene.Title, moveNextNum);
    }

    public void TouchToTitle()
    {
        Debug.Log("오호홍");

        playableDirector.Pause();
        SceneManager.LoadScene(SceneManager.Scene.Title, moveNextNum);
    }

    private void OnDisable()
    {
        playableDirector.stopped -= CheckState;
    }
}

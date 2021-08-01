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

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            playableDirector.Pause();
            SceneManager.LoadScene(SceneManager.Scene.Title, moveNextNum);
        }
    }
}

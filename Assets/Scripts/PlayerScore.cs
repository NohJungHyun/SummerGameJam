using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    public static PlayerScore instance;
    public int score;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        ResetScore();
    }

    void AddScore(int _score)
    {
        score = _score;
    }

    void ResetScore()
    {
        score = 0;
    }
}

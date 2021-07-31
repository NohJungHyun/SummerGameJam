using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    private Text text;
    private Animator animator;

    private void Awake()
    {
        if (instance == null)
        {
            text = transform.GetChild(0).GetChild(0).GetComponent<Text>();
            animator = transform.GetChild(0).GetComponent<Animator>();
            instance = this;
        }
        else
            Destroy(gameObject);
    }

    public static void AddScore(int n)
    {
        int now = int.Parse(instance.text.text);
        now += n + ComboSystem.instance.additionalScore;
        
        instance.animator.SetTrigger("Run");
        instance.text.text = string.Format("{0:D6}", now);
    }



}

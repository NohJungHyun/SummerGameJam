using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour
{
    public static TimerScript instance;

    private Slider slider;

    private void Awake()
    {
        instance = this;
        slider = GetComponent<Slider>();
        GameOver.gameEnd = false;

        slider.interactable = false;
    }

    public GameObject gameOver;

    private void Update()
    {
        if (Time.timeScale == 0)
            return;

        if (slider.value - Time.deltaTime <= 0)
        {
            slider.value = 0;
            if(!gameOver.activeSelf)
                gameOver.SetActive(true);
        }
        else
            slider.value -= Time.deltaTime;
    }

    public static void AddTime(float time)
    {
        instance.slider.value += time;
        instance.slider.value = Mathf.Max(0, instance.slider.value);
        instance.slider.value = Mathf.Min(60, instance.slider.value);
    }
}

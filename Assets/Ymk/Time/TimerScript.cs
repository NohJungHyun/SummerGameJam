using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour
{
    private Slider slider;

    private void Awake()
    {
        slider = GetComponent<Slider>();
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
}

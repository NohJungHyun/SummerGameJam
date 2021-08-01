using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour
{
    public static TimerScript instance;
    public Animation windowAnimation;

    private Slider slider;

    [Header("숫자가 커질수록 애니메이션 속도 상승 폭이 좁아짐")]
    public float animRestrainSpeed = 6; //숫자가 커질수록 상승폭이 좁아짐

    private void Awake()
    {
        windowAnimation = GetComponentInParent<Animation>();

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

        AccelerateAnim();
    }

    public static void AddTime(float time)
    {
        instance.slider.value += time;
        instance.slider.value = Mathf.Max(0, instance.slider.value);
        instance.slider.value = Mathf.Min(60, instance.slider.value);
    }

    public void AccelerateAnim()
    {
        float accel = slider.maxValue - slider.value;
    
        windowAnimation["ingame_idle"].speed = (accel / animRestrainSpeed);
        // print(windowAnimation["ingame_idle"].speed);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhaseDisplayer : MonoBehaviour
{
    public int phaseNum;
    public float changeSpeed;
    public Text phaseNotice;
    public Slider timerObj;

    [ColorUsage(true)]
    public List<Color> colors = new List<Color>();

    void Start()
    {
        ChangeText();

        TimeChecker.TimeOn += ChangeSomething;
    }

    void Update()
    {
        if(phaseNum < colors.Count)
            timerObj.fillRect.GetComponent<Image>().color = Color.Lerp(timerObj.fillRect.GetComponent<Image>().color, colors[phaseNum], changeSpeed * Time.deltaTime);
    }

    void ChangeSomething()
    {
        phaseNum++;

        ChangeText();
        // ChangeColor();
    }

    void ChangeText()
    {
        phaseNotice.text = phaseNum.ToString();
    }

    void ChangeColor()
    {
        timerObj.fillRect.GetComponent<Image>().color = Color.Lerp(timerObj.fillRect.GetComponent<Image>().color, colors[phaseNum], changeSpeed * Time.deltaTime);
    }
}

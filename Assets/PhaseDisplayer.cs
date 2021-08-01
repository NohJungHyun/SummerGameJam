using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhaseDisplayer : MonoBehaviour
{
    public List<RectTransform> flags;

    public Slider timerObj;

    int phaseChecker;

    public float offsetFromTimer;

    void Start() 
    {
        for(int idx = 0; idx < flags.Count; idx++)
        {
            float rectSizeX = GetComponent<RectTransform>().rect.size.x;
            float divide = rectSizeX / flags.Count;

            RectTransform rect = Instantiate(flags[idx], new Vector2((divide * idx) + offsetFromTimer, transform.position.y), Quaternion.identity);

            rect.transform.SetParent(transform);

            flags[idx] = rect; 
        }
        flags.Reverse();

        TimeChecker.TimeOn += DecreaseFlags;
    }

    void DecreaseFlags()
    {
        flags[phaseChecker].gameObject.SetActive(false);
        phaseChecker++;
    }
}

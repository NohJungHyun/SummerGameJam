using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeChecker : MonoBehaviour
{
    public static float spendTime;
    public float totalTime;
    public int phaseBySpendTime;

    public delegate void Dele();
    public static event Dele TimeOn;


    // Start is called before the first frame update
    void Start()
    {
        spendTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        spendTime += Time.deltaTime;

        if(spendTime > phaseBySpendTime && 60 > totalTime)
        {
            totalTime += spendTime;
            spendTime = 0;
            
            TimeOn?.Invoke();
        }
    }    
}

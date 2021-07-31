using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboSystem : MonoBehaviour
{
    public static ComboSystem instance;

    public delegate void IncreaseCount(int count);

    public static int defeatedGhost;

    [Header("콤보 유지 가능한 잔여 시간")]
    public float coninousComboTime;
    public float remainComboTime;

    [Header("점수 곱셈")]
    public float scoreMultiplier;

    private void Awake() 
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        defeatedGhost = 0;
        remainComboTime = 0;
        scoreMultiplier = 1;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IncreaseComboCount(int i)
    {

    }
}

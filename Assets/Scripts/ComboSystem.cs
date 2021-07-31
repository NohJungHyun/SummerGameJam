using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboSystem : MonoBehaviour
{
    public static ComboSystem instance;

    public ParticleSystem encourageParticle;
    public Text comboText;
    // public ScoreManager scoreManager;


    [Header("콤보 유지 가능한 잔여 시간")]
    public float coninousComboTime; //처치 시 얻게 되는 유지 시간
    public float remainComboTime;

    [Header("점수 곱셈")]
    public int additionalScore;

    public int comboCount;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        InitCombo();
    }

    void Update()
    {
        remainComboTime -= Time.deltaTime;

        if (remainComboTime < 0)
            InitCombo();
        
    }

    public void IncreaseComboCount(Vector2 ghostDiePos, bool isCatched)
    {
        if (isCatched)
        {
            comboCount++;
            remainComboTime = coninousComboTime;

            if (comboCount > 2)
            {
                CallEncourageEffect(ghostDiePos);
                comboText.text = comboCount.ToString() + "Hit!";

                if (comboCount >= 10)
                    additionalScore = comboCount;
                
            }
        }
    }

    public void CallEncourageEffect(Vector2 diePos)
    {
        encourageParticle.transform.position = diePos;
        encourageParticle.Play();
    }

    public void InitCombo()
    {
        comboCount = 0;
        remainComboTime = 0;
        additionalScore = 0;
    }
}

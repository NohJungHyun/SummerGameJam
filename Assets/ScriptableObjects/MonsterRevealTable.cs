using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostEncountPercent
{
    public Ghost ghost;
    public float encountPercentage;
    float pay;

    public void SetPay(float p)
    {
        pay = p;
    }

    public float GetPay()
    {
        return pay;
    }
}

public class MonsterRevealTable : ScriptableObject
{

    public List<GhostEncountPercent> monsterRevealList = new List<GhostEncountPercent>();
    // Start is called before the first frame update
    List<GhostEncountPercent> tempList;

    public Ghost RecruitMonster()
    {
        float totalPercent = 0;
        Ghost selectedOne = null;

        for (int reveal = 0; reveal < monsterRevealList.Count; reveal++)
        {
            totalPercent += monsterRevealList[reveal].encountPercentage;
            monsterRevealList[reveal].SetPay(monsterRevealList[reveal].encountPercentage / totalPercent);
        }

        float randTicket = Random.Range(0, 1);

        for (int i = 0; i < monsterRevealList.Count; i++)
        {
            if (monsterRevealList[i].GetPay() > randTicket)
            {
                selectedOne = monsterRevealList[i].ghost;
                break;
            }
            else if (monsterRevealList[i].GetPay() < randTicket)
                continue;

            else if (monsterRevealList[monsterRevealList.Count - 1].GetPay() < randTicket)
            {
                selectedOne = monsterRevealList[monsterRevealList.Count - 1].ghost;
                break;
            }
        }
        return selectedOne;
    }
}

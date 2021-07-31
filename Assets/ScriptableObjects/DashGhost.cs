using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ghost", menuName = "SummerGameJam/DashGhost", order = 1)]
public class DashGhost : GhostProperties
{
    public float shortDist, longDist;
    public float moveAmplified;

    Color color;

    float timer;

    bool isLongdist;

    public void Start()
    {
        ghost.SetBasicPosToProperties();
    }

    public override void Init(Ghost ghost)
    {
        base.Init(ghost);
        target = ghost.dir;
        ghost.SetBasicPosToProperties();
        color = ghost.GetComponentInChildren<SpriteRenderer>().color;
    }

    // public override void Move()
    // {
    //     while (Vector3.Distance(curPos, target) > 0.1f)
    //     {
    //         if (Vector3.Distance(basicPos, curPos) < 1f)
    //         {
    //             //Invoke("NormalMove", Time.deltaTime);
    //         }
    //     }
    // }

    // public void NormalMove()
    // {
    //     ghost.transform.position = Vector3.MoveTowards(basicPos, target, moveSpeed * Time.deltaTime);
    // }


    public override void Move()
    {
        if (ghost.targetPos != -1)
            target = SpawnningPool.Node[ghost.targetPos];

        if (Vector3.Distance(target, curPos) < 0.1f) return;

        if (!isLongdist)
        {
            if (Vector3.Distance(basicPos, curPos) < shortDist)
            {
                ghost.transform.position = Vector3.MoveTowards(curPos, target, moveSpeed * Time.deltaTime);
            }
            else
            {
                ghost.SetBasicPosToProperties();
                isLongdist = true;
            }
        }
        else
        {
            if (Vector3.Distance(basicPos, curPos) < longDist)
            {
                ghost.transform.position = Vector3.MoveTowards(curPos, target, moveSpeed * 2 * Time.deltaTime);
            }
            else
            {
                ghost.SetBasicPosToProperties();
                isLongdist = false;
            }
        }

    }

    // public override IEnumerator Move()
    // {
    //     while (true)
    //     {
    //         Debug.Log("엌ㅋㅋ");
    //         curPos = ghost.transform.position;
    //         target = ghost.dir;

    //         //basicPos = ghost.transform.position;

    //         // yield return new WaitUntil(() => )

    //         if(Vector3.Distance(basicPos, curPos) < shortDist)
    //             ghost.transform.position = Vector3.MoveTowards(curPos, target, moveSpeed * Time.deltaTime);
    //         else
    //             ghost.SetBasicPosToProperties();
            
    //         yield return new WaitUntil() 
    //         // yield return new WaitUntil(() => Vector3.Distance(basicPos, curPos) > shortDist);
    //         Debug.Log("속도 근황1: 특징 없음" + Vector3.Distance(basicPos, curPos));
            
    //         // Debug.Log("광속의 속도!!!");

    //         if(Vector3.Distance(basicPos, curPos) < longDist)
    //             ghost.transform.position = Vector3.MoveTowards(curPos, target, moveSpeed * Time.deltaTime * 2);
    //         else
    //             ghost.SetBasicPosToProperties();
    //         // ghost.transform.position = Vector3.MoveTowards(curPos, target, moveSpeed * Time.deltaTime * 2);
    //         // yield return new WaitUntil(() => Vector3.Distance(basicPos, curPos) < longDist);
    //         Debug.Log("속도 근황2: 2가 곱해지고 있다!" + Vector3.Distance(basicPos, curPos));
            
    //         yield return null;
    //         basicPos = ghost.transform.position;
    //     }
    // }
}

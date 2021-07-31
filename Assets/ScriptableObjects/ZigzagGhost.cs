using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [CreateAssetMenu(fileName = "Ghost", menuName = "SummerGameJam/ZigzagGhost", order = 2)]
public class ZigzagGhost : GhostProperties
{
        // 흔들리는 강도
    public float magnitude;

    Vector2 axis;

    public override void Init(Ghost ghost)
    {
        base.Init(ghost);
        axis = Vector2.up;
        basicPos = ghost.transform.position;
        target = ghost.dir;
    }

    public override void Move()
    {
        float yTwist = Vector2.right.x * Mathf.Abs(Mathf.Sin(Mathf.PingPong(Time.time, 1))) * magnitude;

        Vector2 basecampPos = new Vector3(ghost.transform.position.x, yTwist);
        ghost.transform.position = Vector2.MoveTowards(basecampPos, target, moveSpeed * Time.deltaTime);
        
        
    }

    // public override IEnumerator Move()
    // {
    //     while (true)
    //     {
    //         curPos = ghost.transform.position;
    //         target = ghost.dir;

    //         ghost.transform.position = Vector2.MoveTowards(curPos, target, moveSpeed * Time.deltaTime);
    //         // ghost.transform.Translate((ghost.dir - (Vector2)ghost.transform.position) * moveSpeed * Time.deltaTime);

    //         float yTwist = Vector2.right.x * Mathf.Abs(Mathf.Sin(Mathf.PingPong(Time.time, 1))) * magnitude;

    //         ghost.transform.position = new Vector2(curPos.x, yTwist);

    //         // yield return new WaitUntil(() => Vector2.Distance(basicPos, ghost.transform.position) < dist);
    //         yield return null;
    //     }
    // }
}

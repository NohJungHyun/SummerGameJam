using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [CreateAssetMenu(fileName = "Ghost", menuName = "SummerGameJam/ZigzagGhost", order = 2)]
public class ZigzagGhost : GhostProperties
{
    // 흔들리는 강도
    [Header("흔들리는 강도")]
    [Header("------------------------")]
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
        if (ghost.targetPos != -1)
            target = SpawnningPool.Node[ghost.targetPos];

        Vector2 v = (SpawnningPool.Node[ghost.spawnPos]- SpawnningPool.Node[ghost.targetPos]).normalized;
        v = new Vector2(-v.y, v.x);
        float twist = Vector2.Distance(target, ghost.transform.position) < 1f ? 0 : Mathf.Sin(Time.time) * magnitude;
        v = target + v * twist;
        ghost.transform.position = Vector2.MoveTowards(ghost.transform.position, v, moveSpeed * Time.deltaTime);
        
        
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

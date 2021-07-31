using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [CreateAssetMenu(fileName = "Ghost", menuName = "SummerGameJam/FlashGhost", order = 0)]
public class FlashGhost : GhostProperties
{
    // public float timer;

    // public float flashTime;

    // // 투명화 시간을 체크하는 데 사용.
    // public float checkTime;

    public float moveAmplified;

    PolygonCollider2D polygon;

    List<Color> c = new List<Color>();
    SpriteRenderer[] sprites;

    public float triggerDist;
    public float castOffDist;

    public bool isFlash = false;

    public override void Init(Ghost ghost)
    {
        base.Init(ghost);

        target = ghost.dir;
        polygon = ghost.GetComponent<PolygonCollider2D>();
        sprites = ghost.GetComponentsInChildren<SpriteRenderer>();

        foreach (SpriteRenderer sp in sprites)
            c.Add(sp.color);
        basicPos = ghost.transform.position;
    }

    public override void Move()
    {
        if (Vector3.Distance(target, curPos) < 0.1f) return;


        if (Vector3.Distance(basicPos, curPos) < 1f && !isFlash)
            ghost.transform.position = Vector3.MoveTowards(curPos, target, moveSpeed * Time.deltaTime);
        else
        {
            isFlash = true;
            Flash();
        }
    }

    // public override IEnumerator Move()
    // {
    //     while (true)
    //     {
    //         curPos = ghost.transform.position;
    //         target = ghost.dir;

    //         Debug.Log("BasicPos?: " + basicPos);

    //         ghost.transform.position = Vector3.MoveTowards(basicPos, target, moveSpeed * Time.deltaTime);
    //         //ghost.transform.Translate((ghost.dir - (Vector2)ghost.transform.position) * moveSpeed * Time.deltaTime);

    //         if (Vector3.Distance(basicPos, target) > triggerDist)
    //         {
    //             ghost.SetBasicPosToProperties();

    //             yield return new WaitUntil(() => Flash());
    //         }
    //         yield return null;
    //     }
    // }

    // public override IEnumerator Move()
    // {
    //     // throw new System.NotImplementedException();
    //     yield return null;
    // }

    public void Flash()
    {
        for (int i = 0; i < sprites.Length; i++)
        {
            sprites[i].color = c[i];

            if (Vector3.Distance(basicPos, curPos) < castOffDist)
            {
                ghost.transform.position = Vector3.MoveTowards(curPos, target, moveSpeed * 2 * Time.deltaTime);
                c[i] = Color.Lerp(Color.white, Color.black, Mathf.PingPong(Time.time, 1));

                polygon.enabled = false;
            }
            else
            {
                ghost.SetBasicPosToProperties();
                isFlash = false;
                polygon.enabled = true;
                Debug.Log("세상 속으로..");
            }
        }
    }


    // public override IEnumerator Move()
    // {
    //     while (true)
    //     {
    //         // ghost.transform.position = Vector3.Lerp(ghost.transform.position, ghost.dir, moveSpeed * Time.deltaTime);
    //         ghost.transform.position = Vector3.MoveTowards(ghost.transform.position, ghost.dir, moveSpeed * Time.deltaTime);

    //         timer += Time.deltaTime;
    //         int sec = (int)timer % 60;

    //         if (sec > checkTime)
    //         {
    //             Debug.Log("어둠 속으로..");

    //             sec = 0;
    //             timer = 0;

    //             yield return new WaitUntil(() => Flash());
    //         }
    //         yield return null;
    //     }
    // }

    // public bool Flash()
    // {
    //     timer += Time.deltaTime;
    //     int sec = (int)timer % 60;

    //     ghost.GetComponent<SpriteRenderer>().color = c;

    //     // 점멸 중?
    //     if (sec < flashTime)
    //     {
    //         Debug.Log("아직이다!");
    //         // ghost.transform.position = Vector3.Lerp(ghost.transform.position, ghost.dir, moveSpeed * moveAmplified * Time.deltaTime);
    //         ghost.transform.position = Vector3.MoveTowards(ghost.transform.position, ghost.dir, moveSpeed * moveAmplified * Time.deltaTime);

    //         c = Color.Lerp(Color.white, Color.black, Mathf.PingPong(Time.time, 1));

    //         polygon.enabled = false;
    //         return false;
    //     }
    //     else
    //     {
    //         // c.a = Mathf.Lerp(0, 255, Time.deltaTime * 0.1f);
    //         sec = 0;
    //         timer = 0;

    //         polygon.enabled = true;
    //         Debug.Log("세상 속으로..");
    //         return true;
    //     }
    // }
}

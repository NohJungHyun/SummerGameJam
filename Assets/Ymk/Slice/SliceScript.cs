using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliceScript : MonoBehaviour
{
    private Vector3 start;
    private Vector3 end;

    [Range(0.001f, 1)]
    public float speed;

    private float effectTime = 0;

    private void Update()
    {
        if (Time.deltaTime == 0)
            return;

        Vector2 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
            start = mouse;
        else if (Input.GetMouseButtonUp(0))
        {
            end = mouse;
            effectTime = 0.1f;
            Vector3 center = (end + start) / 2;
            Vector3 dir = (end - start).normalized;

            //공격
            RaycastHit2D[] raycastHit2D = Physics2D.BoxCastAll(center, new Vector3(Vector3.Distance(start, end), 0.5f, 1), Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg, Vector2.zero, 10, 1 << LayerMask.NameToLayer("Ghost")); 

            foreach (RaycastHit2D hit in raycastHit2D)
            {
                Ghost ghost = hit.transform.GetComponent<Ghost>();
                if (ghost == null)
                    continue;
                ghost.Die();
                ScoreManager.AddScore(50);
                GameObject temp = EffectManager.RunEffect(EffectManager.Effect.BOOM_GHOST);
                temp.transform.position = ghost.transform.position;
            }

            {
                //start->end로 이펙트가 이동하도록 설정
                GameObject temp = EffectManager.RunEffect(EffectManager.Effect.SLASH_SWORD);
                temp.transform.position = start;
                SwordMove moveObj = temp.AddComponent<SwordMove>();
                moveObj.speed = speed;
                moveObj.target = end;
            }
        }

    }

}

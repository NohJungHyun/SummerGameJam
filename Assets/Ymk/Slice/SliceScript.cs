using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliceScript : MonoBehaviour
{
    private Vector3 start;
    private Vector3 end;

    [Range(0.001f, 1)]
    public float speed;

    public GameObject swordEffect;

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
            Vector3 center = (end + start) / 2;
            Vector3 dir = (end - start).normalized;

            //����
            RaycastHit2D[] raycastHit2D = Physics2D.BoxCastAll(center, new Vector3(Vector3.Distance(start, end), 0.5f, 1), Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg, Vector2.zero, 10, 1 << LayerMask.NameToLayer("Ghost")); 

            foreach (RaycastHit2D hit in raycastHit2D)
            {
                Ghost ghost = hit.transform.GetComponent<Ghost>();
                if (ghost == null)
                    continue;
                ghost.Die(true);
                ScoreManager.AddScore(50);
            }

            {
                //start->end�� ����Ʈ�� �̵��ϵ��� ����
                GameObject temp = Instantiate(swordEffect);
                temp.transform.position = start;
                SwordMove moveObj = temp.AddComponent<SwordMove>();
                moveObj.speed = speed;
                moveObj.target = end;
            }
        }

    }

}

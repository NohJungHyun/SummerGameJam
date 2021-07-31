using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliceScript : MonoBehaviour
{
    private Vector3 start;
    private Vector3 end;
    public GameObject sword;

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
            RaycastHit2D[] raycastHit2D = Physics2D.LinecastAll(start, end);
            foreach (RaycastHit2D hit in raycastHit2D)
            {
                Ghost ghost = hit.transform.GetComponentInParent<Ghost>();
                if (ghost == null)
                    continue;
                ghost.Die();
            }

            //start->end로 이펙트가 이동하도록 설정
            GameObject temp = Instantiate(sword, start, Quaternion.identity);
            SwordMove moveObj = temp.AddComponent<SwordMove>();
            moveObj.speed = speed;
            moveObj.target = end;
        }

    }
}

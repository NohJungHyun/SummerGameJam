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
        Vector2 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
            start = mouse;
        else if (Input.GetMouseButtonUp(0))
        {         
            end = mouse;
            effectTime = 0.1f;
            RaycastHit2D[] raycastHit2D = Physics2D.LinecastAll(start, end);
            foreach (RaycastHit2D hit in raycastHit2D)
                Debug.Log(hit.transform.name);

            GameObject temp = Instantiate(sword, start, Quaternion.identity);
            SwordMove moveObj = temp.AddComponent<SwordMove>();
            moveObj.speed = speed;
            moveObj.target = end;
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliceScript : MonoBehaviour
{
    Vector3 start;
    Vector3 end;

    void Update()
    {
        Vector2 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
            start = mouse;
        else if (Input.GetMouseButtonUp(0))
        {
            end = mouse;
            RaycastHit2D[] raycastHit2D = Physics2D.LinecastAll(start, end);
            foreach (RaycastHit2D hit in raycastHit2D)
                Debug.Log(hit.transform.name);
        }

    }
}

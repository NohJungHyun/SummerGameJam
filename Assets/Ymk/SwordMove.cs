using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordMove : MonoBehaviour
{
    public float speed;
    public Vector3 target;

    private void Update()
    {
        speed = Mathf.Max(speed, 0.001f);
        transform.position = Vector3.Lerp(transform.position, target, speed);
        if (Vector3.Distance(transform.position, target) < 0.001f)
            Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordMove : MonoBehaviour
{
    public float speed;
    public Vector3 target;

    private void Update()
    {
        if (Time.deltaTime == 0)
            return;

        float nowSpeed = speed;

        if (Application.platform == RuntimePlatform.Android)
            nowSpeed *= TimeScript.platformSpeed();

        transform.position = Vector3.Lerp(transform.position, target, nowSpeed);
        if (Vector3.Distance(transform.position, target) < 0.001f)
            Destroy(gameObject);
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    Ghost ghost = collision.transform.GetComponent<Ghost>();
    //    if (ghost == null)
    //        return;
    //    ghost.Die();
    //}
}

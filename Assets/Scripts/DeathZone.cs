using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    //    private void OnCollisionEnter2D(Collision2D other)
    //    {
    //        if(other.gameObject.GetComponent<Ghost>())
    //        {
    //            Debug.Log(other.gameObject.GetComponent<Ghost>().name);

    //            SpawnningPool.spawnQueue.Enqueue(other.gameObject.GetComponent<Ghost>());
    //            other.gameObject.GetComponent<Ghost>().Die();
    //        }
    //    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.GetComponentInParent<Ghost>())
        {
            Ghost ghost = other.gameObject.GetComponentInParent<Ghost>();
            SpawnningPool.spawnQueue.Enqueue(ghost);
            ghost.Die();
        }
    }
}

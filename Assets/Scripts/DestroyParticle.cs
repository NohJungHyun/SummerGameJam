using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyParticle : MonoBehaviour
 {

     public float lifetime;
 
     private IEnumerator Start()
     {
         yield return new WaitForSeconds(lifetime);
         Destroy(gameObject); 
     }
     
 }

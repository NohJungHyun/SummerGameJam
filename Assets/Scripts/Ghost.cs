using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour, IDamagable
{
    // [SerializeField]
    public GhostProperties ghostProperties;

    public void TakeCount(int count)
    {
        Debug.Log("터치 성공!");
    }

    public void Die() 
    {
        ghostProperties.CallDeadEffect();
        this.gameObject.SetActive(false);
    }
}

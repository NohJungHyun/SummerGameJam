using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour, IDamagable
{
    // [SerializeField]
    public GhostProperties proto;
    public GhostProperties ghostProperties;
    public Vector2 dir;

    void Start() 
    {
        ghostProperties = ScriptableObject.Instantiate(proto);
        ghostProperties.Init(this);   
    }

    private void Update() 
    {
        ghostProperties.curPos = transform.position;
        
        ghostProperties.Move();
    }

    public void SetBasicPosToProperties()
    {
        ghostProperties.basicPos = transform.position;
    }

    public void TakeCount(int count)
    {
        Debug.Log("터치 성공!");
    }

    public void Die() 
    {
        ghostProperties.CallDeadEffect();
        this.gameObject.SetActive(false);
    }

    public void SetDirection(Vector3 dir)
    {
        this.dir = dir;
    }
}

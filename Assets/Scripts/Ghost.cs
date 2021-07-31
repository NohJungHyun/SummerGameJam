using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour, IDamagable
{
    // [SerializeField]
    public GhostProperties proto;
    GhostProperties ghostProperties;
    public Vector2 dir;

    public int spawnPos = -1;
    public int targetPos = -1;
    void Start() 
    {
        ghostProperties = ScriptableObject.Instantiate(proto);
        
        ghostProperties.Init(this);   
    }

    private void Update() 
    {
        ghostProperties.curPos = transform.position;

        ghostProperties.Move();

        Arrive();
    }

    private void Arrive()
    {
        if (Vector3.Distance(transform.position, dir) < 0.1f)
            Die();
    }

    public void SetBasicPosToProperties()
    {
        ghostProperties.basicPos = transform.position;
    }

    public void TakeCount(int count)
    {
        Debug.Log("터치 성공!");
    }

    public void Die(bool effect = false) 
    {
        ghostProperties.CallDeadEffect(effect);
        SpawnningPool.spawnQueue.Enqueue(this);

        ComboSystem.instance.IncreaseComboCount(transform.position, effect);

        this.gameObject.SetActive(false);
        spawnPos = -1;
        SpawnningPool.curGhostNum--;
    }

    public void SetDirection(Vector3 dir)
    {
        this.dir = dir;
    }
}

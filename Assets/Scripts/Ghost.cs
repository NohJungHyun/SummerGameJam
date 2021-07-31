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

        print(this.name + "있니?: " + GetGhostProperties());

        Arrive();
    }

    private void Arrive()
    {
        if (Vector3.Distance(transform.position, dir) < 0.1f)
        {
            Die();
            ScoreManager.AddScore(ComboSystem.instance.paneltyNum);
            BadNews.instance.InstanciateAlert();
        }
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

        ComboSystem.instance.IncreaseComboCount(transform.position, true);

        this.gameObject.SetActive(false);
        spawnPos = -1;
        SpawnningPool.curGhostNum--;
    }

    public void SetDirection(Vector3 dir)
    {
        this.dir = dir;
    }

    public void SetGhostSpeed(float s)
    {
        ghostProperties.moveSpeed = s;
    }

    public GhostProperties GetGhostProperties()
    {
        return ghostProperties;
    }
    public void SetGhostProperties(GhostProperties p)
    {
        ghostProperties = p;
    }

    // 파티클 및 이펙트 보여주기
    public IEnumerator CallBadNews()
    {
        while(true)
        {
            
            yield return null;
        }
        
    }
}


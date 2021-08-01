using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
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
        if (GameOver.gameEnd)
        {
            SpawnningPool.spawnQueue.Enqueue(this);
            this.gameObject.SetActive(false);
        }
        else
        {
            ghostProperties.curPos = transform.position;

            ghostProperties.Move();

            print(this.name + "있니?: " + GetGhostProperties());

            Arrive();
        }
    }

    private void Arrive()
    {
        if (Vector3.Distance(transform.position, dir) < 0.1f)
        {
            Die();
            ScoreManager.AddScore(ComboSystem.instance.paneltyNum);
            ComboSystem.instance.remainComboTime = 0;
            BadNews.instance.badDelegate?.Invoke();
        }
    }

    public void SetBasicPosToProperties()
    {
        ghostProperties.basicPos = transform.position;
    }

    public void Die(bool effect = false)
    {
        ghostProperties.CallDeadEffect(effect);
        SpawnningPool.spawnQueue.Enqueue(this);

        ComboSystem.instance.IncreaseComboCount(this, transform.position, effect);

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
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GhostProperties : ScriptableObject
{
    public Ghost ghost;
    public int takeCount;
    public float moveSpeed;

    public int comboCount;

    public ParticleSystem moveEffect;
    public ParticleSystem deadEffect;
    public Vector2 curPos, basicPos, target;

    public ParticleSystem particleSystem;

    public int ghostScore;

    public virtual void Init(Ghost ghost)
    {
        this.ghost = ghost;
    }

    public abstract void Move();

    public virtual void CallMoveEffect()
    {
        
    }

    public virtual void CallDeadEffect(bool effect = false)
    {
        if(effect)
        {
            Instantiate(deadEffect, curPos, Quaternion.identity);

            if(ComboSystem.instance.comboCount > 10)
                TimerScript.AddTime(0.01f* Mathf.Max(50,ComboSystem.instance.comboCount));
        }
    }

    // public void CallComboParticle(Vector2 pos)
    // {
    //     Instantiate(particleSystem, pos, Quaternion.identity);
    // }
}



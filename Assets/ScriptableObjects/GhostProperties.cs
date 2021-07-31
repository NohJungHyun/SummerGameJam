using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GhostProperties : ScriptableObject
{
    public Ghost ghost;
    public int takeCount;
    public float moveSpeed;

    public ParticleSystem moveEffect;
    public ParticleSystem deadEffect;

    public Vector2 curPos, basicPos, target;

    public virtual void Init(Ghost ghost)
    {
        this.ghost = ghost;
    }

    public abstract void Move();

    public virtual void CallMoveEffect()
    {
        
    }

    public virtual void CallDeadEffect()
    {

    }

}



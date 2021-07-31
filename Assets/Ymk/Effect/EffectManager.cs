using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : Singleton<EffectManager>
{
    public enum Effect
    {
        BOOM_GHOST,
        SLASH_SWORD
    }

    [SerializeField]
    private List<GameObject> effectObj;

    public static GameObject RunEffect(Effect effect)
    {
        return Instantiate(instance.effectObj[(int)effect]);
    }
}

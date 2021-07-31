using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadNews : MonoBehaviour
{
    public static BadNews instance;
    public ParticleSystem missParticle;
    public static Vector2 alertPos;

    private void Awake()
    {
        if (instance)
            Destroy(instance);

        instance = this;
    }

    public void InstanciateAlert()
    {
        Instantiate(missParticle, alertPos, Quaternion.identity);
    }

    public static void SetAlertPos(Vector2 vec)
    {
        alertPos = vec;
    }

}

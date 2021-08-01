using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadNews : MonoBehaviour
{
    public delegate void Dele();

    public Dele badDelegate;

    public static BadNews instance;
    public ParticleSystem missParticle;
    public static Vector2 alertPos;

    public List<GameObject> UIs;
    List<Vector2> basicPos = new List<Vector2>();
    public float shakeMagnitude; // 진도
    public float shakeTime; // 진도의 지속시간
    float remainTime;

    private void Awake()
    {
        if (instance)
            Destroy(instance);

        instance = this;
    }

    public void Start()
    {
        badDelegate += CallShake;
        badDelegate += InstanciateAlert;

        for(int idx = 0; idx < UIs.Count; idx++)
        {
            basicPos.Add(UIs[idx].transform.position);
        }
    }



    public void InstanciateAlert()
    {
        Instantiate(missParticle, alertPos, Quaternion.identity);
    }

    public static void SetAlertPos(Vector2 vec)
    {
        alertPos = vec;
    }

    public void CallShake()
    {
        if (!GameOver.gameEnd)
            StartCoroutine("ShakeUI");
    }

    public IEnumerator ShakeUI()
    {
        remainTime = 0;

        while (true)
        {
            //Debug.Log(remainTime);

            remainTime += Time.deltaTime;

            if (shakeTime < remainTime)
            {
                for (int idx = 0; idx < UIs.Count; idx++)
                {
                    basicPos.Add(UIs[idx].transform.position);
                    UIs[idx].transform.position = basicPos[idx];
                }
                yield break;
            }

            for (int idx = 0; idx < UIs.Count; idx++)
            {
                Vector2 randShakePos = new Vector2(Random.Range(-shakeMagnitude, shakeMagnitude) + UIs[idx].transform.position.x, Random.Range(-shakeMagnitude, shakeMagnitude) + UIs[idx].transform.position.y);
                UIs[idx].transform.position = Vector2.Lerp(UIs[idx].transform.position, randShakePos, Time.deltaTime * 5f) ;
            }

            //Vector2 randShakePos = new Vector2(Random.Range(-shakeMagnitude, shakeMagnitude) + ui.transform.position.x, Random.Range(-shakeMagnitude, shakeMagnitude) + ui.transform.position.y);

            // float randX = Mathf.PingPong(0, randShakePos.x);
            // float randY = -Mathf.PingPong(0, randShakePos.y);

            // ui.transform.position = Vector2.Lerp(ui.transform.position, new Vector2(randX, randY), Time.deltaTime * 3f) ;
            yield return null;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboSystem : MonoBehaviour
{
    public int scorePerSkill;
    public int paneltyNum;

    public Queue<Text> comboTextQueue;
    public static ComboSystem instance;

    public ParticleSystem encourageParticle;
    public Canvas comboTextCanvas;
    public Text comboText;
    public Vector2 particlePos;
    Color basicColor;

    [Header("콤보 유지 가능한 잔여 시간")]
    public float coninousComboTime; //처치 시 얻게 되는 유지 시간
    public float remainComboTime;

    public int comboCount;

    public float fadeoutModifier;
    public float deleteParticleTime;

    IEnumerator coroutine;

    public AudioSource audioSource;
    public List<AudioClip> comboSound;


    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        basicColor = comboText.color;

        comboTextQueue = new Queue<Text>(30);

        for (int i = 0; i < 30; i++)
        {
            Text obj = GameObject.Instantiate(comboText, new Vector2(100, 100), Quaternion.identity);
            obj.transform.SetParent(comboTextCanvas.transform);

            obj.gameObject.SetActive(false);
            comboTextQueue.Enqueue(obj);
        }

        InitCombo();
    }

    void Update()
    {
        remainComboTime -= Time.deltaTime;

        if (remainComboTime < 0)
            InitCombo();
    }

    public void IncreaseComboCount(Ghost g, Vector2 ghostDiePos, bool isCatched)
    {
        if (isCatched)
        {
            comboCount++;
            remainComboTime = coninousComboTime;

            if (comboCount > 10)
                ScoreManager.AddScore(scorePerSkill + ((int)(comboCount * 0.1) * 10));
            else
                ScoreManager.AddScore(scorePerSkill);

            if(comboCount >= 20)
            {
                int temp = comboCount - 20;
            audioSource.PlayOneShot(comboSound[Mathf.Min(comboSound.Count - 1, temp / 20)]);
            }

            if (comboCount > 2)
                CallEncourageEffect(g, ghostDiePos);
        }
    }

    Text backObject;

    public void CallEncourageEffect(Ghost g, Vector2 diePos)
    {
        if (backObject != null)
        {
            Color color = backObject.color;
            color.r *= 0.2f;
            color.g *= 0.2f;
            color.b *= 0.2f;
            color.a *= 0.5f;
            backObject.color = color;
        }

        Text calledText = comboTextQueue.Dequeue();
        calledText.gameObject.SetActive(true);
        calledText.text = comboCount.ToString() + "Hit!";
        calledText.transform.position = Camera.main.WorldToScreenPoint(diePos);

        calledText.color = Color.Lerp(Color.white, Color.red, Mathf.Min(50f, comboCount) / 50f);


        backObject = calledText;

        Vector2 vec = new Vector2(0, -5);
        ParticleSystem p = GameObject.Instantiate(g.GetGhostProperties().particleSystem, particlePos, Quaternion.identity).GetComponent<ParticleSystem>();
        p.gameObject.SetActive(true);

        StartCoroutine(FadeOutText(calledText));
        StartCoroutine(DeleteParticle(p.gameObject));
    }

    public IEnumerator FadeOutText(Text t)
    {
        while (true)
        {
            if (t.color.a > 0.1)
            {
                Color fadeOutColor = new Color(0, 0, 0, 0.1f);
                t.color -= fadeOutColor * fadeoutModifier * ((Application.platform == RuntimePlatform.Android) ? 6 : 1);
            }
            else
            {
                ReturnToQueue(t);
                yield break;
            }
            yield return Time.deltaTime;
        }
    }

    public IEnumerator DeleteParticle(GameObject obj)
    {
        while (true)
        {
            deleteParticleTime += Time.deltaTime;
            if (deleteParticleTime > 2f && obj)
            {
                Destroy(obj);
                deleteParticleTime = 0;
                yield break;
            }
            if (!obj)
                yield break;

            yield return Time.deltaTime;
        }

    }

    public void InitCombo()
    {
        comboCount = 0;
        remainComboTime = 0;
    }

    public void ReturnToQueue(Text t)
    {
        t.gameObject.SetActive(false);
        t.color = basicColor;
        comboTextQueue.Enqueue(t);
    }

}

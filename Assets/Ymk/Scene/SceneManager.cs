using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneManager : MonoBehaviour
{
    private static SceneManager _instance;
    public static SceneManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = Instantiate(Resources.Load("Manager/SceneManager") as GameObject).GetComponent<SceneManager>();
                DontDestroyOnLoad(_instance.gameObject);
            }
            return _instance;
        }
    }


    protected Image fade;
    private bool nowLoade = false;

    //ÀÌµ¿ÇÒ ¾Àµé
    public enum Scene
    {
        Title,
        InGame,
        Ymk,
        Quit
    }

    //ÀÌµ¿ÇÒ¾À,¾ÀÀÌµ¿½Ã°£
    public static void LoadScene(Scene scene,float delay = 1)
    {
        if (instance == null)
            return;
        if (instance.nowLoade)
            return;
        instance.StartCoroutine(instance.Fade(scene, delay));
    }

    private IEnumerator Fade(Scene scene, float delay)
    {
        nowLoade = true;

        float alpha = 0;

        for (int i = +1; i >= -1; i -= 2)
        {
            if (i == 1)
                fade.raycastTarget = true;
            else
                fade.raycastTarget = false;

            while (i == 1 ? alpha < 1 : alpha > 0)
            {
                alpha += Time.deltaTime * delay * 2 * i;
                Color color = fade.color;
                color.a = alpha;
                fade.color = color;
                yield return new WaitForSeconds(Time.deltaTime);
            }

            if(i == 1)
            {
                yield return new WaitForSeconds(0.3f);
                UnityEngine.SceneManagement.SceneManager.LoadScene(scene.ToString());
            }
        }

        nowLoade = false;
    }

    protected void Awake()
    {
        fade = transform.GetChild(0).GetComponent<Image>();
    }
}

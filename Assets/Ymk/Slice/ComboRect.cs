using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboRect : MonoBehaviour
{
    public RectTransform rectTransform;

    public void Update()
    {
        float maxX = +540f - rectTransform.sizeDelta.x * rectTransform.localScale.x * 0.5f;
        float minX = -540f + rectTransform.sizeDelta.x * rectTransform.localScale.x * 0.5f;
        float maxY = +960f - rectTransform.sizeDelta.y * rectTransform.localScale.y * 0.5f;
        float minY = -960f + rectTransform.sizeDelta.y * rectTransform.localScale.y * 0.5f;
        float x = rectTransform.anchoredPosition.x;
        x = Mathf.Min(Mathf.Max(x, minX), maxX);
        float y = rectTransform.anchoredPosition.y;
        y = Mathf.Min(Mathf.Max(y, minX), maxX);
       // Debug.Log(rectTransform.anchoredPosition);
        rectTransform.anchoredPosition = new Vector2(x, y);
    }
}

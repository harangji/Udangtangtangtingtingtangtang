using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    public RectTransform rectTransform;
    public TMP_Text tmp;
    public Queue<DamageText> Queue;
    
    public void ShowText(string text, Vector2 hitPosition, Color textColor = default)
    {
        rectTransform.anchoredPosition = hitPosition;
        tmp.text = text;
        tmp.color = textColor;
        gameObject.SetActive(true);
        StartCoroutine(DisAbleCoroutine());
    }
    
    private IEnumerator DisAbleCoroutine()
    {
        yield return new WaitForSeconds(1f);
        Queue.Enqueue(this);
        gameObject.SetActive(false);
    }
}
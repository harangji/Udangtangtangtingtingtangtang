
using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class DamageTextManager : MonoBehaviour
{
    public Canvas canvas;

    public DamageText textPrefab;
    public DamageText[] texts;
    
    private Queue<DamageText> mQueue = new Queue<DamageText>();
    private Vector2 screenPos;
    
    private void Awake()
    {
        InGameEventHandler.Instance.ShowDamageTextHandler += OnHitEvent;
        
        foreach (var text in texts)
        {
            text.gameObject.SetActive(false);
            text.Queue = mQueue;
            mQueue.Enqueue(text);
        }
    }

    private void OnDisable()
    {
        if (InGameEventHandler.Instance)
            InGameEventHandler.Instance.ShowDamageTextHandler -= OnHitEvent;
    }

    private void OnHitEvent(object _, ShowDamageTextEventArgs e)
    {
        DamageText text;
        
        if (mQueue.Count > 0)
        {
            text = mQueue.Dequeue();
        }
        else
        {
            text = Instantiate(textPrefab, transform);
            text.Queue = mQueue;
        }
        
        screenPos = RectTransformUtility.WorldToScreenPoint(InGameHolder.Instance.mainCamera, e.HitPosition);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, screenPos, canvas.worldCamera, out Vector2 localPosition);
        
        text.ShowText(e.Damage.ToString(), localPosition, e.Color);
    }
}
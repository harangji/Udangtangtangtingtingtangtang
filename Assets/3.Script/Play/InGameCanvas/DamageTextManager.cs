
using System.Collections.Generic;
using UnityEngine;

public class DamageTextManager : MonoBehaviour
{
    public Canvas canvas;

    public DamageText textPrefab;
    public DamageText[] texts;
    
    private Queue<DamageText> mQueue = new Queue<DamageText>();
    private Vector2 screenPos;
    
    private void Awake()
    {
        foreach (var text in texts)
        {
            text.gameObject.SetActive(false);
            text.Queue = mQueue;
            mQueue.Enqueue(text);
        }
    }

    private void Start()
    {
        foreach (var ally in InGameHolder.Instance.Allys)
        {
            ally.CharacterCallBack.ShowDamageAction += OnHitEvent;
        }
        foreach (var enemy in InGameHolder.Instance.Enemies)
        {
            enemy.CharacterCallBack.ShowDamageAction += OnHitEvent;
        }
    }
    
    private void OnHitEvent(int damage, Vector2 hitPosition, Color color)
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
        
        screenPos = RectTransformUtility.WorldToScreenPoint(InGameHolder.Instance.mainCamera, hitPosition);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, screenPos, canvas.worldCamera, out Vector2 localPosition);
        
        text.ShowText(damage.ToString(), localPosition, color);
    }
}
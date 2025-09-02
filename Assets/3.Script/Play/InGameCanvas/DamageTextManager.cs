
using System.Collections.Generic;
using UnityEngine;

public class DamageTextManager : MonoBehaviour
{
    public Canvas canvas;

    public DamageText textPrefab;
    public DamageText[] texts;

    private Camera mMainCamera;
    private Queue<DamageText> mQueue = new Queue<DamageText>();
    private Vector2 screenPos;
    
    private void Awake()
    {
        mMainCamera = Camera.main;
        foreach (var text in texts)
        {
            text.gameObject.SetActive(false);
            text.Queue = mQueue;
            mQueue.Enqueue(text);
        }
    }

    private void Start()
    {
        foreach (var ally in CharacterHolder.Instance.Allys)
        {
            ally.CharacterCallBack.HitAction += OnHitEvent;
        }
        foreach (var enemy in CharacterHolder.Instance.Enemies)
        {
            enemy.CharacterCallBack.HitAction += OnHitEvent;
        }
    }
    
    private void OnHitEvent(int damage, Vector2 hitPosition)
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
        
        screenPos = RectTransformUtility.WorldToScreenPoint(mMainCamera, hitPosition);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, screenPos, canvas.worldCamera, out Vector2 localPosition);
        
        text.ShowText(damage.ToString(), localPosition, Color.red);
    }
}
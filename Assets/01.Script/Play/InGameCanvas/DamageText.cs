using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    public static DamageText Instance { get; private set; } // 싱글톤 인스턴스

    [SerializeField] private Canvas canvas;
    [SerializeField] private DamageText textPrefab; // 자기 자신을 프리팹으로 참조
    public DamageText[] texts; // 초기 풀링을 위한 배열

    private Queue<DamageText> mQueue = new Queue<DamageText>();

    private RectTransform _rectTransform;
    private TMP_Text _tmp;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        _rectTransform = GetComponent<RectTransform>();
        _tmp = GetComponent<TMP_Text>();

        if (_rectTransform == null) Debug.LogError("DamageText: RectTransform 컴포넌트를 찾을 수 없습니다.");
        if (_tmp == null) Debug.LogError("DamageText: TMP_Text 컴포넌트를 찾을 수 없습니다.");

        InGameEventHandler.Instance.ShowDamageTextHandler += OnHitEvent;

        foreach (var text in texts)
        {
            text.gameObject.SetActive(false);
            mQueue.Enqueue(text);
        }
    }

    private void OnDisable()
    {
        if (InGameEventHandler.Instance)
            InGameEventHandler.Instance.ShowDamageTextHandler -= OnHitEvent;
    }

    private void OnHitEvent(object _, ShowAmountTextEventArgs e)
    {
        DamageText text;

        if (mQueue.Count > 0)
        {
            text = mQueue.Dequeue();
        }
        else
        {
            text = Instantiate(textPrefab, transform);
        }

        Vector2 screenPos = RectTransformUtility.WorldToScreenPoint(InGameHolder.Instance.mainCamera, e.HitPosition);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, screenPos, canvas.worldCamera, out Vector2 localPosition);

        text.ShowText(e.Amount.ToString(), localPosition, e.Color);
    }

    public void ShowText(string text, Vector2 hitPosition, Color textColor = default)
    {
        _rectTransform.anchoredPosition = hitPosition;
        _tmp.text = text;
        _tmp.color = textColor;
        gameObject.SetActive(true);
        StartCoroutine(DisAbleCoroutine());
    }

    private IEnumerator DisAbleCoroutine()
    {
        yield return new WaitForSeconds(1f);
        ReturnDamageTextToPool(this); // DamageText.Instance.ReturnDamageTextToPool(this) 대신 직접 호출
    }

    /// <summary>
    /// 사용이 끝난 DamageText를 풀에 반환합니다.
    /// </summary>
    /// <param name="damageText">반환할 DamageText 인스턴스</param>
    public void ReturnDamageTextToPool(DamageText damageText)
    {
        damageText.gameObject.SetActive(false);
        mQueue.Enqueue(damageText);
    }
}
using UnityEngine;
using TMPro;

// 플로팅 텍스트의 동작을 제어하는 스크립트입니다.
public class FloatingText : MonoBehaviour
{
    [Tooltip("텍스트가 화면에 표시될 시간입니다.")]
    public float duration = 1f;
    [Tooltip("텍스트가 위로 떠오르는 속도입니다.")]
    public float floatSpeed = 1f;

    private float timer;
    private TMP_Text tmp;
    private RectTransform rectTransform;

    void Start()
    {
        timer = duration;
        tmp = GetComponentInChildren<TMP_Text>();
        rectTransform = GetComponent<RectTransform>();

        if (tmp == null || rectTransform == null)
        {
            Debug.LogError("FloatingText: 필요한 컴포넌트(TMP_Text 또는 RectTransform)를 찾을 수 없습니다!", this);
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;

            // UI 요소를 위로 이동시킵니다.
            rectTransform.anchoredPosition += Vector2.up * (floatSpeed * Time.deltaTime);

            // 서서히 투명하게 만듭니다 (페이드 아웃).
            var color = tmp.color;
            color.a = timer / duration; // 알파 값을 시간에 따라 1에서 0으로 변경합니다.
            tmp.color = color;
        }
        else
        {
            // 지속 시간이 다 되면 스스로를 파괴합니다.
            Destroy(gameObject);
        }
    }
}

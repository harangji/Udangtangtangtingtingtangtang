using UnityEngine;

/// <summary>
/// 화살표 키 입력을 화면에 시각적으로 표시하는 GUI 스크립트입니다.
/// </summary>
public class ArrowKeyGui : MonoBehaviour
{
    [Tooltip("GUI의 화면상 위치와 크기를 정합니다.")]
    public Rect position = new Rect(10, 10, 150, 100);
    
    [Tooltip("평상시 화살표의 색입니다.")]
    public Color normalColor = new Color(0.5f, 0.5f, 0.5f, 0.5f);
    
    [Tooltip("눌렸을 때 화살표의 색입니다.")]
    public Color pressedColor = new Color(1f, 1f, 1f, 1f);

    private GUIStyle style;
    private bool stylesInitialized = false;

    // 스타일 초기화는 OnGUI에서 한 번만 수행합니다.
    private void InitializeStyles()
    {
        if (stylesInitialized) return;
        
        style = new GUIStyle(GUI.skin.box);
        style.alignment = TextAnchor.MiddleCenter;
        style.fontSize = 20; // 글자 크기를 키워서 가시성을 높입니다.
        
        stylesInitialized = true;
    }

    /// <summary>
    /// Unity의 즉시 모드 GUI를 사용하여 화살표를 그립니다.
    /// </summary>
    void OnGUI()
    {
        // 런타임에만 작동하도록 합니다 (에디터에서는 보이지만, 플레이 모드가 아닐 때 실행될 필요는 없습니다).
        if (!Application.isPlaying) return;

        InitializeStyles();

        // UI 요소들을 그룹으로 묶어 위치를 쉽게 조정합니다.
        GUI.BeginGroup(position);

        // 그룹 내에서 화살표들의 상대적인 위치를 정의합니다.
        Rect upRect = new Rect(50, 0, 50, 50);
        Rect downRect = new Rect(50, 50, 50, 50);
        Rect leftRect = new Rect(0, 50, 50, 50);
        Rect rightRect = new Rect(100, 50, 50, 50);

        // 위쪽 화살표
        GUI.color = Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W) ? pressedColor : normalColor;
        GUI.Box(upRect, "↑", style);

        // 아래쪽 화살표
        GUI.color = Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S) ? pressedColor : normalColor;
        GUI.Box(downRect, "↓", style);

        // 왼쪽 화살표
        GUI.color = Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A) ? pressedColor : normalColor;
        GUI.Box(leftRect, "←", style);

        // 오른쪽 화살표
        GUI.color = Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D) ? pressedColor : normalColor;
        GUI.Box(rightRect, "→", style);

        // 다음 GUI 요소를 위해 색상을 원래대로 복원합니다.
        GUI.color = Color.white;

        GUI.EndGroup();
    }
}

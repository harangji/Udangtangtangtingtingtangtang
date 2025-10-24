using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// 레벨업 UI에서 개별 스킬 선택 버튼의 UI 요소들을 담는 컨테이너 스크립트입니다.
/// </summary>
public class SkillChoiceButtonUI : MonoBehaviour
{
    [Tooltip("스킬 아이콘을 표시할 이미지 컴포넌트")]
    public Image skillIcon;

    [Tooltip("스킬 이름을 표시할 TextMeshProUGUI 컴포넌트")]
    public TextMeshProUGUI skillNameText;

    [Tooltip("스킬 설명을 표시할 TextMeshProUGUI 컴포넌트")]
    public TextMeshProUGUI skillDescriptionText;

    [Tooltip("선택을 위한 버튼 컴포넌트")]
    public Button choiceButton;
}

using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events; // For UnityAction

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

    /// <summary>
    /// 버튼의 UI와 클릭 이벤트를 설정합니다.
    /// </summary>
    /// <param name="skillData">표시할 스킬의 데이터</param>
    /// <param name="onSelectAction">버튼 클릭 시 실행될 액션</param>
    public void Setup(SkillData skillData, UnityAction onSelectAction)
    {
        skillIcon.sprite = skillData.icon;
        skillNameText.text = skillData.skillName;
        skillDescriptionText.text = skillData.description;
        choiceButton.onClick.RemoveAllListeners(); // 이전 리스너를 모두 제거하여 중복을 방지합니다.
        choiceButton.onClick.AddListener(onSelectAction);
    }
}

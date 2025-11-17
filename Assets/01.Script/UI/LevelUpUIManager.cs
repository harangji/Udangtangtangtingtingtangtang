using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// 레벨업 시 스킬 선택 UI를 관리하는 싱글톤 매니저입니다.
/// </summary>
public class LevelUpUIManager : SingletonBase<LevelUpUIManager>
{
    [Header("UI 요소 레퍼런스")]
    [Tooltip("활성화/비활성화될 레벨업 패널 전체")][SerializeField]
    private GameObject levelUpPanel;

    [Tooltip("스킬 선택 버튼의 프리팹")][SerializeField]
    private GameObject skillChoiceButtonPrefab;

    [Tooltip("스킬 선택 버튼들이 생성될 컨테이너의 Transform")][SerializeField]
    private Transform buttonContainer;

    private SkillController _playerSkillController; // PlayerSkillHandler 대신 SkillController를 사용합니다.

    protected override bool dontDestroyOnLoad { get; set; } = false;

    protected override void Awake()
    {
        base.Awake();
        // 게임 시작 시 패널을 숨깁니다.
        if(levelUpPanel != null) levelUpPanel.SetActive(false); 
    }

    private void Start()
    {
        // 씬에서 플레이어의 SkillController를 찾아 참조를 저장합니다.
        // 씬에 플레이어의 SkillController가 하나만 존재한다고 가정합니다.
        _playerSkillController = FindObjectOfType<SkillController>();
        if (_playerSkillController == null)
        {
            Debug.LogError("씬에서 플레이어의 SkillController를 찾을 수 없습니다.");
        }
    }

    /// <summary>
    /// 스킬 선택 옵션을 UI에 표시합니다.
    /// </summary>
    /// <param name="options">표시할 스킬 데이터 리스트</param>
    public void ShowOptions(List<SkillData> options)
    {
        // 이전에 생성된 버튼들을 모두 삭제합니다.
        foreach (Transform child in buttonContainer)
        {
            Destroy(child.gameObject);
        }

        // 레벨업 패널을 활성화하고 게임 시간을 멈춥니다.
        levelUpPanel.SetActive(true);
        Time.timeScale = 0f;

        // 각 스킬 옵션에 대한 버튼을 생성합니다.
        foreach (var skillOption in options)
        {
            GameObject buttonGO = Instantiate(skillChoiceButtonPrefab, buttonContainer);
            SkillChoiceButtonUI buttonUI = buttonGO.GetComponent<SkillChoiceButtonUI>();

            if (buttonUI != null)
            { 
                // 버튼의 UI 내용을 스킬 데이터에 맞게 설정합니다.
                buttonUI.Setup(skillOption, () => OnOptionSelected(skillOption));
            }
        }
    }

    /// <summary>
    /// 플레이어가 스킬 옵션을 선택했을 때 호출됩니다.
    /// </summary>
    /// <param name="selectedSkill">선택된 스킬 데이터</param>
    private void OnOptionSelected(SkillData selectedSkill)
    {
        if (_playerSkillController != null)
        {
            // 플레이어의 스킬 컨트롤러에 선택된 스킬을 추가/업그레이드합니다.
            _playerSkillController.AddSkill(selectedSkill);
        }
        else
        {
            Debug.LogError("씬에서 플레이어의 SkillController를 찾을 수 없습니다.");
        }

        // UI를 숨기고 게임을 재개합니다.
        HidePanel();
    }

    /// <summary>
    /// 레벨업 패널을 숨기고 게임 시간을 원래대로 되돌립니다.
    /// </summary>
    private void HidePanel()
    {
        levelUpPanel.SetActive(false);
        Time.timeScale = 1f; 
    }
}

using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// 플레이어의 레벨과 경험치를 표시하는 HUD를 관리합니다.
/// </summary>
public class PlayerHUD : MonoBehaviour
{
    [Header("UI 요소 레퍼런스")]
    [Tooltip("레벨 텍스트를 표시할 TextMeshProUGUI")]
    [SerializeField] private TextMeshProUGUI levelText;

    [Tooltip("경험치 바를 표시할 UI 이미지")]
    [SerializeField] private Image xpBarImage;

    [Header("참조 대상")]
    [Tooltip("플레이어 캐릭터 참조")]
    [SerializeField] private PlayerCharacter playerCharacter;

    void Start()
    {
        if (playerCharacter == null)
        {
            // 씬에서 플레이어를 찾아 할당합니다.
            playerCharacter = FindObjectOfType<PlayerCharacter>();
            if (playerCharacter == null)
            {
                Debug.LogError("PlayerCharacter를 씬에서 찾을 수 없습니다.");
                enabled = false; // 스크립트 비활성화
                return;
            }
        }

        // 초기 UI 업데이트
        UpdateHUD();
    }

    void Update()
    {
        // 매 프레임 UI를 업데이트합니다.
        UpdateHUD();
    }

    /// <summary>
    /// 플레이어의 현재 상태에 맞게 HUD를 업데이트합니다.
    /// </summary>
    private void UpdateHUD()
    {
        if (playerCharacter == null) return;

        // 레벨 텍스트 업데이트
        if (levelText != null)
        {
            levelText.text = $"Lv. {playerCharacter.Level}";
        }

        // 경험치 바 업데이트
        if (xpBarImage != null)
        { 
            // 경험치가 0일 때도 자연스럽게 보이도록 Mathf.Max 사용
            float fillAmount = (float)playerCharacter.CurrentExperience / playerCharacter.ExperienceToNextLevel;
            xpBarImage.fillAmount = Mathf.Max(fillAmount, 0.01f); // 최소값을 둬서 바가 아예 사라지지 않게 함
        }
    }
}

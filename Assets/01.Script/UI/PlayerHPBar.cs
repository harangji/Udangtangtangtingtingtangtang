
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 플레이어의 HP를 UI 슬라이더에 표시하는 역할을 합니다.
/// </summary>
public class PlayerHPBar : MonoBehaviour
{
    [Tooltip("HP를 표시할 UI 슬라이더")]
    public Slider hpSlider;

    private CharacterBase _playerCharacter;

    void Start()
    {
        //testtesttest
        // InGameHolder를 통해 플레이어 캐릭터의 참조를 얻어옵니다.
        if (InGameHolder.Instance != null && InGameHolder.Instance.playerCharacter != null)
        {
            _playerCharacter = InGameHolder.Instance.playerCharacter;
        }
        else
        {
            Debug.LogError("PlayerHPBar: 플레이어 정보를 찾을 수 없습니다! InGameHolder에 플레이어가 할당되었는지 확인해주세요.");
            // 플레이어를 찾지 못하면 이 컴포넌트를 비활성화합니다.
            enabled = false; 
            return;
        }

        if (hpSlider == null)
        {
            Debug.LogError("PlayerHPBar: hpSlider가 할당되지 않았습니다! 인스펙터에서 슬라이더를 연결해주세요.");
            enabled = false;
        }
    }

    void Update()
    {
        // 플레이어 캐릭터나 체력 정보가 없으면 아무것도 하지 않습니다.
        if (_playerCharacter == null || _playerCharacter.ClampedHp == null)
        {
            return;
        }

        // 슬라이더의 값을 플레이어의 현재 체력 비율로 업데이트합니다.
        // 체력 값의 범위는 0과 1 사이여야 합니다.
        hpSlider.value = (float)_playerCharacter.ClampedHp.Current / _playerCharacter.ClampedHp.Max;
    }
}

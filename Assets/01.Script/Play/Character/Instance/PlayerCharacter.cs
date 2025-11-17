using System.Threading.Tasks;
using TMPro;
using Unity.Collections;
using UnityEngine;

public class PlayerCharacter : CharacterBase
{
    public TMP_Text hpText;
    [Header("레벨/경험치")]
    public int Level = 1;
    public int CurrentExperience = 0;
    public int ExperienceToNextLevel = 10;


    public override void OnCollide(CharacterBase other)
    {
        animator.SetTrigger(DAMAGED);
        Shove(other);
        other.TakeHPChange(CombatSystem.Instance.AmountCalculated(this,other));
        hpText.text = $"{ClampedHp.Current} / {ClampedHp.Max}";
    }

    public override void Shove(CharacterBase character)
    {
        Vector2 dir = (character.transform.position - transform.position).normalized;
        character.Rb.AddForce( dir * 30f, ForceMode2D.Impulse);
    }

    public override void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent(out CharacterBase col))
        {
            if(Camp == col.Camp) return;
            OnCollide(col);
        }
    }
    
    /// <summary>
    /// 캐릭터의 경험치를 증가시키고 레벨업을 확인합니다.
    /// </summary>
    /// <param name="amount">추가할 경험치 양</param>
    public void AddExperience(int amount)
    {
        CurrentExperience += amount;
        Debug.Log($"경험치 {amount} 획득! 현재 경험치: {CurrentExperience}/{ExperienceToNextLevel}");

        while (CurrentExperience >= ExperienceToNextLevel)
        {
            LevelUp();
        }
    }

    /// <summary>
    /// 지정된 레벨에서 다음 레벨로 넘어가기 위해 필요한 '추가 경험치' 양을 계산합니다.
    /// </summary>
    /// <param name="currentLevel">현재 레벨</param>
    /// <returns>다음 레벨로 가기 위해 필요한 경험치</returns>
    private int CalculateExperienceNeededForNextLevel(int currentLevel)
    {
        // 레벨 1 -> 2 에 필요한 경험치: 10
        // 레벨 2 -> 3 에 필요한 경험치: 10 + (2-1)*5 = 15
        // 레벨 3 -> 4 에 필요한 경험치: 10 + (3-1)*5 = 20
        const int BASE_EXP_FOR_LEVEL_1_TO_2 = 10; // 레벨 1에서 2로 가기 위한 기본 경험치
        const int EXP_GROWTH_PER_LEVEL = 5;       // 레벨당 추가로 필요한 경험치 증가량

        // 'currentLevel'은 이미 레벨업이 된 상태의 레벨이므로, 
        // 이 레벨에서 다음 레벨로 가기 위한 필요 경험치를 계산.
        if (currentLevel <= 1)
        {
            return BASE_EXP_FOR_LEVEL_1_TO_2;
        }

        return BASE_EXP_FOR_LEVEL_1_TO_2 + (currentLevel - 1) * EXP_GROWTH_PER_LEVEL;
    }

    /// <summary>
    /// 캐릭터를 레벨업시키고 다음 레벨에 필요한 경험치를 설정합니다.
    /// </summary>
    private void LevelUp()
    {
        Level++;
        CurrentExperience -= ExperienceToNextLevel;
        // 다음 레벨업에 필요한 경험치를 CalculateExperienceNeededForNextLevel 함수를 통해 계산합니다.
        ExperienceToNextLevel = CalculateExperienceNeededForNextLevel(Level);

        Debug.Log($"레벨업! 레벨 {Level} 달성! 스킬 선택 창을 엽니다.");
        
        ClampedHp.SetMinMax(ClampedHp.Min, ClampedHp.Max + (int)(ClampedHp.Max * 0.1f)); 
        ClampedHp.ResetToMax();
        hpText.text = $"{ClampedHp.Current} / {ClampedHp.Max}";
        
        // 스킬 업그레이드 매니저에서 선택지를 가져옵니다.
        var options = SkillUpgradeManager.Instance.GetUpgradeOptions(3);

        if (options != null && options.Count > 0)
        {
            // UI 매니저를 통해 선택지 UI를 표시합니다.
            LevelUpUIManager.Instance.ShowOptions(options);
        }
        else
        {
            // 제공할 옵션이 없을 경우 (모든 스킬 마스터 등)
            Debug.Log("제공할 수 있는 스킬 업그레이드가 없습니다.");
        }
    }
}

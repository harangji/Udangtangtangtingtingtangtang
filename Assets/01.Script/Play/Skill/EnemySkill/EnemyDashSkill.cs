using System.Threading.Tasks;
using UnityEngine;

public abstract class EnemyDashSkill : SkillBase
{
    public EnemyDashSkill(SkillData skillData, BaseSkillHandler owner) : base(skillData, owner)
    {
        if (skillData.skillType != ESkillType.Dash)
        {
            Debug.LogWarning($"SkillData for {skillData.skillName} is not of type Dash.");
        }
    }

    protected override async Task OnActivateAsync()
    {
        /// <summary>
        /// SkillData.skillName: 스킬의 이름을 나타냅니다.
        /// SkillData.amounts[0]: 대쉬 스킬의 속도 값을 나타냅니다.
        /// SkillData.skillRange: 대쉬 스킬의 거리를 나타냅니다.
        /// SkillData.skillEffectPrefab: 스킬 발동 시 생성될 이펙트 프리팹을 나타냅니다.
        /// </summary>
        Debug.Log($"{SkillData.skillName} 대쉬 발동!");

        // 실제 대쉬 효과는 자식 클래스에서 구현
        await ApplyDashEffect();

        await Task.CompletedTask;
    }

    /// <summary>
    /// 실제 대쉬 효과를 적용하는 추상 메서드. 자식 클래스에서 구현합니다.
    /// </summary>
    protected abstract Task ApplyDashEffect();
}

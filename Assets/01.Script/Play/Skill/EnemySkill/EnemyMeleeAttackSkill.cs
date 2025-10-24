using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public abstract class EnemyMeleeAttackSkill : SkillBase
{
    public EnemyMeleeAttackSkill(SkillData skillData, BaseSkillHandler owner) : base(skillData, owner)
    {
        if (skillData.skillType != ESkillType.MeleeAttack)
        {
            Debug.LogWarning($"SkillData for {skillData.skillName} is not of type MeleeAttack.");
        }
    }

    protected override async Task OnActivateAsync()
    {
        /// <summary>
        /// SkillData.skillName: 스킬의 이름을 나타냅니다.
        /// SkillData.skillRange: 근접 공격의 유효 사거리를 나타냅니다.
        /// SkillData.amounts[0]: 근접 공격의 데미지 값을 나타냅니다.
        /// SkillData.skillEffectPrefab: 스킬 발동 시 생성될 이펙트 프리팹을 나타냅니다.
        /// </summary>
        Debug.Log($"{SkillData.skillName} 근접 공격 발동!");

        // 실제 근접 공격 효과는 자식 클래스에서 구현
        await ApplyMeleeAttackEffect();

        await Task.CompletedTask;
    }

    /// <summary>
    /// 실제 근접 공격 효과를 적용하는 추상 메서드. 자식 클래스에서 구현합니다.
    /// </summary>
    protected abstract Task ApplyMeleeAttackEffect();
}

using System.Threading.Tasks;
using UnityEngine;

public class PlayerAttackBuffSkill : BuffSkill
{
    private int _originalAttackDamage;

    public PlayerAttackBuffSkill(SkillData skillData, BaseSkillHandler owner) : base(skillData, owner)
    {
        if (skillData.skillType != ESkillType.Buff)
        {
            Debug.LogWarning($"SkillData for {skillData.skillName} is not of type Buff.");
        }
    }

    protected override async Task ApplyBuffEffect(CharacterBase target)
    {
        /// <summary>
        /// SkillData.amounts[0]: 공격력 증가 버프의 양을 나타냅니다.
        /// </summary>
        if (target.UnitStat != null)
        {
            _originalAttackDamage = target.UnitStat.Attack;
            target.UnitStat.Attack = (int)(target.UnitStat.Attack * SkillData.amounts[0]);
            Debug.Log($"{target.name}의 공격력이 {SkillData.amounts[0]}배 증가했습니다.");
        }
        await Task.CompletedTask;
    }

    protected override async Task RemoveBuffEffect(CharacterBase target)
    {
        if (target != null && target.UnitStat != null)
        {
            target.UnitStat.Attack = _originalAttackDamage;
            Debug.Log($"{SkillData.skillName} effect ended on {target.name}.");
        }
        await Task.CompletedTask;
    }
}

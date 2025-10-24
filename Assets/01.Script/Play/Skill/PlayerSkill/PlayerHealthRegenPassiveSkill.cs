using System.Threading.Tasks;
using UnityEngine;

public class PlayerHealthRegenPassiveSkill : PassiveSkill
{
    public PlayerHealthRegenPassiveSkill(SkillData skillData, BaseSkillHandler owner) : base(skillData, owner)
    {
        if (skillData.skillType != ESkillType.Passive)
        {
            Debug.LogWarning($"SkillData for {skillData.skillName} is not of type Passive.");
        }
    }

    protected override async Task ApplyPassiveEffect()
    {
        /// <summary>
        /// SkillData.amounts[0]: 초당 체력 재생량을 나타냅니다.
        /// </summary>
        float regenAmount = SkillData.amounts.Count > 0 ? SkillData.amounts[0] : 1f; // amounts[0]을 체력 재생량으로 사용, 기본값 1f

        // TODO: 실제 체력 재생 로직 구현
        // 예시: Owner.Character.Heal(regenAmount * Time.deltaTime); // Update에서 지속적으로 호출될 경우
        Debug.Log($"{Owner.Character.name}이(가) 초당 {regenAmount}의 체력을 재생합니다.");

        await Task.CompletedTask;
    }
}

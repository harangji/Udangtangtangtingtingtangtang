using System.Threading.Tasks;
using UnityEngine;

public abstract class PassiveSkill : SkillBase
{
    public PassiveSkill(SkillData skillData, BaseSkillHandler owner) : base(skillData, owner) { }

    // 패시브 스킬은 일반적으로 직접 'Activate'되지 않음.
    // 대신 SkillHandler가 특정 조건(예: 체력 50% 이하)에서 OnActivate를 호출해 줄 수 있음.
    protected override async Task OnActivateAsync()
    {
        /// <summary>
        /// SkillData.skillName: 스킬의 이름을 나타냅니다.
        /// </summary>
        Debug.Log($"{Owner.name}'s passive skill '{SkillData.skillName}' has been triggered!");
        // 실제 패시브 효과는 자식 클래스에서 구현
        await ApplyPassiveEffect();

        await Task.CompletedTask;
    }

    /// <summary>
    /// 실제 패시브 효과를 적용하는 추상 메서드. 자식 클래스에서 구현합니다.
    /// </summary>
    protected abstract Task ApplyPassiveEffect();

    // 패시브는 쿨타임이 없을 수도 있음.
    // 필요하다면 SkillData의 coolTime을 0으로 설정하거나 로직을 수정할 수 있습니다.
}

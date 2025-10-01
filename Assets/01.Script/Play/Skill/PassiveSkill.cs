using UnityEngine;
using Udangtangtang.Skill.Abstractions;

namespace Udangtangtang.Skill
{
    public class PassiveSkill : SkillBase
    {
        public PassiveSkill(SkillData skillData, SkillHandler owner) : base(skillData, owner) { }

        // 패시브 스킬은 일반적으로 직접 'Activate'되지 않음.
        // 대신 SkillHandler가 특정 조건(예: 체력 50% 이하)에서 OnActivate를 호출해 줄 수 있음.
        protected override void OnActivate()
        {
            Debug.Log($"{Owner.name}'s passive skill '{SkillData.skillName}' has been triggered!");
            // 여기에 패시브 효과 로직 구현
        }

        // 패시브는 쿨타임이 없을 수도 있음.
        // 필요하다면 SkillData의 coolTime을 0으로 설정하거나 로직을 수정할 수 있습니다.
    }
}

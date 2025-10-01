using UnityEngine;
using Udangtangtang.Skill.Abstractions;

namespace Udangtangtang.Skill
{
    public class BuffSkill : SkillBase
    {
        public BuffSkill(SkillData skillData, SkillHandler owner) : base(skillData, owner) { }

        protected override void OnActivate()
        {
            // 타겟 지정 로직이 필요. 우선은 자기 자신에게 거는 것으로 구현.
            var target = Owner; 
            
            Debug.Log($"{Owner.name} used {SkillData.skillName} on {target.name}. Duration: {SkillData.duration}, Amount: {SkillData.amount}");

            // 여기에 실제 버프/디버프 로직을 추가해야 합니다.
            // 예를 들어, 대상의 스탯을 일정 시간 동안 변경하는 코루틴을 시작할 수 있습니다.
            // target.Stat.Attack *= SkillData.amount;
            // await Task.Delay((int)(SkillData.duration * 1000));
            // target.Stat.Attack /= SkillData.amount;
        }
    }
}

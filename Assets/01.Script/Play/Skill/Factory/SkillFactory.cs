using System.Collections.Generic;
using UnityEngine;

public static class SkillFactory
{
    public static SkillBase CreateSkill(SkillData skillData, CharacterBase owner)
    {
        // 스킬 데이터의 ESkillType을 기반으로 적절한 스킬 클래스 인스턴스를 생성합니다.
        switch (skillData.skillType)
        {
            case ESkillType.BasicAttack:
                return new BasicAttackSkill(skillData, owner);
            case ESkillType.Fireball:
                return new FireballSkill(skillData, owner);
            // 여기에 다른 스킬 케이스를 추가하세요. 예:
            // case ESkillType.Fireball:
            //     return new FireballSkill(skillData, owner);
            default:
                Debug.LogWarning($"Skill factory doesn't know how to create a skill of type '{skillData.skillType}'. Returning a basic attack skill as a fallback.");
                // 기본 스킬로 대체하거나 필요에 따라 오류를 처리합니다.
                return new BasicAttackSkill(skillData, owner);
        }
    }
}
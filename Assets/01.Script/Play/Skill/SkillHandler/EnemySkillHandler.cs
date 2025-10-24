using UnityEngine;
using System.Collections.Generic;

// 이 스크립트는 Unity 에디터에서 해당 엔티티에 어떤 스킬을 할당할지 관리합니다.
public class EnemySkillHandler : BaseSkillHandler
{
    [SerializeField]
    private List<SkillData> _skillDataList; // 이 리스트에 적군 유닛이 사용할 스킬 데이터를 할당해주세요.

    protected override void Awake()
    {
        base.Awake(); // BaseSkillHandler의 Awake 호출하여 _skills 리스트 초기화

        // _skillDataList에 할당된 스킬들을 BaseSkillHandler의 _skills 리스트에 추가
        if (_skillDataList != null)
        {
            foreach (var skillData in _skillDataList)
            {
                if (skillData == null) continue;
                AddSkill(skillData);
            }
        }
    }

    /// <summary>
    /// SkillData에 따라 구체적인 스킬 인스턴스를 생성합니다.
    /// </summary>
    protected override SkillBase CreateSpecificSkillInstance(SkillData skillData)
    {
        switch (skillData.skillType)
        {
            case ESkillType.MeleeAttack:
                return new EnemyBasicMeleeAttackSkill(skillData, this);
            case ESkillType.Dash:
                return new EnemyBasicDashSkill(skillData, this);
            // TODO: EnemySkill에 맞는 다른 스킬 타입들을 여기에 추가합니다.
            default:
                Debug.LogError($"Unknown or unsupported Enemy Skill Type: {skillData.skillType}");
                return null;
        }
    }
}
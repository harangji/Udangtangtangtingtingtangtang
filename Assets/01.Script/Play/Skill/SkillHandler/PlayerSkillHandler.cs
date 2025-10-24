using UnityEngine;
using System.Collections.Generic;

// 이 스크립트는 Unity 에디터에서 해당 엔티티에 어떤 스킬을 할당할지 관리합니다.
public class PlayerSkillHandler : BaseSkillHandler
{
    [SerializeField]
    private List<SkillData> _skillDataList; // 이 리스트에 플레이어가 사용할 스킬 데이터를 할당해주세요.

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
            case ESkillType.Projectile:
                return new PlayerBasicProjectileSkill(skillData, this);
            case ESkillType.Buff:
                return new PlayerAttackBuffSkill(skillData, this);
            case ESkillType.Passive:
                return new PlayerHealthRegenPassiveSkill(skillData, this);
            case ESkillType.BasicAttack:
                return new PlayerBasicAttackSkill(skillData, this);
            // TODO: PlayerSkill에 맞는 다른 스킬 타입들을 여기에 추가합니다.
            default:
                Debug.LogError($"Unknown or unsupported Player Skill Type: {skillData.skillType}");
                return null;
        }
    }

    /// <summary>
    /// 새로운 스킬을 스킬 핸들러에 추가합니다.
    /// </summary>
    /// <param name="skillData">추가할 스킬의 데이터</param>
    public void AddSkill(SkillData skillData)
    {
        if (GetCurrentSkills().Contains(skillData))
        {
            Debug.Log($"{skillData.skillName} 스킬은 이미 보유 중입니다. 업그레이드 로직을 추가해야 합니다.");
            // TODO: 여기에 스킬 업그레이드 로직 호출 추가
            return;
        }
        
        // _skillDataList는 에디터에서 할당하는 용도이므로, 런타임에 추가되는 스킬은 _skills에 직접 추가합니다.
        base.AddSkill(skillData);
        Debug.Log($"새로운 스킬 {skillData.skillName}을 배웠습니다!");
    }
}
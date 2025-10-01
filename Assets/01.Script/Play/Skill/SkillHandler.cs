using UnityEngine;
using System.Collections.Generic;
using Udangtangtang.Skill;
using Udangtangtang.Skill.Abstractions;

public class SkillHandler : MonoBehaviour
{
    [SerializeField]
    private List<SkillData> _skillDataList;
    private List<SkillBase> _skills;

    private void Awake()
    {
        _skills = new List<SkillBase>();

        foreach (var skillData in _skillDataList)
        {
            if (skillData == null) continue;

            // SkillData의 ESkillType에 따라 적절한 SkillBase 인스턴스 생성
            switch (skillData.skillType)
            {
                case ESkillType.Projectile:
                    _skills.Add(new ProjectileSkill(skillData, this));
                    break;
                case ESkillType.Buff:
                case ESkillType.Debuff:
                    _skills.Add(new BuffSkill(skillData, this));
                    break;
                case ESkillType.Passive:
                    _skills.Add(new PassiveSkill(skillData, this));
                    break;
                default:
                    Debug.LogError($"Unknown skill type: {skillData.skillType}");
                    break;
            }
        }
    }

    private void Update()
    {
        // 모든 스킬의 쿨타임 갱신
        foreach (var skill in _skills)
        {
            skill.Tick();
        }

        // 테스트용: '1' 키를 누르면 첫 번째 스킬 사용
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ActivateSkill(0);
        }
        // 테스트용: '2' 키를 누르면 두 번째 스킬 사용
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ActivateSkill(1);
        }
    }

    /// <summary>
    /// 지정된 인덱스의 스킬을 발동시킵니다.
    /// </summary>
    /// <param name="skillIndex">스킬 리스트의 인덱스</param>
    public void ActivateSkill(int skillIndex)
    {
        if (skillIndex < 0 || skillIndex >= _skills.Count)
        {
            Debug.LogError($"Invalid skill index: {skillIndex}");
            return;
        }

        _skills[skillIndex].Activate();
    }
}

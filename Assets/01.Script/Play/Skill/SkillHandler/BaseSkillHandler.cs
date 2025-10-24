using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public abstract class BaseSkillHandler : MonoBehaviour
{
    public CharacterBase Character { get; private set; }
    private List<SkillBase> _skills = new List<SkillBase>(5);

    protected virtual void Awake()
    {
        Character = GetComponent<CharacterBase>();
        if (Character == null)
        {
            Debug.LogError($"CharacterBase component not found on {gameObject.name}");
        }
    }

    private void Update()
    {
        // 모든 스킬의 쿨타임 갱신 및 자동 사용
        foreach (var skill in _skills)
        {
            skill.Tick(); // 쿨타임 감소
            skill.Activate(); // 쿨타임이 다 됐으면 자동으로 스킬 사용
        }
    }

    /// <summary>
    /// 현재 보유하고 있는 스킬의 SkillData 리스트를 반환합니다.
    /// </summary>
    public List<SkillData> GetCurrentSkills()
    {
        return _skills.Select(s => s.SkillData).ToList();
    }

    /// <summary>
    /// SkillData를 기반으로 실제 스킬 인스턴스를 생성하고 리스트에 추가합니다.
    /// 파생 클래스에서 스킬을 추가할 때 사용합니다.
    /// </summary>
    protected void AddSkill(SkillData skillData)
    {
        if (skillData == null) return;
        SkillBase newSkill = CreateSpecificSkillInstance(skillData);
        if (newSkill != null) // null 체크 추가
        {
            _skills.Add(newSkill);
        }
        else
        {
            Debug.LogError($"Failed to create skill instance for {skillData.skillName} ({skillData.skillType}). It will not be added to the skill list.");
        }
    }

    /// <summary>
    /// SkillData에 따라 구체적인 스킬 인스턴스를 생성하는 추상 팩토리 메서드.
    /// 자식 클래스에서 구현합니다.
    /// </summary>
    protected abstract SkillBase CreateSpecificSkillInstance(SkillData skillData);

    /// <summary>
    /// 지정된 인덱스의 스킬이 쿨타임 중인지 확인합니다.
    /// </summary>
    public bool IsSkillOnCooldown(int skillIndex)
    {
        if (skillIndex < 0 || skillIndex >= _skills.Count)
        {
            Debug.LogError($"Invalid skill index: {skillIndex}");
            return false;
        }
        return _skills[skillIndex].IsOnCooldown;
    }

    /// <summary>
    /// 스킬 타입에 해당하는 스킬의 인덱스를 반환합니다.
    /// </summary>
    public int GetSkillIndex(ESkillType skillType)
    {
        for (int i = 0; i < _skills.Count; i++)
        {
            if (_skills[i].SkillData.skillType == skillType)
            {
                return i;
            }
        }
        return -1; // 해당 스킬 타입이 없을 경우 -1 반환
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
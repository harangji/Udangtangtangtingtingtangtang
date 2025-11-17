using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(CharacterBase))]
public class SkillController : MonoBehaviour
{
    private CharacterBase _owner;
    private List<SkillBase> _skills = new List<SkillBase>();

    private void Awake()
    {
        _owner = GetComponent<CharacterBase>();
        
        // SkillUpgradeManager에 자신을 등록합니다.
        if (SkillUpgradeManager.Instance != null)
        {
            SkillUpgradeManager.Instance.RegisterPlayerSkillController(this);
        }
    }

    private void Update()
    {
        foreach (var skill in _skills)
        {
            skill.Tick(Time.deltaTime);
        }
    }

    public void AddSkill(SkillData skillData)
    {
        if (skillData == null) return;

        var existingSkill = _skills.FirstOrDefault(s => s.SkillData == skillData);

        if (existingSkill != null)
        {
            // Skill already exists, so upgrade it.
            existingSkill.Upgrade();
        }
        else
        {
            // This is a new skill.
            var newSkill = SkillFactory.CreateSkill(skillData, _owner);
            if (newSkill != null)
            {
                _skills.Add(newSkill);
                newSkill.OnAdded();
                Debug.Log($"{skillData.skillName} learned!");
            }
        }
    }

    public void RemoveSkill(SkillBase skill)
    {
        if (skill != null && _skills.Contains(skill))
        {
            skill.OnRemoved();
            _skills.Remove(skill);
        }
    }

    public void RemoveAllSkills()
    {
        foreach (var skill in _skills)
        {
            skill.OnRemoved();
        }
        _skills.Clear();
    }

    private void OnDestroy()
    {
        RemoveAllSkills();
        
        // SkillUpgradeManager에서 자신을 등록 해제합니다.
        if (SkillUpgradeManager.Instance != null)
        {
            SkillUpgradeManager.Instance.UnregisterPlayerSkillController();
        }
    }

    public List<SkillData> GetCurrentSkills()
    {
        return _skills.Select(s => s.SkillData).ToList();
    }

    public List<SkillBase> GetSkillInstances()
    {
        return _skills;
    }
}

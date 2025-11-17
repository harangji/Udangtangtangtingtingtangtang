using UnityEngine;

public abstract class SkillBase
{
    public SkillData SkillData { get; private set; }
    public CharacterBase Owner { get; private set; }
    public float Cooldown { get; protected set; }
    public int CurrentLevel { get; protected set; }

    public SkillBase(SkillData skillData, CharacterBase owner)
    {
        SkillData = skillData;
        Owner = owner;
        CurrentLevel = 1;
        
        // Initialize stats from level 1 data
        if (SkillData.cooldownPerLevel != null && SkillData.cooldownPerLevel.Count > 0)
        {
            Cooldown = SkillData.cooldownPerLevel[0];
        }
    }

    public virtual void Upgrade()
    {
        if (IsMaxLevel()) return;

        CurrentLevel++;
        
        // Update stats based on new level
        if (SkillData.cooldownPerLevel != null && SkillData.cooldownPerLevel.Count >= CurrentLevel)
        {
            Cooldown = SkillData.cooldownPerLevel[CurrentLevel - 1];
        }
        
        Debug.Log($"{SkillData.skillName} has been upgraded to Level {CurrentLevel}");
    }

    public bool IsMaxLevel()
    {
        return CurrentLevel >= SkillData.maxLevel;
    }

    public abstract void Activate();

    public virtual void Tick(float deltaTime)
    {
        if (Cooldown > 0)
        {
            Cooldown -= deltaTime;
        }
    }

    public virtual void OnAdded() { }

    public virtual void OnRemoved() { }
}

using UnityEngine;

public abstract class AutoSkillBase : SkillBase
{
    public AutoSkillBase(SkillData skillData, CharacterBase owner) : base(skillData, owner) { }

    public override void Tick(float deltaTime)
    {
        base.Tick(deltaTime);
        Activate();
    }

    public override void Activate()
    {
        if (Cooldown <= 0)
        {
            Execute();
            // 현재 스킬 레벨에 맞는 쿨다운을 적용합니다.
            if (SkillData.cooldownPerLevel != null && SkillData.cooldownPerLevel.Count >= CurrentLevel)
            {
                Cooldown = SkillData.cooldownPerLevel[CurrentLevel - 1];
            }
            else
            {
                Debug.LogWarning($"Cooldown data not found for {SkillData.skillName} at level {CurrentLevel}. Setting cooldown to 0.");
                Cooldown = 0f;
            }
        }
    }

    protected abstract void Execute();
}

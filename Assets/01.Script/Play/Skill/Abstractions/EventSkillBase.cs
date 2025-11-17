
using System;
using UnityEngine;

public abstract class EventSkillBase<T> : SkillBase where T : EventArgs
{
    public EventSkillBase(SkillData skillData, CharacterBase owner) : base(skillData, owner) { }

    public override void Activate() { }

    public override void OnAdded()
    {
        base.OnAdded();
        SubscribeToEvent();
    }

    public override void OnRemoved()
    {
        base.OnRemoved();
        UnsubscribeFromEvent();
    }

    protected abstract void SubscribeToEvent();
    protected abstract void UnsubscribeFromEvent();

    protected void OnEventTriggered(T eventArgs)
    {
        if (Cooldown <= 0)
        {
            Execute(eventArgs);
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

    protected abstract void Execute(T eventArgs);
}

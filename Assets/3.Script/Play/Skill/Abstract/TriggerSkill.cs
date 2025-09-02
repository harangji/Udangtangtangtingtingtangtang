using System.Threading.Tasks;
using UnityEngine;

public abstract class TriggerSkill : SkillBase
{
    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.TryGetComponent(out IInGameCharacter col))
        {
            if (col.ColliderCamp == TargetColliderCamp) return;
            
            CombatEvent combatEvent = new CombatEvent()
            {
                EventType = ECombatEventType.SkillHit,
                Skill = this,
                Sender = SkillCaster,
                Receiver = col,
            };
            
            CombatSystem.Instance.AddCombatEvent(combatEvent);
            
            CombatSystem.Instance.AddCombatEvent(            
                new CombatEvent()
                {
                    EventType = ECombatEventType.SkillHit,
                    Skill = this,
                    Sender = SkillCaster,
                    Receiver = col,
                }
            );
        }
    }
}
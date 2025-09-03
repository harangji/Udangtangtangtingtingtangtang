using System.Threading.Tasks;
using Unity.Android.Gradle.Manifest;
using UnityEngine;

public abstract class TriggerSkill : SkillBase
{
    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.TryGetComponent(out CharacterBase character))
        {
            if (character.Camp == TargetColliderCamp) return;
            
            CombatSystem.Instance.AddCombatEvent(
                new SkillEvent()
                {
                    Sender = SkillCaster,
                    Receiver = character,
                    Skill = this
                }
            );
        }
    }
}
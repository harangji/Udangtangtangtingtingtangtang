using System.Threading.Tasks;
using UnityEngine;

public abstract class SkillBase : MonoBehaviour, ISkill
{
    public abstract EColliderCamp TargetColliderCamp { get; set; }
    public abstract CharacterBase SkillCaster { get; set; }
    public abstract TaskCompletionSource<bool> Tcs { get; set; }
    public abstract Task ActivateSkill(CharacterBase sender, CollideEvent[] target);

    public virtual void OnCollide(CharacterBase target)
    {
        CombatSystem.Instance.AddCombatEvent(
            new CollideEvent()
            {
                Sender = SkillCaster,
                Receiver = target,
            }
        );
    }
    
    public virtual void CollideEvent(CharacterBase character)
    {

    }
}

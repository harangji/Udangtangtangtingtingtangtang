using System.Threading.Tasks;
using UnityEngine;

public abstract class SkillBase : MonoBehaviour, ISkill
{
    public abstract EColliderCamp TargetColliderCamp { get; set; }
    public abstract IInGameCharacter SkillCaster { get; set; }
    public abstract TaskCompletionSource<bool> Tcs { get; set; }
    public abstract Task ActivateSkill(IInGameCharacter sender, CombatEvent[] target);
    public abstract void ApplySkill(IInGameCharacter[] target);
    public abstract void OnCollide(CombatEvent combatEvent);
}

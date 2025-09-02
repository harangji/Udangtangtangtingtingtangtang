
using System.Threading.Tasks;

public interface ISkill
{
    public abstract EColliderCamp TargetColliderCamp { get; }
    public IInGameCharacter SkillCaster { get; }
    public TaskCompletionSource<bool> Tcs { get; }
    public Task ActivateSkill(IInGameCharacter sender, CombatEvent[] target);
    public void ApplySkill(IInGameCharacter[] target);
}
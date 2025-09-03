
using System.Threading.Tasks;

public interface ISkill
{
    public abstract EColliderCamp TargetColliderCamp { get; }
    public CharacterBase SkillCaster { get; }
    public TaskCompletionSource<bool> Tcs { get; }
    public Task ActivateSkill(CharacterBase sender, CollideEvent[] target);
}
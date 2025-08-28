
using System.Threading.Tasks;

public interface ISkill : ICollidable
{
    public ICharacter Sender { get; }
    public TaskCompletionSource<bool> Tcs { get; }
    public void ExecuteSkill(ICharacter sender, ICombatEvent[] target);
    public void ApplySkill(ICharacter[] target);
}
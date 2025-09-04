
using System.Threading.Tasks;

public interface ISkill
{
    public string Skillname { get; set; }
    public string Description { get; set; }
    public void SkillEffect();
}

public interface ISkillInstance
{
    public TaskCompletionSource<bool> Tcs { get; }
    public Task ActivateSkill(CharacterBase sender, CollideEvent[] target);
}
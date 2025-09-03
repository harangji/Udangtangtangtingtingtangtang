using System.Threading.Tasks;

public interface ISkillCastable
{
    public ISkill[] Skills { get; }
    public Task ExecuteSkill(int index, CharacterBase[] target = null);
}
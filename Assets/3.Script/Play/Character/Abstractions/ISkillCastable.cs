using System.Threading.Tasks;

public interface ISkillCastable
{
    public ISkillInstance[] Skills { get; }
    public Task ExecuteSkill(int index, CharacterBase[] target = null);
}
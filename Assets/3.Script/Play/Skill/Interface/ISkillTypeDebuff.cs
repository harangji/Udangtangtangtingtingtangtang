using System.Collections.Generic;
using System.Threading.Tasks;

public interface ISkillTypeDebuff<T> : ISkill
{
    public float BuffAmount { get; }
    public Task ExecuteDeBuff(T target);
}
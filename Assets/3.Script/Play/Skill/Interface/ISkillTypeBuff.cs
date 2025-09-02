using System.Collections.Generic;

public interface ISkillTypeBuff<T> : ISkill
{
    public float BuffAmount { get; }
    public void ApplyBuff(T target);
}
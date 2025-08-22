using System.Collections.Generic;

public interface ISkillTypeAttack<T> : ISkill
{
    public float AttackAmount { get; }
    public void ExecuteAttack(T target);
}

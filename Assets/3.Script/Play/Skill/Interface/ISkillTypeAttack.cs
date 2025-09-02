using System.Collections.Generic;
using UnityEngine;

public interface ISkillTypeAttack<T>
{
    public float AttackAmount { get; }
    public void ApplyAttack(T target);
}

using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 모든 스킬이 구현해야 하는 기본 인터페이스입니다.
/// </summary>
public interface ISkill
{
    public string Skillname { get; set; }
    public string Description { get; set; }
    public void SkillEffect();
}

/// <summary>
/// 공격 타입의 스킬이 구현하는 인터페이스입니다.
/// </summary>
public interface ISkillTypeAttack<T>
{
    public float AttackAmount { get; }
    public void ApplyAttack(T target);
}

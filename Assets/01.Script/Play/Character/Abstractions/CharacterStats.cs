using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    [Header("Combat Stats")]
    public Stat Hp = new Stat(100f);
    public Stat Damage = new Stat(10f);
    public Stat AttackSpeed = new Stat(1f);
    public Stat Range = new Stat(5f);

    [Header("Movement Stats")]
    public Stat MoveSpeed = new Stat(5f);
    public Stat MaxSpeed = new Stat(10f);

    public void AddHpModifier(StatModifier mod)
    {
        Hp.AddModifier(mod);
    }

    public void RemoveHpModifier(StatModifier mod)
    {
        Hp.RemoveModifier(mod);
    }

    public void AddDamageModifier(StatModifier mod)
    {
        Damage.AddModifier(mod);
    }

    public void RemoveDamageModifier(StatModifier mod)
    {
        Damage.RemoveModifier(mod);
    }

    public void AddAttackSpeedModifier(StatModifier mod)
    {
        AttackSpeed.AddModifier(mod);
    }

    public void RemoveAttackSpeedModifier(StatModifier mod)
    {
        AttackSpeed.RemoveModifier(mod);
    }

    public void AddRangeModifier(StatModifier mod)
    {
        Range.AddModifier(mod);
    }

    public void RemoveRangeModifier(StatModifier mod)
    {
        Range.RemoveModifier(mod);
    }

    public void AddMoveSpeedModifier(StatModifier mod)
    {
        MoveSpeed.AddModifier(mod);
    }

    public void RemoveMoveSpeedModifier(StatModifier mod)
    {
        MoveSpeed.RemoveModifier(mod);
    }

    public void AddMaxSpeedModifier(StatModifier mod)
    {
        MaxSpeed.AddModifier(mod);
    }

    public void RemoveMaxSpeedModifier(StatModifier mod)
    {
        MaxSpeed.RemoveModifier(mod);
    }

    /// <summary>
    /// 모든 스탯에서 특정 출처(source)를 가진 모든 모디파이어를 제거합니다.
    /// </summary>
    public void RemoveAllModifiersFromSource(object source)
    {
        Hp.RemoveAllModifiersFromSource(source);
        Damage.RemoveAllModifiersFromSource(source);
        AttackSpeed.RemoveAllModifiersFromSource(source);
        Range.RemoveAllModifiersFromSource(source);
        MoveSpeed.RemoveAllModifiersFromSource(source);
        MaxSpeed.RemoveAllModifiersFromSource(source);
    }
}

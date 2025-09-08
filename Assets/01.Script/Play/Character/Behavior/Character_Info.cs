
using System;
using UnityEngine;

public class CharacterInfo : ICharacter
{
    public ECharacterType Type { get; }
    public CharacterStat UnitStat { get; set; }
    public ClampedInt ClampedHp { get; set; }
    
    public CharacterInfo(ECharacterType type, CharacterStat unitStat)
    {
        Type = type;
        UnitStat = unitStat;
        ClampedValueObserve();
    }
    
    public void ClampedValueObserve()
    {
        ClampedHp = new ClampedInt(0, UnitStat.Hp, UnitStat.Hp);
    }
}
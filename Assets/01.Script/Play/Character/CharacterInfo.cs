using System;

[Serializable]
public class CharacterStat
{
    public int Hp = 1000;
    public int HpRegenAmount = 1;
    public int HpRegenTerm = 1;
    public int Attack = 20;
    public float Speed = 10f;
};

// public class CharacterContext : IKeyedData
// {
//     public string Key { get; set; }
//     public ECharacterType Type { get; set; }
//     public EColliderType ColliderCamp { get; set; }
//     public ISkill[] Skills { get; set; }
//     public CharacterStat UnitStat { get; set;}
// }
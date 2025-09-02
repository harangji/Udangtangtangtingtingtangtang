
public interface ICharacter
{
    public ECharacterType Type { get; }
    public CharacterStat UnitStat { get; }
}

public interface IInGameCharacter : ICharacter, ISkillCastable, ICollidable, IDamageable
{
    public CharacterBase Instance { get; }
}


public interface ICharacterSample
{
    public ECharacterType Type { get; }
    public EColliderCamp ColliderCamp { get; }
    public CharacterStat CharacterStat { get; }
}


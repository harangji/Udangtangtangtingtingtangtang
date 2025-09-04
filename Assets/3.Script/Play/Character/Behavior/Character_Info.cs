
public class CharacterInfo : ICharacter
{
    public ECharacterType Type { get; }
    public CharacterStat UnitStat { get; }

    public CharacterInfo(ECharacterType type, CharacterStat unitStat)
    {
        Type = type;
        UnitStat = unitStat;
    }
}
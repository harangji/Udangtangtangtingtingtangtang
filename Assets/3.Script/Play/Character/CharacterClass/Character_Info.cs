
public class Character_Info : ICharacter
{
    public ECharacterType Type { get; }
    public CharacterStat UnitStat { get; }

    public Character_Info(ECharacterType type, CharacterStat unitStat)
    {
        this.Type = type;
        this.UnitStat = unitStat;
    }
}
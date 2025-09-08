
public interface ICharacter
{
    public ECharacterType Type { get; }
    public CharacterStat UnitStat { get; set; }
    public ClampedInt ClampedHp { get; }
    public void ClampedValueObserve();
}

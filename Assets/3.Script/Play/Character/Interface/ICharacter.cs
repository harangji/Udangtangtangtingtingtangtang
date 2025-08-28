
public enum ECharacterType
{
    Attacker,
    Defender,
    Healer,    
}

public interface ICharacter : ICollidable
{
    public ECharacterType Type { get; }
    public CharacterBase Character { get; }
    
    public class Stat
    {
        public int Hp;
        public int HpRegenAmount;
        public int HpRegenTerm;

        public int Mp;
        public int MpRegen;
        public int MpRegenTerm;

        public int Attack;
        public float Speed;
    };
    
    public Stat UnitStat { get; set; }

    public void InitializeStat(Stat stat);
}

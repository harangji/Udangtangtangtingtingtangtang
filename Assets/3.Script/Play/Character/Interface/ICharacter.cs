
public interface ICharacter
{
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

    public Stat InitializeStat();
}

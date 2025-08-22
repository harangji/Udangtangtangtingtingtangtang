using System.Threading.Tasks;
using UnityEngine;

public abstract class CharacterBase : MonoBehaviour, ICharacter, ICollidable, ISkillCastable
{
    public class Stat
    { 
        public int Hp = 100;
        public int HpRegenAmount = 10;
        public int HpRegenTerm = 5;

        public int Mp = 100;
        public int MpRegen = 10;
        public int MpRegenTerm = 2;

        public int Attack = 10;
        public float Speed = 1.0f;
    }

    public ICharacter.Stat UnitStat { get; set; }
    
    public abstract ICharacter.Stat InitializeStat();

    public abstract void Shove();

    public abstract void Shoved();

    public abstract Task ExecuteSkill();
}
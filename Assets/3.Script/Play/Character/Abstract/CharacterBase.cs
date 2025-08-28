using System;
using System.Threading.Tasks;
using UnityEngine;


public abstract class CharacterBase : MonoBehaviour, ICharacter, ISkillCastable
{
    public abstract CharacterBase Character { get; }
    public abstract ISkill[] Skills { get; }
    public abstract ECharacterType Type { get; set; }
    public abstract EColliderCamp ColliderCamp { get; set; }
    [field : SerializeField] public Rigidbody2D Rb { get; private set; }
    public ICharacter.Stat UnitStat { get; set; }
    
    
    public abstract void InitializeStat(ICharacter.Stat stat);
    public abstract void OnCollide(ICharacter target);
    public abstract Task ExecuteSkill();
    public abstract void Shove(CharacterBase target);
    public abstract void Shoved(CharacterBase target);
    protected abstract void TakeDamage(int damage);

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent(out ICharacter character))
        {
            if (character.ColliderCamp == ColliderCamp) return; //동일 팀
            
            ICombatEvent combatEvent = new CollideEvent()
            {
                Sender = this,
                Receiver = character,
            };
            
            CombatSystem.Instance.AddCombatEvent(combatEvent);
        }
    }
}
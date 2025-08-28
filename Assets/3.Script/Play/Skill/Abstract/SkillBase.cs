using System.Threading.Tasks;
using UnityEngine;


public abstract class SkillBase : MonoBehaviour, ISkill
{
    public abstract EColliderCamp ColliderCamp { get; set; }
    public abstract ICharacter Sender { get; set; }
    public abstract TaskCompletionSource<bool> Tcs { get; set; }
    public abstract void ExecuteSkill(ICharacter sender, ICombatEvent[] target = null);
    public abstract void ApplySkill(ICharacter[] target);

    public abstract void OnCollide(ICharacter target);
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out ICharacter col))
        {
            if (col.ColliderCamp == ColliderCamp) return;
            
            ICombatEvent combatEvent = new SkillEvent()
            {
                Skill = this,
                Sender = Sender,
                Receiver = col,
            };
            
            CombatSystem.Instance.AddCombatEvent(combatEvent);
        }
    }
}

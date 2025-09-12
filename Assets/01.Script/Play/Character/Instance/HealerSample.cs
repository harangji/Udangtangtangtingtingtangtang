
using UnityEngine;

public class HealerSample : CharacterBase
{
    public override void Shove(CharacterBase _){}
    public override void OnCollide(CharacterBase other)
    {
        other.TakeHPChange(CombatSystem.Instance.AmountCalculated(this,other));
    }
    
    public override void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent(out CharacterBase col))
        {
            if(Camp != col.Camp) return;
            OnCollide(col);
        }
    }
}

using System.Threading.Tasks;
using Unity.Collections;
using UnityEngine;

public class AttackerSample : CharacterBase
{
    public override void OnCollide(CharacterBase other)
    {
        Shove(other);
        other.TakeHPChange(CombatSystem.Instance.AmountCalculated(this,other));
    }

    public override void Shove(CharacterBase character)
    {
        Vector2 dir = (character.transform.position - transform.position).normalized;
        character.Rb.AddForce( dir * 30f, ForceMode2D.Impulse);
    }

    public override void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent(out CharacterBase col))
        {
            if(Camp == col.Camp) return;
            OnCollide(col);
        }
    }
}

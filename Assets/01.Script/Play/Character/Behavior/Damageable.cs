
using UnityEngine;

public class Damageable : IDamageable
{
    private static readonly int DAMAGED = Animator.StringToHash("3_Damaged");
    private readonly CharacterBase mCharacter;
    
    public Damageable(CharacterBase c)
    {
        mCharacter = c;
        mCharacter.Interface.Character.ClampedHp.Events.OnDecreased += GetIsDead;
    }
    
    public void TakeDamage(int amount)
    {
        mCharacter.Interface.Character.ClampedHp.Decrease(amount);
        mCharacter.animator.SetTrigger(DAMAGED);
        InGameEventHandler.Instance.ShowDamageTextHandler?.Invoke(this, 
            new ShowDamageTextEventArgs()
            {
                Damage = amount,
                HitPosition = mCharacter.transform.position,
                Color = Color.red,
            }
        );
    }

    private void GetIsDead(int current, int _)
    {
        if (current > 0) return;
        MyDebug.Log("die", 7);
        
        InGameHolder.Instance.RemoveCharacters(mCharacter);
        mCharacter.BAlive = false;
        mCharacter.gameObject.SetActive(false);
    }
}
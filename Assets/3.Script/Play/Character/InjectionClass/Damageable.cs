
using UnityEngine;

public class Damageable : IDamageable
{
    private readonly CharacterBase mCharacter;
    
    public Damageable(CharacterBase c)
    {
        mCharacter = c;
    }
    
    public void TakeDamage(int amount)
    {
        CharacterStat unitStat = mCharacter.Interface.Character.UnitStat;
        unitStat.Hp -= amount;

        InGameEventHandler.Instance.ShowDamageTextHandler?.Invoke(this, 
            new ShowDamageTextEventArgs()
            {
                Damage = amount,
                HitPosition = mCharacter.transform.position,
                Color = Color.red,
            }
        );
        
        if (unitStat.Hp <= 0)
        {
            MyDebug.Log("die", 7);
            unitStat.Hp = 0;
             
            mCharacter.bAlive = false;
            mCharacter.gameObject.SetActive(false);
        }
    }
}
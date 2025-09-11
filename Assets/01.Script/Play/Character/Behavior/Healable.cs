
using UnityEngine;

public class Healable : IHealable
{
    private readonly CharacterBase mCharacter;
    
    public Healable(CharacterBase c)
    {
        mCharacter = c;
    }
    
    public void TakeHeal(int amount)
    {
        if(!mCharacter.BAlive) return;
        
        mCharacter.Interface.Character.UnitStat.Hp += amount;
        
        InGameEventHandler.Instance.ShowDamageTextHandler?.Invoke(this, 
            new ShowDamageTextEventArgs()
            {
                Damage = amount,
                HitPosition = mCharacter.transform.position,
                Color = Color.green,
            }
        );
    }
}
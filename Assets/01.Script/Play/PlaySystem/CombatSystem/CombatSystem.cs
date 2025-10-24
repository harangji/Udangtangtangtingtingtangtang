using System;
using System.Collections.Generic;

public class CombatSystem : SingletonBase<CombatSystem>
{
    protected override bool dontDestroyOnLoad { get; set; } = false;
    
    public int AmountCalculated(CharacterBase sender, CharacterBase receiver)
    {
        int result = 0;
        CharacterStat senderStat = sender.UnitStat;
        CharacterStat targetStat = receiver.UnitStat;

        switch (sender.Type)
        {
            case ECharacterType.Attacker:
                result = -(int)(senderStat.Attack * (1f - (targetStat.Defense * 0.01f)));
                break;

            case ECharacterType.Defender:
                result = -(int)(senderStat.Attack * (1f - (targetStat.Defense * 0.01f)));
                break;
            
            case ECharacterType.Healer:
                result = senderStat.Attack;
                break;
        }
        
        return result;
    }
}

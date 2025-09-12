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
    
    // public void AddCombatEvent(ICombatEvent combatEvent) 
    // {
    //     if (combatEvents.Count < 1000)
    //     {
    //         combatEvents.Enqueue(combatEvent);
    //     }
    // }
    //
    // private void UpdateCombatEvent(ICombatEvent combatEvent)
    // {
    //     if(combatEvent == null) return;
    //     
    //     InjectionInterface senderInterface = combatEvent.Sender.Interface;
    //     InjectionInterface receiverInterface = combatEvent.Receiver.Interface;
    //     
    //     if(senderInterface.Collidable.Camp == receiverInterface.Collidable.Camp)
    //     {
    //         if (senderInterface.Character.Type == ECharacterType.Healer)
    //         {
    //             receiverInterface.Healable.TakeHeal(AmountCalculated(senderInterface, receiverInterface, combatEvent.Skill));
    //         }
    //     }
    //     else
    //     {
    //         if (senderInterface.Character.Type == ECharacterType.Attacker)
    //         {
    //             if(!combatEvent.Skill)
    //                 senderInterface.Collidable.OnCollide(combatEvent.Receiver.transform.position);
    //             receiverInterface.Damageable.TakeDamage(AmountCalculated(senderInterface, receiverInterface, combatEvent.Skill));
    //         }
    //     }
    // }
}

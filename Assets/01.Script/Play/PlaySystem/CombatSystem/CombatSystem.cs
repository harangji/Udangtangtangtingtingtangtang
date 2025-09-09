using System;
using System.Collections.Generic;

public class CombatSystem : SingletonBase<CombatSystem>
{
    protected override bool dontDestroyOnLoad { get; set; } = true;
    private Queue<ICombatEvent> combatEvents = new Queue<ICombatEvent>();
    
    private void Update()
    {
        if (combatEvents.Count > 0)
        {
            UpdateCombatEvent(combatEvents.Dequeue());
        }
    }

    public void AddCombatEvent(ICombatEvent combatEvent) 
    {
        if (combatEvents.Count < 1000)
        {
            combatEvents.Enqueue(combatEvent);
        }
    }
    
    private void UpdateCombatEvent(ICombatEvent combatEvent)
    {
        if(combatEvent == null) return;
        
        InjectionInterface senderInterface = combatEvent.Sender.Interface;
        InjectionInterface receiverInterface = combatEvent.Receiver.Interface;
        
        if(senderInterface.Collidable.Camp == receiverInterface.Collidable.Camp)
        {
            if (senderInterface.Character.Type == ECharacterType.Healer)
            {
                receiverInterface.Healable.TakeHeal(AmountCalculated(senderInterface, receiverInterface, combatEvent.Skill));
            }
        }
        else
        {
            if (senderInterface.Character.Type == ECharacterType.Attacker)
            {
                if(!combatEvent.Skill)
                    senderInterface.Collidable.OnCollide(combatEvent.Receiver.transform.position);
                receiverInterface.Damageable.TakeDamage(AmountCalculated(senderInterface, receiverInterface, combatEvent.Skill));
            }
        }
    }

    private int AmountCalculated(InjectionInterface senderInterface, InjectionInterface receiverInterface, SkillBase skill = null)
    {
        int result = 0;
        CharacterStat senderStat = senderInterface.Character.UnitStat;
        CharacterStat targetStat = receiverInterface.Character.UnitStat;
        
        //계산
        result = senderStat.Attack; // * 어떤 기준
        
        return result;
    }
}

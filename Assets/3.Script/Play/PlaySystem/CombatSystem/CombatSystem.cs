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

        CharacterBase sender = combatEvent.Sender;
        CharacterBase receiver = combatEvent.Receiver;
        
        if(sender.Interface.Collidable.Camp == receiver.Interface.Collidable.Camp)
        {
            if (sender.Interface.Character.Type == ECharacterType.Healer)
            {
                receiver.Interface.Healable?.TakeHeal(sender.Interface.Character.UnitStat.Attack);
            }
        }
        else
        {
            if (sender.Interface.Character.Type == ECharacterType.Attacker)
            {
                sender.Interface.Collidable?.OnCollide(combatEvent);
                receiver.Interface.Damageable?.TakeDamage(DamageCalculated(sender, receiver));
            }
        }
    }

    private int DamageCalculated(CharacterBase sender, CharacterBase receiver)
    {
        int result = 0;
        CharacterStat senderStat = sender.Interface.Character.UnitStat;
        CharacterStat targetStat = receiver.Interface.Character.UnitStat;
        
        //계산
        result = senderStat.Attack; // * 어떤 기준
        
        return result;
    }
}

using System.Collections.Generic;

public class CombatSystem : SingletonBase<CombatSystem>
{
    protected override bool dontDestroyOnLoad { get; set; } = true;
    private Queue<CombatEvent> combatEvents = new Queue<CombatEvent>();
    
    private void Update()
    {
        if (combatEvents.Count > 0)
        {
            UpdateCombatEvent();
        }
    }

    public void AddCombatEvent(CombatEvent combatEvent) 
    {
        if (combatEvents.Count < 1000)
        {
            combatEvents.Enqueue(combatEvent);
        }
    }
    
    private void UpdateCombatEvent()
    {
        CombatEvent combatEvent = combatEvents.Dequeue();
        
        switch (combatEvent.EventType)
        {
            case ECombatEventType.Collide:
                combatEvent.Sender.OnCollide(combatEvent);
                combatEvent.Receiver.TakeDamage(DamageCalculated(combatEvent));
                break;
            
            case ECombatEventType.SkillHit:
                combatEvent.Skill.OnCollide(combatEvent);
                break;
            default:
                break;
        }
    }

    public int DamageCalculated(CombatEvent combatEvent)
    {
        int result = 0;
        CharacterStat sender = combatEvent.Sender.UnitStat;
        CharacterStat target = combatEvent.Sender.UnitStat;

        result = sender.Attack; // * 어떤 기준
        
        return result;
    }
}

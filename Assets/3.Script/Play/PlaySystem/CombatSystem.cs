using System.Collections.Generic;

public enum CombatEventType
{
    Collide,
    Skill
}
public interface ICombatEvent
{
    public CombatEventType EventType { get; }
    public ICharacter Sender { get; }
    public ICharacter Receiver { get; }
}
public class CollideEvent : ICombatEvent
{
    public CombatEventType EventType => CombatEventType.Collide;
    public ICharacter Sender { get; set; }
    public ICharacter Receiver { get; set; }
}
public class SkillEvent : ICombatEvent
{
    public CombatEventType EventType => CombatEventType.Skill;
    public ICharacter Sender { get; set; }
    public ICharacter Receiver { get; set; }
    public ISkill Skill;
}


public class CombatSystem : SingletonBase<CombatSystem>
{
    protected override bool dontDestroyOnLoad { get; set; } = true;
    private Queue<ICombatEvent> combatEvents = new Queue<ICombatEvent>();
    
    private void Update()
    {
        if (combatEvents.Count > 0)
        {
            UpdateCombatEvent();
        }
    }

    public void AddCombatEvent(ICombatEvent combatEvent) 
    {
        combatEvents.Enqueue(combatEvent);
    }
    
    private void UpdateCombatEvent()
    {
        ICombatEvent combatEvent = combatEvents.Dequeue();
        
        switch (combatEvent.EventType)
        {
            case CombatEventType.Collide:
                MyDebug.Log(combatEvent.Sender.Character.name, 3);
                combatEvent.Sender.OnCollide(combatEvent.Receiver);
                break;
            
            case CombatEventType.Skill:
                SkillEvent skillEvent = (SkillEvent) combatEvent;
                skillEvent.Skill.OnCollide(combatEvent.Receiver);
                break;
            default:
                break;
        }
    }
}

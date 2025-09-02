public enum ECombatEventType
{
    Collide,
    SkillHit
}

public class CombatEvent
{
    public ECombatEventType EventType { get; set; }
    public IInGameCharacter Sender { get; set; }
    public IInGameCharacter Receiver { get; set; }
    public SkillBase Skill { get; set; }
}
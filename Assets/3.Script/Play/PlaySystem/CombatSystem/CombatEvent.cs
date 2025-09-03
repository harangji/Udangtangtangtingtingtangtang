
public interface ICombatEvent
{
    public CharacterBase Sender { get; set; }
    public CharacterBase Receiver { get; set; }
}

public class CollideEvent : ICombatEvent
{
    public CharacterBase Sender { get; set; }
    public CharacterBase Receiver { get; set; }
}

public class SkillEvent : ICombatEvent
{
    public CharacterBase Sender { get; set; }
    public CharacterBase Receiver { get; set; }
    public SkillBase Skill { get; set; }
}
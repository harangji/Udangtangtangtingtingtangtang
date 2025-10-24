
public interface ICombatEvent
{
    public CharacterBase Sender { get; set; }
    public CharacterBase Receiver { get; set; }
}

public class CombatEvent : ICombatEvent
{
    public CharacterBase Sender { get; set; }
    public CharacterBase Receiver { get; set; }
}
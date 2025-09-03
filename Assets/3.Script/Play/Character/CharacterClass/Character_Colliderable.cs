
public class Character_Colliderable : ICollidable
{
    public EColliderCamp Camp { get; set; }
    
    public void OnCollide(CharacterBase character)
    {
        character.CharacterCallBack.CollideAction?.Invoke();
        character.Shove(character);
    }
    
    public void OnCollide(CharacterBase sender, CharacterBase target)
    {
        sender.CharacterCallBack.CollideAction?.Invoke();
        sender.Shove(target);
    }
    
    private Character_Colliderable(EColliderCamp camp)
    {
        this.Camp = camp;
    }
}
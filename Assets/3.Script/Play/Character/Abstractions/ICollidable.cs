public interface ICollidable
{
    public EColliderCamp Camp { get; }
    public void OnCollide(CharacterBase character);
}
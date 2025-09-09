
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Collidable : ICollidable
{
    private CharacterBase mCharacter;
    public EColliderCamp Camp { get; set; }
    public CircleCollider2D Col { get; private set;}
    public Rigidbody2D Rb { get; private set; }
    public void OnCollide(ICombatEvent e)
    {
        throw new System.NotImplementedException();
    }

    public Collidable(CharacterBase character, EColliderCamp camp, CircleCollider2D col, Rigidbody2D rb)
    {
        mCharacter = character;
        Camp = camp;
        Col = col;
        Rb = rb;
    }
    
    public void OnCollide(Vector3 position)
    {
        Shove(position);
    }
    
    private void Shove(Vector3 position)
    {
        Vector2 dir = (mCharacter.transform.position - position).normalized;
        Rb.AddForce( dir * 30f, ForceMode2D.Impulse);
    }
}
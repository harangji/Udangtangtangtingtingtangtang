
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Collidable : MonoBehaviour,ICollidable
{
    private CharacterBase mCharacter;
    public EColliderCamp Camp { get; set; }
    public CircleCollider2D Col { get; private set;}
    public Rigidbody2D Rb { get; private set; }
    
    public void Init(CharacterBase c, EColliderCamp camp, CircleCollider2D col, Rigidbody2D rb)
    {
        mCharacter = c;
        Camp = camp;
        Col = col;
        Rb = rb;
    }
    
    public void OnCollide(ICombatEvent e)
    {
        Shove(e);
    }
    
    private void Shove(ICombatEvent e)
    {
        Vector2 dir = (mCharacter.transform.position - e.Receiver.transform.position).normalized;
        Rb.AddForce( -dir * 30f, ForceMode2D.Impulse); //나와 반대 방향으로 * 힘만큼 밀기
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent(out CharacterBase col))
        {
            CombatSystem.Instance.AddCombatEvent(new CollideEvent()
            {
                Sender = mCharacter,
                Receiver = col,
            });
        }
    }
}
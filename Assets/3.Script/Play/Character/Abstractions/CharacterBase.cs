using System;
using System.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;

public abstract class CharacterBase : MonoBehaviour, ICharacter, ISkillCastable, ICollidable, IDamageable
{
    [field: SerializeField] public ECharacterType Type { get; set; }
    [field: SerializeField] public EColliderCamp Camp { get; set; }
    
    [field: SerializeField] public CircleCollider2D Col { get; private set;}
    [field: SerializeField] public Rigidbody2D Rb { get; private set; }
    public abstract ISkill[] Skills { get; set; }
    
    public const float INIT_RADIUS = 1f;
    public float CurrentRadius { get; private set; } = 1f;
    public CharacterStat UnitStat { get; set; } = new CharacterStat();
    
    public class CallBack
    {
        public Action CollideAction { get; set; }
        public Action<int, Vector2, Color> ShowDamageAction { get; set; }
    }
    
    public CallBack CharacterCallBack { get; } = new CallBack();
    
    private void Awake()
    {
        if (Camp == EColliderCamp.AllyCamp)
        {
            InGameHolder.Instance.Allys.Add(this);
        }
        else
        {
            InGameHolder.Instance.Enemies.Add(this);
        }
    }

    public void UpdateRadius(float updateRadius)
    {
        CurrentRadius = updateRadius;
        transform.localScale = new Vector3(updateRadius, updateRadius, updateRadius);
    }
    
    public virtual void OnCollide(CharacterBase character)
    {
        CharacterCallBack.CollideAction?.Invoke();
        Shove(character);
    }
    
    public virtual void Shove(CharacterBase target)
    {
        Vector2 dir = (transform.position - target.transform.position).normalized;
        target.Rb.AddForce( -dir * 100f, ForceMode2D.Impulse); //나와 반대 방향으로 * 힘만큼 밀기
    }

    public virtual void CollideEvent(CharacterBase character)
    {
        CombatSystem.Instance.AddCombatEvent(
            new CollideEvent()
            {
                Sender = this,
                Receiver = character,
            }
        );
    }
    
    public abstract Task ExecuteSkill(int index, CharacterBase[] target = null);

    public virtual void TakeDamage(int amount)
    {
        UnitStat.Hp -= amount;
        CharacterCallBack.ShowDamageAction?.Invoke(amount, transform.position, Color.red);
        
        //ToDo:: die
        // if (UnitStat.Hp <= 0)
        // {
        //     MyDebug.Log("die", 7);
        //     UnitStat.Hp = 0;
        //     
        //     gameObject.SetActive(false);
        //     //die
        // }
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent(out CharacterBase col))
        {
            if (col.Camp == Camp)
            {
                
                return;
            }
            OnCollide(col);
        }
    }
}
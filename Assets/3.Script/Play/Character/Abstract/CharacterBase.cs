using System;
using System.Threading.Tasks;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterStat
{
    public int Hp = 100;
    public int HpRegenAmount = 1;
    public int HpRegenTerm = 1;
    public int Attack = 20;
    public float Speed = 10f;
};

public class CharacterContext : IKeyedData
{
    public string Key { get; set; }
    public ECharacterType Type { get; set; }
    public EColliderCamp ColliderCamp { get; set; }
    public ISkill[] Skills { get; set; }
    public CharacterStat UnitStat { get; set;}
}

public abstract class CharacterBase : MonoBehaviour, IInGameCharacter
{
    [field: SerializeField] public ECharacterType Type { get; set; }
    [field: SerializeField] public EColliderCamp ColliderCamp { get; set; }
    [field: SerializeField] public CircleCollider2D Col { get; private set;}
    [field: SerializeField] public Rigidbody2D Rb { get; private set; }
    public abstract ISkill[] Skills { get; set; }
    
    [ShowInInspector,ReadOnly] public float InitRadius { get; private set; } = 1f;
    [ShowInInspector,ReadOnly] public float CurrentRadius { get; private set; } = 1f;
    
    public CharacterStat UnitStat { get; set; } = new CharacterStat();
    public CharacterBase Instance => this;
    
    public class CallBack
    {
        public Action<CombatEvent> CollideAction { get; set; }
        public Action<int, Vector2> HitAction { get; set; }
    }
    
    public CallBack CharacterCallBack { get; } = new CallBack();

    private void Awake()
    {
        if (ColliderCamp == EColliderCamp.AllyCamp)
        {
            CharacterHolder.Instance.Allys.Add(this);
        }
        else
        {
            CharacterHolder.Instance.Enemies.Add(this);
        }

        UpdateRadius(2f);
    }

    public CharacterBase InitializeStat(CharacterContext context)
    {
        Type = context.Type;
        ColliderCamp = context.ColliderCamp;
        Skills = context.Skills;
        UnitStat = context.UnitStat;
        
        return this;
    }

    public void UpdateRadius(float updateRadius)
    {
        CurrentRadius = updateRadius;
        transform.localScale = new Vector3(updateRadius, updateRadius, updateRadius);
    }
    
    public virtual void OnCollide(CombatEvent combatEvent)
    {
        CharacterCallBack.CollideAction?.Invoke(combatEvent);
        Shove(combatEvent.Receiver.Instance);
    }
    
    protected virtual void Shove(CharacterBase target)
    {
        Vector2 dir = (transform.position - target.transform.position).normalized;
        target.Rb.AddForce( -dir * 100f, ForceMode2D.Impulse); //나와 반대 방향으로 * 힘만큼 밀기
    }
    
    public abstract Task ExecuteSkill(int index, CharacterBase[] target = null);

    public virtual void TakeDamage(int damage)
    {
        UnitStat.Hp -= damage;
        CharacterCallBack.HitAction?.Invoke(damage, transform.position);
        
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
        if (other.gameObject.TryGetComponent(out IInGameCharacter character))
        {
            if (character.ColliderCamp == ColliderCamp) return; //동일 팀
            
            CombatSystem.Instance.AddCombatEvent(            
                    new CombatEvent()
                    {
                        EventType = ECombatEventType.Collide,
                        Sender = this,
                        Receiver = character,
                    }
                );
        }
    }
}
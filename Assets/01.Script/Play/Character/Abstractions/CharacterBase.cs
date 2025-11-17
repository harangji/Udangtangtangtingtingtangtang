using System;
using System.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public abstract class CharacterBase : MonoBehaviour
{
    public CharacterStats Stats { get; protected set; }
    public ECharacterType Type;
    public EColliderCamp Camp;
    
    public Animator animator;
    public CircleCollider2D Col;
    public Rigidbody2D Rb;
    public CharacterStat UnitStat;
    public ClampedInt ClampedHp { get; set; }
    public bool BAlive { get; set; } = true;
    [ReadOnly] public int testhp;
    
    
    protected readonly int DAMAGED = Animator.StringToHash("3_Damaged");
    
    public void Awake()
    {
        Stats = GetComponent<CharacterStats>();
        ClampedHp = new ClampedInt(0, (int)Stats.Hp.Value, (int)Stats.Hp.Value);
        InGameHolder.Instance.AddCharacters(this);
    }
    
    public void TakeHPChange(int amount)
    {
        if(!BAlive) return;
        
        ClampedHp.Increase(amount);
        testhp = ClampedHp.Current;
        
        if (ClampedHp.IsMin) Dead();
        
        InGameEventHandler.Instance.ShowDamageTextHandler?.Invoke(this, 
            new ShowAmountTextEventArgs()
            {
                Amount = amount,
                HitPosition = transform.position,
                Color = amount >= 0 ? Color.green : Color.red
            }
        );
    }
    
    public virtual void Dead()
    {
        MyDebug.Log("die", 7);
        InGameHolder.Instance.RemoveCharacters(this);
        BAlive = false;
        gameObject.SetActive(false);
    }
    
    public abstract void OnCollide(CharacterBase other);
    public abstract void Shove(CharacterBase character);
    public abstract void OnCollisionEnter2D(Collision2D other);
}
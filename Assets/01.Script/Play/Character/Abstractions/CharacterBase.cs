using System;
using System.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;

public abstract class CharacterBase : MonoBehaviour
{
    [Header("프리팹/오브젝트")]
    public GameObject ProjectilePrefab; // 발사할 투사체 프리팹

    [Header("캐릭터 속성")]
    public ECharacterType Type;
    public EColliderCamp Camp;
    
    [Header("컴포넌트")]
    public Animator animator;
    public CircleCollider2D Col;
    public Rigidbody2D Rb;

    [Header("스탯")]
    public CharacterStat UnitStat;
    public ClampedInt ClampedHp { get; set; }
    public bool BAlive { get; set; } = true;
    public bool IsInvincible { get; set; } = false;
    
    [ReadOnly] public int testhp;
    
    protected readonly int DAMAGED = Animator.StringToHash("3_Damaged");
    
    public void Awake()
    {
        if (UnitStat == null)
        {
            UnitStat = new CharacterStat(); // UnitStat이 할당되지 않았다면 기본값으로 초기화
            Debug.LogWarning($"{gameObject.name}: UnitStat이 할당되지 않아 기본값으로 초기화되었습니다.");
        }
        ClampedHp = new ClampedInt(0, UnitStat.Hp, UnitStat.Hp);
    }

    public void Start()
    {
        InGameHolder.Instance.AddCharacters(this);
    }

    public void TakeHPChange(int amount)
    {
        if(!BAlive || IsInvincible) return;
        
        ClampedHp.Increase(amount);
        testhp = ClampedHp.Current;
        
        if (ClampedHp.IsMin) Dead();
        
        if (InGameEventHandler.IsInitialized)
        {
            InGameEventHandler.Instance.OnShowDamageText(this, 
                new ShowAmountTextEventArgs()
                {
                    Amount = amount, 
                    HitPosition = transform.position,
                    Color = amount >= 0 ? Color.green : Color.red
                }
            );    
        }
    }
    
    protected virtual void Dead()
    {
        MyDebug.Log("die", 7);
        InGameHolder.Instance.RemoveCharacters(this);
        BAlive = false;
        gameObject.SetActive(false);
    }
    
    public abstract void OnCollide(CharacterBase other);
    public abstract void Shove(CharacterBase character);
    public abstract void OnCollisionEnter2D(Collision2D other);

    protected virtual void FixedUpdate()
    {
        // 이 메서드는 이제 비어 있으며, 자식 클래스에서 필요에 따라 재정의(override)하여 사용할 수 있습니다.
    }
}
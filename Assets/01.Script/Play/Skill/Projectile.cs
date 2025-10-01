using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float _damage;
    private float _speed;
    private Rigidbody2D _rb;
    
    // private SkillHandler _owner; // To avoid damaging the owner

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        if (_rb == null)
        {
            Debug.LogError("Projectile requires a Rigidbody2D component.");
            Destroy(gameObject);
        }
    }

    public void Initialize(float damage, float speed, SkillHandler owner)
    {
        _damage = damage;
        _speed = speed;
        // _owner = owner;
        _rb.linearVelocity = transform.up * _speed;
        
        // 일정 시간 후 자동 파괴
        Destroy(gameObject, 5f); 
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // if(other.GetComponent<SkillHandler>() == _owner) return; // don't hit owner
        
        // 여기에 충돌 시 데미지를 주는 로직 추가
        // 예: other.GetComponent<IDamageable>()?.TakeDamage(_damage);
        Debug.Log($"Projectile hit {other.name} for {_damage} damage.");
        
        // 충돌 후 파괴
        Destroy(gameObject);
    }
}

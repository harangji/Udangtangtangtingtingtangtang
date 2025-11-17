
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterBase))]
[RequireComponent(typeof(Rigidbody2D))]
public class AllyAI : MonoBehaviour
{
    private CharacterBase _characterBase;
    private Rigidbody2D _rb;
    private bool _isKnockedBack = false;
    private Transform _playerTransform;

    [Header("플레이어 충돌")] [Tooltip("플레이어와 충돌 시 밀려나는 힘")]
    public float playerShoveForce = 2f;

    [Header("플레이어 공격 피격")] [Tooltip("플레이어 공격에 맞았을 때 튕겨나가는 힘")]
    public float knockbackForce = 20f;

    [Tooltip("플레이어 공격에 맞았을 때 회전하는 힘")] public float knockbackTorque = 100f;
    [Tooltip("넉백 지속 시간")] public float knockbackDuration = 1.5f;
    [Tooltip("넉백 상태에서 적에게 입히는 충돌 데미지")] public int collisionDamage = 10;

    void Awake()
    {
        _characterBase = GetComponent<CharacterBase>();
        _rb = GetComponent<Rigidbody2D>();

        if (InGameHolder.Instance != null && InGameHolder.Instance.playerCharacter != null)
        {
            _playerTransform = InGameHolder.Instance.playerCharacter.transform;
        }
        else
        {
            Debug.LogWarning("AllyAI: 플레이어 정보를 찾을 수 없습니다. AI 동작에 제한이 있을 수 있습니다.");
        }
    }

    void Start()
    {
        // Start 메서드는 이제 비워둡니다.
    }

    public bool IsKnockedBack()
    {
        return _isKnockedBack;
    }

    public void HandleKnockbackCollision(Collision2D collision)
    {
        // 적과 충돌했는지 확인
        CharacterBase otherCharacter = collision.gameObject.GetComponent<CharacterBase>();
        if (otherCharacter != null && otherCharacter.Camp == EColliderCamp.EnemyCamp)
        {
            // 적에게 데미지 입히기
            otherCharacter.TakeHPChange(-collisionDamage);
            Debug.Log($"{name}이(가) 넉백 중 {otherCharacter.name}에게 {collisionDamage}의 충돌 데미지를 입혔습니다.");
        }
    }

    /// <summary>
    /// 플레이어의 기본 공격에 맞았을 때 호출될 함수
    /// </summary>
    public void OnHitByPlayerAttack(Vector2 attackDirection)
    {
        if (_isKnockedBack) return; // 이미 넉백 중이면 무시

        StartCoroutine(KnockbackRoutine(attackDirection));
    }

    private IEnumerator KnockbackRoutine(Vector2 direction)
    {
        _isKnockedBack = true;

        // 튕겨나가기
        _rb.linearVelocity = Vector2.zero; // 기존 속도 초기화
        _rb.AddForce(direction * knockbackForce, ForceMode2D.Impulse);

        // 회전
        _rb.AddTorque(knockbackTorque);

        Debug.Log($"{name}이(가) 플레이어의 공격에 맞아 넉백됩니다!");

        // 일정 시간 후 상태 초기화
        yield return new WaitForSeconds(knockbackDuration);

        _isKnockedBack = false;
        _rb.linearVelocity = Vector2.zero; // 넉백 후 속도 정지
        _rb.angularVelocity = 0; // 회전 정지

        Debug.Log($"{name}의 넉백 상태가 종료되었습니다.");
    }
}

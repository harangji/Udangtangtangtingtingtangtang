using UnityEngine;

/// <summary>
/// 투사체의 행동을 정의하는 스크립트입니다. (이동, 수명 관리, 충돌 처리 등)
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    [Tooltip("투사체의 이동 속도")]
    public float speed = 15f;
    [Tooltip("투사체가 사라지기까지의 시간")]
    public float lifetime = 3f;

    private float _damage;
    public CharacterBase Shooter { get; private set; }

    /// <summary>
    /// 투사체를 초기화하고 발사 방향을 설정합니다.
    /// </summary>
    /// <param name="shooter">투사체를 발사한 캐릭터</param>
    /// <param name="damage">투사체의 공격력</param>
    public void Initialize(CharacterBase shooter, float damage)
    {
        Shooter = shooter;
        _damage = damage;
    }

    private void Start()
    {
        // Rigidbody2D를 이용해 앞(transform.up)으로 발사체를 이동시킵니다.
        GetComponent<Rigidbody2D>().linearVelocity = transform.up * speed;
        // lifetime 이후에 게임 오브젝트를 파괴합니다.
        Destroy(gameObject, lifetime);
    }

    // 다른 오브젝트와 충돌했을 때 호출됩니다.
    private void OnTriggerEnter2D(Collider2D other)
    {
        CharacterBase target = other.GetComponent<CharacterBase>();

        // 충돌 대상이 캐릭터가 아니거나, 자기 자신이거나, 같은 편이면 무시합니다.
        if (target == null || target == Shooter || target.Camp == Shooter.Camp)
        {
            return;
        }

        // 대상에게 데미지를 입힙니다.
        target.TakeHPChange(Mathf.RoundToInt(-_damage)); // 데미지는 음수 값으로 전달합니다.
        Debug.Log($"{target.name}에게 {_damage}의 데미지를 입혔습니다!");

        // 충돌 후 즉시 파괴합니다.
        Destroy(gameObject);
    }
}

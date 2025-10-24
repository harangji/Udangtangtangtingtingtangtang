using UnityEngine;

[RequireComponent(typeof(AllyAI))]
public class AllyCharacter : CharacterBase
{
    [Header("힐러 설정")]
    [Tooltip("캐릭터 타입이 힐러일 경우, 아군과 충돌 시 회복시킬 체력의 양입니다.")]
    public int healAmount = 10;

    private AllyAI _allyAI;

    private void Awake()
    {
        _allyAI = GetComponent<AllyAI>();
    }

    private void Start()
    {
        // Start 메서드는 이제 비워둡니다.
    }

    public override void OnCollide(CharacterBase other)
    {
        // 아군과 충돌했을 때
        if (other.Camp == Camp) // Camp 프로퍼티를 사용하여 같은 편인지 확인
        {
            // 자신이 힐러이고, 상대방이 자신이 아닐 때
            if (Type == ECharacterType.Healer && other != this)
            {
                // 상대방 힐
                other.TakeHPChange(healAmount);
                Debug.Log($"{name}이(가) {other.name}을(를) {healAmount}만큼 치유했습니다.");
            }
        }
        // 적군과 충돌했을 때
        else
        {
            // 자신이 어태커일 때
            if (Type == ECharacterType.Attacker)
            {
                // 상대방에게 데미지
                other.TakeHPChange(CombatSystem.Instance.AmountCalculated(this, other));
                Debug.Log($"{name}이(가) {other.name}에게 충돌 데미지를 입혔습니다.");
            }
        }
    }

    public override void Shove(CharacterBase character)
    {
        Vector2 direction = (transform.position - character.transform.position).normalized;
        Rb.AddForce(direction * 5f, ForceMode2D.Impulse); // 예시로 5의 힘으로 밀어냄
    }

    public override void OnCollisionEnter2D(Collision2D other)
    {
        // AllyAI가 넉백 상태일 때는 AllyAI의 충돌 처리에 맡김
        if (_allyAI.IsKnockedBack())
        {
            _allyAI.HandleKnockbackCollision(other);
            return;
        }

        // 플레이어의 공격에 맞았을 때 (Projectile 스크립트의 발사자 캠프 확인)
        if (other.gameObject.TryGetComponent(out Projectile projectile))
        {
            if (projectile.Shooter != null && projectile.Shooter.Camp == EColliderCamp.AllyCamp) // 아군 투사체에 맞았을 때
            {
                Vector2 attackDirection = (transform.position - other.transform.position).normalized;
                _allyAI.OnHitByPlayerAttack(attackDirection);
                return; // 넉백이 시작되면 다른 충돌 무시
            }
        }

        // 플레이어와 충돌했을 때 (CharacterBase의 Camp 속성 확인)
        if (other.gameObject.TryGetComponent(out CharacterBase playerCol))
        {
            if (playerCol.Camp == EColliderCamp.AllyCamp) // 플레이어(아군)와 충돌
            {
                Vector2 shoveDirection = (transform.position - other.transform.position).normalized;
                Rb.AddForce(shoveDirection * _allyAI.playerShoveForce, ForceMode2D.Impulse);
                return; // 플레이어와의 충돌 처리 후 종료
            }
        }
        
        // 그 외 다른 캐릭터와 충돌했을 때
        CharacterBase otherCharacter = other.gameObject.GetComponent<CharacterBase>();
        if (otherCharacter != null)
        {
            OnCollide(otherCharacter);
        }
    }
}

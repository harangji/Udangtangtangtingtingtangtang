using UnityEngine;

public class EnemyCharacter : CharacterBase
{
    [SerializeField] protected GameObject _experienceGemPrefab; // 죽었을 때 생성할 경험치 보석 프리팹

    protected override void Dead()
    {
        // 경험치 보석 프리팹이 할당되어 있다면, 현재 위치에 생성합니다.
        if (_experienceGemPrefab != null)
        {
            Instantiate(_experienceGemPrefab, transform.position, Quaternion.identity);
        }
        
        // 부모 클래스의 Dead 로직을 실행합니다 (오브젝트 비활성화 등).
        base.Dead();
    }

    public override void OnCollide(CharacterBase other)
    {
        // 같은 편과는 충돌 무시
        if (Camp == other.Camp) return;

        // 어태커 타입일 경우에만 상대방에게 데미지 주기
        if (Type == ECharacterType.Attacker)
        {
            other.TakeHPChange(CombatSystem.Instance.AmountCalculated(this, other));
        }

        // 밀쳐내기
        Shove(other);
    }

    public override void Shove(CharacterBase character)
    {
        Vector2 direction = (transform.position - character.transform.position).normalized;
        Rb.AddForce(direction * 5f, ForceMode2D.Impulse); // 예시로 5의 힘으로 밀어냄
    }

    public override void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent(out CharacterBase col))
        {
            OnCollide(col); // OnCollide 메서드 호출
        }
    }
}

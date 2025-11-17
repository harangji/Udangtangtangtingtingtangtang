using UnityEngine;

[RequireComponent(typeof(CharacterBase))]
[RequireComponent(typeof(SkillController))] // 이제 SkillController를 사용합니다.
public class EnemyAI : MonoBehaviour
{
    private CharacterBase _characterBase;
    private SkillController _skillController; // BaseSkillHandler 대신 SkillController를 사용합니다.
    private Transform _playerTransform;

    void OnEnable()
    {
        _characterBase = GetComponent<CharacterBase>();
        _skillController = GetComponent<SkillController>(); // SkillController를 가져옵니다.

        // 플레이어 타겟 설정
        if (InGameHolder.Instance != null && InGameHolder.Instance.playerCharacter != null)
        {
            _playerTransform = InGameHolder.Instance.playerCharacter.transform;
        }
        else
        {
            Debug.LogError("EnemyAI: 플레이어 정보를 찾을 수 없습니다! AI 비활성화.");
            this.enabled = false; // AI 비활성화
            return;
        }
    }
        
    void FixedUpdate()
    {
        if (_playerTransform == null) return;

        Vector2 direction = (_playerTransform.position - transform.position).normalized;
        // CharacterBase.Stats에서 공격 범위를 가져옵니다.
        // NOTE: CharacterStats에 Range 스탯이 있어야 합니다.
        float attackRange = _characterBase.Stats.Range.Value; 
        float distance = Vector2.Distance(transform.position, _playerTransform.position);

        // 플레이어가 공격 범위 밖에 있으면 플레이어 방향으로 이동
        if (distance > attackRange)
        {
            Move(direction);
        }
        else
        {
            // 공격 범위 안에 있으면 이동을 멈춥니다.
            // AutoSkill이 SkillController에 의해 자동으로 실행됩니다.
            _characterBase.Rb.linearVelocity = Vector2.zero;
        }
    }

    private void Move(Vector2 direction)
    {
        _characterBase.Rb.AddForce(direction * _characterBase.Stats.MoveSpeed.Value);

        // 속도 제한
        if (_characterBase.Rb.linearVelocity.magnitude > _characterBase.Stats.MaxSpeed.Value)
        {
            _characterBase.Rb.linearVelocity = _characterBase.Rb.linearVelocity.normalized * _characterBase.Stats.MaxSpeed.Value;
        }
    }
}
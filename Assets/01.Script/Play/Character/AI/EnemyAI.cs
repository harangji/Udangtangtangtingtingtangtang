using UnityEngine;

[RequireComponent(typeof(CharacterBase))]
[RequireComponent(typeof(BaseSkillHandler))] // AI는 이제 SkillHandler를 반드시 필요로 합니다.
public class EnemyAI : MonoBehaviour
{
    private CharacterBase _characterBase;
    private BaseSkillHandler _skillHandler;
    private Transform _playerTransform;

        void OnEnable()
        {
            _characterBase = GetComponent<CharacterBase>();
            _skillHandler = GetComponent<BaseSkillHandler>();
    
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
    
        void Start()
        {
            // Start 메서드는 이제 비워둡니다.
        }
    void FixedUpdate()
    {
        if (_playerTransform == null) return;

        Vector2 direction = (_playerTransform.position - transform.position).normalized;
        float distance = Vector2.Distance(transform.position, _playerTransform.position);

        // 플레이어가 공격 범위 밖에 있으면 플레이어 방향으로 이동
        if (distance > _characterBase.UnitStat.AttackRange)
        {
            MoveAndDash(direction);
        }
        // 플레이어가 공격 범위 안에 있으면 공격 (SkillHandler 사용)
        else
        {
            if (_skillHandler != null)
            {
                // 주 공격 스킬 (근접 공격)을 찾아서 활성화합니다.
                int attackSkillIndex = _skillHandler.GetSkillIndex(ESkillType.MeleeAttack);
                if (attackSkillIndex == -1)
                {
                    // 근접 공격 스킬이 없으면 기본 공격 스킬을 시도합니다.
                    attackSkillIndex = _skillHandler.GetSkillIndex(ESkillType.BasicAttack);
                }

                if (attackSkillIndex != -1 && !_skillHandler.IsSkillOnCooldown(attackSkillIndex))
                {
                    _skillHandler.ActivateSkill(attackSkillIndex);
                }
            }
        }
    }

    private void MoveAndDash(Vector2 direction)
    {
        _characterBase.Rb.AddForce(direction * _characterBase.UnitStat.MoveSpeed);

        // 속도 제한
        if (_characterBase.Rb.linearVelocity.magnitude > _characterBase.UnitStat.MaxSpeed)
        {
            _characterBase.Rb.linearVelocity = _characterBase.Rb.linearVelocity.normalized * _characterBase.UnitStat.MaxSpeed;
        }

        // 대시 스킬 발동
        int dashSkillIndex = _skillHandler.GetSkillIndex(ESkillType.Dash);
        if (dashSkillIndex != -1 && !_skillHandler.IsSkillOnCooldown(dashSkillIndex))
        {
            _skillHandler.ActivateSkill(dashSkillIndex);
        }
    }
}
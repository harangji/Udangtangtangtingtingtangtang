using System.Threading.Tasks;
using UnityEngine;

public class EnemyBasicDashSkill : EnemyDashSkill
{
    public EnemyBasicDashSkill(SkillData skillData, BaseSkillHandler owner) : base(skillData, owner)
    {
    }

    protected override async Task ApplyDashEffect()
    {
        /// <summary>
        /// SkillData.skillName: 스킬의 이름을 나타냅니다.
        /// SkillData.amounts[0]: 대쉬 스킬의 속도 값을 나타냅니다.
        /// SkillData.skillRange: 대쉬 스킬의 거리를 나타냅니다.
        /// SkillData.skillEffectPrefab: 스킬 발동 시 생성될 이펙트 프리팹을 나타냅니다.
        /// </summary>
        if (Owner.Character == null || Owner.Character.Rb == null)
        {
            Debug.LogError($"{SkillData.skillName}: Owner.Character 또는 Rigidbody2D가 없습니다.");
            return;
        }

        // 대쉬 방향은 현재 캐릭터의 앞 방향으로 설정
        Vector2 dashDirection = Owner.Character.transform.forward; 
        float dashSpeed = SkillData.amounts.Count > 0 ? SkillData.amounts[0] : 10f; // amounts[0]을 대쉬 속도로 사용, 기본값 10f
        float dashDistance = SkillData.skillRange; // skillRange를 대쉬 거리로 사용

        // 대쉬 효과 적용
        Owner.Character.Rb.AddForce(dashDirection * dashSpeed, ForceMode2D.Impulse);

        Debug.Log($"{Owner.Character.name}이(가) {dashDistance} 거리로 {dashSpeed} 속도로 대쉬했습니다.");

        // 스킬 이펙트가 있다면 생성
        if (SkillData.skillEffectPrefab != null)
        {
            // TODO: 이펙트 생성 위치 및 방향 조정
            GameObject effect = GameObject.Instantiate(SkillData.skillEffectPrefab, Owner.Character.transform.position, Quaternion.identity);
            // 일정 시간 후 이펙트 파괴 (예시)
            GameObject.Destroy(effect, 1.0f);
        }

        await Task.CompletedTask;
    }
}

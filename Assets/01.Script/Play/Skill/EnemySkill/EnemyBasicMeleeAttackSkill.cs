using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class EnemyBasicMeleeAttackSkill : EnemyMeleeAttackSkill
{
    public EnemyBasicMeleeAttackSkill(SkillData skillData, BaseSkillHandler owner) : base(skillData, owner)
    {
    }

    protected override async Task ApplyMeleeAttackEffect()
    {
        /// <summary>
        /// SkillData.skillName: 스킬의 이름을 나타냅니다.
        /// SkillData.skillRange: 근접 공격의 유효 사거리를 나타냅니다.
        /// SkillData.amounts[0]: 근접 공격의 데미지 값을 나타냅니다.
        /// SkillData.skillEffectPrefab: 스킬 발동 시 생성될 이펙트 프리팹을 나타냅니다.
        /// </summary>
        // Owner 주변의 적을 찾아 공격
        var enemiesInRange = InGameHolder.Instance.Characters
            .Where(c => c.Camp != Owner.Character.Camp && c.BAlive &&
                        Vector3.Distance(Owner.Character.transform.position, c.transform.position) <= SkillData.skillRange)
            .ToList();

        foreach (var enemy in enemiesInRange)
        {
            // TODO: 실제 데미지 적용 로직 구현
            Debug.Log($"{Owner.Character.name}이(가) {enemy.name}에게 {SkillData.amounts[0]}의 근접 피해를 입혔습니다.");
            // 예시: enemy.TakeDamage(SkillData.amounts[0]);
        }

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

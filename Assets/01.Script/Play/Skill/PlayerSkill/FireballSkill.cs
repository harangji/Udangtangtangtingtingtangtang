
using UnityEngine;

public class FireballSkill : AutoSkillBase
{
    public FireballSkill(SkillData skillData, CharacterBase owner) : base(skillData, owner) { }

    protected override void Execute()
    {
        // 살아있는 모든 적을 찾습니다.
        var enemies = InGameHolder.Instance.Characters.FindAll(c => c.Camp != Owner.Camp && c.BAlive);
        if (enemies.Count == 0) return;

        // 그 중 무작위 적 한 명을 선택합니다.
        var randomEnemy = enemies[Random.Range(0, enemies.Count)];
        
        Debug.Log($"{Owner.name}이(가) {randomEnemy.name}을(를) 향해 {SkillData.skillName}을(를) 시전합니다");

        if (SkillData.projectilePrefab != null)
        {
            // 투사체를 생성합니다.
            GameObject projectileGo = Object.Instantiate(SkillData.projectilePrefab, Owner.transform.position, Quaternion.identity);
            
            // 투사체의 방향을 무작위로 선택된 적으로 설정합니다.
            Vector2 direction = (randomEnemy.transform.position - Owner.transform.position).normalized;
            projectileGo.transform.right = direction;

            // 데미지를 계산합니다.
            float damageMultiplier = 1f;
            if (SkillData.damageMultiplierPerLevel != null && SkillData.damageMultiplierPerLevel.Count >= CurrentLevel)
            {
                damageMultiplier = SkillData.damageMultiplierPerLevel[CurrentLevel - 1];
            }
            var finalDamage = Owner.Stats.Damage.Value * damageMultiplier;

            // TODO: 관통하는 투사체 스크립트에 데미지, 속도 등의 정보를 전달해야 합니다.
            // 이 투사체는 여러 적을 관통하며 데미지를 입힐 수 있습니다.
            // 예: projectileGo.GetComponent<PiercingProjectile>()?.Initialize(finalDamage, SkillData.projectileSpeed);
        }
    }
}


using System.Linq;
using UnityEngine;

public class BasicAttackSkill : AutoSkillBase
{
    private int _projectileCount = 1;

    public BasicAttackSkill(SkillData skillData, CharacterBase owner) : base(skillData, owner)
    {
        // 스킬 레벨 1 데이터로 초기화
        if (SkillData.projectilesPerLevel.Count > 0)
        {
            _projectileCount = SkillData.projectilesPerLevel[0];
        }
    }

    public override void Upgrade()
    {
        base.Upgrade(); // 기본 레벨업 로직 (레벨, 쿨다운)
        
        // 새 레벨에 맞게 투사체 개수 업데이트
        if (SkillData.projectilesPerLevel != null && SkillData.projectilesPerLevel.Count >= CurrentLevel)
        {
            _projectileCount = SkillData.projectilesPerLevel[CurrentLevel - 1];
            Debug.Log($"기본 공격 투사체 개수가 {_projectileCount}개로 업그레이드되었습니다!");
        }
    }

    protected override void Execute()
    {
        var stats = Owner.Stats;
        // 범위 내에서 가장 가까운 적들을 투사체 개수만큼 찾습니다.
        var enemies = TargetingSystem.Instance.FindClosestEnemies(Owner, stats.Range.Value, 1); //_projectileCount

        if (enemies != null && enemies.Any())
        {
            Debug.Log($"{Owner.name}이(가) {SkillData.skillName}을(를) 사용합니다! (대상: {enemies.Count}명)");

            float damageMultiplier = 1f;
            if (SkillData.damageMultiplierPerLevel != null && SkillData.damageMultiplierPerLevel.Count >= CurrentLevel)
            {
                damageMultiplier = SkillData.damageMultiplierPerLevel[CurrentLevel - 1];
            }
            var finalDamage = stats.Damage.Value * damageMultiplier;

            foreach (var enemy in enemies)
            {
                Debug.Log($"{Owner.name}이(가) {enemy.name}에게 {finalDamage}의 데미지를 입힙니다. (레벨: {CurrentLevel})");

                if (SkillData.projectilePrefab != null)
                {
                    // 투사체 생성
                    GameObject projectileGo = Object.Instantiate(SkillData.projectilePrefab, Owner.transform.position, Quaternion.identity);
                    
                    // 투사체의 방향을 적으로 설정
                    Vector2 direction = (enemy.transform.position - Owner.transform.position).normalized;
                    projectileGo.transform.right = direction; // 2D이므로 right 벡터를 방향으로 사용
                    
                    // TODO: 투사체 스크립트에 데미지, 속도 등의 정보를 전달해야 합니다.
                    // 예: projectileGo.GetComponent<Projectile>()?.Initialize(finalDamage, SkillData.projectileSpeed, enemy);
                }
                
                // 데미지 적용 (주석 해제 필요)
                // enemy.TakeHPChange(-(int)finalDamage);
            }
        }
    }
}

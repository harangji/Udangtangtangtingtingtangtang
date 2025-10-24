using UnityEngine;
using System.Threading.Tasks;

public class PlayerBasicProjectileSkill : ProjectileSkill
{
    public PlayerBasicProjectileSkill(SkillData skillData, BaseSkillHandler owner) : base(skillData, owner)
    {
        if (skillData.skillType != ESkillType.Projectile)
        {
            Debug.LogWarning($"SkillData for {skillData.skillName} is not of type Projectile.");
        }
    }

    protected override void FireProjectileEffect(Quaternion rotation)
    {
        /// <summary>
        /// SkillData.projectilePrefab: 발사할 투사체의 프리팹을 나타냅니다.
        /// SkillData.amounts[0]: 투사체 스킬의 데미지 값을 나타냅니다.
        /// SkillData.skillName: 스킬의 이름을 나타냅니다.
        /// </summary>
        // 투사체 생성
        GameObject projectileObject = Object.Instantiate(SkillData.projectilePrefab, Owner.transform.position, rotation);
        
        // 투사체 컴포넌트 가져오기
        Projectile projectile = projectileObject.GetComponent<Projectile>();
        if (projectile != null)
        {
            // 투사체 초기화 (데미지, 발사자 정보 전달)
            var ownerCharacter = Owner.GetComponent<CharacterBase>();
            if (ownerCharacter != null)
            {
                projectile.Initialize(ownerCharacter, SkillData.amounts[0]);
            }
            else
            {
                Debug.LogError($"{SkillData.skillName}: 스킬의 소유자(Owner)에게 CharacterBase 컴포넌트가 없습니다.");
                Object.Destroy(projectileObject);
            }
        }
        else
        {
            Debug.LogError($"{SkillData.skillName}: 투사체 프리팹에 Projectile.cs 스크립트가 없습니다.");
            Object.Destroy(projectileObject); // 잘못된 프리팹이면 파괴
        }
    }
}

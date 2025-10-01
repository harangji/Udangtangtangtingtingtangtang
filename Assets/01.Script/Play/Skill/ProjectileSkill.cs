using UnityEngine;
using Udangtangtang.Skill.Abstractions;

namespace Udangtangtang.Skill
{
    public class ProjectileSkill : SkillBase
    {
        public ProjectileSkill(SkillData skillData, SkillHandler owner) : base(skillData, owner) { }

        protected override void OnActivate()
        {
            if (SkillData.projectilePrefab == null)
            {
                Debug.LogError($"{SkillData.skillName}: Projectile prefab is not set!");
                return;
            }

            // 스킬 시전자의 위치에서 투사체 생성
            GameObject projectileGo = Object.Instantiate(SkillData.projectilePrefab, Owner.transform.position, Owner.transform.rotation);
            
            // 투사체에 데미지와 속도 등 정보 전달 (Projectile 컴포넌트가 있다고 가정)
            Projectile projectile = projectileGo.GetComponent<Projectile>();
            if (projectile != null)
            {
                // projectile.Initialize(SkillData.damage, SkillData.projectileSpeed, Owner); // <- To damage others
            }
            else
            {
                // 간단한 이동 로직이라도 추가
                Rigidbody2D rb = projectileGo.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.linearVelocity = Owner.transform.up * SkillData.projectileSpeed;
                }
                Debug.LogWarning($"{SkillData.skillName}: Projectile component not found on the prefab. Using basic Rigidbody velocity.");
            }
            
            Debug.Log($"{Owner.name} used {SkillData.skillName}!");
        }
    }
}

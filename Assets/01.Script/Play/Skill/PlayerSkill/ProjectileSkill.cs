using UnityEngine;
using System.Linq;
using System.Threading.Tasks;

/// <summary>
/// Projectile 타입의 스킬을 처리하는 클래스입니다.
/// </summary>
public abstract class ProjectileSkill : SkillBase
{
    // 생성자: SkillBase의 생성자를 호출하여 기본 정보를 설정합니다.
    public ProjectileSkill(SkillData skillData, BaseSkillHandler owner) : base(skillData, owner)
    {
    }

    // OnActivateAsync는 SkillBase에서 Activate()가 호출할 때 실행됩니다.
    protected override async Task OnActivateAsync()
    {
        /// <summary>
        /// SkillData.projectilePrefab: 발사할 투사체의 프리팹을 나타냅니다.
        /// SkillData.skillName: 스킬의 이름을 나타냅니다.
        /// SkillData.targetType: 스킬의 타겟팅 방식을 정의합니다.
        /// </summary>
        if (SkillData.projectilePrefab == null)
        {
            Debug.LogError($"{SkillData.skillName}: 투사체 프리팹이 설정되지 않았습니다.");
            return;
        }

        Debug.Log($"'{SkillData.skillName}' 발사.");

        switch (SkillData.targetType)
        {
            case ETargetType.NoTarget:
                FireProjectileEffect(Owner.transform.rotation);
                break;
            case ETargetType.ClosestEnemy: // 기존 SpecificTarget을 대체하고, 새로운 ClosestEnemy 타입 처리
                FireAtClosestEnemy();
                break;
            case ETargetType.RandomTarget:
                FireAtRandomEnemy();
                break;
            case ETargetType.Directional: // 새로운 Directional 타입
                // 플레이어의 입력 방향을 받아야 하지만, 현재는 Owner의 방향을 사용
                FireProjectileEffect(Owner.transform.rotation); 
                break;
            case ETargetType.PlayerOnly: // 새로운 PlayerOnly 타입 (적 스킬용)
                CharacterBase playerTarget = FindPlayer();
                if (playerTarget != null)
                {
                    Vector2 directionToPlayer = (playerTarget.transform.position - Owner.transform.position).normalized;
                    Quaternion rotationToPlayer = Quaternion.LookRotation(Vector3.forward, directionToPlayer);
                    FireProjectileEffect(rotationToPlayer);
                }
                else
                {
                    Debug.LogWarning($"{SkillData.skillName}: Player target not found for PlayerOnly skill.");
                    FireProjectileEffect(Owner.transform.rotation); // 플레이어를 찾지 못하면 기본 방향으로 발사
                }
                break;
            default:
                Debug.LogError($"Unknown or unsupported skill target type for ProjectileSkill: {SkillData.targetType}");
                break;
        }
        await Task.CompletedTask;
    }

    /// <summary>
    /// 실제 투사체를 발사하는 추상 메서드. 자식 클래스에서 구현합니다.
    /// </summary>
    protected abstract void FireProjectileEffect(Quaternion rotation);

    private void FireAtClosestEnemy()
    {
        CharacterBase closestEnemy = FindClosestEnemy();
        if (closestEnemy != null)
        {
            Vector2 direction = (closestEnemy.transform.position - Owner.transform.position).normalized;
            Quaternion rotation = Quaternion.LookRotation(Vector3.forward, direction);
            FireProjectileEffect(rotation);
        }
        else
        {
            // 적이 없으면 그냥 앞으로 발사
            FireProjectileEffect(Owner.transform.rotation);
        }
    }

    private void FireAtRandomEnemy()
    {
        var enemies = InGameHolder.Instance.Characters
            .Where(c => c.Camp != Owner.GetComponent<CharacterBase>().Camp && c.BAlive)
            .ToList();

        if (enemies.Count > 0)
        {
            CharacterBase randomEnemy = enemies[Random.Range(0, enemies.Count)];
            Vector2 direction = (randomEnemy.transform.position - Owner.transform.position).normalized;
            Quaternion rotation = Quaternion.LookRotation(Vector3.forward, direction);
            FireProjectileEffect(rotation);
        }
        else
        {
            FireProjectileEffect(Owner.transform.rotation);
        }
    }

    private CharacterBase FindClosestEnemy()
    {
        CharacterBase closestEnemy = null;
        float minDistance = float.MaxValue;

        foreach (var character in InGameHolder.Instance.Characters)
        {
            // 자기 자신은 제외하고, 살아있는 적 캠프의 캐릭터만 찾도록 수정
            if (character != Owner.GetComponent<CharacterBase>() && character.Camp != Owner.GetComponent<CharacterBase>().Camp && character.BAlive)
            {
                float distance = Vector2.Distance(Owner.transform.position, character.transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    closestEnemy = character;
                }
            }
        }
        return closestEnemy;
    }}
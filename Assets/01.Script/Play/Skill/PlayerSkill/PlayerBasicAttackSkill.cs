using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using Shapes;

public class PlayerBasicAttackSkill : SkillBase
{
    public PlayerBasicAttackSkill(SkillData skillData, BaseSkillHandler owner) : base(skillData, owner)
    {
        if (skillData.skillType != ESkillType.BasicAttack)
        {
            Debug.LogWarning($"SkillData for {skillData.skillName} is not of type BasicAttack.");
        }
    }

    protected override async Task OnActivateAsync()
    {
        /// <summary>
        /// SkillData.skillName: 스킬의 이름을 나타냅니다.
        /// SkillData.amounts[0]: 기본 공격의 데미지 값을 나타냅니다.
        /// SkillData.skillRange: 기본 공격의 유효 사거리를 나타냅니다.
        /// SkillData.skillEffectPrefab: 스킬 발동 시 생성될 이펙트 프리팹을 나타냅니다.
        /// SkillData.duration: 공격 간격을 나타냅니다. (예: 1초마다 공격)
        /// </summary>
        Debug.Log($"{SkillData.skillName} 기본 공격 발동!");

        // 기본 공격은 쿨타임이 0이 아니면 계속 반복
        while (!IsOnCooldown)
        {
            // 부채꼴 범위 시각화
            //DrawSector(Owner.Character.transform.position, Owner.Character.transform.up, SkillData.skillRange, 90f, Color.red, 0.2f);

            // 부채꼴 범위 내 가장 가까운 적 찾기
            CharacterBase closestEnemy = FindClosestEnemyInSector(Owner.Character.transform.position, Owner.Character.transform.up, SkillData.skillRange, 90f); // 90도 부채꼴

            if (closestEnemy != null)
            {
                // TODO: 실제 데미지 적용 로직 구현
                Debug.Log($"{Owner.Character.name}이(가) {closestEnemy.name}에게 {SkillData.amounts[0]}의 기본 공격 피해를 입혔습니다.");
                // 예시: closestEnemy.TakeHPChange(-(int)SkillData.amounts[0]);

                // 스킬 이펙트가 있다면 생성
                if (SkillData.skillEffectPrefab != null)
                {
                    // TODO: 이펙트 생성 위치 및 방향 조정
                    GameObject effect = GameObject.Instantiate(SkillData.skillEffectPrefab, closestEnemy.transform.position, Quaternion.identity);
                    // 일정 시간 후 이펙트 파괴 (예시)
                    GameObject.Destroy(effect, 0.5f);
                }
            }
            else
            {
                Debug.Log($"기본 공격: 범위 내 적을 찾지 못했습니다.");
            }

            // SkillData.duration을 공격 간격으로 사용 (예: 1초마다 공격)
            await Task.Delay((int)(SkillData.duration * 1000));
        }

        await Task.CompletedTask;
    }

    /// <summary>
    /// 특정 위치에서 부채꼴 범위 내 가장 가까운 적을 찾습니다.
    /// </summary>
    /// <param name="center">부채꼴의 중심 위치</param>
    /// <param name="forward">부채꼴의 전방 방향</param>
    /// <param name="radius">부채꼴의 반지름 (사거리)</param>
    /// <param name="angle">부채꼴의 각도 (예: 90도)</param>
    /// <returns>가장 가까운 적 캐릭터</returns>
    private CharacterBase FindClosestEnemyInSector(Vector3 center, Vector3 forward, float radius, float angle)
    {
        CharacterBase closestEnemy = null;
        float minDistance = float.MaxValue;

        var enemies = InGameHolder.Instance.Characters
            .Where(c => c.Camp != Owner.Character.Camp && c.BAlive &&
                        Vector3.Distance(center, c.transform.position) <= radius) // 사거리 내에 있는 적
            .ToList();

        foreach (var enemy in enemies)
        {
            Vector3 directionToEnemy = (enemy.transform.position - center).normalized;
            float angleToEnemy = Vector3.Angle(forward, directionToEnemy);

            if (angleToEnemy <= angle * 0.5f) // 부채꼴 각도 내에 있는 적
            {
                float distance = Vector3.Distance(center, enemy.transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    closestEnemy = enemy;
                }
            }
        }
        return closestEnemy;
    }

    // /// <summary>
    // /// Shapes 에셋을 사용하여 부채꼴을 그립니다.
    // /// </summary>
    // /// <param name="center">부채꼴의 중심 위치</param>
    // /// <param name="forward">부채꼴의 전방 방향</param>
    // /// <param name="radius">부채꼴의 반지름</param>
    // /// <param name="angle">부채꼴의 각도 (도 단위)</param>
    // /// <param name="color">부채꼴의 색상</param>
    // /// <param name="duration">부채꼴이 표시될 시간</param>
    // private void DrawSector(Vector3 center, Vector3 forward, float radius, float angle, Color color, float duration)
    // {
    //     // Shapes 에셋의 Draw.Command를 사용하여 런타임에 그립니다.
    //     // Draw.Command는 using 블록 내에서 사용해야 합니다.
    //     using (Draw.Command(Camera.main))
    //     {
    //         Draw.Color = color;
    //         Draw.BlendMode = ShapesBlendMode.Transparent;
    //         Draw.Thickness = 0.1f; // 선 두께
    //         Draw.ThicknessSpace = ThicknessSpace.Meters; // 두께 단위를 미터로 설정
    //
    //         // forward 벡터를 기준으로 시작 각도를 계산합니다.
    //         float startAngle = Mathf.Atan2(forward.y, forward.x) * Mathf.Rad2Deg - angle * 0.5f;
    //
    //         // Draw.Arc는 라디안 각도를 사용하므로 도 단위를 라디안으로 변환합니다.
    //         // Shapes 에셋의 Draw.Arc는 AngStart와 AngEnd를 도 단위로 직접 설정할 수 있습니다.
    //         // 2D 환경에서는 Vector2를 사용하고, 각도는 라디안으로 변환하여 전달합니다.
    //         Draw.Arc(center, radius, startAngle * Mathf.Deg2Rad, (startAngle + angle) * Mathf.Deg2Rad);
    //     }
    //
    //     // Draw.Command로 그린 Shapes는 매 프레임 다시 그려지지 않으면 사라집니다.
    //     // 일회성으로 표시하고 싶다면, 이펙트 프리팹을 사용하는 것이 더 적절할 수 있습니다.
    //     // 현재는 OnActivateAsync에서 매번 호출되므로 계속 그려질 것입니다.
    //     // 일정 시간 후 파괴 로직은 Draw.Command 방식에서는 직접 구현하기 어렵습니다.
    //     // 만약 일정 시간 후 사라지게 하려면, ShapesRenderer 컴포넌트를 가진 프리팹을 만들고
    //     // 해당 프리팹을 인스턴스화하여 제어하는 방식이 더 적합합니다.
    // }
}


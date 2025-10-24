using UnityEngine;
using System.Linq;
using System.Threading.Tasks;

public abstract class BuffSkill : SkillBase
    {
        public BuffSkill(SkillData skillData, BaseSkillHandler owner) : base(skillData, owner) { }

        protected override async Task OnActivateAsync()
        {
            /// <summary>
            /// SkillData.targetType: 스킬의 타겟팅 방식을 정의합니다.
            /// SkillData.skillType: 스킬의 기본 형식을 정의합니다.
            /// SkillData.skillName: 스킬의 이름을 나타냅니다.
            /// </summary>
            switch (SkillData.targetType)
            {
                case ETargetType.NoTarget:
                    await ApplyBuffCommon(Owner.GetComponent<CharacterBase>());
                    break;
                case ETargetType.SpecificTarget: // 기존 SpecificTarget은 아군 버프에만 사용
                    CharacterBase allyTarget = FindClosestAlly();
                    if (allyTarget != null) await ApplyBuffCommon(allyTarget);
                    break;
                case ETargetType.ClosestEnemy: // 새로운 ClosestEnemy 타입
                    CharacterBase enemyTarget = FindClosestEnemy();
                    if (enemyTarget != null) await ApplyBuffCommon(enemyTarget);
                    break;
                case ETargetType.RandomTarget:
                    CharacterBase randomTarget = (SkillData.skillType == ESkillType.Buff) ? FindRandomAlly() : FindRandomEnemy();
                    if (randomTarget != null) await ApplyBuffCommon(randomTarget);
                    break;
                case ETargetType.PlayerOnly: // 새로운 PlayerOnly 타입 (적 스킬용)
                    CharacterBase playerTarget = FindPlayer();
                    if (playerTarget != null)
                    {
                        await ApplyBuffCommon(playerTarget);
                    }
                    else
                    {
                        Debug.LogWarning($"{SkillData.skillName}: Player target not found for PlayerOnly skill.");
                    }
                    break;
                default:
                    Debug.LogError($"Unknown or unsupported skill target type for BuffSkill: {SkillData.targetType}");
                    break;
            }
        }

        private async Task ApplyBuffCommon(CharacterBase target)
        {
            if (target == null) return;

            /// <summary>
            /// SkillData.skillName: 스킬의 이름을 나타냅니다.
            /// SkillData.duration: 버프/디버프의 지속 시간을 나타냅s니다.
            /// SkillData.amounts[0]: 버프/디버프의 양을 나타냅니다.
            /// </summary>
            Debug.Log($"{Owner.name} used {SkillData.skillName} on {target.name}. Duration: {SkillData.duration}, Amount: {SkillData.amounts[0]}");

            // 실제 버프/디버프 효과는 자식 클래스에서 구현
            await ApplyBuffEffect(target);

            // 지속 시간이 있다면 일정 시간 후 효과 제거 (공통 로직)
            if (SkillData.duration > 0)
            {
                await Task.Delay((int)(SkillData.duration * 1000));

                if (target != null)
                {
                    await RemoveBuffEffect(target);
                    Debug.Log($"{SkillData.skillName} effect ended on {target.name}.");
                }
            }
        }

        /// <summary>
        /// 실제 버프/디버프 효과를 적용하는 추상 메서드. 자식 클래스에서 구현합니다.
        /// </summary>
        protected abstract Task ApplyBuffEffect(CharacterBase target);

        /// <summary>
        /// 버프/디버프 효과를 제거하는 추상 메서드. 자식 클래스에서 구현합니다.
        /// </summary>
        protected abstract Task RemoveBuffEffect(CharacterBase target);

        private CharacterBase FindClosestAlly()
        {
            return InGameHolder.Instance.Characters
                .Where(c => c.Camp == Owner.GetComponent<CharacterBase>().Camp && c != Owner.GetComponent<CharacterBase>() && c.BAlive)
                .OrderBy(c => Vector2.Distance(Owner.transform.position, c.transform.position))
                .FirstOrDefault();
        }

        private CharacterBase FindClosestEnemy()
        {
            return InGameHolder.Instance.Characters
                .Where(c => c.Camp != Owner.GetComponent<CharacterBase>().Camp && c.BAlive)
                .OrderBy(c => Vector2.Distance(Owner.transform.position, c.transform.position))
                .FirstOrDefault();
        }

        private CharacterBase FindRandomAlly()
        {
            var allies = InGameHolder.Instance.Characters
                .Where(c => c.Camp == Owner.GetComponent<CharacterBase>().Camp && c != Owner.GetComponent<CharacterBase>() && c.BAlive)
                .ToList();
            return allies.Count > 0 ? allies[Random.Range(0, allies.Count)] : null;
        }

        private CharacterBase FindRandomEnemy()
        {
            var enemies = InGameHolder.Instance.Characters
                .Where(c => c.Camp != Owner.GetComponent<CharacterBase>().Camp && c.BAlive)
                .ToList();
            return enemies.Count > 0 ? enemies[Random.Range(0, enemies.Count)] : null;
        }
    }
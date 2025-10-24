using UnityEngine;

/// <summary>
/// 스킬의 타겟팅 방식을 정의합니다.
/// </summary>
public enum ETargetType
    {
        /// <summary>
        /// 타겟이 필요 없는 스킬 (예: 자신에게 버프)
        /// </summary>
        NoTarget,

        /// <summary>
        /// 특정 대상을 지정해야 하는 스킬
        /// </summary>
        SpecificTarget,

        /// <summary>
        /// 범위 내에서 무작위 대상을 지정하는 스킬
        /// </summary>
        RandomTarget,

        /// <summary>
        /// 플레이어만 타겟팅하는 스킬 (주로 적 스킬)
        /// </summary>
        PlayerOnly,

        /// <summary>
        /// 공격 방향키의 방향대로 발사되는 스킬 (주로 플레이어 스킬)
        /// </summary>
        Directional,

        /// <summary>
        /// 가장 가까운 적을 타겟팅하는 스킬 (주로 플레이어 스킬)
        /// </summary>
        ClosestEnemy,
    }

    /// <summary>
    /// 스킬의 기본 형식을 정의합니다.
    /// </summary>
    public enum ESkillType
    {
        /// <summary>
        /// 지속적으로 효과를 발휘하는 패시브 스킬
        /// </summary>
        Passive,

        /// <summary>
        /// 투사체를 발사하는 스킬
        /// </summary>
        Projectile,

        /// <summary>
        /// 대상에게 이로운 효과를 주는 스킬
        /// </summary>
        Buff,
        
        /// <summary>
        /// 대상에게 해로운 효과를 주는 스킬
        /// </summary>
        Debuff,

        /// <summary>
        /// 주변에 오라를 생성하는 스킬
        /// </summary>
        Aura,

        /// <summary>
        /// 채찍처럼 공격하는 스킬
        /// </summary>
        Whip,

        /// <summary>
        /// 번개를 내리치는 스킬
        /// </summary>
        Lightning,

        /// <summary>
        /// 근접 공격 스킬
        /// </summary>
        MeleeAttack,

        /// <summary>
        /// 대쉬 스킬
        /// </summary>
        Dash,

        /// <summary>
        /// 기본 공격 스킬
        /// </summary>
        BasicAttack,
    }

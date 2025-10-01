namespace Udangtangtang.Skill.Abstractions
{
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
    }
}

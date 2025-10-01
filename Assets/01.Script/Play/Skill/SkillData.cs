using UnityEngine;
using Udangtangtang.Skill.Abstractions;

[CreateAssetMenu(fileName = "New SkillData", menuName = "Udangtangtang/Skill Data", order = 0)]
public class SkillData : ScriptableObject
{
    [Header("기본 정보")]
    public string skillName;
    [TextArea]
    public string description;
    public Sprite icon;
    public float coolTime;

    [Header("스킬 분류")]
    public ESkillType skillType;
    public ETargetType targetType;

    [Header("투사체 스킬 정보")]
    [Tooltip("skillType이 Projectile일 경우에만 사용됩니다.")]
    public GameObject projectilePrefab;
    public float projectileSpeed;
    public float damage;

    [Header("버프/디버프 스킬 정보")]
    [Tooltip("skillType이 Buff 또는 Debuff일 경우에만 사용됩니다.")]
    public float duration;
    public float amount; // 버프/디버프 양 (예: 공격력 1.2배, 이동속도 0.8배)
}

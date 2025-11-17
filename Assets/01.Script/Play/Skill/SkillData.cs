using UnityEngine;
using System.Collections.Generic;

public class SkillData
{
    [Header("기본 정보")]
    public string skillName;
    [TextArea]
    public string description;
    public Sprite icon;
    public GameObject skillEffectPrefab; // 추가된 부분

    [Header("스킬 분류")]
    public ESkillType skillType;
    public ETargetType targetType;

    [Header("투사체 스킬 정보")]
    [Tooltip("skillType이 Projectile일 경우에만 사용됩니다.")]
    public GameObject projectilePrefab;
    public float projectileSpeed;

    [Header("레벨 정보")]
    public int maxLevel = 5;
    public List<float> damageMultiplierPerLevel;
    public List<float> cooldownPerLevel;
    public List<int> projectilesPerLevel;

    [Header("스킬 수치 정보")]
    [Tooltip("스킬에 사용되는 다양한 수치들 (예: 데미지 계수, 지속 시간, 범위 등)")]
    public float duration;
    public float skillRange;
}

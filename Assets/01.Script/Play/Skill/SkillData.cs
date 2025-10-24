using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New SkillData", menuName = "Udangtangtang/Skill Data", order = 0)]
public class SkillData : ScriptableObject
{
    [Header("기본 정보")]
    public string skillName;
    [TextArea]
    public string description;
    public Sprite icon;
    public GameObject skillEffectPrefab; // 추가된 부분
    public float coolTime;

    [Header("스킬 분류")]
    public ESkillType skillType;
    public ETargetType targetType;

    [Header("투사체 스킬 정보")]
    [Tooltip("skillType이 Projectile일 경우에만 사용됩니다.")]
    public GameObject projectilePrefab;
    public float projectileSpeed;

    [Header("스킬 수치 정보")]
    [Tooltip("스킬에 사용되는 다양한 수치들 (예: 데미지, 버프/디버프 양, 지속 시간, 범위 등)")]
    public float duration;
    public float skillRange; // 추가된 부분
    public List<float> amounts;
}

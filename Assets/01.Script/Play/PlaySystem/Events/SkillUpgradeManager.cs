using UnityEngine;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// 레벨업 시 플레이어에게 제공할 스킬 업그레이드/신규 스킬 옵션을 관리하는 싱글톤 매니저입니다.
/// </summary>
public class SkillUpgradeManager : SingletonBase<SkillUpgradeManager>
{
    [Tooltip("게임에 존재하는 모든 스킬의 목록입니다. 인스펙터에서 할당해야 합니다.")]
    [SerializeField]
    private List<SkillData> allSkillsPool;

    private SkillController _playerSkillController;

    protected override bool dontDestroyOnLoad { get; set; } = false;

    protected override void Awake()
    {
        base.Awake();
    }

    /// <summary>
    /// 플레이어의 SkillController를 매니저에 등록합니다.
    /// </summary>
    /// <param name="playerSkillController">플레이어의 스킬 컨트롤러</param>
    public void RegisterPlayerSkillController(SkillController playerSkillController)
    {
        _playerSkillController = playerSkillController;
    }

    /// <summary>
    /// 플레이어의 SkillController를 매니저에서 등록 해제합니다.
    /// </summary>
    public void UnregisterPlayerSkillController()
    {
        _playerSkillController = null;
    }

    /// <summary>
    /// 플레이어에게 제공할 업그레이드/신규 스킬 옵션을 생성합니다.
    /// </summary>
    /// <param name="count">제공할 옵션의 수</param>
    /// <returns>선별된 스킬 데이터 리스트</returns>
    public List<SkillData> GetUpgradeOptions(int count)
    {
        if (_playerSkillController == null)
        {
            Debug.LogWarning("Player Skill Controller가 SkillUpgradeManager에 등록되지 않았습니다.");
            return new List<SkillData>();
        }

        var potentialOptions = new List<SkillData>();

        // 1. 업그레이드 옵션: 현재 보유한 스킬 중 최대 레벨이 아닌 것들을 선택지에 추가합니다.
        var currentSkillInstances = _playerSkillController.GetSkillInstances();
        var upgradableSkills = currentSkillInstances.Where(skill => !skill.IsMaxLevel());
        potentialOptions.AddRange(upgradableSkills.Select(skill => skill.SkillData));

        // 2. 신규 스킬 옵션: 아직 배우지 않은 스킬들을 선택지에 추가합니다.
        var currentSkillData = currentSkillInstances.Select(skill => skill.SkillData);
        var newSkills = allSkillsPool.Where(skill => !currentSkillData.Contains(skill));
        potentialOptions.AddRange(newSkills);

        // 3. 전체 옵션 중에서 무작위로 'count'만큼 중복 없이 선택합니다.
        var finalOptions = potentialOptions.Distinct().OrderBy(x => Random.value).Take(count).ToList();

        return finalOptions;
    }
}

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

    private BaseSkillHandler _playerSkillHandler;

    protected override bool dontDestroyOnLoad { get; set; } = false;

    protected override void Awake()
    {
        base.Awake();
        // 플레이어의 SkillHandler 참조를 찾습니다.
        // 씬에 SkillHandler가 하나만 존재한다고 가정합니다.
        _playerSkillHandler = FindObjectOfType<BaseSkillHandler>();
        if (_playerSkillHandler == null)
        {
            Debug.LogError("SkillHandler를 씬에서 찾을 수 없습니다.");
        }
    }

    /// <summary>
    /// 플레이어에게 제공할 업그레이드/신규 스킬 옵션을 생성합니다.
    /// </summary>
    /// <param name="count">제공할 옵션의 수</param>
    /// <returns>선별된 스킬 데이터 리스트</returns>
    public List<SkillData> GetUpgradeOptions(int count)
    {
        if (_playerSkillHandler == null) return new List<SkillData>();

        var potentialOptions = new List<SkillData>();

        // 현재 플레이어가 가진 스킬 목록을 가져옵니다.
        List<SkillData> currentSkills = _playerSkillHandler.GetCurrentSkills();

        // TODO: 이미 마스터 레벨인 스킬은 업그레이드 목록에서 제외하는 로직이 필요합니다.

        // 1. 업그레이드 옵션: 현재 보유한 스킬을 선택지에 추가합니다.
        potentialOptions.AddRange(currentSkills);

        // 2. 신규 스킬 옵션: 아직 배우지 않은 스킬들을 선택지에 추가합니다.
        var newSkills = allSkillsPool.Where(skill => !currentSkills.Contains(skill));
        potentialOptions.AddRange(newSkills);

        // 3. 전체 옵션 중에서 무작위로 'count'만큼 중복 없이 선택합니다.
        var finalOptions = new List<SkillData>();
        var random = new System.Random();
        var shuffledOptions = potentialOptions.OrderBy(item => random.Next());
        
        foreach (var option in shuffledOptions)
        {
            if (finalOptions.Count < count)
            {
                finalOptions.Add(option);
            }
            else
            {
                break;
            }
        }

        return finalOptions;
    }
}

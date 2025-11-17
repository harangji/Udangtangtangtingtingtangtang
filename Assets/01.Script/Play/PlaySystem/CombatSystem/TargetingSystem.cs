using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TargetingSystem : SingletonBase<TargetingSystem>
{
    protected override bool dontDestroyOnLoad { get; set; } = false;

    /// <summary>
    /// 지정된 범위 내에서 가장 가까운 적들을 찾습니다.
    /// </summary>
    /// <param name="owner">탐색의 기준이 되는 캐릭터</param>
    /// <param name="range">탐색 범위</param>
    /// <param name="count">찾을 적의 최대 수</param>
    /// <returns>찾은 적들의 리스트</returns>
    public List<CharacterBase> FindClosestEnemies(CharacterBase owner, float range, int count)
    {
        // InGameHolder에서 적 리스트를 가져옵니다.
        var enemyCharacters = InGameHolder.Instance.Enemies;

        if (enemyCharacters == null || enemyCharacters.Count == 0)
        {
            return new List<CharacterBase>();
        }

        var enemies = enemyCharacters
            .Where(enemy => enemy != null && enemy.BAlive) // 살아있는 적만 필터링
            .Select(enemy => new
            {
                Enemy = enemy,
                Distance = Vector2.Distance(owner.transform.position, enemy.transform.position)
            })
            .Where(x => x.Distance <= range) // 범위 내의 적만 필터링
            .OrderBy(x => x.Distance) // 거리가 가까운 순으로 정렬
            .Take(count) // 지정된 수만큼 선택
            .Select(x => x.Enemy)
            .ToList();

        return enemies;
    }
}

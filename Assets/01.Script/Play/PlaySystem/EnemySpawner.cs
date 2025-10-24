using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    public float spawnRadius = 15f;
    public float spawnInterval = 1f;
    private Transform _playerTransform;

    public void BeginSpawning(Transform player)
    {
        _playerTransform = player;
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            if (_playerTransform != null && enemyPrefab != null)
            {
                // 플레이어 주변의 랜덤한 위치에 적 생성
                Vector2 spawnPos = _playerTransform.position;
                spawnPos += Random.insideUnitCircle.normalized * spawnRadius;

                Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
            }
            else if (enemyPrefab == null)
            {
                Debug.LogWarning("EnemySpawner: enemyPrefab이 할당되지 않아 적을 생성할 수 없습니다.");
            }
        }
    }
}

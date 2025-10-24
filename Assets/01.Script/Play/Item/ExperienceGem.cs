
using UnityEngine;

/// <summary>
/// 플레이어가 수집할 수 있는 경험치 보석의 동작을 처리합니다.
/// </summary>
public class ExperienceGem : MonoBehaviour
{
    [Tooltip("이 보석이 제공하는 경험치 양")]
    public int experienceValue = 1;

    [Tooltip("플레이어에게 끌려가기 시작하는 거리")]
    public float attractionDistance = 3f;
    
    [Tooltip("플레이어에게 끌려가는 속도")]
    public float attractionSpeed = 5f;

    private Transform _playerTransform;
    private bool _isAttracted = false;

    private void Awake()
    {
        // InGameHolder를 통해 플레이어 참조를 얻어옵니다.
        if (InGameHolder.Instance != null && InGameHolder.Instance.playerCharacter != null)
        {
            _playerTransform = InGameHolder.Instance.playerCharacter.transform;
        }
        else
        {
            // 플레이어를 찾지 못하면 로그를 남기고 비활성화합니다.
            Debug.LogWarning("플레이어를 찾을 수 없어 ExperienceGem이 비활성화됩니다.");
            gameObject.SetActive(false);
        }
    }

    private void Start()
    {
        // Start 메서드는 이제 비워둡니다.
    }

    private void Update()
    {
        if (_playerTransform == null) return;

        // 플레이어와의 거리를 확인하여 끌림 효과를 활성화합니다.
        if (!_isAttracted)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, _playerTransform.position);
            if (distanceToPlayer <= attractionDistance)
            {
                _isAttracted = true;
            }
        }

        // 활성화된 경우 플레이어 방향으로 이동합니다.
        if (_isAttracted)
        {
            Vector2 direction = (_playerTransform.position - transform.position).normalized;
            transform.position += (Vector3)direction * attractionSpeed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 플레이어와 충돌했는지 확인합니다.
        if (other.gameObject.TryGetComponent(out PlayerCharacter player))
        {
            // 플레이어에게 경험치를 주고 자신을 파괴합니다.
            player.AddExperience(experienceValue);
            Destroy(gameObject);
        }
    }
}

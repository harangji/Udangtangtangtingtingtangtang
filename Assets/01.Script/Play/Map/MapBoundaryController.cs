using UnityEngine;

// 참고: 이 클래스는 더 이상 카메라에 의존하지 않으므로, 'ManualBoundarySetter' 등으로 이름을 바꾸는 것을 추천합니다.
[RequireComponent(typeof(EdgeCollider2D))]
public class MapBoundaryController : MonoBehaviour
{
    [Header("수동 맵 경계 설정")]
    [Tooltip("맵의 전체 너비입니다.")]
    public float mapWidth = 30f;
    [Tooltip("맵의 전체 높이입니다.")]
    public float mapHeight = 20f;

    [Header("필수 컴포넌트")]
    [SerializeField] private EdgeCollider2D edge;

    // 월드 좌표 기준 경계값
    private float _worldLeft;
    private float _worldRight;
    private float _worldBottom;
    private float _worldTop;

    private void Awake()
    {
        if (edge == null)
        {
            edge = GetComponent<EdgeCollider2D>();
        }
        UpdateBoundaryCollider();
    }

    private void Start()
    {
        // 이 오브젝트의 위치를 중심으로 월드 좌표 경계를 계산합니다.
        Vector3 center = transform.position;
        float halfWidth = mapWidth / 2f;
        float halfHeight = mapHeight / 2f;

        _worldLeft   = center.x - halfWidth;
        _worldRight  = center.x + halfWidth;
        _worldBottom = center.y - halfHeight;
        _worldTop    = center.y + halfHeight;
    }

    private void FixedUpdate()
    {
        // 경계 밖으로 나간 캐릭터를 중앙으로 돌려보내는 로직은 그대로 유지됩니다.
        // 이제 이 로직은 수동으로 설정된 경계를 기준으로 작동합니다.
        foreach (var character in InGameHolder.Instance.Characters)
        {
            Vector3 targetPosition = character.transform.position;
            if (targetPosition.x < _worldLeft || targetPosition.x > _worldRight || 
                targetPosition.y < _worldBottom || targetPosition.y > _worldTop)
            {
                Debug.Log($"{character.gameObject.name}이(가) 경계를 벗어나 원점으로 돌아갑니다.");
                character.transform.position = Vector2.zero;
            }
        }
    }
    
    /// <summary>
    /// 설정된 너비와 높이에 맞춰 EdgeCollider2D의 모양을 업데이트합니다.
    /// </summary>
    private void UpdateBoundaryCollider()
    {
        float halfWidth = mapWidth / 2f;
        float halfHeight = mapHeight / 2f;

        Vector2[] points = new Vector2[5];
        points[0] = new Vector2(-halfWidth, -halfHeight);
        points[1] = new Vector2(-halfWidth, halfHeight);
        points[2] = new Vector2(halfWidth, halfHeight);
        points[3] = new Vector2(halfWidth, -halfHeight);
        points[4] = new Vector2(-halfWidth, -halfHeight); // 마지막 점을 처음 점과 연결

        if(edge != null) edge.points = points;
    }

    /// <summary>
    /// 에디터에서 경계를 시각적으로 보여주는 기즈모를 그립니다.
    /// </summary>
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        // 이 게임 오브젝트의 위치를 중심으로 와이어 큐브를 그립니다.
        Gizmos.DrawWireCube(transform.position, new Vector3(mapWidth, mapHeight, 0));
    }

    /// <summary>
    /// 에디터에서 값이 변경될 때마다 콜라이더와 기즈모를 업데이트합니다.
    /// </summary>
    private void OnValidate()
    {
        if (edge == null) 
        {
            edge = GetComponent<EdgeCollider2D>();
        }
        UpdateBoundaryCollider();
    }
}

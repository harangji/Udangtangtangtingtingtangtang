using UnityEngine;
using Shapes; // Shapes 네임스페이스를 추가합니다.

[RequireComponent(typeof(PolygonCollider2D))] // PolygonCollider2D 컴포넌트가 필요합니다.
public class HollowPolygonCollider2D : MonoBehaviour
{
    public float outerWidth = 10f; // 바깥 사각형의 너비입니다.
    public float outerHeight = 10f; // 바깥 사각형의 높이입니다.
    public float innerWidth = 8f;  // 안쪽 사각형의 너비입니다.
    public float innerHeight = 8f; // 안쪽 사각형의 높이입니다.

    public Color lineColor = Color.yellow; // 선의 색상입니다.
    public float lineThickness = 0.1f; // 선의 두께입니다.

    private PolygonCollider2D polygonCollider;

    void Awake()
    {
        polygonCollider = GetComponent<PolygonCollider2D>();
        GenerateHollowSquareCollider();
    }

    void OnValidate() // 에디터에서 값 변경 시 즉시 반영됩니다.
    {
        if (polygonCollider == null)
        {
            polygonCollider = GetComponent<PolygonCollider2D>();
        }
        if (polygonCollider != null)
        {
            GenerateHollowSquareCollider();
        }
    }

    void GenerateHollowSquareCollider()
    {
        // 유효성 검사입니다.
        if (innerWidth >= outerWidth || innerHeight >= outerHeight)
        {
            Debug.LogError("안쪽 너비/높이는 바깥 너비/높이보다 작아야 합니다! 콜라이더 생성 실패입니다.");
            polygonCollider.pathCount = 0; // 콜라이더를 비워줍니다.
            return;
        }

        // 바깥 사각형의 점들을 정의합니다. (시계 반대 방향)
        Vector2[] outerPoints = new Vector2[4];
        outerPoints[0] = new Vector2(outerWidth / 2, outerHeight / 2);
        outerPoints[1] = new Vector2(-outerWidth / 2, outerHeight / 2);
        outerPoints[2] = new Vector2(-outerWidth / 2, -outerHeight / 2);
        outerPoints[3] = new Vector2(outerWidth / 2, -outerHeight / 2);

        // 안쪽 사각형의 점들을 정의합니다. (시계 방향 - 구멍을 뚫을 때 필요합니다.)
        Vector2[] innerPoints = new Vector2[4];
        innerPoints[0] = new Vector2(innerWidth / 2, -innerHeight / 2);
        innerPoints[1] = new Vector2(-innerWidth / 2, -innerHeight / 2);
        innerPoints[2] = new Vector2(-innerWidth / 2, innerHeight / 2);
        innerPoints[3] = new Vector2(innerWidth / 2, innerHeight / 2);

        // PolygonCollider2D에 두 개의 경로를 설정합니다.
        polygonCollider.pathCount = 2; // 바깥 경로와 안쪽 경로 (구멍) 입니다.
        polygonCollider.SetPath(0, outerPoints); // 첫 번째 경로는 바깥 사각형입니다.
        polygonCollider.SetPath(1, innerPoints); // 두 번째 경로는 안쪽 사각형 (구멍) 입니다.
    }

    // Shapes를 사용하여 내부 테두리를 그립니다.
    void OnDrawGizmos()
    {
        // Shapes 드로잉 모드를 설정합니다.
        using (Draw.Command(Camera.current))
        {
            Draw.Matrix = transform.localToWorldMatrix; // 오브젝트의 로컬 좌표계를 사용합니다.
            Draw.Color = lineColor; // 선의 색상을 설정합니다.
            Draw.Thickness = lineThickness; // 선의 두께를 설정합니다.
            Draw.LineGeometry = LineGeometry.Flat2D; // 2D 평면 선으로 그립니다.

            // 안쪽 사각형의 점들을 다시 계산합니다.
            Vector2[] innerPoints = new Vector2[4];
            innerPoints[0] = new Vector2(innerWidth / 2, -innerHeight / 2);
            innerPoints[1] = new Vector2(-innerWidth / 2, -innerHeight / 2);
            innerPoints[2] = new Vector2(-innerWidth / 2, innerHeight / 2);
            innerPoints[3] = new Vector2(innerWidth / 2, innerHeight / 2);

            // PolylinePath를 생성하고 점들을 추가합니다.
            PolylinePath path = new PolylinePath();
            foreach (Vector2 p in innerPoints)
            {
                path.AddPoint(p);
            }

            // Polyline으로 안쪽 사각형을 그립니다. (닫힌 루프)
            Draw.Polyline(path, true);
        }
    }
}

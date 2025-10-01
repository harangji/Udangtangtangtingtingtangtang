using UnityEngine;
using UnityEditor;
using System.Linq;
using System.Text.RegularExpressions;

public class ScoreZoneArrangerWindow : EditorWindow
{
    private float spacing = 0.1f;

    [MenuItem("Tools/선택된 오브젝트 수평 정렬")]
    public static void ShowWindow()
    {
        // 기존에 열려있는 창 인스턴스가 없으면 새로 만듭니다.
        EditorWindow.GetWindow(typeof(ScoreZoneArrangerWindow), false, "오브젝트 정렬");
    }

    void OnGUI()
    {
        GUILayout.Label("수평 정렬 설정", EditorStyles.boldLabel);
        spacing = EditorGUILayout.FloatField("간격", spacing);

        if (GUILayout.Button("선택된 오브젝트 정렬"))
        {
            ArrangeObjects(spacing);
        }
    }

    private void ArrangeObjects(float spacing)
    {
        // 계층 구조에서 선택된 게임 오브젝트를 가져옵니다.
        GameObject[] selectedObjects = Selection.gameObjects;

        if (selectedObjects.Length < 2)
        {
            EditorUtility.DisplayDialog("오브젝트 부족", "정렬하려면 계층 구조에서 최소 두 개 이상의 오브젝트를 선택하세요.", "확인");
            return;
        }

        // 이름에 포함된 숫자를 올바르게 인식하여 정렬합니다 (Natural Sort).
        // 예: "Object10"이 "Object2"보다 뒤에 오도록 합니다.
        var sortedObjects = selectedObjects.OrderBy(go => Regex.Replace(go.name, @"\d+", m => m.Value.PadLeft(10, '0'))).ToArray();

        // 첫 번째 오브젝트가 기준점이 됩니다. 모든 것을 이 오브젝트에 상대적으로 정렬합니다.
        var anchorObject = sortedObjects[0];
        var anchorCollider = anchorObject.GetComponent<Collider2D>();

        if (anchorCollider == null)
        {
            EditorUtility.DisplayDialog("정렬 오류", $"첫 번째 오브젝트({anchorObject.name})의 크기를 결정하려면 Collider2D가 있어야 합니다.", "확인");
            return;
        }

        // 단일 실행 취소 작업을 위해 모든 트랜스폼을 등록합니다.
        Undo.RecordObjects(sortedObjects.Select(go => go.transform).ToArray(), "선택된 오브젝트 정렬");

        // 이 커서는 이전에 배치된 오브젝트의 가장 오른쪽 가장자리를 추적합니다.
        float cursorX = anchorObject.transform.position.x + anchorCollider.bounds.extents.x;

        // 두 번째 오브젝트부터 정렬합니다.
        for (int i = 1; i < sortedObjects.Length; i++)
        {
            var currentObject = sortedObjects[i];
            var currentCol = currentObject.GetComponent<Collider2D>();

            if (currentCol == null)
            {
                Debug.LogWarning($"오브젝트 '{currentObject.name}'에 Collider2D가 없어 건너뛰었습니다.", currentObject);
                continue;
            }

            float halfWidth = currentCol.bounds.extents.x;
            
            // 현재 오브젝트의 새 중심 위치를 계산합니다.
            float newX = cursorX + spacing + halfWidth;
            Vector3 newPosition = new Vector3(newX, anchorObject.transform.position.y, anchorObject.transform.position.z);
            currentObject.transform.position = newPosition;

            // 방금 배치한 오브젝트의 오른쪽 가장자리로 커서를 업데이트합니다.
            cursorX = newX + halfWidth;

            // 변경 사항이 저장되도록 합니다.
            EditorUtility.SetDirty(currentObject);
        }

        Debug.Log($"{sortedObjects.Length}개의 오브젝트가 수평으로 정렬되었습니다 (간격: {spacing}).");
    }
}
using UnityEngine;
using UnityEditor;
using System.Linq;
using System.Collections.Generic;

public class PyramidArrangerWindow : EditorWindow
{
    private float horizontalSpacing = 0.1f;
    private float verticalSpacing = 0.1f;

    [MenuItem("Tools/선택된 오브젝트 피라미드 정렬")]
    public static void ShowWindow()
    {
        // 기존에 열려있는 창 인스턴스가 없으면 새로 만듭니다.
        EditorWindow.GetWindow(typeof(PyramidArrangerWindow), false, "피라미드 정렬");
    }

    void OnGUI()
    {
        GUILayout.Label("피라미드 정렬 설정", EditorStyles.boldLabel);
        horizontalSpacing = EditorGUILayout.FloatField("수평 간격", horizontalSpacing);
        verticalSpacing = EditorGUILayout.FloatField("수직 간격", verticalSpacing);

        if (GUILayout.Button("선택된 오브젝트 정렬"))
        {
            ArrangeObjects(horizontalSpacing, verticalSpacing);
        }
    }

    private void ArrangeObjects(float hSpacing, float vSpacing)
    {
        // 계층 구조에서 선택된 게임 오브젝트를 가져옵니다.
        GameObject[] selectedObjects = Selection.gameObjects;

        if (selectedObjects.Length == 0)
        {
            EditorUtility.DisplayDialog("오브젝트 없음", "정렬할 오브젝트를 하나 이상 선택하세요.", "확인");
            return;
        }

        // 일관된 정렬 순서를 보장하기 위해 이름순으로 정렬합니다.
        var sortedObjects = selectedObjects.OrderBy(go => go.name).ToList();

        // 첫 번째 오브젝트를 기준으로 크기와 시작 위치를 결정합니다.
        var anchorObject = sortedObjects[0];
        var anchorCollider = anchorObject.GetComponent<Collider2D>();

        if (anchorCollider == null)
        {
            EditorUtility.DisplayDialog("정렬 오류", $"기준 오브젝트({anchorObject.name})의 크기를 결정하려면 Collider2D가 있어야 합니다.", "확인");
            return;
        }

        // 모든 오브젝트의 크기가 동일하다고 가정하고 첫 번째 오브젝트의 크기를 사용합니다.
        float objectWidth = anchorCollider.bounds.size.x;
        float objectHeight = anchorCollider.bounds.size.y;

        // 피라미드의 최상단 위치는 기준 오브젝트의 위치로 설정합니다.
        Vector3 pyramidTopPosition = anchorObject.transform.position;

        // 단일 실행 취소 작업을 위해 모든 트랜스폼을 등록합니다.
        Undo.RecordObjects(sortedObjects.Select(go => go.transform).ToArray(), "피라미드 정렬");

        int objectIndex = 0;
        int row = 0;
        while (objectIndex < sortedObjects.Count)
        {
            int itemsInRow = row + 1;

            // 현재 행의 전체 너비를 계산합니다. (오브젝트 너비 + 간격)
            float rowTotalWidth = (itemsInRow - 1) * (objectWidth + hSpacing);
            // 현재 행의 시작 X 위치를 계산합니다. (중앙 정렬)
            float rowStartX = pyramidTopPosition.x - rowTotalWidth / 2f;

            // 현재 행의 Y 위치를 계산합니다.
            float rowY = pyramidTopPosition.y - row * (objectHeight + vSpacing);

            for (int col = 0; col < itemsInRow; col++)
            {
                if (objectIndex >= sortedObjects.Count)
                {
                    break; // 배치할 오브젝트가 더 이상 없으면 중단합니다.
                }

                var currentObject = sortedObjects[objectIndex];
                
                // 현재 오브젝트의 새 X 위치를 계산합니다.
                float newX = rowStartX + col * (objectWidth + hSpacing);
                
                // 오브젝트 위치를 설정합니다.
                currentObject.transform.position = new Vector3(newX, rowY, pyramidTopPosition.z);
                
                // 변경 사항이 저장되도록 합니다.
                EditorUtility.SetDirty(currentObject);
                
                objectIndex++;
            }
            row++;
        }

        Debug.Log($"{objectIndex}개의 오브젝트를 피라미드 형태로 정렬했습니다.");
    }
}

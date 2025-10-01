using UnityEngine;
using TMPro;

// FloatingText의 생성을 관리하는 싱글톤 매니저입니다.
public class FloatingTextManager : MonoBehaviour
{
    public static FloatingTextManager Instance { get; private set; }

    [Tooltip("생성할 플로팅 텍스트의 프리팹입니다.")]
    public GameObject floatingTextPrefab;

    private Camera mainCamera;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        mainCamera = Camera.main;
    }
    

    // 지정된 월드 좌표에 플로팅 텍스트를 표시하는 메소드입니다.
    public void Show(string text, Vector3 worldPosition)
    {
        if (floatingTextPrefab == null || mainCamera == null) return;

        // 월드 좌표를 스크린 좌표로 변환합니다.
        Vector3 screenPos = mainCamera.WorldToScreenPoint(worldPosition);

        // 오브젝트가 카메라 뒤에 있으면 텍스트를 표시하지 않습니다.
        if (screenPos.z < 0) return;

        // 캔버스를 부모로 하여 UI 오브젝트를 생성합니다.
        GameObject textObj = Instantiate(floatingTextPrefab, transform);
        // 변환된 스크린 좌표로 위치를 설정합니다.
        textObj.transform.position = screenPos;
        
        // 자식 오브젝트에 있을 수도 있고, TextMeshPro 또는 TextMeshProUGUI일 수 있으므로
        // 공통 부모인 TMP_Text를 GetComponentInChildren으로 찾습니다.
        var tmp = textObj.GetComponentInChildren<TMP_Text>();
        if (tmp != null)
        {
            tmp.text = text;
        }
        else
        {
            Debug.LogError("FloatingTextManager: 프리팹 또는 그 자식에 TextMeshPro 혹은 TextMeshProUGUI 컴포넌트가 없습니다!", textObj); 
        }
    }
}

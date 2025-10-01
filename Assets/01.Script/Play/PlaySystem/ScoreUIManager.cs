using UnityEngine;
using TMPro;
using System.Collections.Generic;

// 월드 공간의 ScoreZone에 연결된, 화면 공간 캔버스 위 점수 텍스트 표시를 관리합니다.
public class ScoreUIManager : MonoBehaviour
{
    public static ScoreUIManager Instance { get; private set; }

    [Header("UI References")]
    [Tooltip("점수를 표시하기 위한 UI 텍스트 프리팹입니다.")]
    public TextMeshProUGUI scoreTextPrefab;

    [Tooltip("점수 UI가 연결될 캔버스입니다.")]
    public Canvas mainCanvas;

    private Camera mainCamera;
    private Dictionary<ScoreZone, TextMeshProUGUI> scoreUIs = new Dictionary<ScoreZone, TextMeshProUGUI>();

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

    private void Start()
    {
        if (mainCanvas == null)
        {
            mainCanvas = FindObjectOfType<Canvas>();
            if (mainCanvas == null)
            {
                Debug.LogError("ScoreUIManager: 씬에서 캔버스를 찾을 수 없습니다. 하나를 할당해주세요.", this);
                enabled = false;
                return;
            }
        }
        if (scoreTextPrefab == null)
        {
            Debug.LogError("ScoreUIManager: 점수 텍스트 프리팹이 할당되지 않았습니다.", this);
            enabled = false;
            return;
        }
    }

    public void RegisterScoreZone(ScoreZone zone)
    {
        if (scoreUIs.ContainsKey(zone) || scoreTextPrefab == null) return;

        TextMeshProUGUI newScoreText = Instantiate(scoreTextPrefab, mainCanvas.transform);
        newScoreText.text = zone.score.ToString();
        scoreUIs.Add(zone, newScoreText);

        UpdateTextPosition(zone, newScoreText);
    }

    public void UnregisterScoreZone(ScoreZone zone)
    {
        if (scoreUIs.TryGetValue(zone, out TextMeshProUGUI uiText))
        {
            Destroy(uiText.gameObject);
            scoreUIs.Remove(zone);
        }
    }

    private void UpdateTextPosition(ScoreZone zone, TextMeshProUGUI uiText)
    {
        // 월드 좌표를 스크린 좌표로 변환합니다.
        Vector3 screenPos = mainCamera.WorldToScreenPoint(zone.transform.position);

        // UI 요소의 위치를 설정합니다.
        // Screen Space - Overlay 캔버스에서는 screenPos의 Z 구성 요소가 무시됩니다.
        if (uiText != null)
        {
            uiText.transform.position = screenPos;
            // 선택 사항: 카메라 뒤에 있으면 텍스트를 숨깁니다.
            uiText.enabled = screenPos.z > 0;
        }
    }
}

using PachinkoTest;
using UnityEngine;

// ScoreZone.cs
// 핀볼 보드 하단의 각 점수 구역 트리거에 연결합니다.
[RequireComponent(typeof(Collider2D))]
public class ScoreZone : MonoBehaviour
{
    [Tooltip("이 구역의 점수 값입니다.")]
    public int score = 1;

    [Header("색상 그라데이션")]
    [Tooltip("색상 그라데이션의 기준이 되는 최대 점수입니다.")]
    public int maxScoreForGradient = 100;
    [Tooltip("색상을 변경할 SpriteRenderer입니다. 비워두면 자동으로 찾습니다.")]
    public SpriteRenderer spriteRenderer;

    private void Start()
    {
        // 점수에 따라 색상을 설정합니다.
        SetColorByScore();

        // 콜라이더가 트리거인지 확인합니다.
        var col = GetComponent<Collider2D>();
        if (!col.isTrigger)
        {
            Debug.LogWarning($"Collider on {gameObject.name} is not set to 'Is Trigger'. Please enable it.", this);
            col.isTrigger = true;
        }

        // UI 매니저에 등록하여 점수를 표시합니다.
        if (ScoreUIManager.Instance != null)
        {
            ScoreUIManager.Instance.RegisterScoreZone(this);
        }
        else
        {
            Debug.LogWarning("ScoreUIManager not found. Score text will not be displayed.", this);
        }
    }

    private void OnDestroy()
    {
        // 이 오브젝트가 파괴될 때 UI 매니저에서 등록을 해제합니다.
        if (ScoreUIManager.Instance != null)
        {
            ScoreUIManager.Instance.UnregisterScoreZone(this);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 공의 태그가 "Player"인지 확인합니다.
        if (other.CompareTag("Player"))
        {
            Ball ball = other.GetComponent<Ball>();

            // 공이 존재하고, 아직 점수를 획득하지 않은 경우에만 점수를 처리합니다.
            if (ball != null && !ball.hasScored)
            {
                ball.hasScored = true; // 점수 획득 상태로 변경하여 중복 획득을 방지합니다.

                if (PachinkoGameManager.Instance != null)
                {
                    Debug.Log($"{gameObject.name} triggered by {other.name}! Adding {score} points.");
                    PachinkoGameManager.Instance?.AddScore(score);

                    // 플로팅 텍스트를 표시합니다.
                    if (FloatingTextManager.Instance != null)
                    {
                        FloatingTextManager.Instance.Show("+" + score.ToString(), transform.position);
                    }
                }
                else
                {
                    Debug.LogError("PinballManager instance not found in the scene!");
                }

                // 점수를 획득한 공은 파괴합니다.
                Destroy(other.gameObject);
            }
        }
    }

    private void SetColorByScore()
    {
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        if (spriteRenderer == null)
        {
            // 이 경고는 색상 변경 기능에만 해당되므로, 기능이 실패해도 계속 진행합니다.
            Debug.LogWarning("ScoreZone에 색상을 적용할 SpriteRenderer가 없습니다.", this); 
            return;
        }

        // 점수 범위를 0과 1 사이의 값으로 정규화합니다. (최소 점수는 1로 가정)
        float t = Mathf.InverseLerp(1, maxScoreForGradient, score);

        // t 값에 따라 빨간색과 노란색 사이를 보간(Lerp)합니다.
        Color newColor = Color.Lerp(Color.red, Color.yellow, t);

        spriteRenderer.color = newColor;
    }
}
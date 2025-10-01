using UnityEngine;

// PinballManager.cs
// A singleton manager to handle the pinball minigame logic.
public class PinballManager : MonoBehaviour
{
    public static PinballManager Instance { get; private set; }

    public int TotalScore { get; private set; }

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
        TotalScore = 0;
    }

    public void AddScore(int score)
    {
        TotalScore += score;
        Debug.Log($"Score Added: {score}. Total Score: {TotalScore}");
        // 여기에 UI나 다른 게임 요소를 업데이트하는 로직을 추가할 수 있다냥.
    }

    public void ResetScore()
    {
        TotalScore = 0;
        Debug.Log("Score reset.");
    }
}

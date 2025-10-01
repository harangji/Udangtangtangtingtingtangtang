using UnityEngine;

namespace PachinkoTest
{
    public class PachinkoGameManager : MonoBehaviour
    {
        public static PachinkoGameManager Instance { get; private set; }

        [Header("Game Elements")]
        public GameObject ballPrefab;
        public Transform dropPosition;

        [Header("Game State")]
        public int currency = 20; // Starting currency
        public int totalScore = 0;
        public int dropCost = 1;

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
        }

        public void DropBall()
        {
            if (currency >= dropCost)
            {
                currency -= dropCost;
                Instantiate(ballPrefab, dropPosition.position, Quaternion.identity);
                PachinkoUIController.Instance?.UpdateCurrencyText(currency);
                Debug.Log("Ball dropped! Currency left: " + currency);
            }
            else
            {
                Debug.LogWarning("Not enough currency to drop a ball!");
            }
        }

        public void AddScore(int score)
        {
            totalScore += score;
            PachinkoUIController.Instance?.UpdateScoreText(totalScore);
            Debug.Log("Score added: " + score + ". Total score: " + totalScore);
        }
    }
}

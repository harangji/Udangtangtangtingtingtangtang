using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace PachinkoTest
{
    public class PachinkoUIController : MonoBehaviour
    {
        public static PachinkoUIController Instance { get; private set; }

        [Header("UI Elements")]
        public Button dropButton;
        public TextMeshProUGUI scoreText;
        public TextMeshProUGUI currencyText;

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

        private void Start()
        {
            if (dropButton != null)
            {
                dropButton.onClick.AddListener(OnDropButtonClicked);
            }

            // Initialize UI text
            if (PachinkoGameManager.Instance != null)
            {
                UpdateScoreText(PachinkoGameManager.Instance.totalScore);
                UpdateCurrencyText(PachinkoGameManager.Instance.currency);
            }
        }

        private void OnDropButtonClicked()
        {
            PachinkoGameManager.Instance?.DropBall();
        }

        public void UpdateScoreText(int score)
        {
            if (scoreText != null)
            {
                scoreText.text = "Score: " + score.ToString();
            }
        }

        public void UpdateCurrencyText(int currency)
        {
            if (currencyText != null)
            {
                currencyText.text = "Currency: " + currency.ToString();
            }
        }
    }
}

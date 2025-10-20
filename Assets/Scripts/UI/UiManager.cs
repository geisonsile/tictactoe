using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private GameObject _gameOverPanel;
    [SerializeField] private TextMeshProUGUI _resultText;
    [SerializeField] private Button _restartButton;
    void Start()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnGameWon += HandleGameWon;
            GameManager.Instance.OnGameDraw += HandleGameDraw;
            GameManager.Instance.OnGameStarted += HandleGameStarted;
        }
        
        HandleGameStarted();
        _restartButton.onClick.AddListener(RestartGame);
    }

    private void OnDisable()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnGameWon -= HandleGameWon;
            GameManager.Instance.OnGameDraw -= HandleGameDraw;
             GameManager.Instance.OnGameStarted -= HandleGameStarted;
        }
    }
    private void HandleGameStarted()
    {
        _gameOverPanel.SetActive(false);
    }
    private void HandleGameWon(Player player)
    {
        _resultText.text = $"Jogador <color=green><b>{player}</b></color> venceu!";
        _gameOverPanel.SetActive(true);
    }

    private void HandleGameDraw()
    {
        _resultText.text = "Empate!";
        _gameOverPanel.SetActive(true);
    }

    private void RestartGame()
    {
        GameManager.Instance.StartGame();
    }
}

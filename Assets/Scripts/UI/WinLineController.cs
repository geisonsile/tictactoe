using UnityEngine;
using UnityEngine.UI;

public class WinLineController : MonoBehaviour
{
    [SerializeField] private Image _winLineImage;
    [SerializeField] private BoardUI _boardUI;

    void Start()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnGameWon += HandleGameWon;
            GameManager.Instance.OnGameStarted += HandleGameStarted;
        }
    }

    private void OnDisable()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnGameWon -= HandleGameWon;
            GameManager.Instance.OnGameStarted -= HandleGameStarted;
        }
    }

    private void HandleGameStarted()
    {
        _winLineImage.gameObject.SetActive(false);
    }

    private void HandleGameWon(Player winner)
    {
        WinLine winLine = GameManager.Instance.GetWinLine();

        //Pega os RectTransforms reais das células de início e fim
        RectTransform startCell = _boardUI.GetCellTransform(winLine.StartCellIndex.x, winLine.StartCellIndex.y);
        RectTransform endCell = _boardUI.GetCellTransform(winLine.EndCellIndex.x, winLine.EndCellIndex.y);

        if (startCell == null || endCell == null) return;

        //Pega as posições locais (anchoredPosition) das células dentro do BoardPanel
        Vector2 startPos = startCell.localPosition;
        Vector2 endPos = endCell.localPosition;

        RectTransform lineRect = _winLineImage.rectTransform;
        _winLineImage.gameObject.SetActive(true);

        lineRect.localPosition = (startPos + endPos) / 2f;
        float distance = Vector2.Distance(startPos, endPos);
        lineRect.sizeDelta = new Vector2(distance, 50f);

        Vector2 direction = (endPos - startPos).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        lineRect.localEulerAngles = new Vector3(0, 0, angle);
    }
}
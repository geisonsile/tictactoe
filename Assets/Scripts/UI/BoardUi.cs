using UnityEngine;

public class BoardUi : MonoBehaviour
{
    [SerializeField] private CellUI[] _cells;

    void Start()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnMoveMade += UpdateBoard;
            GameManager.Instance.OnGameStarted += HandleGameStarted;
        }

        for (int i = 0; i < _cells.Length; i++)
        {
            int row = i / 3;
            int col = i % 3;
            _cells[i].Initialize(row, col);
        }
    }
    private void OnDisable()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnMoveMade -= UpdateBoard;
            GameManager.Instance.OnGameStarted -= HandleGameStarted;
        }
    }

    /// <summary>
    /// Este método é executado quando o evento OnMoveMade acontece.
    /// </summary>
    private void UpdateBoard(int row, int col, Player player)
    {
        // Converte a coordenada (linha, coluna) para o índice do array.
        int index = row * 3 + col;

        // Validação para garantir que o índice está dentro dos limites do array.
        if (index >= 0 && index < _cells.Length)
        {
            _cells[index].UpdateCell(player);
        }
    }

    private void ResetBoard()
    {
        foreach (var cell in _cells)
        {
            cell.ResetCell();
        }
    }

    private void HandleGameStarted()
    {
        ResetBoard();
    }  
    
}

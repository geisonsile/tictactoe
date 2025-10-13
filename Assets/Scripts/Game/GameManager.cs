using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public event Action<Player> OnPlayerTurnChanged;
    public event Action<Player> OnGameWon;
    public event Action OnGameDraw;

    private Board _board;
    private Player _currentPlayer;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        _board = new Board();
    }
    void Start()
    {
        StartGame();
    }

    public void StartGame()
    {
        _board.Clear();
        _currentPlayer = Player.X;
        OnPlayerTurnChanged?.Invoke(_currentPlayer);
    }

    public void MakeMove(int row, int col)
    {
        if (!_board.IsCellEmpty(row, col)) return;

        _board.SetCell(row, col, _currentPlayer);

        if (CheckForWin(row, col))
        {
            OnGameWon?.Invoke(_currentPlayer);
        }
        else if (CheckForDraw())
        {
            OnGameDraw?.Invoke();
        }
        else
        {
            SwitchPlayer();
        }
    }

    private bool CheckForWin(int lastMoveRow, int lastMoveCol)
    {
        // Lógica para checar vitória
        return false; // Implementar
    }

    private bool CheckForDraw()
    {
        // Lógica para checar empate
        return false; // Implementar
    }

    private void SwitchPlayer()
    {
        _currentPlayer = _currentPlayer == Player.X ? Player.O : Player.X;
        OnPlayerTurnChanged?.Invoke(_currentPlayer);
    }
}

using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public event Action<Player> OnPlayerTurnChanged;
    public event Action<Player> OnGameWon;
    public event Action OnGameDraw;
    public event Action<int, int, Player> OnMoveMade;
    public event Action OnGameStarted;

    private Board _board;
    private Player _currentPlayer;
    private bool _isGameOver;
    private int _movesMade;

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
        _isGameOver = false;
        _movesMade = 0;
        _currentPlayer = Player.X;

        OnGameStarted?.Invoke();
        OnPlayerTurnChanged?.Invoke(_currentPlayer);
    }

    public void MakeMove(int row, int col)
    {
        if (_isGameOver || !_board.IsCellEmpty(row, col)) return;

        _board.SetCell(row, col, _currentPlayer);
        _movesMade++;

        OnMoveMade?.Invoke(row, col, _currentPlayer);

        if (CheckForWin(row, col))
        {
            _isGameOver = true;
            OnGameWon?.Invoke(_currentPlayer);
        }
        else if (CheckForDraw())
        {
            _isGameOver = true;
            OnGameDraw?.Invoke();
        }
        else
        {
            SwitchPlayer();
        }
    }
    private void SwitchPlayer()
    {
        _currentPlayer = _currentPlayer == Player.X ? Player.O : Player.X;
        OnPlayerTurnChanged?.Invoke(_currentPlayer);
    }

    /// <summary>
    /// Verifica se o jogador atual venceu o jogo após sua última jogada.
    /// É mais eficiente checar apenas as linhas, colunas e diagonais relevantes à última jogada.
    /// </summary>
    /// <param name="lastMoveRow">A linha da última jogada.</param>
    /// <param name="lastMoveCol">A coluna da última jogada.</param>
    /// <returns>True se o jogador atual venceu, false caso contrário.</returns>
    private bool CheckForWin(int lastMoveRow, int lastMoveCol)
    {
        // Checar a linha
        if (_board.GetCell(lastMoveRow, 0) == _currentPlayer &&
            _board.GetCell(lastMoveRow, 1) == _currentPlayer &&
            _board.GetCell(lastMoveRow, 2) == _currentPlayer)
        {
            return true;
        }

        // Checar a coluna
        if (_board.GetCell(0, lastMoveCol) == _currentPlayer &&
            _board.GetCell(1, lastMoveCol) == _currentPlayer &&
            _board.GetCell(2, lastMoveCol) == _currentPlayer)
        {
            return true;
        }

        // Checar a diagonal principal (se a jogada foi nela)
        if (lastMoveRow == lastMoveCol)
        {
            if (_board.GetCell(0, 0) == _currentPlayer &&
                _board.GetCell(1, 1) == _currentPlayer &&
                _board.GetCell(2, 2) == _currentPlayer)
            {
                return true;
            }
        }

        // Checar a diagonal secundária (se a jogada foi nela)
        if (lastMoveRow + lastMoveCol == 2)
        {
            if (_board.GetCell(0, 2) == _currentPlayer &&
                _board.GetCell(1, 1) == _currentPlayer &&
                _board.GetCell(2, 0) == _currentPlayer)
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// Verifica se o jogo empatou.
    /// </summary>
    /// <returns>True se o jogo empatou, false caso contrário.</returns>
    private bool CheckForDraw()
    {
        // Um empate só pode ocorrer se todas as 9 casas foram preenchidas.
        // Como já checamos a vitória antes, se chegamos em 9 jogadas, é empate.
        return _movesMade == 9;
    }
}

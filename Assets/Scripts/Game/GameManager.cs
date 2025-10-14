using System;
using UnityEngine;

/// <summary>
/// Gerencia a lógica do jogo Tic Tac Toe.
/// Uso do padrão Strategy para verificação de vitória e empate.
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public event Action<Player> OnPlayerTurnChanged;
    public event Action<Player> OnGameWon;
    public event Action OnGameDraw;
    public event Action<int, int, Player> OnMoveMade;
    public event Action OnGameStarted;

    public Board CurrentBoard => _board;
    public bool IsHumanTurn { get; private set; }

    private Board _board;
    private Player _currentPlayer;
    private bool _isGameOver;
    private int _movesMade;

    // Estratégias para verificação de condições
    private readonly IWinCondition[] _winConditions;
    private readonly IDrawCondition _drawCondition;

    public GameManager()
    {
        _winConditions = new IWinCondition[]
        {
            new RowWinCondition(),
            new ColumnWinCondition(),
            new MainDiagonalWinCondition(),
            new AntiDiagonalWinCondition()
        };

        _drawCondition = new FullBoardDrawCondition();
    }

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
        IsHumanTurn = true; // O jogador X (humano) sempre começa

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
            EndGame(true);
        }
        else if (CheckForDraw())
        {
           EndGame(false);
        }
        else
        {
            SwitchPlayer();
        }
    }
    private void SwitchPlayer()
    {
        _currentPlayer = _currentPlayer == Player.X ? Player.O : Player.X;

        //Se o jogador for O, é a vez da IA
        IsHumanTurn = _currentPlayer == Player.X;

        OnPlayerTurnChanged?.Invoke(_currentPlayer);
    }

    /// <summary>
    /// Verifica se o jogador atual venceu o jogo após sua última jogada.
    /// Utiliza o padrão Strategy para verificar todas as condições de vitória.
    /// </summary>
    private bool CheckForWin(int lastMoveRow, int lastMoveCol)
    {
        foreach (var winCondition in _winConditions)
        {
            if (winCondition.IsSatisfied(_board, lastMoveRow, lastMoveCol, _currentPlayer))
                return true;
        }

        return false;
    }

    /// <summary>
    /// Verifica se o jogo empatou.
    /// Utiliza o padrão Strategy para verificar a condição de empate.
    /// </summary>
    private bool CheckForDraw()
    {
        return _drawCondition.IsSatisfied(_movesMade);
    }

    /// <summary>
    /// Encerra o jogo, definindo o estado apropriado e disparando eventos.
    /// </summary>
    /// <param name="isWin">True se o jogo terminou com vitória, false se terminou em empate.</param>
    private void EndGame(bool isWin)
    {
        _isGameOver = true;
        IsHumanTurn = false;

        if (isWin)
        {
            OnGameWon?.Invoke(_currentPlayer);
        }
        else
        {
            OnGameDraw?.Invoke();
        }
    }
}

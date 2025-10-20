using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPlayer : MonoBehaviour
{
    [Tooltip("O tempo que a IA 'pensa' antes de jogar.")]
    [SerializeField] private float _thinkingTime = 1f;
    
    //O Jogador O será a IA.
    private const Player AI_PLAYER = Player.O;

    void Start()
    {
        if (GameManager.Instance != null)
        {
            // A IA ouve o evento de troca de turno para saber quando agir.
            GameManager.Instance.OnPlayerTurnChanged += HandlePlayerTurnChanged;
        }
    }

    private void OnDisable()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnPlayerTurnChanged -= HandlePlayerTurnChanged;
        }
    }

    private void HandlePlayerTurnChanged(Player currentPlayer)
    {
        if (currentPlayer == AI_PLAYER)
        {
            StartCoroutine(MakeMoveAfterDelay());
        }
    }

    private IEnumerator MakeMoveAfterDelay()
    {
        yield return new WaitForSeconds(_thinkingTime);

        Vector2Int move = FindBestMove();

        //Garante que uma jogada válida foi encontrada antes de fazer a jogada.
        if(move.x != -1) 
        {
            GameManager.Instance.MakeMove(move.x, move.y);
        }
    }

    private Vector2Int FindBestMove()
    {
        List<Vector2Int> availableMoves = new List<Vector2Int>();
        Board board = GameManager.Instance.CurrentBoard;

        // Percorre o tabuleiro para encontrar todas as casas vazias.
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (board.IsCellEmpty(i, j))
                {
                    availableMoves.Add(new Vector2Int(i, j));
                }
            }
        }

        // Para cada jogada possível, a IA simula se ela venceria.
        foreach (var move in availableMoves)
        {
            
            board.SetCell(move.x, move.y, AI_PLAYER);

            if (board.CheckWinCondition(AI_PLAYER))
            {
                board.ClearCell(move.x, move.y); 
                return move;
            }

            board.ClearCell(move.x, move.y);
        }

        // Se não encontrou uma jogada para vencer, a IA verifica se o jogador pode vencer.
        Player humanPlayer = Player.X;
        foreach (var move in availableMoves)
        {
            board.SetCell(move.x, move.y, humanPlayer);

            if (board.CheckWinCondition(humanPlayer))
            {
                board.ClearCell(move.x, move.y);
                return move;
            }

            board.ClearCell(move.x, move.y);
        }

        // Procura um canto vazio para IA jogar
        List<Vector2Int> cornerMoves = new List<Vector2Int>();
        if (board.IsCellEmpty(0, 0)) cornerMoves.Add(new Vector2Int(0, 0));
        if (board.IsCellEmpty(0, 2)) cornerMoves.Add(new Vector2Int(0, 2));
        if (board.IsCellEmpty(2, 0)) cornerMoves.Add(new Vector2Int(2, 0));
        if (board.IsCellEmpty(2, 2)) cornerMoves.Add(new Vector2Int(2, 2));

        if (cornerMoves.Count > 0)
        {
            int randomIndex = Random.Range(0, cornerMoves.Count);
            return cornerMoves[randomIndex];
        }

        // Se não há jogadas de vitória ou bloqueio, joga aleatoriamente
        if (availableMoves.Count > 0)
        {
            int randomIndex = Random.Range(0, availableMoves.Count);
            return availableMoves[randomIndex];
        }

        return new Vector2Int(-1, -1);
    }
}

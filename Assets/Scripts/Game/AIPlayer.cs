using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPlayer : MonoBehaviour
{
    [Tooltip("O tempo que a IA 'pensa' antes de jogar.")]
    [SerializeField] private float _thinkingTime = 1f;
    
    // Vamos designar o Jogador 'O' como sendo a IA.
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
        // Se o turno for do jogador da IA, ela começa a "pensar".
        if (currentPlayer == AI_PLAYER)
        {
            StartCoroutine(MakeMoveAfterDelay());
        }
    }

    private IEnumerator MakeMoveAfterDelay()
    {
        // Espera um pouco para a jogada não ser instantânea.
        yield return new WaitForSeconds(_thinkingTime);

        // Encontra a melhor jogada (no nosso caso, uma aleatória válida)
        Vector2Int move = FindBestMove();

        // Executa a jogada
        if(move.x != -1) // Garante que uma jogada válida foi encontrada
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
            // Simula a jogada da IA
            board.SetCell(move.x, move.y, AI_PLAYER);

            // Verifica se essa jogada resulta em vitória
            if (board.CheckWinCondition(AI_PLAYER))
            {
                board.ClearCell(move.x, move.y); // Limpa a simulação
                return move; // Retorna a jogada vencedora!
            }

            board.ClearCell(move.x, move.y); // Desfaz a simulação para o próximo teste
        }

        // Se não encontrou uma jogada para vencer, a IA verifica se o jogador pode vencer.
        Player humanPlayer = Player.X;
        foreach (var move in availableMoves)
        {
            // Simula a jogada do HUMANO
            board.SetCell(move.x, move.y, humanPlayer);

            // Verifica se o humano venceria com essa jogada
            if (board.CheckWinCondition(humanPlayer))
            {
                board.ClearCell(move.x, move.y); // Limpa a simulação
                return move; // Retorna a jogada de bloqueio!
            }

            board.ClearCell(move.x, move.y); // Desfaz a simulação
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

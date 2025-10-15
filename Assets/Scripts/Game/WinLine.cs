using UnityEngine;

/// <summary>
/// Estrutura que retorna os índices (linha, coluna) das células vencedoras de início e fim.
/// </summary>
public struct WinLine
{
    public readonly Vector2Int StartCellIndex;
    public readonly Vector2Int EndCellIndex;

    public WinLine(Vector2Int startCellIndex, Vector2Int endCellIndex)
    {
        StartCellIndex = startCellIndex;
        EndCellIndex = endCellIndex;
    }
}
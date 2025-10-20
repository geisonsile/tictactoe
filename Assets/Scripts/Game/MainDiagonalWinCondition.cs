using UnityEngine;

/// <summary>
/// Verifica vitória na diagonal principal (0,0 até 2,2).
/// </summary>
public class MainDiagonalWinCondition : IWinCondition
{
    public bool IsSatisfied(Board board, int lastMoveRow, int lastMoveCol, Player player)
    {
        if (lastMoveRow != lastMoveCol) return false;

        return board.GetCell(0, 0) == player &&
               board.GetCell(1, 1) == player &&
               board.GetCell(2, 2) == player;
    }

    public WinLine GetWinLine()
    {
        Vector2Int start = new Vector2Int(0, 0);
        Vector2Int end = new Vector2Int(2, 2);
        return new WinLine(start, end);
    }
}

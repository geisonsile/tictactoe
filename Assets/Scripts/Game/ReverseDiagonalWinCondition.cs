using UnityEngine;

/// <summary>
/// Verifica vitória na diagonal secundária (0,2 até 2,0).
/// </summary>
public class ReverseDiagonalWinCondition : IWinCondition
{
    public bool IsSatisfied(Board board, int lastMoveRow, int lastMoveCol, Player player)
    {
        if (lastMoveRow + lastMoveCol != 2) return false;

        return board.GetCell(0, 2) == player &&
               board.GetCell(1, 1) == player &&
               board.GetCell(2, 0) == player;
    }

    public WinLine GetWinLine()
    {
        Vector2Int start = new Vector2Int(0, 2);
        Vector2Int end = new Vector2Int(2, 0);
        return new WinLine(start, end);
    }
}
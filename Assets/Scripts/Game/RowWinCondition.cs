using UnityEngine;

/// <summary>
/// Verifica vitória na linha onde o último movimento foi feito.
/// </summary>
public class RowWinCondition : IWinCondition
{
    private int _winRow;
    public bool IsSatisfied(Board board, int lastMoveRow, int lastMoveCol, Player player)
    {
        bool isWin = board.GetCell(lastMoveRow, 0) == player &&
                     board.GetCell(lastMoveRow, 1) == player &&
                     board.GetCell(lastMoveRow, 2) == player;

        if (isWin)
            _winRow = lastMoveRow;

        return isWin;
    }

    public WinLine GetWinLine()
    {
        Vector2Int start = new Vector2Int(_winRow, 0);
        Vector2Int end = new Vector2Int(_winRow, 2);
        return new WinLine(start, end);
    }
}

using UnityEngine;

/// <summary>
/// Verifica vitória na coluna onde o último movimento foi feito.
/// </summary>
public class ColumnWinCondition : IWinCondition
{
    private int _winCol;
    public bool IsSatisfied(Board board, int lastMoveRow, int lastMoveCol, Player player)
    {
        bool isWin = board.GetCell(0, lastMoveCol) == player &&
                     board.GetCell(1, lastMoveCol) == player &&
                     board.GetCell(2, lastMoveCol) == player;

        if (isWin)
            _winCol = lastMoveCol;

        return isWin;
    }

    public WinLine GetWinLine()
    {
        Vector2Int start = new Vector2Int(0, _winCol);
        Vector2Int end = new Vector2Int(2, _winCol);
        return new WinLine(start, end);
    }
}

/// <summary>
/// Verifica vitória na linha onde o último movimento foi feito.
/// </summary>
public class RowWinCondition : IWinCondition
{
    public bool IsSatisfied(Board board, int lastMoveRow, int lastMoveCol, Player player)
    {
        return board.GetCell(lastMoveRow, 0) == player &&
               board.GetCell(lastMoveRow, 1) == player &&
               board.GetCell(lastMoveRow, 2) == player;
    }
}

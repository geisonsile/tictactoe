/// <summary>
/// Verifica vitória na coluna onde o último movimento foi feito.
/// </summary>
public class ColumnWinCondition : IWinCondition
{
    public bool IsSatisfied(Board board, int lastMoveRow, int lastMoveCol, Player player)
    {
        return board.GetCell(0, lastMoveCol) == player &&
               board.GetCell(1, lastMoveCol) == player &&
               board.GetCell(2, lastMoveCol) == player;
    }
}

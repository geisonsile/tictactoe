/// <summary>
/// Verifica vitória na diagonal secundária (0,2 até 2,0).
/// </summary>
public class AntiDiagonalWinCondition : IWinCondition
{
    public bool IsSatisfied(Board board, int lastMoveRow, int lastMoveCol, Player player)
    {
        // Verifica se o movimento foi na diagonal secundária
        if (lastMoveRow + lastMoveCol != 2)
            return false;

        return board.GetCell(0, 2) == player &&
               board.GetCell(1, 1) == player &&
               board.GetCell(2, 0) == player;
    }
}
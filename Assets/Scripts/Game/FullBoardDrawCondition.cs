/// <summary>
/// Verifica empate quando todas as 9 casas foram preenchidas.
/// </summary>
public class FullBoardDrawCondition : IDrawCondition
{
    private const int TotalCells = 9;

    public bool IsSatisfied(int movesMade)
    {
        return movesMade >= TotalCells;
    }
}

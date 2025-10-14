/// <summary>
/// Interface para definir condições de empate no jogo.
/// </summary>
public interface IDrawCondition
{
    bool IsSatisfied(int movesMade);
}
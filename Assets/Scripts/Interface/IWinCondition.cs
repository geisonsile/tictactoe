/// <summary>
/// Interface para definir condições de vitória no jogo.
/// Seguindo o princípio Open/Closed: aberto para extensão, fechado para modificação.
/// </summary>
public interface IWinCondition
{
    /// <summary>
    /// Verifica se existe uma condição de vitória.
    /// </summary>
    /// <param name="board">O tabuleiro do jogo.</param>
    /// <param name="lastMoveRow">Linha do último movimento.</param>
    /// <param name="lastMoveCol">Coluna do último movimento.</param>
    /// <param name="player">O jogador a verificar.</param>
    /// <returns>True se a condição de vitória foi atendida.</returns>
    bool IsSatisfied(Board board, int lastMoveRow, int lastMoveCol, Player player);
}

public class Board
{
    private readonly Player[,] _grid = new Player[3, 3];

    public Player GetCell(int row, int col) => _grid[row, col];

    public bool IsCellEmpty(int row, int col) => _grid[row, col] == Player.None;

    public void SetCell(int row, int col, Player player)
    {
        if (!IsCellEmpty(row, col)) return;
        
        _grid[row, col] = player; 
    }

    public void Clear()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                _grid[i, j] = Player.None;
            }
        }
    }

     // Faz uma jogada de simulação para ver se o player especificado venceu.
    public bool CheckWinCondition(Player player)
    {
        // Checar linhas e colunas
        for (int i = 0; i < 3; i++)
        {
            if ((_grid[i, 0] == player && _grid[i, 1] == player && _grid[i, 2] == player) ||
                (_grid[0, i] == player && _grid[1, i] == player && _grid[2, i] == player))
            {
                return true;
            }
        }

        // Checar diagonais
        if ((_grid[0, 0] == player && _grid[1, 1] == player && _grid[2, 2] == player) ||
            (_grid[0, 2] == player && _grid[1, 1] == player && _grid[2, 0] == player))
        {
            return true;
        }

        return false;
    }

    // Desfaz uma jogada de simulação.
    public void ClearCell(int row, int col)
    {
        _grid[row, col] = Player.None;
    }    
}

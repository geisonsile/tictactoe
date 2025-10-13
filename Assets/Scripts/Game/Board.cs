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
}

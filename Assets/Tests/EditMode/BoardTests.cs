using NUnit.Framework;

public class BoardTests
{
    private Board _board;
    
    [SetUp]
    public void Setup()
    {
        _board = new Board();
    }

    [Test]
    public void NewBoard_IsEmpty()
    {
        //Verifica todas as 9 células.
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                //Verifica se o valor esperado é igual ao valor atual.
                Assert.AreEqual(Player.None, _board.GetCell(i, j));
            }
        }
    }

    [Test]
    public void SetCell_WhenEmpty_ChangesPlayer()
    {
        //Tenta fazer uma jogada na célula (0,0).
        _board.SetCell(0, 0, Player.X);

        //Verifica se a célula foi de fato alterada.
        Assert.AreEqual(Player.X, _board.GetCell(0, 0));
    }

    [Test]
    public void CheckWinCondition_HorizontalWin_ReturnsTrue()
    {
        //Prepara um cenário de vitória para o Jogador O.
        _board.SetCell(0, 0, Player.O);
        _board.SetCell(0, 1, Player.O);
        _board.SetCell(0, 2, Player.O);
        
        bool hasWon = _board.CheckWinCondition(Player.O);

        Assert.IsTrue(hasWon);
    }
    
    [Test]
    public void CheckWinCondition_NoWin_ReturnsFalse()
    {
        //Prepara um cenário sem vitória.
        _board.SetCell(0, 0, Player.X);
        _board.SetCell(0, 1, Player.O);
        _board.SetCell(0, 2, Player.X);
        
        bool xHasWon = _board.CheckWinCondition(Player.X);
        bool oHasWon = _board.CheckWinCondition(Player.O);
        
        Assert.IsFalse(xHasWon);
        Assert.IsFalse(oHasWon);
    }
}

using NUnit.Framework;
using UnityEngine;

public class AIPlayerTests
{
    private Board _board;
    private AIPlayer _aiPlayer;

    [SetUp]
    public void Setup()
    {
        _board = new Board();
        // Como AIPlayer é um MonoBehaviour, precisamos criar um GameObject temporário para hospedá-lo.
        var aiGameObject = new GameObject();
        _aiPlayer = aiGameObject.AddComponent<AIPlayer>();
    }

    [Test]
    public void FindBestMove_AITriesToWin()
    {
        //Cenário onde a IA (O) pode vencer em (0,2)
        _board.SetCell(0, 0, Player.O);
        _board.SetCell(0, 1, Player.O);
        _board.SetCell(1, 0, Player.X);
        _board.SetCell(1, 1, Player.X);

        //Pede à IA para encontrar a melhor jogada
        Vector2Int bestMove = _aiPlayer.FindBestMove(_board);
        
        //A IA deve escolher a jogada da vitória
        Assert.AreEqual(new Vector2Int(0, 2), bestMove);
    }

    [Test]
    public void FindBestMove_AITriesToBlock()
    {
        //Cenário onde o Humano (X) pode vencer em (1,2)
        _board.SetCell(1, 0, Player.X);
        _board.SetCell(1, 1, Player.X);
        _board.SetCell(0, 0, Player.O);
        
        Vector2Int bestMove = _aiPlayer.FindBestMove(_board);
        
        //A IA deve escolher a jogada de bloqueio
        Assert.AreEqual(new Vector2Int(1, 2), bestMove);
    }
}
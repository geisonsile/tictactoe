using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class GameManagerTests
{
    //Antes de cada teste, carrega a cena de teste.
    [SetUp]
    public void Setup()
    {
        // Carrega a cena de teste de forma síncrona para garantir que tudo está pronto.
        SceneManager.LoadScene("TestScene", LoadSceneMode.Single);
    }

    [UnityTest]
    public IEnumerator StartGame_InitializesCorrectly()
    {
        //A cena é carregada no Setup, e o GameManager executa Start() automaticamente.
        
        //Espera um único frame para garantir que todos os métodos Start() foram executados.
        yield return null; 

        //Verifica o estado inicial do jogo.
        Assert.IsNotNull(GameManager.Instance);
        Assert.AreEqual(Player.None, GameManager.Instance.CurrentBoard.GetCell(0, 0));
        Assert.IsTrue(GameManager.Instance.IsHumanTurn);
    }

    [UnityTest]
    public IEnumerator MakeMove_SwitchesPlayerAndTurn()
    {
        //Espera a cena carregar e o jogo começar
        yield return null;
        
        //O jogador humano (X) faz uma jogada
        GameManager.Instance.MakeMove(0, 0);

        // Espera um frame para que todas as lógicas de troca de turno sejam processadas.
        yield return null;

        //O estado do jogo deve ter mudado.
        Assert.AreEqual(Player.X, GameManager.Instance.CurrentBoard.GetCell(0, 0));
        Assert.IsFalse(GameManager.Instance.IsHumanTurn); // Agora deve ser a vez da IA (Jogador O)
    }

    [UnityTest]
    public IEnumerator FullGame_PlayerXWins_TriggersWinEvent()
    {
        yield return null;

        bool gameWon = false;
        Player winner = Player.None;

        // Se inscreve no evento de vitória.
        GameManager.Instance.OnGameWon += (player) => 
        {
            gameWon = true;
            winner = player;
        };
        
        //Simula uma sequência de jogadas que resulta na vitória do Jogador X
        GameManager.Instance.MakeMove(0, 0); // X
        GameManager.Instance.MakeMove(0, 1); // O joga (simula IA)
        GameManager.Instance.MakeMove(1, 1); // X
        GameManager.Instance.MakeMove(1, 0); // O joga (simula IA)
        GameManager.Instance.MakeMove(2, 2); // X (jogada da vitória)
        
        // Espera alguns frames para garantir que o evento de vitória seja disparado e processado.
        yield return new WaitForSeconds(2f);
        
        Assert.IsTrue(gameWon, "O evento OnGameWon não foi disparado.");
        Assert.AreEqual(Player.X, winner, "O vencedor incorreto foi reportado.");
    }
}
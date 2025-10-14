using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CellUI : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private TextMeshProUGUI _text;

    private int _row;
    private int _col;

    public void Initialize(int row, int col)
    {
        _row = row;
        _col = col;
        _button.onClick.AddListener(OnCellClicked);
    }
    private void OnCellClicked()
    {
        // SÓ PERMITE O CLIQUE SE FOR O TURNO DO HUMANO E O JOGO NÃO TIVER ACABADO
        if (GameManager.Instance.IsHumanTurn)
        {
            GameManager.Instance.MakeMove(_row, _col);
        } 
    }

    public void UpdateCell(Player player)
    {
        _text.text = (player == Player.None) ? "" : player.ToString();
        _button.interactable = false;
    }

    public void ResetCell()
    {
        _text.text = string.Empty;
        _button.interactable = true;
    }
}

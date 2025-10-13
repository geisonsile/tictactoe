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
        GameManager.Instance.MakeMove(_row, _col);
    }

    private void UpdateCell(Player player)
    {
        _text.text = player.ToString();
        _button.interactable = false;
    }

    public void ResetCell()
    {
        _text.text = string.Empty;
        _button.interactable = true;
    }
}

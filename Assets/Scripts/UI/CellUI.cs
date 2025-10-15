using UnityEngine;
using UnityEngine.UI;

public class CellUI : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private Image _image;
    [SerializeField] private Sprite[] _imagePlayers;

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
        if (GameManager.Instance.IsHumanTurn)
        {
            GameManager.Instance.MakeMove(_row, _col);
        } 
    }

    public void UpdateCell(Player player)
    {
        _image.sprite = (player == Player.None) ? null : _imagePlayers[(int)player - 1];
        _image.gameObject.SetActive(true);
        _button.interactable = false;
    }

    public void ResetCell()
    {
        _image.sprite = null;
        _image.gameObject.SetActive(false);
        _button.interactable = true;
    }
}

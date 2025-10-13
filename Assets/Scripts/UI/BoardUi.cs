using UnityEngine;

public class BoardUi : MonoBehaviour
{
   [SerializeField] private CellUI[] _cells;
    void Start()
    {
        for (int i = 0; i < _cells.Length; i++)
        {
            int row = i / 3;
            int col = i % 3;
            _cells[i].Initialize(row, col);
        }
    }
}

using System.Collections.Generic;
using UnityEngine;

public abstract class Grid : MonoBehaviour
{
    private readonly int _gridSizeX = 25;
    private readonly int _gridSizeY = 20;
    private readonly float _gridCellSize = 1f;

    #region Properties

    protected int GridSizeX => _gridSizeX;
    protected int GridSizeY => _gridSizeY;
    protected float GridCellSize => _gridCellSize;

    #endregion

    protected List<Vector2Int> OccupiedCells => _occupiedCells;

    [HideInInspector] [SerializeField] private GameObject gridCellPrefab;


    public List<Vector2Int> _occupiedCells = new();

    protected bool IsCellOccupied(Vector2Int cellPosition)
    {
        return _occupiedCells.Contains(cellPosition);
    }

    protected bool IsCellWithinGrid(Vector2Int cellPosition)
    {
        return cellPosition.x >= 0 && cellPosition.x < _gridSizeX && cellPosition.y >= 0 && cellPosition.y < _gridSizeY;
    }

    // Cell to 
    protected Vector3 CellToWorldPosition(Vector2Int cellPosition)
    {
        return new Vector3(cellPosition.x * _gridCellSize, 0.5f, cellPosition.y * _gridCellSize);
    }

    // GamePlay Grid cells created here
    protected void CreateGrid(Transform gridParent)
    {
        for (var x = 0; x < _gridSizeX; x++)
        {
            for (var y = 0; y < _gridSizeY; y++)
            {
                Vector3 cellPosition = new Vector3(x * _gridCellSize, 0f, y * _gridCellSize);
                var cell = Instantiate(gridCellPrefab, cellPosition, Quaternion.identity);
                cell.transform.localScale = new Vector3(_gridCellSize, 0.1f, _gridCellSize);
                cell.GetComponent<Renderer>().material.color = Color.white;
                cell.name = $"Cell ({x} , {y} )";
                cell.transform.parent = gridParent;
            }
        }
    }
}
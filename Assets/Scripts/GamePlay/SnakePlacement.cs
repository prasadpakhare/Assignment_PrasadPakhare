using System;
using System.Collections;
using System.Collections.Generic;
using Core;
using GamePlay;
using UnityEngine;


[DefaultExecutionOrder(-1)]
public class SnakePlacement : Grid
{
    [Header("Snake Info")] public int snakeLength = 2;
    public GameObject snakePrefab;
    public float movementSpeed = 0.5f;
    public GameObject bodySegmentPrefab;
    [SerializeField] private Transform GridParent;

    private float _timer = 0;
    private Vector2Int _currentDirection = Vector2Int.right;
    private GameObject _snake;

    [Header("Game over")] private bool _isGameOver;

    public static event Action OnGameOver;
    public static event Action<GameObject> OnSnakeSpawnCompleted;


    private void Awake()
    {
        CreateGrid(GridParent);
    }

    private void Start()
    {
        PlaceSnake();
    }

    private void OnEnable()
    {
        SnakeHead.OnFoodEat += IncreaseSnakeLength;
    }

    private void OnDisable()
    {
        SnakeHead.OnFoodEat -= IncreaseSnakeLength;
    }

    private void PlaceSnake()
    {
        // Place snake initially at center of screen
        Vector2Int snakeHeadPosition = new Vector2Int(GridSizeX / 2, GridSizeY / 2);
        OccupiedCells.Add(snakeHeadPosition);

        Vector3 snakePosition =
            new Vector3(snakeHeadPosition.x * GridCellSize, 0.5f, snakeHeadPosition.y * GridCellSize);
        _snake = Instantiate(snakePrefab, snakePosition, Quaternion.identity);
        snakeLength = _snake.transform.childCount;

        for (int i = 1; i < snakeLength; i++)
        {
            Vector2Int snakeBodyPosition = snakeHeadPosition - new Vector2Int(i, 0);
            OccupiedCells.Add(snakeBodyPosition);
        }

        OnSnakeSpawnCompleted?.Invoke(_snake);
        Debug.Log("Snake spawn--");
    }

    private void Update()
    {
        // Change the current direction based on player input
#if UNITY_EDITOR
        // PC inputs
        if (Input.GetKeyDown(KeyCode.UpArrow) && _currentDirection != Vector2Int.down)
        {
            _currentDirection = Vector2Int.up;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && _currentDirection != Vector2Int.up)
        {
            _currentDirection = Vector2Int.down;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && _currentDirection != Vector2Int.right)
        {
            _currentDirection = Vector2Int.left;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && _currentDirection != Vector2Int.left)
        {
            _currentDirection = Vector2Int.right;
        }
#endif

        //mobile inputs
        if (InputDetection.rightSwipe && _currentDirection != Vector2Int.left)
        {
            _currentDirection = Vector2Int.right;
        }
        else if (InputDetection.leftSwipe && _currentDirection != Vector2Int.right)
        {
            _currentDirection = Vector2Int.left;
        }
        else if (InputDetection.upSwipe && _currentDirection != Vector2Int.down)
        {
            _currentDirection = Vector2Int.up;
        }
        else if (InputDetection.downSwipe && _currentDirection != Vector2Int.up)
        {
            _currentDirection = Vector2Int.down;
        }
    }

    private void FixedUpdate()
    {
        if (_isGameOver) return;
        _timer += Time.deltaTime;

        if (_timer >= movementSpeed)
        {
            _timer = 0f;

            //get new head position based on the current direction
            Vector2Int newHeadPosition = OccupiedCells[0] + _currentDirection;

            if (IsCellWithinGrid(newHeadPosition) && !IsCellOccupied(newHeadPosition))
            {
                //move the snake head to the new position
                OccupiedCells.Insert(0, newHeadPosition);
                OccupiedCells.RemoveAt(OccupiedCells.Count - 1);

                // New Head world position
                Vector3 newHeadWorldPosition = new Vector3(newHeadPosition.x * GridCellSize, 0.5f,
                    newHeadPosition.y * GridCellSize);
                _snake.transform.GetChild(0).position = newHeadWorldPosition;

                // Update the positions of the snake body
                for (int i = 1; i < snakeLength; i++)
                {
                    Vector3 snakeBody = new Vector3(OccupiedCells[i].x * GridCellSize, 0.5f,
                        OccupiedCells[i].y * GridCellSize);
                    _snake.transform.GetChild(i).position = snakeBody;
                }
            }
            else
            {
                //Debug.Log("Game over!");
                _isGameOver = true;
                OnGameOver?.Invoke();
            }
        }
    }


    private void IncreaseSnakeLength()
    {
        //get the last body part of snake
        Vector2Int lastCell = OccupiedCells[OccupiedCells.Count - 1];
        Vector2Int newBodySegmentPosition = lastCell;

        OccupiedCells.Add(newBodySegmentPosition);

        Vector3 newBodyPart = CellToWorldPosition(newBodySegmentPosition);
        GameObject bodySegment = Instantiate(bodySegmentPrefab, newBodyPart, Quaternion.identity);
        bodySegment.transform.SetParent(_snake.transform);


        snakeLength = _snake.transform.childCount;
    }
}
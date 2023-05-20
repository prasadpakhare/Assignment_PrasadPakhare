using System;
using System.Collections;
using System.Collections.Generic;
using Core;
using GamePlay;
using TMPro;
using UnityEngine;
using Grid = GamePlay.Grid;

public class Streak : Grid
{
    private int _counter = 1;
    private int _score = 0;
    [SerializeField] private GameObject particle;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text streakText;

    private void OnEnable()
    {
        SnakeHead.OnFoodEatPoints += StreakInfo;
    }

    private void OnDisable()
    {
        SnakeHead.OnFoodEatPoints -= StreakInfo;
    }

    private void Start()
    {
        _score = 0;
        scoreText.text = "0";
        streakText.text = "1X";
    }

    private void StreakInfo(int points)
    {
        if (Constants.previousColor == Constants.currentColor)
        {
            _counter++;
            _score += _counter * points;
            // Debug.Log($"same points : {counter * points} and score is {score} & counter is {counter}" );
            scoreText.text = _score.ToString();
            streakText.text = $"{_counter}X";
            Constants.CurrentScore = _score;
        }
        else
        {
            _counter = 1;
            _score += _counter * points;
            //Debug.Log($"new points : {counter * points} and score is {score}");
            scoreText.text = _score.ToString();
            streakText.text = $"{_counter}X";
            Constants.CurrentScore = _score;
        }

        //StartCoroutine(WaitAndDestroy());
    }

    private IEnumerator WaitAndDestroy()
    {
        //one cell above the snake head
        Debug.Log("Particle Spawn");
        Vector2Int headPosition = GameManager.Instance().occupiedCells[0];
        Vector3 particlePosition = CellToWorldPosition(new Vector2Int(headPosition.x, headPosition.y + 1));
        var streakParticle = Instantiate(particle, particlePosition, Quaternion.identity);
        yield return new WaitForSeconds(2);
        Destroy(streakParticle, 2);
    }
    
}
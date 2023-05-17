using Core;
using TMPro;
using UnityEngine;

namespace GamePlay
{
    public class GameOver : MonoBehaviour
    {
        [SerializeField] private GameObject gameOverPopup;
        [SerializeField] private TMP_Text currentScore;


        private void OnEnable()
        {
            SnakePlacement.OnGameOver += ShowGameOverPopup;
        }

        private void OnDisable()
        {
            SnakePlacement.OnGameOver -= ShowGameOverPopup;
        }
        private void Start()
        {
            gameOverPopup.SetActive(false);
        }

        private void ShowGameOverPopup()
        {
            // High score Update
            if (PlayerPrefs.GetInt(Constants.HIGH_SCORE) < Constants.CurrentScore)
            {
                PlayerPrefs.SetInt(Constants.HIGH_SCORE, Constants.CurrentScore);
                PlayerPrefs.Save();
            }

            // Enable Game over popup
            gameOverPopup.SetActive(true);
            currentScore.text = Constants.CurrentScore.ToString();
        }
    
    }
}